from django.urls import path

from . import category_views, views

app_name = "offers"

urlpatterns = [
    path("", views.explore, name="explore"),
    path("popular/", views.popular, name="popular"),
    path("categories/", views.categories, name="categories"),
    path("categories/<int:pk>/", views.category_detail, name="category"),
    path("manage/categories/", category_views.category_manage, name="category_manage"),
    path("manage/categories/new/", category_views.category_create, name="category_create"),
    path(
        "manage/categories/<int:pk>/edit/",
        category_views.category_update,
        name="category_update",
    ),
    path(
        "manage/categories/<int:pk>/delete/",
        category_views.category_delete,
        name="category_delete",
    ),
    path("offers/new/", views.offer_create, name="create"),
    path("offers/<int:pk>/", views.offer_detail, name="detail"),
    path("offers/<int:pk>/edit/", views.offer_update, name="update"),
    path("offers/<int:pk>/delete/", views.offer_delete, name="delete"),
    path("offers/<int:pk>/approve/", views.offer_approve, name="approve"),
    path("offers/<int:pk>/reject/", views.offer_reject, name="reject"),
    path("my-offers/", views.my_offers, name="my_offers"),
    path("moderation/", views.moderation, name="moderation"),
]
