using System;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Services;

public class NotificationService {
    
    public static NotificationService Instance { get; } = new();

    public event Action<Notification>? NotificationRequested;

    public void Send(string title, string message, NotificationType type) {
        NotificationRequested?.Invoke(new Notification {
            Title = title,
            Message = message,
            Type = type
        });
    }

    public void Send(string title, NotificationType type) {
        NotificationRequested?.Invoke(new Notification {
            Title = title,
            Type = type
        });
    }
}