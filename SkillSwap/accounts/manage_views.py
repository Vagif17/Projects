from django.contrib.auth.decorators import login_required
from django.core.exceptions import PermissionDenied
from django.shortcuts import render

from .models import User


@login_required
def users_manage(request):
    if not request.user.is_moderator:
        raise PermissionDenied

    q = request.GET.get("q", "").strip()
    members = User.objects.order_by("username")
    if q:
        needle = q.casefold()
        ids = [
            pk
            for pk, name in members.values_list("pk", "username")
            if needle in name.casefold()
        ]
        members = members.filter(pk__in=ids)

    return render(request, "accounts/users_manage.html", {"members": members, "q": q})
