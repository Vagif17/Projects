namespace CAR_SERVICE_EF_RIDER.Models;

using System.Globalization;
using System.Windows.Controls;

public class IntegerValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return new ValidationResult(false, "Введите число");

        if (int.TryParse(value.ToString(), out _))
            return ValidationResult.ValidResult;

        return new ValidationResult(false, "Можно вводить только целые числа");
    }
}
