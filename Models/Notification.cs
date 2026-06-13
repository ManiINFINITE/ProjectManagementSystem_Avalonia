using ProjectManagementSystem.Enums;

namespace ProjectManagementSystem.Models;

public class Notification {
    
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; } = NotificationType.Normal;
}