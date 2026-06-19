using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Repositories;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Services;

public class AppSettingsService {

    public static AppSettingsService? Instance { get; } = new();
    
    private static readonly string GlobalSettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "ProjectManagementSystem",
        "settings.json"
    );
    
    private static readonly string DefaultSettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "ProjectManagementSystem",
        "settings.json"
    );
    
    public GlobalSettings GlobalSettings { get; private set; } = new();
    public UserSettings CurrentSettings { get; private set; } = new();

    private static string GetUserSettingsPath(int userId) => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "ProjectManagementSystem",
        $"settings.user.{userId}.json"
    );

    public void Load() {
        try {
            if (File.Exists(GlobalSettingsPath)) {
                var json = File.ReadAllText(GlobalSettingsPath);
                GlobalSettings = JsonSerializer.Deserialize<GlobalSettings>(json) ?? new GlobalSettings();
            }
        } catch {
            GlobalSettings = new GlobalSettings();
        }

        // If last user exists, load their settings
        if (GlobalSettings.LastUserId.HasValue)
            LoadForUser(GlobalSettings.LastUserId.Value);
        else
            Apply(CurrentSettings);
    }

    public void Save() {
        if (GlobalSettings.LastUserId == null) return;
        var path = GetUserSettingsPath(GlobalSettings.LastUserId.Value);
        try {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            var json = JsonSerializer.Serialize(CurrentSettings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        } catch { /**/ }
    }

    public void ApplyAccentColor(AccentColorOption option) {
        if (Application.Current == null) return;
        
        Application.Current.Resources["PrimaryAccentColor"] = option.PrimaryColor.Color;
        Application.Current.Resources["SecondaryAccentColor"] = option.SecondaryColor.Color;
        Application.Current.Resources["ImageTextForeground"] = option.ImageTextForeground;
        Application.Current.Resources["ButtonHoverBackground"] = option.ButtonHoverBackground;
        AuthenticationViewModel.Instance?.UpdateImage(option.ImagePath);
        
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
        
        // Save to DB if user is logged in
        var currentUser = SessionService.Instance.CurrentUser;
        if (currentUser != null) {
            currentUser.AccentColorName = option.Name;
            _ = new UserRepository().UpdateAccentColorAsync(currentUser.Id, option.Name);
        }

        CurrentSettings.AccentColorName = option.Name;
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

    private void Apply(UserSettings settings) {
        if (Application.Current == null) return;

        var accent = AccentColorsBase.AccentColors.FirstOrDefault(c => c.Name == settings.AccentColorName)
                     ?? AccentColorsBase.AccentColors.First();
        
        ApplyAccentColor(accent);

        Application.Current.RequestedThemeVariant = settings.Theme switch {
            "Light" => ThemeVariant.Light,
            "Dark" => ThemeVariant.Dark,
            _ => ThemeVariant.Default
        };

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
    
    public void LoadForUser(int userId) {
        var path = GetUserSettingsPath(userId);
        try {
            if (File.Exists(path)) {
                var json = File.ReadAllText(path);
                CurrentSettings = JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
            } else {
                CurrentSettings = new UserSettings();
            }
        } catch {
            CurrentSettings = new UserSettings();
        }
        
        // Override accent color from DB
        var user = SessionService.Instance.CurrentUser;
        if (user != null) {
            CurrentSettings.AccentColorName = user.AccentColorName;
        }

        Apply(CurrentSettings);
    }

    public void SaveLastUser(int userId) {
        GlobalSettings.LastUserId = userId;
        SaveGlobal();
    }
    
    private void SaveGlobal() {
        try {
            Directory.CreateDirectory(Path.GetDirectoryName(GlobalSettingsPath)!);
            var json = JsonSerializer.Serialize(GlobalSettings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GlobalSettingsPath, json);
        } catch { /**/ }
    }

    public void AddRememberedUser(int userId) {
        if (!GlobalSettings.RememberedUsers.Contains(userId)) {
            GlobalSettings.RememberedUsers.Insert(0, userId);
            if (GlobalSettings.RememberedUsers.Count > 6) {
                GlobalSettings.RememberedUsers.RemoveAt(6);
            }
        } else {
            // Move to front if already exists
            GlobalSettings.RememberedUsers.Remove(userId);
            GlobalSettings.RememberedUsers.Insert(0, userId);
        }
        SaveGlobal();
    }
}