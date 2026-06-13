using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class NotificationViewModel : ViewModelBase {
    
    [ObservableProperty] private string _title =  string.Empty;
    [ObservableProperty] private string _message = string.Empty;
    [ObservableProperty] private NotificationType _type;
    [ObservableProperty] private bool _isVisible;
    [ObservableProperty] private double _offsetY = 120;
    [ObservableProperty] private double _opacity;

    public NotificationViewModel() {
        NotificationService.Instance.NotificationRequested += OnNotificationRequested;
    }

    private async void OnNotificationRequested(Notification notification) {
        await Dispatcher.UIThread.InvokeAsync(async () => {
            Title = notification.Title;
            Message = notification.Message;
            Type = notification.Type;

            await SlideIn();
        });
    }

    private async Task SlideIn() {
        IsVisible = true;
        
        // Animate in - slide up + fade in
        for (int i = 0; i <= 10; i++) {
            OffsetY = 120 - (i * 12);
            Opacity = i / 10.0;
            await Task.Delay(16); // ~60fps
        }

        OffsetY = 0;
        Opacity = 1;
    }

    [RelayCommand]
    private async Task Dismiss() {
        for (int i = 10; i >= 0; i--) {
            OffsetY = 120 - (i * 12);
            Opacity = i / 10.0;
            await Task.Delay(16);
        }

        IsVisible = false;
        OffsetY = 120;
        Opacity = 0;
    }
}