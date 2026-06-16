using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Views;

public partial class ProjectCreationView : UserControl {
    public ProjectCreationView() {
        InitializeComponent();
    }

    private void DiscardIcon_Tapped(object sender, TappedEventArgs e) {
        if (DataContext is ProjectCreationViewModel vm) {
            vm.DiscardCommand.Execute(null);
        }
    }

    private async void CreateProject_Tapped(object sender, RoutedEventArgs e) {
        if (DataContext is ProjectCreationViewModel vm) {
            await vm.CreateProjectCommand.ExecuteAsync(null);
        }
    }
}