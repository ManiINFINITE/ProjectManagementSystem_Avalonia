using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Converters;

public class RoleTextColorConverter : IValueConverter
{
    public static readonly RoleTextColorConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string role && !string.IsNullOrWhiteSpace(role))
            return new SolidColorBrush(RoleColorPalette.GetBaseColor(role), 0.73); 
    
        return new SolidColorBrush(Colors.Gray, 0.73);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}