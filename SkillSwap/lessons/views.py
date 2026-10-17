from django.contrib import messages
from django.contrib.auth.decorators import login_required
from django.core.exceptions import PermissionDenied
from django.db import IntegrityError, transaction
from django.http import Http404
from django.shortcuts import get_object_or_404, redirect, render
from django.views.decorators.http import require_POST

from offers.models import SkillOffer

from .forms import RatingForm, RequestForm
from .models import LessonRequest, Rating

S = LessonRequest.Status

ACTIONS = {
    "accept":   {"who": "owner",   "from": {S.PENDING},               "to": S.ACCEPTED,  "msg": "Запрос принят."},
    "reject":   {"who": "owner",   "from": {S.PENDING},               "to": S.REJECTED,  "msg": "Запрос отклонён."},
    "complete": {"who": "owner",   "from": {S.ACCEPTED},              "to": S.COMPLETED, "msg": "Занятие отмечено как завершённое."},
    "cancel":   {"who": "student", "from": {S.PENDING, S.ACCEPTED},   "to": S.CANCELLED, "msg": "Запрос отменён."},
}


def _forbid_banned(user):
    if user.is_banned:
        raise PermissionDenied("Ваш аккаунт заблокирован.")



@login_required
@require_POST
def request_create(request, offer_pk):
    _forbid_banned(request.user)
    offer = get_object_or_404(
        SkillOffer, pk=offer_pk, status=SkillOffer.Status.PUBLISHED
    )
    back = redirect("offers:detail", pk=offer.pk)

    if offer.author_id == request.user.id:
        messages.error(request, "Нельзя отправить запрос самому себе.")
        return back

    already = LessonRequest.objects.filter(
        offer=offer, student=request.user, status__in=LessonRequest.ACTIVE
    ).exists()
    if already:
        messages.error(request, "У вас уже есть активный запрос на это предложение.")
        return back

    form = RequestForm(request.POST)
    if not form.is_valid():
        messages.error(request, "; ".join(form.errors.get("message", ["Ошибка в форме."])))
        return back

    lesson = form.save(commit=False)
    lesson.offer = offer
    lesson.student = request.user
    try:
        with transaction.atomic():
            lesson.save()
    except IntegrityError:  
        messages.error(request, "У вас уже есть активный запрос на это предложение.")
        return back

    messages.success(request, "Запрос отправлен автору предложения.")
    return redirect("lessons:my_requests")


@login_required
def my_requests(request):
    sent = LessonRequest.objects.filter(student=request.user).select_related(
        "offer", "offer__author", "rating"
    )
    received = LessonRequest.objects.filter(offer__author=request.user).select_related(
        "offer", "student", "rating"
    )
    return render(
        request, "lessons/my_requests.html", {"sent": sent, "received": received}
    )


@login_required
@require_POST
def request_action(request, pk, action):
    rule = ACTIONS.get(action)
    if rule is None:
        raise Http404

    lesson = get_object_or_404(LessonRequest.objects.select_related("offer"), pk=pk)
    if rule["who"] == "owner":
        allowed = lesson.offer.author_id == request.user.id
    else:
        allowed = lesson.student_id == request.user.id
    if not allowed:
        raise PermissionDenied

    if lesson.status not in rule["from"]:
        messages.error(request, "Это действие сейчас недоступно для данного запроса.")
        return redirect("lessons:my_requests")

    lesson.status = rule["to"]
    lesson.save(update_fields=["status"])
    messages.success(request, rule["msg"])
    return redirect("lessons:my_requests")



@login_required
def rate_lesson(request, pk):
    _forbid_banned(request.user)
    lesson = get_object_or_404(
        LessonRequest.objects.select_related("offer"), pk=pk, student=request.user
    )
    if lesson.status != S.COMPLETED:
        raise PermissionDenied

    rating = getattr(lesson, "rating", None)  # None, если ещё не оценивали
    form = RatingForm(request.POST or None, instance=rating)

    if request.method == "POST" and form.is_valid():
        new_rating = form.save(commit=False)
        new_rating.lesson = lesson
        new_rating.save()
        messages.success(request, "Спасибо! Ваша оценка сохранена.")
        return redirect("offers:detail", pk=lesson.offer_id)

    return render(
        request,
        "lessons/rate.html",
        {"form": form, "lesson": lesson, "editing": rating is not None},
    )


@login_required
@require_POST
def review_delete(request, pk):
    rating = get_object_or_404(Rating.objects.select_related("lesson"), pk=pk)
    is_owner = rating.lesson.student_id == request.user.id
    if not (is_owner or request.user.is_moderator):
        raise PermissionDenied

    rating.review = ""
    rating.save(update_fields=["review"])
    messages.success(request, "Отзыв удалён.")
    return redirect("offers:detail", pk=rating.lesson.offer_id)
