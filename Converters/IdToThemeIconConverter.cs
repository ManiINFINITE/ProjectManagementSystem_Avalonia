using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace ProjectManagementSystem.Converters;

public class ThemeToIconConverter : IValueConverter {
    
    public static readonly ThemeToIconConverter Instance = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is string theme) {
            
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}