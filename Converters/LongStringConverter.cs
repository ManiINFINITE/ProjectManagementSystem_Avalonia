using System;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

namespace ProjectManagementSystem.Converters;

public class LongStringConverter : IValueConverter {
    
    public static readonly LongStringConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {

        if (value is string s && parameter is string paramStr && int.TryParse(paramStr, out var max)) {
            if (s.Length > max) {
                return s.Substring(0, max) + "...";
            }

            return s;
        }
        return value;
    }
    
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}