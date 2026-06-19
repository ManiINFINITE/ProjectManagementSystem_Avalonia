using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Views;

public partial class WelcomeView : UserControl {
    public WelcomeView() {
        InitializeComponent();
    }

    private void UserCard_Tapped(object sender, RoutedEventArgs e) {
        if (sender is Border { DataContext: User user } &&
            DataContext is WelcomeViewModel vm) {
            vm.SelectUserCommand.Execute(user);
        }
    }
}