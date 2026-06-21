using System;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Converters;

public class AccentColorNameToSecondaryConverter : IValueConverter {
    public static readonly AccentColorNameToSecondaryConverter Instance = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is not string name) return null;
        return AccentColorsBase.AccentColors
                   .FirstOrDefault(c => c.Name == name)?.SecondaryColor.Color
               ?? AccentColorsBase.AccentColors.First().SecondaryColor.Color;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}