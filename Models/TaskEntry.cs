using System;
using ProjectManagementSystem.Enums;

namespace ProjectManagementSystem.Models;

public class TaskEntry {
    
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateOnly? Deadline { get; set; }
    public User? Assignee { get; set; }
}