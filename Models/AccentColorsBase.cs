using System.Collections.ObjectModel;
using Avalonia.Media;

namespace ProjectManagementSystem.Models;

public class AccentColorsBase {
    
    public static ObservableCollection<AccentColorOption> AccentColors { get; } = new() {
    
    // Cool & Professional
    new AccentColorOption {
        Name = "Slate Violet",
        PrimaryColor = new SolidColorBrush(Color.Parse("SlateBlue")),
        SecondaryColor = new SolidColorBrush(Color.Parse("BlueViolet")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#BDC2FF")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#8D80FF")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#343469")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#0E0E1F")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/slate-violet.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#9692FF")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#3E3E7C"))
    },
    new AccentColorOption {
        Name = "Google Blue",
        PrimaryColor = new SolidColorBrush(Color.Parse("#1A73E8")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#0D47A1")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#BDD8FF")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#80ABFF")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#0D1F3C")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#060F1F")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/google-blue.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#f0f7ff")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#0D3D7A"))
    },
    new AccentColorOption {
        Name = "Cyan Deep",
        PrimaryColor = new SolidColorBrush(Color.Parse("#00BCD4")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#006064")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#B2EEF5")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#80D8E8")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#0A2E32")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#051518")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/cyan-deep.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#e3c9ff")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#004D55"))
    },
    new AccentColorOption {
        Name = "Indigo",
        PrimaryColor = new SolidColorBrush(Color.Parse("#5C6BC0")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#283593")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#C5C9F0")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#9BA4E8")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#181C3A")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#0A0E20")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/indigo.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#f2ce8f")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#2E3A7A"))
    },
    new AccentColorOption {
        Name = "Sky Navy",
        PrimaryColor = new SolidColorBrush(Color.Parse("#29B6F6")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#01579B")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#B3E5FC")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#81D4FA")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#0A2030")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#050F18")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/sky-navy.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#2cdafc")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#0D3D6E"))
    },

    // Warm & Vibrant
    new AccentColorOption {
        Name = "Sunset Orange",
        PrimaryColor = new SolidColorBrush(Color.Parse("#FF6B35")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#D84315")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#FFD4C0")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#FFB399")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#3D1A0A")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#1F0A03")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/sunset-orange.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#feffc0")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#9C3010"))
    },
    new AccentColorOption {
        Name = "Hot Pink",
        PrimaryColor = new SolidColorBrush(Color.Parse("#FF4081")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#C51162")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#FFC0D6")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#FF80AB")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#3D0A1E")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#1F0510")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/hot-pink.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#ffd2fe")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#8C0D47"))
    },
    new AccentColorOption {
        Name = "Amber",
        PrimaryColor = new SolidColorBrush(Color.Parse("#FF8F00")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#E65100")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#FFE0B2")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#FFCC80")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#3D2200")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#1F1000")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/amber.jpeg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#e44f12")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#9C3800"))
    },
    new AccentColorOption {
        Name = "Rose Pink",
        PrimaryColor = new SolidColorBrush(Color.Parse("#F06292")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#AD1457")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#F8C0D0")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#F090B0")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#380A1E")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#1C050F")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/rose-pink.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#fdc7f3")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#8C0D3F"))
    },
    new AccentColorOption {
        Name = "Gold Orange",
        PrimaryColor = new SolidColorBrush(Color.Parse("#FFA726")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#E65100")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#FFE0B2")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#FFCC80")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#3D2200")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#1F1000")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/gold-orange.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#1e1209")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#9C3800"))
    },

    // Dark & Moody
    new AccentColorOption {
        Name = "Deep Purple",
        PrimaryColor = new SolidColorBrush(Color.Parse("#7B2FBE")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#4A148C")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#D9C0F0")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#C090E8")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#1E0A36")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#0F051C")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/deep-purple.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#f911ff")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#3A0D6E"))
    },
    new AccentColorOption {
        Name = "Blue Grey",
        PrimaryColor = new SolidColorBrush(Color.Parse("#37474F")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#102027")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#C5CDD0")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#A0B0B5")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#0D1518")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#06090A")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/blue-grey-dark.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#b4cde8")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#0D1A1F"))
    },
    new AccentColorOption {
        Name = "Emerald",
        PrimaryColor = new SolidColorBrush(Color.Parse("#4CAF50")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#1B5E20")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#C8EEC9")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#A0DCA2")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#0A1F0B")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#050F06")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/emerald-dark.png",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#edff8c")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#174D1A"))
    },
    new AccentColorOption {
        Name = "Deep Red",
        PrimaryColor = new SolidColorBrush(Color.Parse("#EF5350")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#B71C1C")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#FFC5C5")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#FF9494")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#3D0A0A")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#1F0505")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/deep-red.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#ff0d00")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#8C1010"))
    },

    // Unique & Stylish
    new AccentColorOption {
        Name = "Magenta Rose",
        PrimaryColor = new SolidColorBrush(Color.Parse("#EC407A")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#880E4F")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#F8C0D5")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#F090B8")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#380A1C")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#1C050E")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/magenta-rose.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#fad2fd")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#6E0A3A"))
    },
    new AccentColorOption {
        Name = "Teal Cyan",
        PrimaryColor = new SolidColorBrush(Color.Parse("#26C6DA")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#00838F")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#B2EDF2")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#80D8E0")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#0C2E32")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#051519")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/teal-cyan.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#eafffa")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#006570"))
    },
    new AccentColorOption {
        Name = "Lime Forest",
        PrimaryColor = new SolidColorBrush(Color.Parse("#32CD32")),
        SecondaryColor = new SolidColorBrush(Color.Parse("#228B22")),
        PrimaryLoginPanelAccentColor_LIGHT   = new SolidColorBrush(Color.Parse("#C8EEC9")),
        SecondaryLoginPanelAccentColor_LIGHT = new SolidColorBrush(Color.Parse("#A5D6A7")),
        PrimaryLoginPanelAccentColor_DARK    = new SolidColorBrush(Color.Parse("#0F200F")),
        SecondaryLoginPanelAccentColor_DARK  = new SolidColorBrush(Color.Parse("#071007")),
        ImagePath = "avares://ProjectManagementSystem/Assets/Images/lime-forest.jpg",
        ImageTextForeground = new SolidColorBrush(Color.Parse("#dfffe7")),
        ButtonHoverBackground = new SolidColorBrush(Color.Parse("#1F5C22"))
    }
};
}