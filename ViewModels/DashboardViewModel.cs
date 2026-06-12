using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class DashboardViewModel : ViewModelBase {

    private readonly User? _currentUser;

    [ObservableProperty] private ViewModelBase _currentRightPanelView;

    public DashboardViewModel() {
        if (Design.IsDesignMode) {
            _currentRightPanelView = new SettingsViewModel();
            return;
        }
        
        _currentUser = SessionService.Instance?.CurrentUser!;
        _currentRightPanelView = new SettingsViewModel();
    }
    
    [RelayCommand]
    private void NavigateToHome() => CurrentRightPanelView = new HomeViewModel();
    
    [RelayCommand]
    private void NavigateToMessages() => CurrentRightPanelView = new MessagesViewModel();
    
    [RelayCommand]
    private void NavigateToTasks() => CurrentRightPanelView = new TasksViewModel();
    
    [RelayCommand]
    private void NavigateToMembers() => CurrentRightPanelView = new MembersViewModel();
    
    [RelayCommand]
    private void NavigateToSettings() => CurrentRightPanelView = new SettingsViewModel();

    [RelayCommand]
    private void Logout() {
        SessionService.Instance?.Logout();
    }
}