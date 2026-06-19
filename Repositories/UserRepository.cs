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

    public async Task<User?> GetByUsernameWithProfilePictureAsync(string username) {
        await  using var db = new AppDbContext();
        return await db.Users
            .Include(u => u.ProfilePicture)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<bool> UpdateProfilePictureAsync(int userId, byte[] pictureData) {
        await using var db = new AppDbContext();

        var existing = await db.UserProfilePictures.FindAsync(userId);
        if (existing != null) {
            // Update existing
            existing.PictureData = pictureData;
            existing.UploadedAt = System.DateTime.UtcNow;
        } else {
            // Insert new
            await db.UserProfilePictures.AddAsync(new UserProfilePicture {
                UserId =  userId,
                PictureData = pictureData
            });
        }
        
        await db.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteProfilePictureAsync(int userId) {
        await using var db = new AppDbContext();

        var picture = await db.UserProfilePictures.FindAsync(userId);
        if (picture == null) return false;

        db.UserProfilePictures.Remove(picture);
        await db.SaveChangesAsync();
        return true;
    }
}