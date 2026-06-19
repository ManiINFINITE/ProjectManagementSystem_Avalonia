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
        var connectionString = "Server=localhost;Database=ProjectManagementSystem;User=root;Password=Mani@1384;";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<UserProject>()
            .HasKey(up => new { up.UserId, up.ProjectId });
    }
}