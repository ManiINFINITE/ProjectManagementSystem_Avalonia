using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Views;

public partial class NotificationView : UserControl {
    public NotificationView() {
        InitializeComponent();
    }

    private async void Dismiss_Tapped(object sender, RoutedEventArgs e) {
        if (DataContext is NotificationViewModel vm) {
            await vm.DismissCommand.ExecuteAsync(null);
        }
    } 
}