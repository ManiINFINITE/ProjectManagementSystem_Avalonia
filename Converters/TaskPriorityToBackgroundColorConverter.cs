using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ProjectManagementSystem.Enums;

namespace ProjectManagementSystem.Converters;

public class TaskPriorityToBackgroundColorConverter : IValueConverter {
    public static readonly TaskPriorityToBackgroundColorConverter Instance = new();

    private readonly SolidColorBrush Low = new(Color.Parse("#bfffad"));
    private readonly SolidColorBrush Medium = new(Color.Parse("#ffecad"));
    private readonly SolidColorBrush High = new(Color.Parse("#ffa6a6"));
    private readonly  SolidColorBrush Default = new(Color.Parse("#e0e0e0"));

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is TaskPriority priority) {
            return priority switch {
                TaskPriority.Low => Low,
                TaskPriority.Medium => Medium,
                TaskPriority.High => High,
                _ => Default
            };
        }

        return Default;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}