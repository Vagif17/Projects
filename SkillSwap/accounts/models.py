from cloudinary.provisioning import Role
from django.db import models
from django.contrib.auth.models import AbstractUser
from cloudinary.models import CloudinaryField

class User(AbstractUser):
    class Role(models.TextChoices):
        SUPERADMIN = "superadmin", "Super Admin"
        ADMIN = "admin", "Admin"
        USER = "user", "User"

    role = models.CharField(max_length=20, choices=Role.choices, default=Role.USER)
    avatar = CloudinaryField('avatar', blank=True, null=True)
    bio = models.CharField(max_length=200, blank=True, null=True)
    is_banned = models.BooleanField(default=False)

    @property
    def is_super(self):
        return self.role == self.Role.SUPERADMIN

    @property
    def is_moderator(self):
        return self.role in (self.Role.ADMIN, self.Role.SUPERADMIN)

    def save(self, *args, **kwargs):
        if self.is_superuser:
            self.role = self.Role.SUPERADMIN
        super().save(*args, **kwargs)
