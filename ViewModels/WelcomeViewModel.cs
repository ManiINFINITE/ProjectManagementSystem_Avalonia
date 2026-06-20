using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Repositories;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class WelcomeViewModel : ViewModelBase {
    
    private readonly UserRepository _userRepository = new();
    
    [ObservableProperty] private ObservableCollection<User> _rememberedUsers = [];

    public WelcomeViewModel() {
        _ = LoadRememberedUserAsync();
    }
    
    private async Task LoadRememberedUserAsync() {
        var ids = AppSettingsService.Instance!.GlobalSettings.RememberedUsers;
        var users = new ObservableCollection<User>();
        foreach (var id in ids) {
            var user = await _userRepository.GetByIdWithProfilePictureAsync(id);
            if (user != null) users.Add(user);
        }
        RememberedUsers = users;
    }

    [RelayCommand]
    private void SelectUser(User user) {
        var AuthVm = new AuthenticationViewModel(user.AccentColorName) {
            CurrentAuthViewModel = new SignInViewModel {
                Username =  user.Username
            }
        };
        
        // Apply selected user's accent color
        var accent = AccentColorsBase.AccentColors.FirstOrDefault(c => c.Name == user.AccentColorName)
            ??  AccentColorsBase.AccentColors.First();
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
}