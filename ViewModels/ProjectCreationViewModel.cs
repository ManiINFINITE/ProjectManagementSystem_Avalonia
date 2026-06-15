using CommunityToolkit.Mvvm.Input;

namespace ProjectManagementSystem.ViewModels;

public partial class ProjectCreationViewModel : ViewModelBase {

    [RelayCommand]
    private void Discard() {
        DashboardViewModel.Instance?.CloseCreateProject();
    }
}