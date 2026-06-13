using CommunityToolkit.Mvvm.ComponentModel;

namespace ProjectManagementSystem.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {
    
    public static MainWindowViewModel? Instance { get; private set; }

    [ObservableProperty] private NotificationViewModel _notificationViewModel = new();

    [ObservableProperty] private ViewModelBase _currentView;

    public MainWindowViewModel() {
        Instance = this;
        _currentView = new AuthenticationViewModel();
    }

    public void NavigateTo(ViewModelBase viewModel) {
        CurrentView = viewModel;
    }
}