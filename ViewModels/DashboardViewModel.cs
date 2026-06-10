using ProjectManagementSystem.Models;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class DashboardViewModel : ViewModelBase {
    
    private readonly User _currentUser = SessionService.Instance.CurrentUser!;
    
}