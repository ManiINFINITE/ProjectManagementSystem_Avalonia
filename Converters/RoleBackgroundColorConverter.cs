using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Converters;

public class RoleBackgroundColorConverter : IValueConverter {
    
    public static readonly RoleBackgroundColorConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string role && !string.IsNullOrWhiteSpace(role))
            return new SolidColorBrush(RoleColorPalette.GetBaseColor(role), 0.19); // or 0.73 for text
    
        return new SolidColorBrush(Colors.Gray, 0.19);
    }
    
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}