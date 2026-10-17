from django.contrib import admin

from .models import LessonRequest, Rating


@admin.register(LessonRequest)
class LessonRequestAdmin(admin.ModelAdmin):
    list_display = ("offer", "student", "status", "created_at")
    list_filter = ("status",)
    search_fields = ("offer__title", "student__username")


@admin.register(Rating)
class RatingAdmin(admin.ModelAdmin):
    list_display = ("lesson", "score", "created_at")
    list_filter = ("score",)
