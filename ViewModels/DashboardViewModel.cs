using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Repositories;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class DashboardViewModel : ViewModelBase {

    public static DashboardViewModel? Instance { get; private set; }

    private readonly UserProjectRepository _userProjectRepository = new();
    private readonly User? _currentUser;
    
    [ObservableProperty] private ProjectCreationViewModel _projectCreationViewModel = new();
    [ObservableProperty] private ViewModelBase _currentRightPanelView;
    [ObservableProperty] private bool _isCreateProjectOpen;
    [ObservableProperty] private ObservableCollection<Project> _userProjects = [];

    public DashboardViewModel() {
        Instance = this;
        
        if (Design.IsDesignMode) {
            _currentRightPanelView = new SettingsViewModel();
            return;
        }
        
        _currentUser = SessionService.Instance?.CurrentUser!;
        _currentRightPanelView = new SettingsViewModel();

        _ = LoadProjectsAsync();
    }

    private async Task LoadProjectsAsync() {
        if (_currentUser == null) return;
        var projects = await _userProjectRepository.GetProjectsByUserIdAsync(_currentUser.Id);
        UserProjects = new ObservableCollection<Project>(projects);
    }

    public async Task RefreshUserProjectsAsync() {
        await LoadProjectsAsync();
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

    public void OpenCreateProject() {
        ProjectCreationViewModel = new ProjectCreationViewModel();
        IsCreateProjectOpen = true;
    }

    public void CloseCreateProject() {
        IsCreateProjectOpen = false;
    }

    [RelayCommand]
    private void Logout() {
        SessionService.Instance.Logout();
        NotificationService.Instance.Send("Logged Out!", "You've logged out successfully.", NotificationType.Success);
    }
}