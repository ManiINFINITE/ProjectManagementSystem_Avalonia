using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using ProjectManagementSystem.Views;

namespace ProjectManagementSystem.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {
    
    public static MainWindowViewModel? Instance { get; private set; }

    [ObservableProperty] private NotificationViewModel _notificationViewModel = new();
    [ObservableProperty] private ViewModelBase _currentView;
    
    // Warmup
    [ObservableProperty] private ViewModelBase? _warmupView;
    [ObservableProperty] private bool _isWarmingUp = true;

    public MainWindowViewModel() {
        Instance = this;
        _currentView = new WelcomeViewModel();
    }

    public async Task WarmUpAsync() {
        try {
            // give the window one full render pass to settle before animating anything
            await Dispatcher.UIThread.InvokeAsync(() => { }, DispatcherPriority.Render);
            await Task.Delay(50);

            MainWindowView.Instance?.FadeInSplash();
            await Task.Delay(650);

            WarmupView = new WelcomeViewModel(false);
            await Dispatcher.UIThread.InvokeAsync(() => { }, DispatcherPriority.Render);

            WarmupView = new AuthenticationViewModel("Slate Violet", false);
            await Dispatcher.UIThread.InvokeAsync(() => { }, DispatcherPriority.Render);

            WarmupView = new DashboardViewModel(false);
            await Dispatcher.UIThread.InvokeAsync(() => { }, DispatcherPriority.Render);

            WarmupView = null;
        } catch {
            // best-effort
        } finally {
            MainWindowView.Instance?.FadeOutSplash();
            await Task.Delay(650);
            IsWarmingUp = false;
        }
    }

    public void NavigateTo(ViewModelBase viewModel) {
        CurrentView = viewModel;
    }
}