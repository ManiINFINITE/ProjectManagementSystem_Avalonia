using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Views;

public partial class TaskInfoView : UserControl {
    public TaskInfoView() {
        InitializeComponent();
    }

    private void RemoveTask_Tapped(object sender, RoutedEventArgs e) {
        if (sender is TextBlock { DataContext: TaskEntry entry } &&
            DataContext is ProjectCreationViewModel vm) {
            vm.RemoveTaskCommand.Execute(entry);
        }
    }
}