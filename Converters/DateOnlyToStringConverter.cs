using System;
using System.Globalization;
using Avalonia.Data.Converters;
using ProjectManagementSystem.Helpers;

namespace ProjectManagementSystem.Converters;

public class DateOnlyToStringConverter : IValueConverter {
    
    public static readonly DateOnlyToStringConverter Instance = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {

        return value switch {
            DateOnly dateOnly => DateTimeFormatter.ToDateString(dateOnly.ToDateTime(TimeOnly.MinValue)),
            DateTime dateTime => DateTimeFormatter.ToDateString(dateTime),
            _ => string.Empty
        };
    }
    
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}