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
            if (DataContext is MainWindowViewModel vm) {
                _ = vm.WarmUpAsync();
            } else {
                Console.WriteLine("[MainWindowView] DataContext was NOT MainWindowViewModel!");
            }
        };
    }

    public void FadeInSplash() {
        SplashText!.Opacity = 1;
    }

    public void FadeOutSplash() {
        SplashText!.Opacity = 0;
    }
}