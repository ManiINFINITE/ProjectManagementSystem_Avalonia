using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Views;

public partial class SignInView : UserControl {
    public SignInView() {
        InitializeComponent();
    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e) {
        if (sender is not TextBox current) return;

        TextBox? next = null;
        TextBox? prev =  null;
        
        // Define navigation order
        if (current == UsernameTextBox) {
            next = PasswordTextBox;
            prev = null;
        } else if (current == PasswordTextBox) {
            next = null;
            prev = UsernameTextBox;
        }

        if (e.Key == Key.Down && next != null) {
            next.Focus();
            e.Handled = true;
        } else if (e.Key == Key.Up && prev != null) {
            prev.Focus();
            e.Handled = true;
        } else if (e.Key == Key.Enter) {
            if (DataContext is SignInViewModel vm) {
                vm.SignInCommand.Execute(null);
            }
            e.Handled = true;
        }
    }
}