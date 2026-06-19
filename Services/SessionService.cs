using ProjectManagementSystem.Models;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Services;

public class SessionService {

    public static SessionService Instance { get; } = new();
    
    public User? CurrentUser { get; private set; }
    
    public bool IsLoggedIn => CurrentUser != null;

    public void Login(User user) {
        CurrentUser = user;
    }

    public void Logout() {
        string accentColorName = CurrentUser?.AccentColorName!;
        CurrentUser = null;
        NavigationService.Instance?.NavigateTo(new AuthenticationViewModel(accentColorName));
    }
}