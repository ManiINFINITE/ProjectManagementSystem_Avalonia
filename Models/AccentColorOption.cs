using Avalonia.Media;

namespace ProjectManagementSystem.Models;

public class AccentColorOption {
    
    public string Name { get; set; } = string.Empty;
    public SolidColorBrush PrimaryColor { get; set; } = new(Colors.Transparent);
    public SolidColorBrush SecondaryColor { get; set; } =  new(Colors.Transparent);
    public SolidColorBrush PrimaryLoginPanelAccentColor_LIGHT { get; set; } = new(Colors.Transparent);
    public SolidColorBrush SecondaryLoginPanelAccentColor_LIGHT { get; set; } = new(Colors.Transparent);
    public SolidColorBrush PrimaryLoginPanelAccentColor_DARK { get; set; } = new(Colors.Transparent);
    public SolidColorBrush SecondaryLoginPanelAccentColor_DARK { get; set; } = new(Colors.Transparent);
    public string ImagePath { get; set; } = "avares://ProjectManagementSystem/Assets/Images/slate-violet.jpg";
    public SolidColorBrush ImageTextForeground { get; set; } = new(Colors.Transparent);
    public SolidColorBrush ButtonHoverBackground { get; set; } = new(Colors.Transparent);
}