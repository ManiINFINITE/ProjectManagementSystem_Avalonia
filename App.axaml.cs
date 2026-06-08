using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.ViewModels;
using ProjectManagementSystem.Views;

namespace ProjectManagementSystem;

public partial class App : Application {
    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {
        using var db = new AppDbContext();
        db.Database.EnsureCreated();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new AuthenticationView {
                DataContext = new AuthenticationViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}