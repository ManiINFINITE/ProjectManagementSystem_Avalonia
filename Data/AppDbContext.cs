using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Data;

public class AppDbContext : DbContext {
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectTask> Tasks => Set<ProjectTask>();
    public DbSet<UserProject> UserProjects => Set<UserProject>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ProjectManagementSystem",
            "project_management.db"
            );

        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<UserProject>()
            .HasIndex(up => new { up.UserId, up.ProjectId })
            .IsUnique();
    }
}