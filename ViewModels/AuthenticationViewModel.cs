using System;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Repositories;

namespace ProjectManagementSystem.ViewModels;

public partial class AuthenticationViewModel : ViewModelBase {
    
    // User information
    [ObservableProperty] private string _firstname = string.Empty;
    [ObservableProperty] private string _lastname = string.Empty;
    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _password = string.Empty;

    [ObservableProperty] private AuthenticationMode _currentAuthentication = AuthenticationMode.SignUp;
    [ObservableProperty] private bool _isSignUpMode = true;
    
    // Information in page
    [ObservableProperty] private string _imageText = "Continue Your Business";
    [ObservableProperty] private string _titleText = "Sign In";
    [ObservableProperty] private string _changeAuthMessageText = "Don't Have an account?";
    [ObservableProperty] private string _changeAuthButtonText = "Create Account";
    [ObservableProperty] private Thickness _infoStackPanelMargin = new(0, 40, 0, 0);
    
    // Live validation
    [ObservableProperty] private bool _isUsernameValid;
    [ObservableProperty] private bool _isEmailValid;
    [ObservableProperty] private bool _isPasswordValid;

    [RelayCommand]
    private void ChangeAuthentication() {
        if (CurrentAuthentication == AuthenticationMode.SignIn) {
            ChangeToSignUp();
        } else if (CurrentAuthentication == AuthenticationMode.SignUp) {
            ChangeToSignIn();
        }
    }

    [RelayCommand]
    private void Submit() {
        if (CurrentAuthentication == AuthenticationMode.SignUp) {
            SignUp();
        } else {
            SignIn();
        }
    }
    
    private void ChangeToSignUp() {
        IsSignUpMode = true;
        ImageText = "Start Your Business";
        TitleText = "Sign Up";
        ChangeAuthMessageText = "Already have an account?";
        ChangeAuthButtonText = "Sign In";
        InfoStackPanelMargin = new Thickness(0, 40, 0, 0);
        
        CurrentAuthentication = AuthenticationMode.SignUp;
    }
    
    private void ChangeToSignIn() {
        IsSignUpMode = false;
        ImageText = "Continue Your Business";
        TitleText = "Sign In";
        ChangeAuthMessageText = "Don't Have an account?";
        ChangeAuthButtonText = "Create Account";
        InfoStackPanelMargin = new Thickness(0, 60, 0, 0);
        
        CurrentAuthentication = AuthenticationMode.SignIn;
    }

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

        var user = new User {
            FirstName = Firstname,
            LastName = Lastname,
            Username = Username,
            Email = Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password)
        };
        
        repository.Add(user);
        
        ChangeToSignIn();
    }

    private void SignIn() {
        var repository = new UserRepository();
        
        // Find Username
        var user = repository.GetByUsername(Username);

        if (user == null) {
            Console.WriteLine("User not found!");
            return;
        }
        
        // Verify Password
        bool passwordValid = BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash);

        if (!passwordValid) {
            Console.WriteLine("Incorrect password!");
            return;
        }
        
        // Success
        Console.WriteLine($"Welcome {user.Username}! Login successful.");
    }
}