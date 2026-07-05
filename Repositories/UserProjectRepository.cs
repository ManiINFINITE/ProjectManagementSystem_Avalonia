using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Repositories;

public class UserProjectRepository {

    public async Task AddAsync(UserProject userProject) {
        await using var db = new AppDbContext();
        db.UserProjects.Add(userProject);
        await db.SaveChangesAsync();
    }

    public async Task<bool> RemoveAsync(int userId, int projectId) {
        await using var db = new AppDbContext();
        var userProject = await db.UserProjects
            .FirstOrDefaultAsync(up => up.UserId == userId && up.ProjectId == projectId);
        if (userProject == null) return false;
        db.UserProjects.Remove(userProject);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<List<UserProject>> GetMembersByProjectIdAsync(int projectId) {
        await using var db = new AppDbContext();
        return await db.UserProjects
            .Include(up => up.User)
            .ThenInclude(u => u.ProfilePicture)
            .Where(up => up.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<List<Project>> GetProjectsByUserIdAsync(int userId)
    {
        await using var db = new AppDbContext();

        var userProjects = await db.UserProjects
            .Where(up => up.UserId == userId)
            .Include(up => up.Project)
            .ThenInclude(p => p.Owner)
            .Include(up => up.Project)
            .ThenInclude(p => p.Members)
            .ThenInclude(m => m.User)
            .Include(up => up.Project)
            .ThenInclude(p => p.Tasks)
            .ThenInclude(t => t.Assignee)
            .ToListAsync();

        return userProjects
            .Select(up => up.Project)
            .ToList();
    }

    public async Task<UserProject?> GetByIdsAsync(int userId, int projectId) {
        await using var db = new AppDbContext();
        return await db.UserProjects
            .FirstOrDefaultAsync(up => up.UserId == userId && up.ProjectId == projectId);
    }
    
    public async Task RemoveAllByProjectIdAsync(int projectId) {
        await using var db = new AppDbContext();
        var members = await db.UserProjects
            .Where(up => up.ProjectId == projectId)
            .ToListAsync();
        
        db.UserProjects.RemoveRange(members);
        await db.SaveChangesAsync();
    }
}