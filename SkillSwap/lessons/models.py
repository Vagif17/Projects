from django.conf import settings
from django.core.validators import MaxValueValidator, MinValueValidator
from django.db import models
from django.db.models import Q


class LessonRequest(models.Model):
    class Status(models.TextChoices):
        PENDING = "pending", "Pending"
        ACCEPTED = "accepted", "Accepted"
        REJECTED = "rejected", "Rejected"
        COMPLETED = "completed", "Completed"
        CANCELLED = "cancelled", "Cancelled"

    # «Активные» запросы: пока такой есть, новый на то же предложение создать нельзя
    ACTIVE = ("pending", "accepted")

    offer = models.ForeignKey(
        "offers.SkillOffer", on_delete=models.CASCADE, related_name="lessons"
    )
    student = models.ForeignKey(
        settings.AUTH_USER_MODEL, on_delete=models.CASCADE, related_name="sent_requests"
    )
    message = models.TextField(blank=True)
    status = models.CharField(
        max_length=10, choices=Status.choices, default=Status.PENDING
    )
    created_at = models.DateTimeField(auto_now_add=True)

    class Meta:
        ordering = ["-created_at"]
        constraints = [
            models.UniqueConstraint(
                fields=["offer", "student"],
                condition=Q(status__in=["pending", "accepted"]),
                name="unique_active_request",
            )
        ]

    def __str__(self):
        return f"{self.student} → {self.offer} ({self.status})"


class Rating(models.Model):
    # OneToOne: одна оценка (и отзыв) на одно завершённое занятие
    lesson = models.OneToOneField(
        LessonRequest, on_delete=models.CASCADE, related_name="rating"
    )
    score = models.PositiveSmallIntegerField(
        validators=[MinValueValidator(1), MaxValueValidator(5)]
    )
    review = models.TextField(blank=True)
    created_at = models.DateTimeField(auto_now_add=True)

    class Meta:
        ordering = ["-created_at"]

    def __str__(self):
        return f"{self.score}/5 · {self.lesson}"
