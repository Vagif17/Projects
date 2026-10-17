from django.contrib import messages
from django.contrib.auth.decorators import login_required
from django.core.exceptions import PermissionDenied
from django.db.models import Count, Q
from django.http import Http404
from django.shortcuts import get_object_or_404, redirect, render
from django.views.decorators.http import require_POST

from favorites.models import Favorite
from favorites.utils import favorite_ids_for
from lessons.forms import RequestForm
from lessons.models import LessonRequest, Rating

from .forms import OfferForm
from .models import Category, SkillOffer

Status = SkillOffer.Status

FILTER_KEYS = ("q", "category", "level", "format", "min_rating")

CATEGORY_ICONS = {
    "Programming": "💻",
    "Design": "🎨",
    "Languages": "🌍",
    "Marketing": "📣",
    "Music": "🎵",
    "Photography": "📷",
    "Business": "💼",
    "Mathematics": "📐",
    "Other": "✨",
}


def _forbid_banned(user):
    if user.is_banned:
        raise PermissionDenied("Ваш аккаунт заблокирован.")


def _can_manage(user, offer):
    return user.is_authenticated and (offer.author_id == user.id or user.is_moderator)


def _status_from_action(request):
    return Status.DRAFT if request.POST.get("action") == "draft" else Status.PENDING


def _filter_values(request):
    return {key: request.GET.get(key, "").strip() for key in FILTER_KEYS}


def _filtered_offers(values, base=None):
    offers = base if base is not None else SkillOffer.objects.published()

    if values["q"]:
        needle = values["q"].casefold()
        ids = [
            pk
            for pk, title in offers.values_list("pk", "title")
            if needle in title.casefold()
        ]
        offers = offers.filter(pk__in=ids)

    if values["category"].isdigit():
        offers = offers.filter(category_id=int(values["category"]))
    if values["level"] in SkillOffer.Level.values:
        offers = offers.filter(level=values["level"])
    if values["format"] in SkillOffer.Format.values:
        offers = offers.filter(format=values["format"])

    offers = offers.with_stats().select_related("author", "category")

    if values["min_rating"] in {"1", "2", "3", "4"}:
        offers = offers.filter(avg_rating__gte=int(values["min_rating"]))
    return offers


def _listing_context(request, values, **extra):
    context = {
        "categories": Category.objects.order_by("name"),
        "levels": SkillOffer.Level.values,
        "formats": SkillOffer.Format.values,
        "rating_options": ["4", "3", "2", "1"],
        "filters": values,
        "favorite_ids": favorite_ids_for(request.user),
    }
    context.update(extra)
    return context


def explore(request):
    values = _filter_values(request)
    offers = _filtered_offers(values)
    return render(
        request,
        "offers/listing.html",
        _listing_context(
            request,
            values,
            title="Explore",
            subtitle="Все опубликованные предложения: сначала новые",
            offers=offers,
            show_hero=True,
        ),
    )


def popular(request):
    values = _filter_values(request)
    values["min_rating"] = "4"
    offers = _filtered_offers(values).order_by("-avg_rating", "-created_at")
    return render(
        request,
        "offers/listing.html",
        _listing_context(
            request,
            values,
            title="Popular",
            subtitle="Предложения с рейтингом 4 и выше",
            offers=offers,
            hide_rating=True,
        ),
    )


def categories(request):
    items = list(
        Category.objects.annotate(
            offers_count=Count("offers", filter=Q(offers__status=Status.PUBLISHED))
        ).order_by("name")
    )
    for category in items:
        category.icon = CATEGORY_ICONS.get(category.name, "✨")
    return render(request, "offers/categories.html", {"categories": items})


def category_detail(request, pk):
    category = get_object_or_404(Category, pk=pk)
    values = _filter_values(request)
    base = SkillOffer.objects.published().filter(category=category)
    offers = _filtered_offers(values, base=base)
    return render(
        request,
        "offers/listing.html",
        _listing_context(
            request,
            values,
            title=category.name,
            subtitle="Все предложения этой категории",
            offers=offers,
            hide_category=True,
            back_to_categories=True,
        ),
    )


def offer_detail(request, pk):
    offer = get_object_or_404(
        SkillOffer.objects.with_stats().select_related("author", "category"), pk=pk
    )
    can_manage = _can_manage(request.user, offer)
    if offer.status != Status.PUBLISHED and not can_manage:
        raise Http404

    reviews = (
        Rating.objects.filter(lesson__offer=offer)
        .exclude(review="")
        .select_related("lesson__student")
        .order_by("-created_at")
    )

    my_request = None
    can_request = False
    is_favorite = False
    if request.user.is_authenticated:
        is_favorite = Favorite.objects.filter(user=request.user, offer=offer).exists()
        if offer.status == Status.PUBLISHED and offer.author_id != request.user.id:
            my_request = LessonRequest.objects.filter(
                offer=offer, student=request.user, status__in=LessonRequest.ACTIVE
            ).first()
            can_request = my_request is None and not request.user.is_banned

    return render(
        request,
        "offers/offer_detail.html",
        {
              "offer": offer,
            "can_manage": can_manage,
            "reviews": reviews,
            "my_request": my_request,
            "can_request": can_request,
            "is_favorite": is_favorite,
            "request_form": RequestForm(),
        },
    )


@login_required
def my_offers(request):
    offers = (
        SkillOffer.objects.filter(author=request.user)
        .with_stats()
        .select_related("category", "author")
    )
    return render(request, "offers/my_offers.html", {"offers": offers})


@login_required
def offer_create(request):
    _forbid_banned(request.user)
    form = OfferForm(request.POST or None, request.FILES or None)

    if request.method == "POST" and form.is_valid():
        offer = form.save(commit=False)
        offer.author = request.user
        offer.status = _status_from_action(request)
        offer.save()

        if offer.status == Status.DRAFT:
            messages.success(request, "Черновик сохранён.")
        else:
            messages.success(request, "Предложение отправлено на модерацию.")
        return redirect("offers:my_offers")

    return render(
        request,
        "offers/offer_form.html",
        {"form": form, "page_title": "Новое предложение"},
    )


@login_required
def offer_update(request, pk):
    _forbid_banned(request.user)
    offer = get_object_or_404(SkillOffer, pk=pk)
    if not _can_manage(request.user, offer):
        raise PermissionDenied

    is_author = offer.author_id == request.user.id
    form = OfferForm(request.POST or None, request.FILES or None, instance=offer)

    if request.method == "POST" and form.is_valid():
        offer = form.save(commit=False)
        if is_author:
            offer.status = _status_from_action(request)
            if offer.status == Status.PENDING:
                messages.success(request, "Изменения отправлены на модерацию.")
            else:
                messages.success(request, "Черновик сохранён.")
        else:
            messages.success(request, "Предложение обновлено.")
        offer.save()
        return redirect("offers:detail", pk=offer.pk)

    return render(
        request,
        "offers/offer_form.html",
        {
            "form": form,
            "page_title": "Редактирование",
            "moderator_edit": not is_author,
        },
    )


@login_required
def offer_delete(request, pk):
    _forbid_banned(request.user)
    offer = get_object_or_404(SkillOffer, pk=pk)
    if not _can_manage(request.user, offer):
        raise PermissionDenied

    if request.method == "POST":
        offer.delete()
        messages.success(request, "Предложение удалено.")
        return redirect("offers:my_offers")
    return render(request, "offers/offer_confirm_delete.html", {"offer": offer})


@login_required
def moderation(request):
    if not request.user.is_moderator:
        raise PermissionDenied
    offers = SkillOffer.objects.filter(status=Status.PENDING).select_related(
        "author", "category"
    ).order_by("created_at")
    return render(request, "offers/moderation.html", {"offers": offers})


def _moderate(request, pk, new_status, text):
    if not request.user.is_moderator:
        raise PermissionDenied
    offer = get_object_or_404(SkillOffer, pk=pk)
    offer.status = new_status
    offer.save(update_fields=["status"])
    messages.success(request, f"«{offer.title}»: {text}")
    return redirect(request.POST.get("next") or "offers:moderation")


@login_required
@require_POST
def offer_approve(request, pk):
    return _moderate(request, pk, Status.PUBLISHED, "опубликовано.")


@login_required
@require_POST
def offer_reject(request, pk):
    return _moderate(request, pk, Status.REJECTED, "отклонено.")
