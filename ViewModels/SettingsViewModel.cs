using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class SettingsViewModel : ViewModelBase {

    [ObservableProperty] private string _firstname;
    [ObservableProperty] private string _lastname;
    [ObservableProperty] private string _username;
    [ObservableProperty] private string _email;
    [ObservableProperty] private string _selectedTheme = "System";
    [ObservableProperty] private AccentColorOption? _selectedAccentColor;
    
    public ObservableCollection<AccentColorOption> AccentColors { get; } = new() {
    // Cool & Professional
    new AccentColorOption { Name = "Slate Violet", PrimaryColor = new SolidColorBrush(Color.Parse("SlateBlue")), SecondaryColor = new SolidColorBrush(Color.Parse("BlueViolet"))},
    new AccentColorOption { Name = "Slate Purple", PrimaryColor = new SolidColorBrush(Color.Parse("#7B68EE")), SecondaryColor = new SolidColorBrush(Color.Parse("#8A2BE2")) },
    new AccentColorOption { Name = "Google Blue",  PrimaryColor = new SolidColorBrush(Color.Parse("#1A73E8")), SecondaryColor = new SolidColorBrush(Color.Parse("#0D47A1")) },
    new AccentColorOption { Name = "Cyan Deep",    PrimaryColor = new SolidColorBrush(Color.Parse("#00BCD4")), SecondaryColor = new SolidColorBrush(Color.Parse("#006064")) },
    new AccentColorOption { Name = "Indigo",       PrimaryColor = new SolidColorBrush(Color.Parse("#5C6BC0")), SecondaryColor = new SolidColorBrush(Color.Parse("#283593")) },
    new AccentColorOption { Name = "Sky Navy",     PrimaryColor = new SolidColorBrush(Color.Parse("#29B6F6")), SecondaryColor = new SolidColorBrush(Color.Parse("#01579B")) },
    // Warm & Vibrant
    new AccentColorOption { Name = "Sunset Orange", PrimaryColor = new SolidColorBrush(Color.Parse("#FF6B35")), SecondaryColor = new SolidColorBrush(Color.Parse("#D84315")) },
    new AccentColorOption { Name = "Hot Pink",      PrimaryColor = new SolidColorBrush(Color.Parse("#FF4081")), SecondaryColor = new SolidColorBrush(Color.Parse("#C51162")) },
    new AccentColorOption { Name = "Amber",         PrimaryColor = new SolidColorBrush(Color.Parse("#FF8F00")), SecondaryColor = new SolidColorBrush(Color.Parse("#E65100")) },
    new AccentColorOption { Name = "Rose Pink",     PrimaryColor = new SolidColorBrush(Color.Parse("#F06292")), SecondaryColor = new SolidColorBrush(Color.Parse("#AD1457")) },
    new AccentColorOption { Name = "Gold Orange",   PrimaryColor = new SolidColorBrush(Color.Parse("#FFA726")), SecondaryColor = new SolidColorBrush(Color.Parse("#E65100")) },
    // Dark & Moody
    new AccentColorOption { Name = "Deep Purple", PrimaryColor = new SolidColorBrush(Color.Parse("#7B2FBE")), SecondaryColor = new SolidColorBrush(Color.Parse("#4A148C")) },
    new AccentColorOption { Name = "Blue Grey",   PrimaryColor = new SolidColorBrush(Color.Parse("#37474F")), SecondaryColor = new SolidColorBrush(Color.Parse("#102027")) },
    new AccentColorOption { Name = "Emerald",     PrimaryColor = new SolidColorBrush(Color.Parse("#4CAF50")), SecondaryColor = new SolidColorBrush(Color.Parse("#1B5E20")) },
    new AccentColorOption { Name = "Deep Red",    PrimaryColor = new SolidColorBrush(Color.Parse("#EF5350")), SecondaryColor = new SolidColorBrush(Color.Parse("#B71C1C")) },
    // Unique & Stylish
    new AccentColorOption { Name = "Magenta Rose", PrimaryColor = new SolidColorBrush(Color.Parse("#EC407A")), SecondaryColor = new SolidColorBrush(Color.Parse("#880E4F")) },
    new AccentColorOption { Name = "Teal Cyan",    PrimaryColor = new SolidColorBrush(Color.Parse("#26C6DA")), SecondaryColor = new SolidColorBrush(Color.Parse("#00838F")) },
    new AccentColorOption { Name = "Lime Forest",  PrimaryColor = new SolidColorBrush(Color.Parse("#32CD32")), SecondaryColor = new SolidColorBrush(Color.Parse("#228B22")) },
    new AccentColorOption { Name = "Violet Blue",  PrimaryColor = new SolidColorBrush(Color.Parse("#7B2FBE")), SecondaryColor = new SolidColorBrush(Color.Parse("#1A73E8")) },
};
    
    public SettingsViewModel() {
        var user = SessionService.Instance.CurrentUser;
        _firstname = user?.FirstName ?? string.Empty;
        _lastname = user?.LastName ?? string.Empty;
        _username = user?.Username ?? string.Empty;
        _email = user?.Email ?? string.Empty;
        
        SelectedAccentColor = AccentColors.FirstOrDefault(c => c.Name == "Teal Cyan");
        SelectedTheme = "Light";
    }
    
    [RelayCommand]
    private void SelectTheme(string theme) {
        SelectedTheme = theme;
    }
    
    partial void OnSelectedThemeChanged(string value) {
        if (Application.Current == null) return;

        Application.Current.RequestedThemeVariant = value switch {
            "Light" => ThemeVariant.Light,
            "Dark" => ThemeVariant.Dark,
            _ => ThemeVariant.Default  // System
        };
    }

    partial void OnSelectedAccentColorChanged(AccentColorOption? value) {
        if (value == null) return;
        
        Application.Current!.Resources["PrimaryAccentColor"] = value.PrimaryColor.Color;
        Application.Current.Resources["SecondaryAccentColor"] = value.SecondaryColor.Color;
    }
}