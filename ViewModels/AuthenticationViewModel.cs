using CommunityToolkit.Mvvm.ComponentModel;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Views;

namespace ProjectManagementSystem.ViewModels;

public partial class AuthenticationViewModel : ViewModelBase {
    
    // Singleton ------------------------------------------
    public static AuthenticationViewModel? Instance { get; set; }

    public AuthenticationViewModel() {
        Instance = this;
    }

    [ObservableProperty] private string _imageText = "Continue Your Business";
    
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
}