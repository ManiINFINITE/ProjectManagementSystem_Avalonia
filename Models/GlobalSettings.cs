using System.Collections.Generic;

namespace ProjectManagementSystem.Models;

public class GlobalSettings {

    public int? LastUserId { get; set; } = null;
    public List<int> RememberedUsers { get; set; } = [];
}