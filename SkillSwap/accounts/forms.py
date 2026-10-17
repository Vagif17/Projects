from django import forms
from django.contrib.auth.forms import UserCreationForm
from django.core.files.uploadedfile import UploadedFile

from .models import User

MAX_AVATAR_MB = 5


class RegisterForm(UserCreationForm):
    email = forms.EmailField(required=True)

    class Meta:
        model = User
        fields = ("username", "email", "password1", "password2")

    def clean_email(self):
        email = self.cleaned_data["email"].lower()
        if User.objects.filter(email__iexact=email).exists():
            raise forms.ValidationError("Пользователь с таким email уже существует.")
        return email


class ProfileForm(forms.ModelForm):
    avatar = forms.ImageField(
        label="Аватар",
        required=False,
        widget=forms.FileInput(attrs={"accept": "image/*"}),
    )

    class Meta:
        model = User
        fields = ["avatar", "bio"]
        labels = {"bio": "Краткое описание"}
        widgets = {
            "bio": forms.Textarea(
                attrs={"rows": 4, "placeholder": "Расскажите о себе и своём опыте"}
            )
        }

    def clean_avatar(self):
        avatar = self.cleaned_data.get("avatar")
        if isinstance(avatar, UploadedFile) and avatar.size > MAX_AVATAR_MB * 1024 * 1024:
            raise forms.ValidationError(f"Файл слишком большой (максимум {MAX_AVATAR_MB} МБ).")
        return avatar
