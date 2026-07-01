using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using ProjectManagementSystem.Enums;

namespace ProjectManagementSystem.Converters;

public class TaskPriorityToForegroundColorConverter : IValueConverter {
    public static readonly TaskPriorityToForegroundColorConverter Instance = new();
    
    private readonly SolidColorBrush Low = new(Color.Parse("#13c406"));
    private readonly SolidColorBrush Medium = new(Color.Parse("#9c8f1c"));
    private readonly SolidColorBrush High = new(Color.Parse("#bf0a0a"));
    private readonly  SolidColorBrush Default = new(Color.Parse("#707070"));

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