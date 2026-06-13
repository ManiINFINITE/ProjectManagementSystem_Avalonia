using System.Linq;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Repositories;

public class UserRepository {

    public void Add(User user) {
        using var db = new AppDbContext();
        db.Users.Add(user);
        db.SaveChanges();
    }

    public User? GetById(int id) {
        using var db = new AppDbContext();
        return db.Users.Find(id);
    }

    public User? GetByUsername(string username) {
        using var db = new AppDbContext();
        return db.Users.FirstOrDefault(u => u.Username == username);
    }

    public User? GetByEmail(string email) {
        using var db = new AppDbContext();
        return db.Users.FirstOrDefault(u => u.Email == email);
    }

    public void UpdateProfilePicture(int userId, byte[] pictureData) {
        using var db = new AppDbContext();

        var user = db.Users.Find(userId);
        if (user == null) return;
        user.ProfilePicture = pictureData;
        db.SaveChanges();
    }
    
    public void DeleteProfilePicture(int userId) {
        using var db = new AppDbContext();
        var user = db.Users.Find(userId);
        if (user == null) return;
        user.ProfilePicture = null;
        db.SaveChanges();
    }
}