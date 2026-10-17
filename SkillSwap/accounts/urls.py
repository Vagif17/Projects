from django.contrib.auth import views as auth_views
from django.urls import path

from . import manage_views, views

urlpatterns = [
    path("register/", views.register, name="register"),
    path(
        "login/",
        auth_views.LoginView.as_view(redirect_authenticated_user=True),
        name="login",
    ),
    path("logout/", auth_views.LogoutView.as_view(), name="logout"),
    path("mentors/", views.mentors, name="mentors"),
    path("manage/users/", manage_views.users_manage, name="users_manage"),
    path("profile/edit/", views.profile_edit, name="profile_edit"),
    path("users/<str:username>/", views.profile, name="profile"),
    path("users/<str:username>/ban/", views.user_toggle_ban, name="user_ban"),
    path("users/<str:username>/role/", views.user_set_role, name="user_role"),
]
