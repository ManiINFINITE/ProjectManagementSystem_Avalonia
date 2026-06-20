using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Views;

public partial class WelcomeView : UserControl {
    public WelcomeView() {
        InitializeComponent();
    }

    private void UserCard_PointerPressed(object sender, PointerPressedEventArgs e) {
        if (sender is Border { Parent: Border shadowBorder }) {
            shadowBorder.Classes.Add("Pressed");
        }
    }
    
    private void UserCard_PointerReleased(object sender, PointerReleasedEventArgs e) {
        if (sender is Border {Parent: Border shadowBorder} border) {
            shadowBorder.Classes.Remove("Pressed");
            
            if (border.DataContext is User user && DataContext is WelcomeViewModel vm) {
                vm.SelectUserCommand.Execute(user);
            }
        }
    }

    private void UserCard_OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e) {
        if (sender is Border {Parent: Border shadowBorder}) {
            shadowBorder.Classes.Remove("Pressed");
        }
    }
}