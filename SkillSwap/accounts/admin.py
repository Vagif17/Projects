from django.contrib import admin
from django.contrib.auth.admin import UserAdmin

from .models import User


@admin.register(User)
class CustomUserAdmin(UserAdmin):
    fieldsets = UserAdmin.fieldsets + (
        ("SkillSwap", {"fields": ("role", "avatar", "bio", "is_banned")}),
    )
    list_display = ("username", "email", "role", "is_banned", "is_staff")
    list_filter = UserAdmin.list_filter + ("role", "is_banned")
