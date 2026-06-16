namespace ProjectManagementSystem.Models;

public class AssigneeEntry {

    public User User { get; set; } = null!;
    public string Role { get; set; } = string.Empty;
}