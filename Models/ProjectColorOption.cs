using Avalonia.Media;

namespace ProjectManagementSystem.Models;

public class ProjectColorOption {
    
    public string Name { get; set; } = string.Empty;
    public SolidColorBrush Brush { get; set; } = new(Colors.Transparent);
    public string HexColor => Brush.Color.ToString();
}