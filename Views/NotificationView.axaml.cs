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

    private async void DismissTop_Tapped(object sender, RoutedEventArgs e) {
        if (DataContext is NotificationViewModel vm) {
            await vm.DismissTopCommand.ExecuteAsync(null);
        }
    }
    
    private async void DismissAll_Tapped(object sender, RoutedEventArgs e) {
        if (DataContext is NotificationViewModel vm) {
            await vm.DismissAllCommand.ExecuteAsync(null);
        }
    } 
}