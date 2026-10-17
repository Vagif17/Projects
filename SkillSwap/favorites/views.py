from django.contrib import messages
from django.contrib.auth.decorators import login_required
from django.shortcuts import get_object_or_404, redirect, render
from django.utils.http import url_has_allowed_host_and_scheme
from django.views.decorators.http import require_POST

from offers.models import SkillOffer

from .models import Favorite
from .utils import favorite_ids_for


@login_required
def favorites_list(request):
    offers = (
        SkillOffer.objects.published()
        .filter(favorites__user=request.user)
        .with_stats()
        .select_related("author", "category")
    )
    return render(
        request,
        "favorites/list.html",
        {"offers": offers, "favorite_ids": favorite_ids_for(request.user)},
    )


@login_required
@require_POST
def toggle_favorite(request, offer_pk):
    offer = get_object_or_404(
        SkillOffer, pk=offer_pk, status=SkillOffer.Status.PUBLISHED
    )
    favorite, created = Favorite.objects.get_or_create(user=request.user, offer=offer)
    if created:
        messages.success(request, "Добавлено в избранное.")
    else:
        favorite.delete()
        messages.success(request, "Удалено из избранного.")

    next_url = request.POST.get("next", "")
    if next_url and url_has_allowed_host_and_scheme(
        next_url, allowed_hosts={request.get_host()}
    ):
        return redirect(next_url)
    return redirect("offers:detail", pk=offer.pk)
