using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Converters;

public class UserAvatarColorConverter : IValueConverter {
    
    public static readonly UserAvatarColorConverter Instance = new();

    private static readonly string[] Palette = {
        "#F94144", "#F3722C", "#F8961E", "#F9C74F",
        "#90BE6D", "#43AA8B", "#577590", "#277DA1",
        "#9D4EDD", "#FF5DA2", "#4D908E", "#6A4C93"
    };

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {

        if (value is User user) {
            var seed = user.Id != 0 ? user.Id : user.Username.GetHashCode();
            var index = Math.Abs(seed) %  Palette.Length;
            return SolidColorBrush.Parse(Palette[index]);
        }

        return Brushes.Gray;
    }
    
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}