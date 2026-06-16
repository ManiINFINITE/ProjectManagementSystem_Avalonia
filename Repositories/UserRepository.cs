using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Repositories;

public class UserRepository {

    public async Task AddAsync(User user) {
        await using var db = new AppDbContext();
        
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllAsync() {
        await using var db = new AppDbContext();
        return await db.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id) {
        await using var db = new AppDbContext();
        
        return await db.Users.FindAsync(id);
    }

    public async Task<User?> GetByUsernameAsync(string username) {
        await using var db = new AppDbContext();
        
        return await db.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetByEmailAsync(string email) {
        await using var db = new AppDbContext();
        
        return await db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> UpdateProfilePictureAsync(int userId, byte[] pictureData) {
        await using var db = new AppDbContext();

        var user = await db.Users.FindAsync(userId);
        if (user == null) return false;
        user.ProfilePicture = pictureData;
        await db.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteProfilePictureAsync(int userId) {
        await using var db = new AppDbContext();
        
        var user = await db.Users.FindAsync(userId);
        if (user == null) return false;
        user.ProfilePicture = null;
        await db.SaveChangesAsync();
        return true;
    }
}