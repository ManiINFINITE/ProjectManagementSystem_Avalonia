using System;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Converters;

public class AccentColorNameToPrimaryConverter : IValueConverter {
    public static readonly AccentColorNameToPrimaryConverter Instance = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is not string name) return null;
        return AccentColorsBase.AccentColors
                   .FirstOrDefault(c => c.Name == name)?.PrimaryColor.Color
               ?? AccentColorsBase.AccentColors.First().PrimaryColor.Color;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}