using System;
using System.Globalization;
using Avalonia.Data.Converters;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.Converters;

public class IdToThemeIconConverter : IValueConverter {
    
    public static readonly IdToThemeIconConverter Instance = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is int userId) {
            var theme = AppSettingsService.Instance!.getThemeForUser(userId);
            return theme switch {
                "Dark" => "\uE330",
                "Light" => "\uE472",
                _ => "\uE272"
            };
        }
        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}