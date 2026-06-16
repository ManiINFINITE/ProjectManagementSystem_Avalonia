using System.Collections.ObjectModel;
using Avalonia.Media;

namespace ProjectManagementSystem.Models;

public class ProjectColorsBase {

    public static ObservableCollection<ProjectColorOption> ProjectColors { get; } = new() {
        
        new ProjectColorOption {
            Name = "Red",
            Brush = new SolidColorBrush(Colors.Red),
        },
        new ProjectColorOption {
            Name = "Green",
            Brush = new SolidColorBrush(Colors.Green),
        },
        new ProjectColorOption {
            Name = "Yellow",
            Brush = new SolidColorBrush(Colors.Yellow),
        },
        new ProjectColorOption {
            Name = "Blue",
            Brush = new SolidColorBrush(Colors.Blue),
        },
        new ProjectColorOption {
            Name = "Purple",
            Brush = new SolidColorBrush(Colors.BlueViolet),
        },
        new ProjectColorOption {
            Name = "Orange",
            Brush = new SolidColorBrush(Colors.OrangeRed),
        },
    };
}