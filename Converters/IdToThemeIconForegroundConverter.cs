using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.Converters;

public class IdToThemeIconForegroundConverter : IValueConverter {
    
    public static readonly IdToThemeIconForegroundConverter Instance = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is int userId) {
            var theme = AppSettingsService.Instance!.getThemeForUser(userId);
            return theme switch {
                "Dark" => new SolidColorBrush(Color.Parse("#2A2A2A")),
                "Light" => new SolidColorBrush(Colors.GhostWhite),
                _ => new SolidColorBrush(Colors.Gray)
            };
        }
        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}