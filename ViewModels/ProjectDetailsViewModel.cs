using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.Repositories;

namespace ProjectManagementSystem.ViewModels;

public partial class ProjectDetailsViewModel : ViewModelBase {
    private readonly UserProjectRepository _userProjectRepository = new();
    private readonly ProjectTaskRepository _projectTaskRepository = new();

    [ObservableProperty] private Project _project;
    [ObservableProperty] private ObservableCollection<UserProject> assignees = [];

    // --- All tasks unfiltered ---
    private List<ProjectTask> _allTasks = [];

    // --- Filtered columns ---
    [ObservableProperty] private ObservableCollection<ProjectTask> _toDoTasks = [];
    [ObservableProperty] private ObservableCollection<ProjectTask> _inProgressTasks = [];
    [ObservableProperty] private ObservableCollection<ProjectTask> _doneTasks = [];

    // --- Priority filter ---
    public IReadOnlyList<PriorityFilterOption> PriorityOptions { get; } = [
        new() { Name = "All", Value = null },
        new() { Name = "Low", Value = TaskPriority.Low },
        new() { Name = "Medium", Value = TaskPriority.Medium },
        new() { Name = "High", Value = TaskPriority.High }
    ];

    [ObservableProperty] private PriorityFilterOption? _selectedPriorityOption;

    // --- Deadline Filter ---
    [ObservableProperty] private DateTimeOffset? _deadlineFilter;

    public ProjectDetailsViewModel(Project project) {
        _project = project;
        SelectedPriorityOption = PriorityOptions[0];

        if (Design.IsDesignMode) return;

        _ = LoadAssigneesAsync();
        _ = LoadTasksAsync();
    }

    private async Task LoadAssigneesAsync() {
        try {
            var members = await _userProjectRepository.GetMembersByProjectIdAsync(Project.ProjectId);
            Assignees = new ObservableCollectionListSource<UserProject>(members);
        } catch (Exception ex) {
            Console.WriteLine($"=== Failed to load assignees: {ex.Message}");
        }
    }

    private async Task LoadTasksAsync() {
        try {
            _allTasks = await _projectTaskRepository.GetByProjectIdAsync(Project.ProjectId);
            ApplyFilters();
        } catch (Exception ex) {
            Console.WriteLine($"=== Failed to load tasks: {ex.Message}");
        }
    }

    partial void OnSelectedPriorityOptionChanged(PriorityFilterOption? value) {
        ApplyFilters();
    }

    partial void OnDeadlineFilterChanged(DateTimeOffset? value) {
        ApplyFilters();
    }

    private void ApplyFilters() {
        DateOnly? deadlineCutoff = DeadlineFilter is { } d ? DateOnly.FromDateTime(d.DateTime) : null;

        var selectedPriority = SelectedPriorityOption?.Value;

        var filtered = _allTasks.Where(t =>
            (selectedPriority is null || t.Priority == selectedPriority) &&
            (deadlineCutoff is null || t.Deadline is null || t.Deadline <= deadlineCutoff)
        ).ToList();

        ToDoTasks = new ObservableCollection<ProjectTask>(
            filtered.Where(t => t.Status == ProjectTaskStatus.ToDo));
        InProgressTasks = new ObservableCollection<ProjectTask>(
            filtered.Where(t => t.Status == ProjectTaskStatus.InProgress));
        DoneTasks = new ObservableCollection<ProjectTask>(
            filtered.Where(t => t.Status == ProjectTaskStatus.Done));
    }
}