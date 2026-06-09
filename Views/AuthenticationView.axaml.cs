using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using ProjectManagementSystem.Services;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Views;

public partial class AuthenticationView : Window {
    
    private bool _moved;
    
    public AuthenticationView() {
        InitializeComponent();
        
        // Listen to the shared animation service
        SharedAnimationService.Instance.ToggleRequested += async () =>
            await RunBorderAnimation();
    }

    private async Task RunBorderAnimation() {
        CornerRadius leftCurve  = new CornerRadius(15, 0, 0, 15);
        CornerRadius rightCurve = new CornerRadius(0, 15, 15, 0);

        double borderWidth     = ImageBorder.Bounds.Width;

        double from            = _moved ? borderWidth : 0;
        double to              = _moved ? 0 : borderWidth;
        double fromRightBorder = _moved ? -borderWidth : 0;
        double toRightBorder   = _moved ? 0 : -borderWidth;

        CornerRadius leftFrom  = _moved ? rightCurve : leftCurve;
        CornerRadius leftTo    = _moved ? leftCurve  : rightCurve;
        CornerRadius rightFrom = _moved ? leftCurve  : rightCurve;
        CornerRadius rightTo   = _moved ? rightCurve : leftCurve;
        
        // Always the center regardless of direction
        double midLeft  =  borderWidth / 2;
        double midRight = -borderWidth / 2;

        _moved = !_moved;

        // --- First half: borders slide to the middle ---
        var leftHalf1 = new Animation {
            Duration = TimeSpan.FromSeconds(0.25),
            Easing = new CubicEaseIn(),
            FillMode = FillMode.Both,
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = {
                    new Setter(TranslateTransform.XProperty, from),
                    new Setter(Border.CornerRadiusProperty, leftFrom)
                }},
                new KeyFrame { Cue = new Cue(1), Setters = {
                    new Setter(TranslateTransform.XProperty, midLeft),
                    new Setter(Border.CornerRadiusProperty, leftTo)
                }}
            }
        };

        var rightHalf1 = new Animation {
            Duration = TimeSpan.FromSeconds(0.25),
            Easing = new CubicEaseIn(),
            FillMode = FillMode.Both,
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = {
                    new Setter(TranslateTransform.XProperty, fromRightBorder),
                    new Setter(Border.CornerRadiusProperty, rightFrom)
                }},
                new KeyFrame { Cue = new Cue(1), Setters = {
                    new Setter(TranslateTransform.XProperty, midRight),
                    new Setter(Border.CornerRadiusProperty, rightTo)
                }}
            }
        };

        await Task.WhenAll(leftHalf1.RunAsync(ImageBorder), rightHalf1.RunAsync(InfoBorder));

        // --- Midpoint: swap the view ---
        AuthenticationViewModel.Instance?.ChangeAuthMode();

        // --- Second half: borders slide to final position ---
        var leftHalf2 = new Animation {
            Duration = TimeSpan.FromSeconds(0.25),
            Easing = new CubicEaseOut(),
            FillMode = FillMode.Both,
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = {
                    new Setter(TranslateTransform.XProperty, midLeft),
                    new Setter(Border.CornerRadiusProperty, leftTo)
                }},
                new KeyFrame { Cue = new Cue(1), Setters = {
                    new Setter(TranslateTransform.XProperty, to),
                    new Setter(Border.CornerRadiusProperty, leftTo)
                }}
            }
        };

        var rightHalf2 = new Animation {
            Duration = TimeSpan.FromSeconds(0.25),
            Easing = new CubicEaseOut(),
            FillMode = FillMode.Both,
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = {
                    new Setter(TranslateTransform.XProperty, midRight),
                    new Setter(Border.CornerRadiusProperty, rightTo)
                }},
                new KeyFrame { Cue = new Cue(1), Setters = {
                    new Setter(TranslateTransform.XProperty, toRightBorder),
                    new Setter(Border.CornerRadiusProperty, rightTo)
                }}
            }
        };

        await Task.WhenAll(leftHalf2.RunAsync(ImageBorder), rightHalf2.RunAsync(InfoBorder));

        await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() => {
            ((TranslateTransform?)ImageBorder.RenderTransform)?.X  = to;
            ImageBorder.CornerRadius                             = leftTo;
            ((TranslateTransform?)InfoBorder.RenderTransform)?.X  = toRightBorder;
            InfoBorder.CornerRadius                              = rightTo;
        });
    }
}