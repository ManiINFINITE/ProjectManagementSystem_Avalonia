namespace ProjectManagementSystem.Models;

public class UserSettings {

    public string Theme { get; set; } = "System";
    public string AccentColorName { get; set; } = "Slate Violet";
    public bool NotificationsEnabled { get; set; } = true;
    public int NotificationAutoDismissDuration { get; set; } = 10000; // milliseconds
}