using System;

namespace ProjectManagementSystem.Services;

public class SharedAnimationService {
    
    public static SharedAnimationService Instance { get; } = new();
    public event Action? ToggleRequested;
    
    public void RequestToggle() => ToggleRequested?.Invoke();
}