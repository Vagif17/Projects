from django import forms
from django.core.files.uploadedfile import UploadedFile

from .models import Category, SkillOffer

MAX_IMAGE_MB = 5


class OfferForm(forms.ModelForm):
    # Обычное поле-картинка: размер проверяем ДО загрузки в Cloudinary.
    # Сама загрузка произойдёт при offer.save() (её делает CloudinaryField).
    image = forms.ImageField(
        label="Картинка",
        required=False,
        widget=forms.FileInput(attrs={"accept": "image/*"}),
    )

    class Meta:
        model = SkillOffer
        fields = [
            "title",
            "category",
            "level",
            "format",
            "duration_minutes",
            "description",
            "image",
        ]
        labels = {
            "title": "Название",
            "category": "Категория",
            "level": "Уровень",
            "format": "Формат",
            "duration_minutes": "Длительность занятия (минут)",
            "description": "Описание",
        }
        widgets = {
            "title": forms.TextInput(attrs={"placeholder": "Например: Python с нуля"}),
            "description": forms.Textarea(
                attrs={"rows": 6, "placeholder": "Чему вы научите и как пройдёт занятие"}
            ),
            "duration_minutes": forms.NumberInput(attrs={"min": 15, "max": 480, "step": 5}),
        }

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.fields["category"].queryset = Category.objects.order_by("name")
        self.fields["category"].empty_label = "Выберите категорию"

    def clean_title(self):
        title = self.cleaned_data["title"].strip()
        if len(title) < 5:
            raise forms.ValidationError("Название должно содержать минимум 5 символов.")
        return title

    def clean_description(self):
        description = self.cleaned_data["description"].strip()
        if len(description) < 30:
            raise forms.ValidationError("Опишите подробнее: минимум 30 символов.")
        return description

    def clean_duration_minutes(self):
        minutes = self.cleaned_data["duration_minutes"]
        if not 15 <= minutes <= 480:
            raise forms.ValidationError("Длительность: от 15 до 480 минут.")
        return minutes

    def clean_image(self):
        image = self.cleaned_data.get("image")
        # Проверяем только новый загруженный файл (старая картинка уже в Cloudinary)
        if isinstance(image, UploadedFile) and image.size > MAX_IMAGE_MB * 1024 * 1024:
            raise forms.ValidationError(f"Файл слишком большой (максимум {MAX_IMAGE_MB} МБ).")
        return image
