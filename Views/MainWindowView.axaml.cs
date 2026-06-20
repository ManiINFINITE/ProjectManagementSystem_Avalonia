using System;
using Avalonia.Controls;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Views;

public partial class MainWindowView : Window {
    
    public static MainWindowView? Instance { get; private set; }
    
    public MainWindowView() {
        InitializeComponent();
        Instance = this;
        Loaded += (_, _) => {
            Console.WriteLine($"[MainWindowView] Loaded fired. DataContext type: {DataContext?.GetType().Name ?? "null"}");
            if (DataContext is MainWindowViewModel vm) {
                Console.WriteLine("[MainWindowView] Starting warm-up");
                _ = vm.WarmUpAsync();
            } else {
                Console.WriteLine("[MainWindowView] DataContext was NOT MainWindowViewModel!");
            }
        };
    }

    public void FadeInSplash() {
        Console.WriteLine($"[View] FadeInSplash. SplashText is null? {SplashText is null}");
        SplashText!.Opacity = 1;
    }

    public void FadeOutSplash() {
        Console.WriteLine($"[View] FadeOutSplash. SplashText is null? {SplashText is null}");
        SplashText!.Opacity = 0;
    }
}