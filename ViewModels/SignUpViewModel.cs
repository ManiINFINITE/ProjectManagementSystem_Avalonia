using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            Console.WriteLine("This Username already exists!");
            return;
        }
        if (repository.GetByEmail(Email) != null) {
            Console.WriteLine("This Email is already registered!");
            return;
        }
        
        var rawPassword = Password;

        var hash = BCrypt.Net.BCrypt.HashPassword(rawPassword);

        Console.WriteLine($"SIGNUP RAW: '{rawPassword}'");
        Console.WriteLine($"SIGNUP HASH: {hash}");

        Console.WriteLine(
            $"SELF VERIFY: {BCrypt.Net.BCrypt.Verify(rawPassword, hash)}"
        );
        
        var user = new User {
            FirstName = Firstname,
            LastName = Lastname,
            Username = Username,
            Email = Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password)
        };
        
        repository.Add(user);
        
        // Go to sign in page after registration
        ChangeToSignIn();
    }

    [RelayCommand]
    private void ChangeToSignIn() {
        SharedAnimationService.Instance.RequestToggle();
        //AuthenticationViewModel.Instance?.ChangeAuthMode();
    }
}