using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class DashboardViewModel : ViewModelBase {
    
    private readonly User _currentUser = SessionService.Instance.CurrentUser!;

    [RelayCommand]
    private void Logout() {
        SessionService.Instance?.Logout();
    }
    
}