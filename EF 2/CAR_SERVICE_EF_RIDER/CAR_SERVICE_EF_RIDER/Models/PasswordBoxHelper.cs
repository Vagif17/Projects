namespace CAR_SERVICE_EF_RIDER.Models;

using System.Windows;
using System.Windows.Controls;

/*
 Сделал полностью ИИ чтобы привязать значения в PasswordBox с свойствами с использованием MVVM.
 Я не нашел другого способа кроме как этого,по самому этому коду есть вопросы
*/
public static class PasswordBoxHelper
{
    public static readonly DependencyProperty BoundPassword =
        DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxHelper),
            new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

    public static string GetBoundPassword(DependencyObject obj)
    {
        return (string)obj.GetValue(BoundPassword);
    }

    public static void SetBoundPassword(DependencyObject obj, string value)
    {
        obj.SetValue(BoundPassword, value);
    }

    private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        
        if (d is PasswordBox passwordBox)
        {
            
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            if ((string)e.NewValue != passwordBox.Password)
                passwordBox.Password = (string)e.NewValue ?? string.Empty;

            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            
        }
    }

    private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is PasswordBox passwordBox)
        {
            SetBoundPassword(passwordBox, passwordBox.Password ?? string.Empty);
        }
    }
}