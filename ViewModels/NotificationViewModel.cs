using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class NotificationViewModel : ViewModelBase {

    [ObservableProperty] private ObservableCollection<NotificationItemViewModel> _visibleNotifications = [];

    private readonly Queue<Notification> _queue = new();
    private bool _isProcessing;
    private const int MAX_VISIBLE = 3;

    public NotificationViewModel() {
        NotificationService.Instance.NotificationRequested += OnNotificationRequested;
    }

    private void OnNotificationRequested(Notification notification) {
        Dispatcher.UIThread.InvokeAsync(() => {
            _queue.Enqueue(notification);
            if (!_isProcessing) ProcessQueue();
        });
    }

    private async void ProcessQueue() {
        _isProcessing = true;

        while (_queue.Count > 0) {
            // Wait if already at max visible
            while (VisibleNotifications.Count >= MAX_VISIBLE)
                await Task.Delay(200);
            
            var notification = _queue.Dequeue();
            var item = new NotificationItemViewModel {
                Title = notification.Title,
                Message = notification.Message,
                Type =  notification.Type
            };
            
            VisibleNotifications.Add(item); // newest on top
            UpdateScales();
            
            // Slide in + auto dismiss after 10s
            _ = Task.Run(async () => {
                await Dispatcher.UIThread.InvokeAsync(async () => item.SlideIn());
                await Task.Delay(10000);
                if (!item.IsDismissed)
                    await Dispatcher.UIThread.InvokeAsync(async () => await DismissSingle(item));
            });

            await Task.Delay(150);
        }
        
        _isProcessing = false;
    }

    [RelayCommand]
    private async Task DismissTop() {
        if (VisibleNotifications.Count == 0) return;
        await DismissSingle(VisibleNotifications[^1]); // last = newest = top
    }

    [RelayCommand]
    private async Task DismissAll() {
        var list = VisibleNotifications.ToList();
        list.Reverse();
        foreach (var item in list) {
            await DismissSingle(item);
        }
    }

    private async Task DismissSingle(NotificationItemViewModel item) {
        if (item.IsDismissed) return;
        await item.SlideOut();
        VisibleNotifications.Remove(item);
        UpdateScales();
    }

    private void UpdateScales() {
        int count = VisibleNotifications.Count;
        for (int i = 0; i < count; i++) {
            var item = VisibleNotifications[i];
            int depthFromTop = (count - 1) - i; // 0 = top, 1 = behind, 2 = furthest behind
            item.Scale = 1 - (depthFromTop * 0.05);
            item.StackOffsetY = -(depthFromTop * 12);
            item.ZIndex = i; // last in collection = highest ZIndex
        }
    }
}