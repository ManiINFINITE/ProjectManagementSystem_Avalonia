namespace ProjectManagementSystem.Models;

public class AppSettings {

    public string Theme { get; set; } = "System";
    public string PrimaryAccentColor { get; set; } = "SlateBlue";
    public string SecondaryAccentColor { get; set; } = "BlueViolet";
    public string PrimaryLoginPanelAccentColor_LIGHT { get; set; } = "#BDC2FF";
    public string SecondaryLoginPanelAccentColor_LIGHT { get; set; } = "#8D80FF";
    public string PrimaryLoginPanelAccentColor_DARK { get; set; } = "#343469";
    public string SecondaryLoginPanelAccentColor_DARK { get; set; } = "#0E0E1F";
    public string AccentColorName { get; set; } = "Slate Violet";
    public string ImagePath { get; set; } = "avares://ProjectManagementSystem/Assets/Images/slate-violet.jpg";
    public string ImageTextForeground { get; set; } = "#9692FF";
    public string ButtonHoverBackground { get; set; } = "#3E3E7C";
    public int? LastUserId { get; set; }
    public bool NotificationsEnabled { get; set; } = true;
}