using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementSystem.Models;

[Table("UserProfilePictures")]
public class UserProfilePicture {
    
    [Key]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    public byte[] PictureData { get; set; } = [];
    
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation
    public User User { get; set; } = null!;
}