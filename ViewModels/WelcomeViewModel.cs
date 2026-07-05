using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Repositories;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class WelcomeViewModel : ViewModelBase {
    private readonly UserRepository _userRepository = new();

    [ObservableProperty] private ObservableCollection<User> _quickLoginUsers = [];

    private User? _rememberedUser;

    public WelcomeViewModel(bool initialize = true) {
        if (initialize) _ = InitializeAsync();
    }

    private async Task InitializeAsync() {
        await LoadRememberedUserAsync();

        if (_rememberedUser is not null) {
            await SignInRememberedUserAsync();
            return;
        }

        await LoadQuickLoginUsersAsync();

        if (QuickLoginUsers.Count == 0) SignInWithDifferentAccount();
    }

    private async Task LoadQuickLoginUsersAsync() {
        var ids = AppSettingsService.Instance!.GlobalSettings.QuickLoginUsers;
        var users = new ObservableCollection<User>();
        foreach (var id in ids) {
            var user = await _userRepository.GetByIdWithProfilePictureAsync(id);
            if (user != null) users.Add(user);
        }

        QuickLoginUsers = users;
    }

    private async Task LoadRememberedUserAsync() {
        var id = AppSettingsService.Instance!.GlobalSettings.RememberedUser;
        if (id == -1) return;

        var user = await _userRepository.GetByIdWithProfilePictureAsync(id);
        if (user != null) _rememberedUser = user;
    }

    [RelayCommand]
    private void SelectUser(User user) {
        var AuthVm = new AuthenticationViewModel(user.AccentColorName) {
            CurrentAuthViewModel = new SignInViewModel {
                Username = user.Username
            }
        };

        // Apply selected user's accent color
        var accent = AccentColorsBase.AccentColors.FirstOrDefault(c => c.Name == user.AccentColorName)
                     ?? AccentColorsBase.AccentColors.First();
        AppSettingsService.Instance!.ApplyAccentColor(accent);

        // Apply selected user's theme
        var theme = AppSettingsService.Instance.getThemeForUser(user.Id);
        AppSettingsService.Instance.ApplyTheme(theme!);

        NavigationService.Instance.NavigateTo(AuthVm);
    }

    [RelayCommand]
    private void SignInWithDifferentAccount() {
        // Apply default accent color
        var accent = AccentColorsBase.AccentColors.First();
        AppSettingsService.Instance!.ApplyAccentColor(accent);

        // Apply system theme
        AppSettingsService.Instance.ApplyTheme("System");

        NavigationService.Instance.NavigateTo(new AuthenticationViewModel("Slate Violet"));
    }

    private async Task SignInRememberedUserAsync() {
        SessionService.Instance.Login(_rememberedUser!);

        AppSettingsService.Instance!.LoadForUser(_rememberedUser!.Id);

        var accent = AccentColorsBase.AccentColors.FirstOrDefault(c => c.Name == _rememberedUser.AccentColorName) ??
                     AccentColorsBase.AccentColors.First();

        AppSettingsService.Instance.ApplyAccentColor(accent);

        var theme = AppSettingsService.Instance.getThemeForUser(_rememberedUser.Id);
        AppSettingsService.Instance.ApplyTheme(theme!);

        NavigationService.Instance.NavigateTo(new DashboardViewModel());

        await Task.CompletedTask;
        
        NotificationService.Instance.Send("Welcome Back!", $"Welcome back {_rememberedUser.Username}.",
            NotificationType.Success);
    }
}