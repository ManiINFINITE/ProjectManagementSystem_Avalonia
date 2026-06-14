# 📋 Project Management System
> A cross-platform desktop application built with **Avalonia UI** and **.NET 10**, featuring multi-user authentication, per-user settings, a custom notification system, and a fully themed UI.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet)
![Avalonia](https://img.shields.io/badge/Avalonia-12.0-8B5CF6?style=flat)
![EF Core](https://img.shields.io/badge/EF%20Core-10.0-blue?style=flat)
![SQLite](https://img.shields.io/badge/SQLite-003B57?style=flat&logo=sqlite)
![Build](https://github.com/ManiINFINITE/ProjectManagementSystem_Avalonia/actions/workflows/dotnet.yml/badge.svg)

---

## ✨ Features

### 🔐 Authentication
- User registration with input validation (email format, password strength)
- Secure login with **BCrypt** password hashing
- Smooth Animated sign-in / sign-up transition
- Session management via a singleton `SessionService`

### 👤 Per-User Account Management
- Profile picture upload, display, and deletion (stored as `BLOB` in SQLite per user)
- Profile initials fallback when no picture is set
- Per-user settings persisted in individual JSON files (`settings.user.{id}.json`)
- Last logged-in user's settings automatically restored on app startup

### 🎨 Appearance & Theming
- **3 theme modes**: System, Light, Dark
- **17 accent color presets** with gradient previews (Slate Violet, Google Blue, Deep Purple, Emerald, and more)
- Each accent color preset comes with its **own unique signin Picture**
- All colors applied dynamically via Avalonia resource dictionaries
- Accent colors are looked up by name from a central `AccentColorsBase` — no stale color values stored in JSON

### 🔔 Notification System
- JetBrains-style toast notifications sliding in from the **bottom right**
- **4 notification types**: Normal, Success, Warning, Error — each with a distinct color accent
- **Queue system** — notifications are stacked and processed one by one
- Up to **3 notifications visible** at once with a stacked card depth effect (scale + offset)
- Per-notification **10s auto-dismiss** timer (configurable: 3s, 5s, 10s, 20s, Never)
- **Dismiss top** (minus icon) or **dismiss all** (cross icon) controls
- Smooth slide-in / slide-out animations at ~60fps

### ⚙️ Settings Page
- Theme switcher with radio buttons
- Accent color picker (circular gradient swatches)
- Profile picture management
- Notification toggle (enable/disable)
- Auto-dismiss duration selector

---

## 📸 Screenshots

| | |
|---|---|
| ![Sign In With Slate Violet Theme LIGHT](Screenshots/slate-violet-signin.png) | ![Sign Up With Slate Violet Theme LIGHT](Screenshots/slate-violet-signup.png) |
| ![Settings Page with Slate Violet Theme LIGHT](Screenshots/slate-violet-settings-LIGHT.png) | ![Settings Page with Slate Violet Theme DARK](Screenshots/slate-violet-settings-DARK.png) |
| ![Sign In With Deep Red Theme DARK](Screenshots/deep-red-signin.png) | ![Settings With Deep Red Theme DARK](Screenshots/deep-red-settings-DARK.png) |
| ![Settings With Deep Red Theme LIGHT](Screenshots/deep-red-settings-LIGHT.png) | ![Settings With Lime Forest Theme LIGHT](Screenshots/lime-forest-settings-LIGHT.png) |

---

## 🏗️ Project Structure

```
ProjectManagementSystem/
├── Assets/
│   └── Icons/           # icons used for main window
│   └── Images/          # 17 accent color background images (Git LFS)
├── Data/
│   └── AppDbContext.cs  # EF Core SQLite context
├── Enums/               # NotificationType, AuthenticationMode
├── Helpers/
│   └── UserValidator.cs # Email & password validation
├── Models/              # User, Notification, AppSettings, AccentColorOption, etc.
├── Repositories/        # UserRepository (data access layer)
├── Services/            # AppSettingsService, NotificationService, SessionService, SharedAnimationService
├── Styles/              # Avalonia AXAML stylesheets per view
├── ViewModels/          # MVVM ViewModels (CommunityToolkit.Mvvm)
├── Views/               # Avalonia UserControls and Windows
└── .github/workflows/   # CI build via GitHub Actions
```

---

## 🛠️ Tech Stack

| Technology | Purpose |
|---|---|
| [Avalonia UI 12](https://avaloniaui.net/) | Cross-platform desktop UI framework |
| [.NET 10](https://dotnet.microsoft.com/) | Runtime & language (C# 13) |
| [EF Core 10 + SQLite](https://learn.microsoft.com/en-us/ef/core/) | Database ORM |
| [CommunityToolkit.Mvvm](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/) | MVVM source generators |
| [BCrypt.Net-Next](https://github.com/BcryptNet/bcrypt.net) | Password hashing |
| [Git LFS](https://git-lfs.com/) | Large image asset storage |
| GitHub Actions | CI — build on every push |

---

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Any IDE: [Rider](https://www.jetbrains.com/rider/), [Visual Studio 2022](https://visualstudio.microsoft.com/), or [VS Code](https://code.visualstudio.com/)
- Git LFS installed: `git lfs install`

### Run Locally

```bash
# Clone the repo
git clone https://github.com/ManiINFINITE/ProjectManagementSystem_Avalonia.git
cd ProjectManagementSystem_Avalonia

# Restore dependencies
dotnet restore

# Run the app
dotnet run
```

The SQLite database (`project_management.db`) is created automatically on first run via `EnsureCreated()`.

---

## 🗺️ Roadmap

- [ ] Project creation and management
- [ ] Task assignment to users
- [ ] Project dashboard and tracking
- [ ] In-app messaging between users
- [ ] Role-based permissions (Admin, Member, Viewer)

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

---

## 👨‍💻 Author

**ManiINFINITE** — Designed and developed independently as a portfolio project.

[![GitHub](https://img.shields.io/badge/GitHub-ManiINFINITE-181717?style=flat&logo=github)](https://github.com/ManiINFINITE)
