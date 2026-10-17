from django.urls import path

from . import views

app_name = "lessons"

urlpatterns = [
    path("offers/<int:offer_pk>/request/", views.request_create, name="create"),
    path("requests/", views.my_requests, name="my_requests"),
    path("requests/<int:pk>/rate/", views.rate_lesson, name="rate"),
    path("requests/<int:pk>/<slug:action>/", views.request_action, name="action"),
    path("reviews/<int:pk>/delete/", views.review_delete, name="review_delete"),
]
