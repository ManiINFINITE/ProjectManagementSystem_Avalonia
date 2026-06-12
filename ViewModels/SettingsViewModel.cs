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

    [ObservableProperty] private ObservableCollection<AccentColorOption> _accentColors = AccentColorsBase.AccentColors;
    
    public SettingsViewModel() {
        var user = SessionService.Instance.CurrentUser;
        _firstname = user?.FirstName ?? string.Empty;
        _lastname = user?.LastName ?? string.Empty;
        _username = user?.Username ?? string.Empty;
        _email = user?.Email ?? string.Empty;

        // Restore saved accent color selection
        var savedName = AppSettingsService.Instance!.CurrentSettings.AccentColorName;
        _selectedAccentColor = AccentColors.FirstOrDefault(c => c.Name == savedName) ?? AccentColors.First();
        
        // Restore saved theme
        _selectedTheme = AppSettingsService.Instance!.CurrentSettings.Theme;
    }
    
    [RelayCommand]
    private void SelectTheme(string theme) {
        SelectedTheme = theme;
    }
    
    partial void OnSelectedThemeChanged(string value) {
        if (Application.Current == null) return;

        AppSettingsService.Instance!.ApplyTheme(value);
    }

    partial void OnSelectedAccentColorChanged(AccentColorOption? value) {
        if (value == null) return;
        
        AppSettingsService.Instance!.ApplyAccentColor(
            value
            );
    }
}