from django.contrib import admin

from .models import Category, SkillOffer


@admin.register(Category)
class CategoryAdmin(admin.ModelAdmin):
    list_display = ("name",)
    search_fields = ("name",)


@admin.register(SkillOffer)
class SkillOfferAdmin(admin.ModelAdmin):
    list_display = ("title", "author", "category", "status", "created_at")
    list_filter = ("status", "category", "level", "format")
    search_fields = ("title", "author__username")
