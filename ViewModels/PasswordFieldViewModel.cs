using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ProjectManagementSystem.ViewModels;

public partial class PasswordFieldViewModel : ViewModelBase {

    [ObservableProperty] private bool _passwordVisible;
    [ObservableProperty] private string _passwordChar = "•";
    
    [RelayCommand]
    private void ToggleVisibility() {
        PasswordChar = (!PasswordVisible) ? "" : "•";
        PasswordVisible = !PasswordVisible;
    }
}