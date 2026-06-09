using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Repositories;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class SignInViewModel : ViewModelBase {

    [ObservableProperty] private string _username =  string.Empty;
    [ObservableProperty] private string _password =  string.Empty;

    [RelayCommand]
    private void SignIn() {
        var repository = new UserRepository();

        // Find username
        var user = repository.GetByUsername(Username);

        if (user == null) {
            Console.WriteLine("User not found!");
            return;
        }
        
        // Verify password
        bool passwordValid = BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash);

        if (!passwordValid) {
            Console.WriteLine("Invalid password!");
            return;
        }
        
        // Success
        Console.WriteLine($"Welcome {user.Username}! Login successful!");
        
        // Go to user dashboard view
        NavigationService.Instance?.NavigateTo(new DashboardViewModel());
    }

    [RelayCommand]
    private void ChangeToSignUp() {
        SharedAnimationService.Instance.RequestToggle();
    }
}