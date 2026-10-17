from django.contrib import messages
from django.contrib.auth import login
from django.contrib.auth.decorators import login_required
from django.core.exceptions import PermissionDenied
from django.db.models import Avg, Count, F, Q
from django.shortcuts import get_object_or_404, redirect, render
from django.views.decorators.http import require_POST

from favorites.utils import favorite_ids_for
from lessons.models import LessonRequest, Rating
from offers.models import SkillOffer

from .forms import ProfileForm, RegisterForm
from .models import User


def register(request):
    if request.user.is_authenticated:
        return redirect("offers:explore")

    form = RegisterForm(request.POST or None)
    if request.method == "POST" and form.is_valid():
        user = form.save()
        login(request, user)
        messages.success(request, "Добро пожаловать в SkillSwap!")
        return redirect("offers:explore")
    return render(request, "accounts/register.html", {"form": form})


def _can_ban(viewer, person):
    return (
        viewer.is_authenticated
        and viewer.is_moderator
        and viewer.pk != person.pk
        and not person.is_super
        and (viewer.is_super or person.role == User.Role.USER)
    )


def _can_set_role(viewer, person):
    return (
        viewer.is_authenticated
        and viewer.is_super
        and viewer.pk != person.pk
        and not person.is_super
    )


def profile(request, username):
    person = get_object_or_404(User, username=username)

    ratings = Rating.objects.filter(lesson__offer__author=person)
    rating_stats = ratings.aggregate(avg=Avg("score"), count=Count("id"))

    offers = list(
        SkillOffer.objects.published()
        .filter(author=person)
        .with_stats()
        .select_related("author", "category")
    )
    reviews = (
        ratings.exclude(review="")
        .select_related("lesson__student", "lesson__offer")
        .order_by("-created_at")
    )
    completed_count = LessonRequest.objects.filter(
        offer__author=person, status=LessonRequest.Status.COMPLETED
    ).count()

    return render(
        request,
        "accounts/profile.html",
        {
            "person": person,
            "offers": offers,
            "reviews": reviews,
            "avg_rating": rating_stats["avg"],
            "ratings_count": rating_stats["count"],
            "published_count": len(offers),
            "completed_count": completed_count,
            "favorite_ids": favorite_ids_for(request.user),
            "can_ban": _can_ban(request.user, person),
            "can_set_role": _can_set_role(request.user, person),
        },
    )


@login_required
def profile_edit(request):
    form = ProfileForm(request.POST or None, request.FILES or None, instance=request.user)
    if request.method == "POST" and form.is_valid():
        form.save()
        messages.success(request, "Профиль обновлён.")
        return redirect("profile", username=request.user.username)
    return render(request, "accounts/profile_edit.html", {"form": form})


def mentors(request):
    published = Q(offers__status=SkillOffer.Status.PUBLISHED)
    items = (
        User.objects.annotate(
            published_count=Count("offers", filter=published, distinct=True),
            avg_rating=Avg("offers__lessons__rating__score"),
            ratings_count=Count("offers__lessons__rating", distinct=True),
            completed_count=Count(
                "offers__lessons",
                filter=Q(offers__lessons__status="completed"),
                distinct=True,
            ),
        )
        .filter(published_count__gt=0)
        .order_by(F("avg_rating").desc(nulls_last=True), "-completed_count", "username")
    )
    return render(request, "accounts/mentors.html", {"mentors": items})


@login_required
@require_POST
def user_toggle_ban(request, username):
    person = get_object_or_404(User, username=username)
    if not _can_ban(request.user, person):
        raise PermissionDenied
    person.is_banned = not person.is_banned
    person.save(update_fields=["is_banned"])
    if person.is_banned:
        messages.success(request, f"{person.username} заблокирован.")
    else:
        messages.success(request, f"{person.username} разблокирован.")
    return redirect("profile", username=person.username)


@login_required
@require_POST
def user_set_role(request, username):
    person = get_object_or_404(User, username=username)
    if not _can_set_role(request.user, person):
        raise PermissionDenied
    role = request.POST.get("role")
    if role not in (User.Role.ADMIN, User.Role.USER):
        messages.error(request, "Неизвестная роль.")
        return redirect("profile", username=person.username)
    person.role = role
    person.save(update_fields=["role"])
    messages.success(request, f"Роль пользователя {person.username} изменена.")
    return redirect("profile", username=person.username)
