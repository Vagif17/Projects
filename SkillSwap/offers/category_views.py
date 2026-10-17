from django import forms
from django.contrib import messages
from django.contrib.auth.decorators import login_required
from django.core.exceptions import PermissionDenied
from django.db.models import Count, ProtectedError
from django.shortcuts import get_object_or_404, redirect, render

from .models import Category


class CategoryForm(forms.ModelForm):
    class Meta:
        model = Category
        fields = ["name"]
        labels = {"name": "Название категории"}
        widgets = {"name": forms.TextInput(attrs={"placeholder": "Например: Cooking"})}

    def clean_name(self):
        name = self.cleaned_data["name"].strip()
        if len(name) < 2:
            raise forms.ValidationError("Название должно содержать минимум 2 символа.")
        duplicates = Category.objects.filter(name__iexact=name)
        if self.instance.pk:
            duplicates = duplicates.exclude(pk=self.instance.pk)
        if duplicates.exists():
            raise forms.ValidationError("Такая категория уже существует.")
        return name


def _require_super(user):
    if not user.is_super:
        raise PermissionDenied


@login_required
def category_manage(request):
    _require_super(request.user)
    categories = Category.objects.annotate(offers_count=Count("offers")).order_by("name")
    return render(request, "offers/category_manage.html", {"categories": categories})


@login_required
def category_create(request):
    _require_super(request.user)
    form = CategoryForm(request.POST or None)
    if request.method == "POST" and form.is_valid():
        form.save()
        messages.success(request, "Категория создана.")
        return redirect("offers:category_manage")
    return render(
        request,
        "offers/category_form.html",
        {"form": form, "page_title": "Новая категория"},
    )


@login_required
def category_update(request, pk):
    _require_super(request.user)
    category = get_object_or_404(Category, pk=pk)
    form = CategoryForm(request.POST or None, instance=category)
    if request.method == "POST" and form.is_valid():
        form.save()
        messages.success(request, "Категория обновлена.")
        return redirect("offers:category_manage")
    return render(
        request,
        "offers/category_form.html",
        {"form": form, "page_title": "Редактирование категории"},
    )


@login_required
def category_delete(request, pk):
    _require_super(request.user)
    category = get_object_or_404(
        Category.objects.annotate(offers_count=Count("offers")), pk=pk
    )
    if request.method == "POST":
        try:
            category.delete()
            messages.success(request, "Категория удалена.")
        except ProtectedError:
            messages.error(
                request, "Нельзя удалить категорию, в которой есть предложения."
            )
        return redirect("offers:category_manage")
    return render(request, "offers/category_confirm_delete.html", {"category": category})
