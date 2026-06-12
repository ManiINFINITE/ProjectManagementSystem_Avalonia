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

public partial class AuthenticationView : UserControl {
    
    private bool _moved;
    private Action? _animationHandler;
    
    public AuthenticationView() {
        InitializeComponent();

        _animationHandler = async () => await RunBorderAnimation();
        SharedAnimationService.Instance.ToggleRequested += _animationHandler;
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e) {
        base.OnDetachedFromVisualTree(e);
        if (_animationHandler != null)
            SharedAnimationService.Instance.ToggleRequested -= _animationHandler;
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
        
        // ImageText
        double controlExitPos = _moved ? borderWidth : -borderWidth;
        double controlEnterPos = _moved ? -borderWidth : borderWidth;

        _moved = !_moved;
        
        // First half animations
        var leftHalf1 = BuildAnimation(from, leftFrom, midLeft, leftTo, new CubicEaseIn());
        var rightHalf1 = BuildAnimation(fromRightBorder, rightFrom, midRight, rightTo, new CubicEaseIn());
        var textExit = BuildAnimation(0.0, controlExitPos, new CubicEaseIn());
        var authContentExit = BuildAnimation(0.0, -controlExitPos, new CubicEaseIn());

        await Task.WhenAll(leftHalf1.RunAsync(ImageBorder), rightHalf1.RunAsync(InfoBorder), textExit.RunAsync(ImageText), authContentExit.RunAsync(AuthContent));

        // --- Midpoint: swap the view and reposition text ---
        AuthenticationViewModel.Instance?.ChangeAuthMode();
        ((TranslateTransform?)ImageText.RenderTransform)?.X = controlEnterPos;
        ((TranslateTransform?)AuthContent.RenderTransform)?.X = -controlEnterPos;
        
        // Second half animations
        var leftHalf2 = BuildAnimation(midLeft, leftTo, to, leftTo, new CubicEaseOut());
        var rightHalf2 = BuildAnimation(midRight, rightTo, toRightBorder, rightTo, new CubicEaseOut());
        var textEnter = BuildAnimation(controlEnterPos, 0.0, new CubicEaseOut());
        var authContentEnter = BuildAnimation(-controlEnterPos, 0.0, new CubicEaseOut());

        await Task.WhenAll(leftHalf2.RunAsync(ImageBorder), rightHalf2.RunAsync(InfoBorder), textEnter.RunAsync(ImageText), authContentEnter.RunAsync(AuthContent));

        await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() => {
            ((TranslateTransform?)ImageBorder.RenderTransform)?.X = to;
            ImageBorder.CornerRadius = leftTo;
            ((TranslateTransform?)InfoBorder.RenderTransform)?.X = toRightBorder;
            InfoBorder.CornerRadius = rightTo;
            ((TranslateTransform?)ImageText.RenderTransform)?.X = 0;
            ((TranslateTransform?)AuthContent.RenderTransform)?.X = 0;
        });
    }
    
    private Animation BuildAnimation(double fromX, double toX, Easing easing) {
        var animation = new Animation {
            Duration = TimeSpan.FromSeconds(0.25),
            Easing = easing,
            FillMode = FillMode.Both,
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = {
                    new Setter(TranslateTransform.XProperty, fromX)
                }},
                new KeyFrame { Cue = new Cue(1), Setters = {
                    new Setter(TranslateTransform.XProperty, toX)
                }}
            }
        };
        
        return animation;
    }

    private Animation BuildAnimation(double fromX, CornerRadius fromCornerRadius, double toX, CornerRadius toCornerRadius, Easing easing) {
        var animation = new Animation {
            Duration = TimeSpan.FromSeconds(0.25),
            Easing = easing,
            FillMode = FillMode.Both,
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = {
                    new Setter(TranslateTransform.XProperty, fromX),
                    new Setter(Border.CornerRadiusProperty, fromCornerRadius)
                }},
                new KeyFrame { Cue = new Cue(1), Setters = {
                    new Setter(TranslateTransform.XProperty, toX),
                    new Setter(Border.CornerRadiusProperty, toCornerRadius)
                }}
            }
        };
        
        return animation;
    }
}