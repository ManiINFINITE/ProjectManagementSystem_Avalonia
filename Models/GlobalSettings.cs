using System.Collections.Generic;

namespace ProjectManagementSystem.Models;

public class GlobalSettings {

    public List<int> QuickLoginUsers { get; set; } = [];
    public int RememberedUser { get; set; } = -1;
}