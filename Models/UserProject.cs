using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementSystem.Models;

[Table("UserProject")]
public class UserProject {
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Role  { get; set; } = string.Empty;
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int ProjectId { get; set; }

    [ForeignKey(nameof(UserId))] 
    public User User { get; set; } = null!;
    
    [ForeignKey(nameof(ProjectId))]
    public Project Project { get; set; } = null!;
}