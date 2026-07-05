using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Repositories;

public class ProjectRepository {
    public async Task AddAsync(Project project) {
        await using var db = new AppDbContext();
        await db.Projects.AddAsync(project);
        await db.SaveChangesAsync();
    }

    public async Task<Project?> GetByIdAsync(int id) {
        await using var db = new AppDbContext();
        return await db.Projects.FindAsync(id);
    }

    public async Task<List<Project>> GetAllAsync() {
        await using var db = new AppDbContext();
        return await db.Projects.ToListAsync();
    }

    public async Task UpdateAsync(Project project) {
        await using var db = new AppDbContext();
        db.Projects.Update(project);
        await db.SaveChangesAsync();
    }

    public async Task<bool> RemoveAsync(int id) {
        await using var db = new AppDbContext();
        var project = await db.Projects.FindAsync(id);
        if (project == null) return false;
        db.Projects.Remove(project);
        await db.SaveChangesAsync();
        return true;
    }
}