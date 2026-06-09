using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.Views;

public partial class AuthenticationView : Window {
    
    private bool _moved;
    
    public AuthenticationView() {
        InitializeComponent();
        
        // Listen to the shared animation service
        SharedAnimationService.Instance.ToggleRequested += async () =>
            await RunAuthModeAnimation();
    }

    private async Task RunAuthModeAnimation() {
        
        CornerRadius leftCurve  = new CornerRadius(25, 0, 0, 25);
        CornerRadius rightCurve = new CornerRadius(0, 25, 25, 0);
        double distance = MiddleBorder.Bounds.Width / 2;

        double from             = _moved ? distance : 0;
        double to               = _moved ? 0 : distance;
        double fromRightBorder  = _moved ? -distance : 0;
        double toRightBorder    = _moved ? 0 : -distance;

        CornerRadius leftFrom   = _moved ? rightCurve : leftCurve;
        CornerRadius leftTo     = _moved ? leftCurve  : rightCurve;
        CornerRadius rightFrom  = _moved ? leftCurve  : rightCurve;
        CornerRadius rightTo    = _moved ? rightCurve : leftCurve;

        _moved = !_moved;

        var leftBorderAnim = new Animation {
            Duration = TimeSpan.FromSeconds(0.5),
            Easing = new SineEaseInOut(),
            FillMode = FillMode.Both,
            Children = {
                new KeyFrame {
                    Cue = new Cue(0),
                    Setters = {
                        new Setter(TranslateTransform.XProperty, from),
                        new Setter(Border.CornerRadiusProperty, leftFrom)
                    }
                },
                new KeyFrame {
                    Cue = new Cue(1),
                    Setters = {
                        new Setter(TranslateTransform.XProperty, to),
                        new Setter(Border.CornerRadiusProperty, leftTo)
                    }
                }
            }
        };

        var rightBorderAnim = new Animation {
            Duration = TimeSpan.FromSeconds(0.5),
            Easing = new SineEaseInOut(),
            FillMode = FillMode.Both,
            Children = {
                new KeyFrame {
                    Cue = new Cue(0),
                    Setters = {
                        new Setter(TranslateTransform.XProperty, fromRightBorder),
                        new Setter(Border.CornerRadiusProperty, rightFrom)
                    }
                },
                new KeyFrame {
                    Cue = new Cue(1),
                    Setters = {
                        new Setter(TranslateTransform.XProperty, toRightBorder),
                        new Setter(Border.CornerRadiusProperty, rightTo)
                    }
                }
            }
        };

        await Task.WhenAll(leftBorderAnim.RunAsync(ImageBorder), rightBorderAnim.RunAsync(InfoBorder));

        ((TranslateTransform?)ImageBorder.RenderTransform)?.X  = to;
        ImageBorder.CornerRadius = leftTo;

        ((TranslateTransform?)InfoBorder.RenderTransform)?.X = toRightBorder;
        InfoBorder.CornerRadius = rightTo;
    }
}