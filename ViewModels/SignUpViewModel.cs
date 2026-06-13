using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Helpers;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Repositories;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class SignUpViewModel : ViewModelBase {

    [ObservableProperty] private string _firstname = string.Empty;
    [ObservableProperty] private string _lastname = string.Empty;
    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _password = string.Empty;

    [RelayCommand]
    private void SignUp() {
        var repository = new UserRepository();

        // Check if user already exists
        if (repository.GetByUsername(Username) != null) {
            NotificationService.Instance.Send("Username Already Exists!", "Try signing in or pick another username.", NotificationType.Error);
            return;
        }
        // Check if Email already exists
        if (repository.GetByEmail(Email) != null) {
            NotificationService.Instance.Send("Email Already Exists!", "This email is already registered.\nTry signing in.",  NotificationType.Error);
            return;
        }
        // Validate the email
        if (!UserValidator.ValidateEmail(Email)) {
            NotificationService.Instance.Send("Invalid Email Format!", "Please enter a valid email address:\n*******@gmail.com.",  NotificationType.Error);
            return;
        }
        // Validate the password
        string passwordValidateResult = UserValidator.ValidatePassword(Password) ?? string.Empty;
        if (passwordValidateResult != string.Empty) {
            NotificationService.Instance.Send("Weak Password!", passwordValidateResult, NotificationType.Error);
            return;
        }
        
        var user = new User {
            FirstName = Firstname,
            LastName = Lastname,
            Username = Username,
            Email = Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password)
        };
        
        // Success
        repository.Add(user);
        NotificationService.Instance.Send("Signed Up", $"{user.FirstName + " " +  user.LastName} successfully signed up!", NotificationType.Success);
        ChangeToSignIn();
    }

    [RelayCommand]
    private void ChangeToSignIn() {
        SharedAnimationService.Instance.RequestToggle();
    }
}