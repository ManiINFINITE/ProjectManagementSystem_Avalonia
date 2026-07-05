using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Repositories;

public class ProjectTaskRepository {

    public async Task AddAsync(ProjectTask task) {
        await using var db = new AppDbContext();
        await db.Tasks.AddAsync(task);
        await db.SaveChangesAsync();
    }

    public async Task<ProjectTask?> GetByIdAsync(int id) {
        await using var db = new AppDbContext();
        return await db.Tasks.FindAsync(id);
    }

    public async Task<List<ProjectTask>> GetByProjectIdAsync(int projectId) {
        await using var db = new AppDbContext();
        return await db.Tasks
            .Include(t => t.Assignee)
            .ThenInclude(u => u.ProfilePicture)
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<List<ProjectTask>> GetByAssigneeIdAsync(int assigneeId) {
        await using var db = new AppDbContext();
        return await db.Tasks
            .Where(t => t.AssigneeId == assigneeId)
            .ToListAsync();
    }

    public async Task UpdateAsync(ProjectTask task) {
        await using var db = new AppDbContext();
        db.Tasks.Update(task);
        await db.SaveChangesAsync();
    }
    
    public async Task RemoveAsync(ProjectTask task) {
        await using var db = new AppDbContext();
        db.Tasks.Remove(task);
        await db.SaveChangesAsync();
    }
    
    public async Task<bool> RemoveAsync(int taskId) {
        await using var db = new AppDbContext();
        var task = await db.Tasks.FindAsync(taskId);
        if (task == null) return false;
        db.Tasks.Remove(task);
        await db.SaveChangesAsync();
        return true;
    }
    
    public async Task RemoveAllByProjectIdAsync(int projectId) {
        await using var db = new AppDbContext();
        
        var tasks = await db.Tasks
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
        
        db.Tasks.RemoveRange(tasks);
        await db.SaveChangesAsync();
    }
}