using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectManagementSystem.Enums;

namespace ProjectManagementSystem.Models;

[Table("Tasks")]
public class ProjectTask {
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaskId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    [Required]
    public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.ToDo;
    
    [DataType(DataType.Date)]
    public DateOnly? Deadline { get; set; } = null;
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required]
    public int AssigneeId { get; set; }
    
    [ForeignKey(nameof(AssigneeId))]
    public User Assignee { get; set; } = null!;
    
    [Required]
    public int ProjectId { get; set; }
    
    [ForeignKey(nameof(ProjectId))]
    public Project Project { get; set; } = null!;
}