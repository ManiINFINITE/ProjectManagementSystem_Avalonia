using Avalonia.Media;

namespace ProjectManagementSystem.Models;

public class AccentColorOption {
    
    public string Name { get; set; } = string.Empty;
    public SolidColorBrush PrimaryColor { get; set; } = new(Colors.Transparent);
    public SolidColorBrush SecondaryColor { get; set; } =  new(Colors.Transparent);
}