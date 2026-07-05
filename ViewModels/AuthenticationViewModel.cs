using System;
using System.Linq;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.ViewModels;

public partial class AuthenticationViewModel : ViewModelBase {
    
    // Singleton ------------------------------------------
    public static AuthenticationViewModel? Instance { get; private set; }
    
    [ObservableProperty] private Bitmap? _image;
    [ObservableProperty] private string _imageText = "Continue Your Business";

    public AuthenticationViewModel(string accentColorName, bool initialize = true) {
        if (!initialize) return;
        
        Instance = this;
        
        var accent = AccentColorsBase.AccentColors.FirstOrDefault(c => c.Name == accentColorName)
                     ?? AccentColorsBase.AccentColors.First();
        
        LoadImage(accent.ImagePath);
    }
    
    private ViewModelBase _currentAuthViewModel = new SignInViewModel();
    private AuthenticationMode CurrentAuthMode = AuthenticationMode.SignIn;

    public ViewModelBase CurrentAuthViewModel {
        get => _currentAuthViewModel;
        set => SetProperty(ref _currentAuthViewModel, value);
    }

    public void ChangeAuthMode() {
        if (CurrentAuthMode is AuthenticationMode.SignIn) {
            ChangeToSignUp();
        } else {
            ChangeToSignIn();
        }
    }

    private void ChangeToSignIn() {
        CurrentAuthViewModel = new SignInViewModel();
        ImageText = "Continue Your Business";
        CurrentAuthMode = AuthenticationMode.SignIn;
    }

    private void ChangeToSignUp() {
        CurrentAuthViewModel = new SignUpViewModel();
        ImageText = "Start Your Business";
        CurrentAuthMode = AuthenticationMode.SignUp;
    }

    public void UpdateImage(string imagePath) {
        LoadImage(imagePath);
    }

    private void LoadImage(string path) {
        var uri = new Uri(path);
        var asset = AssetLoader.Open(uri);
        Image = new Bitmap(asset);
    }
}