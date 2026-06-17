using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ProjectManagementSystem.Enums;

namespace ProjectManagementSystem.Converters;

public class TaskPriorityColorConverter : IValueConverter {
    
    public static readonly TaskPriorityColorConverter Instance = new();

    private static readonly SolidColorBrush LowBrush = new(Color.Parse("#49c730"));
    private static readonly SolidColorBrush MediumBrush = new(Color.Parse("#fc9003"));
    private static readonly SolidColorBrush HighBrush = new(Color.Parse("#db2a2a"));
    private static readonly SolidColorBrush DefaultBrush = new(Colors.Gray);

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {

        return value switch {
            TaskPriority.Low => LowBrush,
            TaskPriority.Medium => MediumBrush,
            TaskPriority.High => HighBrush,
            _ => DefaultBrush
        };
    }
    
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}