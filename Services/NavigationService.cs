using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Services;

public class NavigationService {

    public static NavigationService Instance { get; } = new();

    public void NavigateTo(ViewModelBase viewModel) {
        MainWindowViewModel.Instance?.NavigateTo(viewModel);
    }
}