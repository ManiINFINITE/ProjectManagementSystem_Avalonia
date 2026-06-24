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

    public async Task<List<Project>> GetProjectsByUserIdAsync(int userId) {
        await using var db = new AppDbContext();
        return await db.UserProjects
            .Include(up => up.Project)
            .Where(up => up.UserId == userId)
            .Select(up => up.Project)
            .ToListAsync();
    }

    public async Task<UserProject?> GetByIdsAsync(int userId, int projectId) {
        await using var db = new AppDbContext();
        return await db.UserProjects
            .FirstOrDefaultAsync(up => up.UserId == userId && up.ProjectId == projectId);
    }
}