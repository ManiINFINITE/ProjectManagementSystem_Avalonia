using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Services;

public class AppSettingsService {

    public static AppSettingsService? Instance { get; } = new();

    private string _currentSettingsPath = DefaultSettingsPath;

    private static readonly string SettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "ProjectManagementSystem",
        "settings.json"
        );

#if DEBUG
    private static readonly string DebugSettingsPath = Path.Combine(
        AppContext.BaseDirectory,
        "..", "..", "..",
        "settings.debug.json"
        );
#endif

    public AppSettings CurrentSettings { get; private set; } = new();

    public void Load() {
        try {
            if (File.Exists(SettingsPath)) {
                var json = File.ReadAllText(SettingsPath);
                CurrentSettings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
        } catch {
            CurrentSettings = new AppSettings();
        }

        Apply(CurrentSettings);
    }

    private void Save() {
        try {
            Directory.CreateDirectory(Path.GetDirectoryName(_currentSettingsPath)!);
            var json = JsonSerializer.Serialize(CurrentSettings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_currentSettingsPath, json);
#if DEBUG
            File.WriteAllText(DebugSettingsPath, json);
#endif
        } catch { /* handle silently */ }
    }

    public void ApplyAccentColor(AccentColorOption option) {
        if (Application.Current == null) return;
        
        Application.Current.Resources["PrimaryAccentColor"] = option.PrimaryColor.Color;
        Application.Current.Resources["SecondaryAccentColor"] = option.SecondaryColor.Color;
        AuthenticationViewModel.Instance?.UpdateImage(option.ImagePath);
        Application.Current.Resources["ImageTextForeground"] = option.ImageTextForeground;
        Application.Current.Resources["ButtonHoverBackground"] = option.ButtonHoverBackground;
        
        // Apply login panel colors to the correct theme dictionaries
        if (Application.Current.Resources.ThemeDictionaries.TryGetValue(ThemeVariant.Light, out var lightDict)
            && lightDict is ResourceDictionary light) {
            light["PrimaryLoginPanelAccentColor"] = option.PrimaryLoginPanelAccentColor_LIGHT.Color;
            light["SecondaryLoginPanelAccentColor"] = option.SecondaryLoginPanelAccentColor_LIGHT.Color;
        }

        if (Application.Current.Resources.ThemeDictionaries.TryGetValue(ThemeVariant.Dark, out var darkDict)
            && darkDict is ResourceDictionary dark) {
            dark["PrimaryLoginPanelAccentColor"] = option.PrimaryLoginPanelAccentColor_DARK.Color;
            dark["SecondaryLoginPanelAccentColor"] = option.SecondaryLoginPanelAccentColor_DARK.Color;
        }

        CurrentSettings.PrimaryAccentColor = option.PrimaryColor.Color.ToString();
        CurrentSettings.SecondaryAccentColor = option.SecondaryColor.Color.ToString();
        CurrentSettings.AccentColorName = option.Name;
        CurrentSettings.ImagePath = option.ImagePath;
        CurrentSettings.ImageTextForeground = option.ImageTextForeground.ToString();
        CurrentSettings.PrimaryLoginPanelAccentColor_LIGHT   = option.PrimaryLoginPanelAccentColor_LIGHT.Color.ToString();
        CurrentSettings.SecondaryLoginPanelAccentColor_LIGHT = option.SecondaryLoginPanelAccentColor_LIGHT.Color.ToString();
        CurrentSettings.PrimaryLoginPanelAccentColor_DARK    = option.PrimaryLoginPanelAccentColor_DARK.Color.ToString();
        CurrentSettings.SecondaryLoginPanelAccentColor_DARK  = option.SecondaryLoginPanelAccentColor_DARK.Color.ToString();
        CurrentSettings.ButtonHoverBackground = option.ButtonHoverBackground.ToString();
        Save();
    }

    public void ApplyTheme(string theme) {
        if (Application.Current == null) return;

        Application.Current.RequestedThemeVariant = theme switch {
            "Light" => ThemeVariant.Light,
            "Dark" => ThemeVariant.Dark,
            _ => ThemeVariant.Default
        };

        CurrentSettings.Theme = theme;
        Save();
    }

    private void Apply(AppSettings settings) {
        if (Application.Current == null) return;

        var accent = AccentColorsBase.AccentColors.FirstOrDefault(c => c.Name == settings.AccentColorName);
        if (accent != null) {
            ApplyAccentColor(accent);
        } else {
            Application.Current.Resources["PrimaryAccentColor"]   = Color.Parse(settings.PrimaryAccentColor);
            Application.Current.Resources["SecondaryAccentColor"] = Color.Parse(settings.SecondaryAccentColor);
            Application.Current.Resources["ImageTextForeground"] = Color.Parse(settings.ImageTextForeground);
            Application.Current.Resources["ButtonHoverBackground"] = Color.Parse(settings.ButtonHoverBackground);

            if (Application.Current.Resources.ThemeDictionaries.TryGetValue(ThemeVariant.Light, out var lightDict)
                && lightDict is ResourceDictionary light) {
                light["PrimaryLoginPanelAccentColor"]   = Color.Parse(settings.PrimaryLoginPanelAccentColor_LIGHT);
                light["SecondaryLoginPanelAccentColor"] = Color.Parse(settings.SecondaryLoginPanelAccentColor_LIGHT);
            }

            if (Application.Current.Resources.ThemeDictionaries.TryGetValue(ThemeVariant.Dark, out var darkDict)
                && darkDict is ResourceDictionary dark) {
                dark["PrimaryLoginPanelAccentColor"]   = Color.Parse(settings.PrimaryLoginPanelAccentColor_DARK);
                dark["SecondaryLoginPanelAccentColor"] = Color.Parse(settings.SecondaryLoginPanelAccentColor_DARK);
            }
        }

        Application.Current.RequestedThemeVariant = settings.Theme switch {
            "Light" => ThemeVariant.Light,
            "Dark"  => ThemeVariant.Dark,
            _       => ThemeVariant.Default
        };
    }

    private static string GetSettingsPath(int userId) => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "ProjectManagementSystem",
        $"settings.user.{userId}.json"
        );
    
    // Keep a global fallback for pre-login (theme etc.)
    private static readonly string DefaultSettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "ProjectManagementSystem",
        "settings.json"
    );

    public void LoadForUser(int userId) {
        _currentSettingsPath = GetSettingsPath(userId);

        try {
            if (File.Exists(_currentSettingsPath)) {
                var json = File.ReadAllText(_currentSettingsPath);
                CurrentSettings = JsonSerializer.Deserialize<AppSettings>(json) ??  new AppSettings();
            } else {
                CurrentSettings = new AppSettings();
            }
        } catch {
            CurrentSettings = new AppSettings();
        }
        
        Apply(CurrentSettings);
    }

    public void SaveLastUser(int userId) {
        try {
            // Read existing global settings, update LastUserId, write back
            AppSettings global = new();
            if (File.Exists(DefaultSettingsPath)) {
                var existing = File.ReadAllText(DefaultSettingsPath);
                global = JsonSerializer.Deserialize<AppSettings>(existing) ?? new AppSettings();
            }
            global.LastUserId = userId;
            Directory.CreateDirectory(Path.GetDirectoryName(DefaultSettingsPath)!);
            File.WriteAllText(DefaultSettingsPath, JsonSerializer.Serialize(global, new JsonSerializerOptions { WriteIndented = true }));
        } catch { /**/ }
    }
}