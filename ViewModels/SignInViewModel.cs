using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Repositories;

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
        
        Console.WriteLine($"Username: '{Username}'");
        Console.WriteLine($"Password: '{Password}'");
        
        Console.WriteLine($"'{Password}'");
        Console.WriteLine(Password.Length);
        
        Console.WriteLine(typeof(BCrypt.Net.BCrypt).Assembly.FullName);
        
        // Verify password
        bool passwordValid = BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash);

        if (!passwordValid) {
            Console.WriteLine("Invalid password!");
            return;
        }
        
        // Success
        Console.WriteLine($"Welcome {user.Username}! Login successful!");
    }

    [RelayCommand]
    private void ChangeToSignUp() {
        AuthenticationViewModel.Instance?.ChangeAuthMode();
    }
}