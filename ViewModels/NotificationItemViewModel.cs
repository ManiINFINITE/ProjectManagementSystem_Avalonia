using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using ProjectManagementSystem.Enums;

namespace ProjectManagementSystem.ViewModels;

public partial class NotificationItemViewModel : ViewModelBase {
    
    [ObservableProperty] private string _title = string.Empty;
    [ObservableProperty] private string _message = string.Empty;
    [ObservableProperty] private NotificationType _type;
    [ObservableProperty] private double _offsetY = 120;
    [ObservableProperty] private double _opacity;
    [ObservableProperty] private double _scale = 1;
    [ObservableProperty] private double _stackOffsetY;
    [ObservableProperty] private int _zIndex;
    
    public bool IsDismissed { get; private set; }

    public async Task SlideIn() {
        for (int i = 0; i <= 10; i++) {
            OffsetY = 120 - (i * 12);
            Opacity = i / 10.0;
            await Task.Delay(16);
        }
        OffsetY = 0;
        Opacity = 1;
    }
    
    public async Task SlideOut() {
        IsDismissed = true;
        for (int i = 10; i >= 0; i--) {
            OffsetY = 120 - (i * 12);
            Opacity = i / 10.0;
            await Task.Delay(16);
        }
    }
}