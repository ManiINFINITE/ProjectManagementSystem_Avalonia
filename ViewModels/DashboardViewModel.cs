using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class DashboardViewModel : ViewModelBase {
    
    private readonly User _currentUser = SessionService.Instance.CurrentUser!;
    
    [ObservableProperty] private string _greeting;

    public DashboardViewModel() {
        _greeting = $"Welcome {_currentUser.Username}!";
    }

    [RelayCommand]
    private void Logout() {
        SessionService.Instance?.Logout();
    }
}