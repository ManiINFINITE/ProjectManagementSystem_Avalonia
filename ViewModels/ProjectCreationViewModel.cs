using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Repositories;
using ProjectManagementSystem.Services;

namespace ProjectManagementSystem.ViewModels;

public partial class ProjectCreationViewModel : ViewModelBase {

    private readonly UserRepository _userRepository = new();
    private readonly ProjectRepository _projectRepository = new();
    private readonly ProjectTaskRepository _taskRepository = new();
    private readonly UserProjectRepository _userProjectRepository = new();
    
    // --- Project Info ---
    [ObservableProperty] private ObservableCollection<ProjectColorOption> _projectColors = ProjectColorsBase.ProjectColors;
    [ObservableProperty] private ProjectColorOption? _selectedProjectColor;
    [ObservableProperty] private string _projectName = string.Empty;
    [ObservableProperty] private string _projectDescription = string.Empty;
    [ObservableProperty] private DateTime? _projectDeadline;
    
    // --- Assignees ---
    [ObservableProperty] private ObservableCollection<User> _allUsers = [];
    [ObservableProperty] private User? _selectedUser;
    [ObservableProperty] private string _assigneeRole = string.Empty;
    [ObservableProperty] private ObservableCollection<AssigneeEntry> _assignees = [];
    
    // --- Tasks ---
    [ObservableProperty] private string _taskTitle = string.Empty;
    [ObservableProperty] private string _taskDescription = string.Empty;
    [ObservableProperty] private TaskPriority _selectedPriority = TaskPriority.Medium;
    [ObservableProperty] private DateTime? _taskDeadline;
    [ObservableProperty] private User? _selectedTaskAssignee;
    [ObservableProperty] private ObservableCollection<TaskEntry> _tasks = [];
    
    // Read-only display
    public string CreatedAt => DateTime.Now.ToString("d MMMM, yyyy h:mm tt");
    public string CreatedBy => SessionService.Instance.CurrentUser?.Username ?? string.Empty;
    public string Status => nameof(ProjectStatus.Active);
    public IEnumerable<TaskPriority> Priorities => Enum.GetValues<TaskPriority>();
    public IEnumerable<User> AssigneeUsers => Assignees.Select(a => a.User);

    public ProjectCreationViewModel() {
        LoadUsers();
        Assignees.CollectionChanged += (_, _) => OnPropertyChanged(nameof(AssigneeUsers));
    }

    private async void LoadUsers() {
        var users = await _userRepository.GetAllAsync();
        AllUsers = new ObservableCollection<User>(users);
    }

    [RelayCommand]
    private void AssignUser() {
        if (SelectedUser == null || string.IsNullOrWhiteSpace(AssigneeRole)) return;
        if (Assignees.Any(a => a.User.Id == SelectedUser.Id)) return;
    
        Assignees.Add(new AssigneeEntry { User = SelectedUser, Role = AssigneeRole });
        SelectedUser = null;
        AssigneeRole = string.Empty;
    }

    [RelayCommand]
    private void RemoveAssignee(AssigneeEntry entry) {
        Assignees.Remove(entry);
    }

    [RelayCommand]
    private void AddTask() {
        if (string.IsNullOrWhiteSpace(TaskTitle)) return;
        
        Tasks.Add(new TaskEntry {
            Title = TaskTitle,
            Description = TaskDescription,
            Priority = SelectedPriority,
            Deadline = TaskDeadline.HasValue ? DateOnly.FromDateTime(TaskDeadline.Value) : null,
            Assignee = SelectedTaskAssignee
        });
        
        // Reset fields
        TaskTitle = string.Empty;
        TaskDescription = string.Empty;
        SelectedPriority = TaskPriority.Medium;
        TaskDeadline = null;
        SelectedTaskAssignee = null;
    }

    [RelayCommand]
    private void RemoveTask(TaskEntry entry) {
        Tasks.Remove(entry);
    }

    [RelayCommand]
    private async Task CreateProject() {
        if (string.IsNullOrWhiteSpace(ProjectName)) {
            NotificationService.Instance.Send("Missing Name!", "Please enter a project name.", NotificationType.Error);
            return;
        }
        
        var owner = SessionService.Instance.CurrentUser!;

        var project = new Project {
            Name = ProjectName,
            Description = ProjectDescription,
            Deadline = ProjectDeadline.HasValue ? DateOnly.FromDateTime(ProjectDeadline.Value) : null,
            Status =  ProjectStatus.Active,
            CreatedAt = DateTime.Now,
            OwnerId = owner.Id,
            Color = SelectedProjectColor?.HexColor ?? "#6C63FF"
        };
        
        await _projectRepository.AddAsync(project);
        
        // Add Assignees
        foreach (var entry in Assignees) {
            await _userProjectRepository.AddAsync(new UserProject {
                UserId = entry.User.Id,
                ProjectId = project.ProjectId,
                Role = entry.Role
            });
        }
        
        // Add tasks
        foreach (var task in Tasks) {
            await _taskRepository.AddAsync(new ProjectTask {
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = ProjectTaskStatus.ToDo,
                Deadline = task.Deadline,
                CreatedAt = DateTime.Now,
                AssigneeId = task.Assignee?.Id ?? owner.Id,
                ProjectId =  project.ProjectId
            });
        }
        
        NotificationService.Instance.Send("Project Created!", $"{project.Name} created successfully!", NotificationType.Success);
        DashboardViewModel.Instance?.CloseCreateProject();
    }

    [RelayCommand]
    private void Discard() {
        DashboardViewModel.Instance?.CloseCreateProject();
    }
}