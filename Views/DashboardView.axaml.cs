using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Views;

public partial class DashboardView : UserControl {

    private bool _isExpanded = true;
    private const double EXPANDED_WIDTH = 250;
    private  const double COLLAPSED_WIDTH = 60;
    private const double HLINE_COLLAPSED_WIDTH = 30.0;
    private const double HLINE_EXPANDED_WIDTH = 220.0;
    private const double ADD_BUTTON_COLLAPSED_OFFSET = -194.0;
    
    public DashboardView() {
        InitializeComponent();
    }

    private async void CollapseButton_Click(object? sender, RoutedEventArgs e) {
        if (_isExpanded) {
            await Collapse();
        } else {
            await Expand();
        }
        
        _isExpanded = !_isExpanded;
    }

    private async Task Collapse() {
        var moveAddButton = new Animation {
            Duration = TimeSpan.FromSeconds(0.3),
            FillMode = FillMode.Both,
            Easing = new CubicEaseInOut(),
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = { new Setter(TranslateTransform.XProperty, 0) } },
                new KeyFrame {
                    Cue = new Cue(1),
                    Setters = { new Setter(TranslateTransform.XProperty, ADD_BUTTON_COLLAPSED_OFFSET) }
                }
            }
        };
        
        // Rotate icon 180deg
        var rotateAnim = new Animation {
            Duration = TimeSpan.FromSeconds(0.3),
            FillMode = FillMode.Both,
            Easing = new CubicEaseInOut(),
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = {
                    new Setter(RotateTransform.AngleProperty, 0.0),
                    new Setter(MarginProperty, new Thickness(0))
                } },
                new KeyFrame { Cue = new Cue(1), Setters = {
                    new Setter(RotateTransform.AngleProperty, 180.0),
                    new Setter(MarginProperty, new Thickness(0, 0, 5, 0))
                } }
            }
        };
        
        // Fade out texts
        var fadeOut = new Animation {
            Duration = TimeSpan.FromSeconds(0.15),
            FillMode = FillMode.Both,
            Easing = new CubicEaseIn(),
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = { new Setter(OpacityProperty, 1.0) }},
                new KeyFrame { Cue = new Cue(1), Setters = { new Setter(OpacityProperty, 0.0) }},
            }
        };
        
        // Shrink the panel
        var shrink = new Animation {
            Duration = TimeSpan.FromSeconds(0.3),
            FillMode = FillMode.Both,
            Easing = new CubicEaseInOut(),
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = {
                    new Setter(Border.WidthProperty, EXPANDED_WIDTH),
                }},
                new KeyFrame { Cue = new Cue(1), Setters = {
                    new Setter(Border.WidthProperty, COLLAPSED_WIDTH),
                }},
            }
        };
        
        var shrinkHLines = new Animation {
            Duration = TimeSpan.FromSeconds(0.3),
            FillMode = FillMode.Both,
            Easing = new CubicEaseInOut(),
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = {
                    new Setter(Border.WidthProperty, HLINE_EXPANDED_WIDTH),
                }},
                new KeyFrame { Cue = new Cue(1), Setters = {
                    new Setter(Border.WidthProperty, HLINE_COLLAPSED_WIDTH),
                }},
            }
        };

        await Task.WhenAll(
            rotateAnim.RunAsync(CollapseButton),
            fadeOut.RunAsync(HomeText),
            fadeOut.RunAsync(MessagesText),
            fadeOut.RunAsync(TasksText),
            fadeOut.RunAsync(MembersText),
            fadeOut.RunAsync(SettingsText),
            moveAddButton.RunAsync(AddProjectButton),
            fadeOut.RunAsync(MyProjectsText),
            fadeOut.RunAsync(LogoutText),
            fadeOut.RunAsync(ProjectMText),
            fadeOut.RunAsync(ExpandedLeftPanelHeader),
            shrink.RunAsync(LeftPanel), 
            shrinkHLines.RunAsync(HLine1), 
            shrinkHLines.RunAsync(HLine2),
            shrinkHLines.RunAsync(HLine3)
        );
        
        // Hide texts
        HomeText.IsVisible = false;
        MessagesText.IsVisible = false;
        TasksText.IsVisible = false;
        MembersText.IsVisible = false;
        SettingsText.IsVisible = false;
        MyProjectsText.IsVisible = false;
        LogoutText.IsVisible = false;
        ProjectMText.IsVisible = false;
        ExpandedLeftPanelHeader.IsVisible = false;
        
        LeftPanel.Width = COLLAPSED_WIDTH;
        HLine1.Width = HLINE_COLLAPSED_WIDTH;
        HLine2.Width = HLINE_COLLAPSED_WIDTH;
        HLine3.Width = HLINE_COLLAPSED_WIDTH;
        ((RotateTransform?)CollapseButton.RenderTransform)?.Angle = 180.0;
        CollapseButton.Margin = new Thickness(0, 0, 5, 0);
        ((TranslateTransform?)AddProjectButton.RenderTransform)?.X = ADD_BUTTON_COLLAPSED_OFFSET;
    }

    private async Task Expand() {
        var moveAddButton = new Animation {
            Duration = TimeSpan.FromSeconds(0.3),
            FillMode = FillMode.Both,
            Easing = new CubicEaseInOut(),
            Children = {
                new KeyFrame {
                    Cue = new Cue(0),
                    Setters = { new Setter(TranslateTransform.XProperty, ADD_BUTTON_COLLAPSED_OFFSET) }
                },
                new KeyFrame {
                    Cue = new Cue(1),
                    Setters = { new Setter(TranslateTransform.XProperty, 0.0) }
                }
            }
        };
        
        // Expand panel first
        var expand = new Animation {
            Duration = TimeSpan.FromSeconds(0.3),
            FillMode = FillMode.Both,
            Easing = new CubicEaseInOut(),
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = {
                    new Setter(Border.WidthProperty, COLLAPSED_WIDTH),
                }},
                new KeyFrame { Cue = new Cue(1), Setters = { new Setter(Border.WidthProperty, EXPANDED_WIDTH) }}
            }
        };
        
        var expandHLines = new Animation {
            Duration = TimeSpan.FromSeconds(0.3),
            FillMode = FillMode.Both,
            Easing = new CubicEaseInOut(),
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = { new Setter(Border.WidthProperty, HLINE_COLLAPSED_WIDTH) }},
                new KeyFrame { Cue = new Cue(1), Setters = { new Setter(Border.WidthProperty, HLINE_EXPANDED_WIDTH) }}
            }
        };
        
        // Rotate icon back while expanding
        var rotateBack = new Animation {
            Duration = TimeSpan.FromSeconds(0.3),
            FillMode = FillMode.Both,
            Easing = new CubicEaseInOut(),
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = {
                    new Setter(RotateTransform.AngleProperty, 180.0),
                    new Setter(MarginProperty, new Thickness(0, 5, 0, 0))
                }},
                new KeyFrame { Cue = new Cue(1), Setters = {
                    new Setter(RotateTransform.AngleProperty, 0.0),
                    new Setter(MarginProperty,  new Thickness(0))
                }}
            }
        };
        
        // Fade in texts
        var fadeIn = new Animation {
            Duration = TimeSpan.FromSeconds(0.2),
            FillMode = FillMode.Both,
            Easing = new CubicEaseOut(),
            Children = {
                new KeyFrame { Cue = new Cue(0), Setters = { new Setter(OpacityProperty, 0.0) }},
                new KeyFrame { Cue = new Cue(1), Setters = { new Setter(OpacityProperty, 1.0) }}
            }
        };
        
        // Show texts
        HomeText.IsVisible = true;
        MessagesText.IsVisible = true;
        TasksText.IsVisible = true;
        MembersText.IsVisible = true;
        SettingsText.IsVisible = true;
        MyProjectsText.IsVisible = true;
        LogoutText.IsVisible = true;
        ProjectMText.IsVisible = true;
        ExpandedLeftPanelHeader.IsVisible = true;
        
        await Task.WhenAll(
            fadeIn.RunAsync(HomeText),
            fadeIn.RunAsync(MessagesText),
            fadeIn.RunAsync(TasksText),
            fadeIn.RunAsync(MembersText),
            fadeIn.RunAsync(SettingsText),
            moveAddButton.RunAsync(AddProjectButton),
            fadeIn.RunAsync(MyProjectsText),
            fadeIn.RunAsync(LogoutText),
            fadeIn.RunAsync(ProjectMText),
            fadeIn.RunAsync(ExpandedLeftPanelHeader),
            expand.RunAsync(LeftPanel),
            expandHLines.RunAsync(HLine1),
            expandHLines.RunAsync(HLine2),
            expandHLines.RunAsync(HLine3),
            rotateBack.RunAsync(CollapseButton)
        );
        
        LeftPanel.Width = EXPANDED_WIDTH;
        HLine1.Width = HLINE_EXPANDED_WIDTH;
        HLine2.Width = HLINE_EXPANDED_WIDTH;
        HLine3.Width = HLINE_EXPANDED_WIDTH;
        ((RotateTransform?)CollapseButton.RenderTransform)?.Angle = 0.0;
        CollapseButton.Margin = new Thickness(0);
        ((TranslateTransform?)AddProjectButton.RenderTransform)?.X = 0;
    }

    private void AddProjectButton_Click(object? sender, RoutedEventArgs e) {
        if (DataContext is DashboardViewModel vm) {
            vm.OpenCreateProject();
        }
    }
}