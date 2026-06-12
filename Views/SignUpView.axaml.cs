using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Views;

public partial class SignUpView : UserControl {
    public SignUpView() {
        InitializeComponent();
    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e) {
        if (sender is not TextBox current) return;

        TextBox? right = null;
        TextBox? left = null;
        TextBox? down = null;
        TextBox? up = null;

        if (current == FirstnameTextBox) {
            right = LastnameTextBox;
            left = null;
            down = UsernameTextBox;
            up = null;
        } else if (current == LastnameTextBox) {
            right = null;
            left = FirstnameTextBox;
            down = UsernameTextBox;
            up = null;
        } else if (current == UsernameTextBox) {
            right = null;
            left = null;
            down = EmailTextBox;
            up = FirstnameTextBox;
        } else if (current == EmailTextBox) {
            right = null;
            left = null;
            down = PasswordTextBox;
            up = UsernameTextBox;
        } else if (current == PasswordTextBox) {
            right = null;
            left = null;
            down = null;
            up = EmailTextBox;
        }

        if (e.Key == Key.Right && right != null) {
            right.Focus();
            e.Handled = true;
        } else if (e.Key == Key.Left && left != null) {
            left.Focus();
            e.Handled = true;
        } else if (e.Key == Key.Up && up != null) {
            up.Focus();
            e.Handled = true;
        } else if (e.Key == Key.Down && down != null) {
            down.Focus();
            e.Handled = true;
        } else if (e.Key == Key.Enter) {
            if (DataContext is SignUpViewModel vm) {
                vm.SignUpCommand.Execute(null);
            }
            e.Handled = true;
        }
    }
}