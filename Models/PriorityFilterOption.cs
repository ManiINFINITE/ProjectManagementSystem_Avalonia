using ProjectManagementSystem.Enums;

namespace ProjectManagementSystem.Models;

public class PriorityFilterOption {
    public string Name { get; init; } = "";
    public TaskPriority? Value { get; init; }
}