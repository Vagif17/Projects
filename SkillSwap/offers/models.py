from cloudinary.models import CloudinaryField
from django.conf import settings
from django.db import models
from django.db.models import Avg, Count, Q


class Category(models.Model):
    name = models.CharField(max_length=40, unique=True)

    class Meta:
        verbose_name_plural = "categories"
        ordering = ["name"]

    def __str__(self):
        return self.name


class OfferQuerySet(models.QuerySet):
    def published(self):
        return self.filter(status=SkillOffer.Status.PUBLISHED)

    def with_stats(self):
        return self.annotate(
            avg_rating=Avg("lessons__rating__score"),
            ratings_count=Count("lessons__rating", distinct=True),
            completed_count=Count(
                "lessons",
                filter=Q(lessons__status="completed"),
                distinct=True,
            ),
        )


class SkillOffer(models.Model):
    class Level(models.TextChoices):
        BEGINNER = "Beginner"
        INTERMEDIATE = "Intermediate"
        ADVANCED = "Advanced"

    class Format(models.TextChoices):
        ONLINE = "Online"
        OFFLINE = "Offline"

    class Status(models.TextChoices):
        DRAFT = "Draft"
        PENDING = "Pending"
        PUBLISHED = "Published"
        REJECTED = "Rejected"

    author = models.ForeignKey(
        settings.AUTH_USER_MODEL, on_delete=models.CASCADE, related_name="offers"
    )
    category = models.ForeignKey(
        Category, on_delete=models.PROTECT, related_name="offers"
    )
    title = models.CharField(max_length=90)
    description = models.TextField()
    image = CloudinaryField("image", blank=True, null=True)
    level = models.CharField(
        max_length=15, choices=Level.choices, default=Level.BEGINNER
    )
    format = models.CharField(
        max_length=15, choices=Format.choices, default=Format.OFFLINE
    )
    duration_minutes = models.PositiveIntegerField()
    status = models.CharField(
        max_length=15, choices=Status.choices, default=Status.PENDING
    )
    created_at = models.DateTimeField(auto_now_add=True)

    pending_changes = models.JSONField(blank=True, null=True)

    objects = OfferQuerySet.as_manager()

    class Meta:
        ordering = ["-created_at"]

    def __str__(self):
        return self.title