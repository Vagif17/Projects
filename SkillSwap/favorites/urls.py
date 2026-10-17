from django.urls import path

from . import views

app_name = "favorites"

urlpatterns = [
    path("favorites/", views.favorites_list, name="list"),
    path("favorites/toggle/<int:offer_pk>/", views.toggle_favorite, name="toggle"),
]
