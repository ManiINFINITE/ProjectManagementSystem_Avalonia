using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Repositories;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class SignInViewModel : ViewModelBase {

    [ObservableProperty] private string _username =  string.Empty;
    [ObservableProperty] private string _password =  string.Empty;
    [ObservableProperty] private bool _rememberMe;

    public PasswordFieldViewModel PasswordField { get; } = new();

    [RelayCommand]
    private async Task SignIn() {
        var repository = new UserRepository();

        // Find username
        var user = await repository.GetByUsernameWithProfilePictureAsync(Username);

        if (user == null) {
            NotificationService.Instance.Send("User Not Found!", "The Username you entered was not found! Please try again!",  NotificationType.Error);
            return;
        }
        
        // Verify password
        bool passwordValid = BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash);

        if (!passwordValid) {
            NotificationService.Instance.Send("Invalid Password!", "Password is incorrect. Please try again!", NotificationType.Error);
            return;
        }
        
        // Success
        SessionService.Instance.Login(user);
        NotificationService.Instance.Send("Signed In", $"Welcome {user.FirstName}! Let's Get to work. There are a lot of projects and tasks waiting for you!", NotificationType.Success);
        AppSettingsService.Instance!.LoadForUser(user.Id);
        if (RememberMe) AppSettingsService.Instance.AddRememberedUser(user.Id);
        NavigationService.Instance.NavigateTo(new DashboardViewModel());
    }

    [RelayCommand]
    private void ChangeToSignUp() {
        SharedAnimationService.Instance.RequestToggle();
    }
}