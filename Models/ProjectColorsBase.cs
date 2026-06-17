using System.Collections.ObjectModel;
using Avalonia.Media;

namespace ProjectManagementSystem.Models;

public class ProjectColorsBase {

    public static ObservableCollection<ProjectColorOption> ProjectColors { get; } = new() {
        
        new ProjectColorOption {
            Name = "Red",
            Brush = new SolidColorBrush(Color.FromRgb(245, 66, 66)),
        },
        new ProjectColorOption {
            Name = "Green",
            Brush = new SolidColorBrush(Color.FromRgb(74, 247, 83)),
        },
        new ProjectColorOption {
            Name = "Pink",
            Brush = new SolidColorBrush(Color.FromRgb(245, 88, 200)),
        },
        new ProjectColorOption {
            Name = "Blue",
            Brush = new SolidColorBrush(Color.FromRgb(88, 88, 245)),
        },
        new ProjectColorOption {
            Name = "Purple",
            Brush = new SolidColorBrush(Color.FromRgb(172, 71, 255)),
        },
        new ProjectColorOption {
            Name = "Orange",
            Brush = new SolidColorBrush(Color.FromRgb(245, 114, 49)),
        },
    };
}