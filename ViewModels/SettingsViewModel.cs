using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Repositories;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class SettingsViewModel : ViewModelBase {
    
    private readonly UserRepository _userRepository = new();
    
    [ObservableProperty] private string _firstname;
    [ObservableProperty] private string _lastname;
    [ObservableProperty] private string _username;
    [ObservableProperty] private string _email;
    [ObservableProperty] private string _selectedTheme;
    [ObservableProperty] private AccentColorOption? _selectedAccentColor;
    [ObservableProperty] private Bitmap? _profilePicture;
    [ObservableProperty] private bool _notificationsEnabled = true;

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
        _selectedTheme = AppSettingsService.Instance.CurrentSettings.Theme;
        
        // Profile Picture
        if (user?.ProfilePicture != null) {
            using var ms = new MemoryStream(user.ProfilePicture);
            _profilePicture = new Bitmap(ms);
        }
        
        // Notifications enabled
        _notificationsEnabled = AppSettingsService.Instance.CurrentSettings.NotificationsEnabled;
    }

    public bool IsSystemTheme => SelectedTheme == "System";
    public bool IsLightTheme => SelectedTheme == "Light";
    public bool IsDarkTheme => SelectedTheme == "Dark";
    
    public string ProfileInitials => 
        $"{(string.IsNullOrEmpty(Firstname) ? "" : Firstname[0].ToString())}{(string.IsNullOrEmpty(Lastname) ? "" : Lastname[0].ToString())}".ToUpper();

    [RelayCommand]
    private async Task UploadProfilePicture() {
        var topLevel = TopLevel.GetTopLevel(
            (Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime)
            ?.MainWindow
            );
        
        if (topLevel == null) return;

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions {
            Title = "Select Profile Picture",
            AllowMultiple = false,
            FileTypeFilter = [
                new FilePickerFileType("Images") {
                    Patterns = [ "*.jpg", "*.jpeg", "*.png",  "*.bmp" ]
                }
            ]
        });

        if (files.Count == 0) {
            NotificationService.Instance.Send("No File Selected!", NotificationType.Error);
            return;
        }
        
        var file = files[0];
        await using var stream = await file.OpenReadAsync();
        
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        var imageBytes = ms.ToArray();

        ms.Position = 0;
        ProfilePicture = new Bitmap(ms);

        var userId = SessionService.Instance.CurrentUser!.Id;
        _userRepository.UpdateProfilePicture(userId, imageBytes);

        SessionService.Instance.CurrentUser!.ProfilePicture = imageBytes;
        NotificationService.Instance.Send("Profile Picture Updated!", "Your profile picture is updated successfully", NotificationType.Success);
    }

    [RelayCommand]
    private void DeleteProfilePicture() {
        if (SessionService.Instance.CurrentUser == null) return;
        
        var userId = SessionService.Instance.CurrentUser.Id;
        _userRepository.DeleteProfilePicture(userId);

        SessionService.Instance.CurrentUser.ProfilePicture = null;
        ProfilePicture = null;
    }
    
    [RelayCommand]
    private void SelectTheme(string theme) {
        SelectedTheme = theme;
    }
    
    partial void OnSelectedThemeChanged(string value) {
        if (Application.Current == null) return;
        
        OnPropertyChanged(nameof(IsSystemTheme));
        OnPropertyChanged(nameof(IsLightTheme));
        OnPropertyChanged(nameof(IsDarkTheme));

        AppSettingsService.Instance!.ApplyTheme(value);
        
        // Change theme Notif
        NotificationService.Instance.Send("Theme Changed", $"Theme changed successfully to {value}", NotificationType.Success);
    }

    partial void OnSelectedAccentColorChanged(AccentColorOption? value) {
        if (value == null) return;
        
        AppSettingsService.Instance!.ApplyAccentColor(
            value
            );
        
        NotificationService.Instance.Send("Accent Color Changed!", $"Accent color changed to {value.Name} successfully.", NotificationType.Success);
    }

    partial void OnNotificationsEnabledChanged(bool value) {
        
        string notifTitle = value ? "Notifications Enabled!" : "Notifications Disabled!";
        string notifMessage = value ? "Notifications enabled successfully!" : "Notifications disabled successfully!";
        
        if (!value) NotificationService.Instance.Send(notifTitle, notifMessage, NotificationType.Success);
        
        AppSettingsService.Instance!.CurrentSettings.NotificationsEnabled = value;
        AppSettingsService.Instance.Save();
        
        if (value) NotificationService.Instance.Send(notifTitle, notifMessage, NotificationType.Success);
    }
}