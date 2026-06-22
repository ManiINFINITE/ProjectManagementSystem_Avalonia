using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Views;

public partial class DashboardView : UserControl {

    private bool _isExpanded = true;
    
    public DashboardView() {
        InitializeComponent();
    }

    private void CollapseButton_Click(object? sender, RoutedEventArgs e) {
        if (_isExpanded) {
            Collapse();
        } else {
            Expand();
        }
        
        _isExpanded = !_isExpanded;
        if (DataContext is DashboardViewModel vm) {
            vm.IsLeftPanelExpanded = _isExpanded;
        }
    }

    private async void Collapse() {
        LeftPanel.Classes.Add("Collapsed");
        HLine1.Classes.Add("Collapsed");
        HLine2.Classes.Add("Collapsed");
        HLine3.Classes.Add("Collapsed");
        CollapseButton.Classes.Add("Collapsed");
        AddProjectButton.Classes.Add("Collapsed");
        HomeText.Classes.Add("Collapsed");
        MessagesText.Classes.Add("Collapsed");
        TasksText.Classes.Add("Collapsed");
        MembersText.Classes.Add("Collapsed");
        SettingsText.Classes.Add("Collapsed");
        MyProjectsText.Classes.Add("Collapsed");
        LogoutText.Classes.Add("Collapsed");
        ProjectMText.Classes.Add("Collapsed");
        ExpandedLeftPanelHeader.Classes.Add("Collapsed");

        await Task.Delay(300);

        HomeText.IsVisible = false;
        MessagesText.IsVisible = false;
        TasksText.IsVisible = false;
        MembersText.IsVisible = false;
        SettingsText.IsVisible = false;
        MyProjectsText.IsVisible = false;
        ProjectMText.IsVisible = false;
        ExpandedLeftPanelHeader.IsVisible = false;
    }

    private void Expand() {
        
        HomeText.IsVisible = true;
        MessagesText.IsVisible = true;
        TasksText.IsVisible = true;
        MembersText.IsVisible = true;
        SettingsText.IsVisible = true;
        MyProjectsText.IsVisible = true;
        ProjectMText.IsVisible = true;
        ExpandedLeftPanelHeader.IsVisible = true;
        
        LeftPanel.Classes.Remove("Collapsed");
        HLine1.Classes.Remove("Collapsed");
        HLine2.Classes.Remove("Collapsed");
        HLine3.Classes.Remove("Collapsed");
        CollapseButton.Classes.Remove("Collapsed");
        AddProjectButton.Classes.Remove("Collapsed");
        HomeText.Classes.Remove("Collapsed");
        MessagesText.Classes.Remove("Collapsed");
        TasksText.Classes.Remove("Collapsed");
        MembersText.Classes.Remove("Collapsed");
        SettingsText.Classes.Remove("Collapsed");
        MyProjectsText.Classes.Remove("Collapsed");
        LogoutText.Classes.Remove("Collapsed");
        ProjectMText.Classes.Remove("Collapsed");
        ExpandedLeftPanelHeader.Classes.Remove("Collapsed");
    }
    
    private void AddProjectButton_Click(object? sender, RoutedEventArgs e) {
        if (DataContext is DashboardViewModel vm) {
            vm.OpenCreateProject();
        }
    }

    private void ProjectButton_OnPointerEntered(object? sender, PointerEventArgs e) {
        
    }
}