from .models import Favorite


def favorite_ids_for(user):
    if not user.is_authenticated:
        return set()
    return set(Favorite.objects.filter(user=user).values_list("offer_id", flat=True))
