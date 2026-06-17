using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectManagementSystem.Enums;
using ProjectManagementSystem.Helpers;

namespace ProjectManagementSystem.Models;

[Table("Projects")]
public class Project {
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProjectId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } =  string.Empty;
    
    [Required]
    [MaxLength(500)]
    public string Description { get; set; } =  string.Empty;
    
    [DataType(DataType.Date)]
    public DateOnly? Deadline { get; set; }
    
    [Required]
    public ProjectStatus Status { get; set; } = ProjectStatus.Active;
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required]
    [MaxLength(10)]
    public string Color { get; set; } =  string.Empty;
    
    [Required]
    public int OwnerId { get; set; }

    [ForeignKey(nameof(OwnerId))]
    public User Owner { get; set; } = null!;
    
    public ICollection<ProjectTask> Tasks { get; set; } = [];
    public ICollection<UserProject> Members { get; set; } = [];
}