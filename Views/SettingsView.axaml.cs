using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Views;

public partial class SettingsView : UserControl {
    public SettingsView() {
        InitializeComponent();
    }

    private void ProfilePictureUpload_Tapped(object sender, TappedEventArgs e) {
        if (DataContext is SettingsViewModel vm) {
            vm.UploadProfilePictureCommand.Execute(null);
        }
    }

    private void ProfilePictureDelete_Tapped(object sender, TappedEventArgs e) {
        if (DataContext is SettingsViewModel vm) {
            vm.DeleteProfilePictureCommand.Execute(null);
        }
    }
}