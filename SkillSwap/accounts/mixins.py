from django.contrib.auth.mixins import LoginRequiredMixin, UserPassesTestMixin
from django.core.exceptions import PermissionDenied


class NotBannedMixin(LoginRequiredMixin):
    def dispatch(self, request, *args, **kwargs):
        if request.user.is_authenticated and request.user.is_banned:
            raise PermissionDenied("Вы заблокированы")
        return super().dispatch(request, *args, **kwargs)

class OfferOwnerOrModeratorMixin(UserPassesTestMixin):
    def test_func(self):
        o, u = self.get_object(), self.request.user
        return o.author_id == u.id or u.is_moderator