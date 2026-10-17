from django import forms

from .models import LessonRequest, Rating


class RequestForm(forms.ModelForm):
    class Meta:
        model = LessonRequest
        fields = ["message"]
        widgets = {
            "message": forms.Textarea(
                attrs={"rows": 3, "placeholder": "Сообщение автору (необязательно)"}
            )
        }

    def clean_message(self):
        message = self.cleaned_data["message"].strip()
        if len(message) > 500:
            raise forms.ValidationError("Сообщение слишком длинное (максимум 500 символов).")
        return message


class RatingForm(forms.ModelForm):
    score = forms.TypedChoiceField(
        label="Оценка",
        coerce=int,
        choices=[
            (5, "★★★★★  5: отлично"),
            (4, "★★★★☆  4: хорошо"),
            (3, "★★★☆☆  3: нормально"),
            (2, "★★☆☆☆  2: плохо"),
            (1, "★☆☆☆☆  1: ужасно"),
        ],
    )

    class Meta:
        model = Rating
        fields = ["score", "review"]
        labels = {"review": "Отзыв"}
        widgets = {
            "review": forms.Textarea(
                attrs={"rows": 5, "placeholder": "Как прошло занятие? (необязательно)"}
            )
        }

    def clean_review(self):
        review = self.cleaned_data["review"].strip()
        if len(review) > 1000:
            raise forms.ValidationError("Отзыв слишком длинный (максимум 1000 символов).")
        return review
