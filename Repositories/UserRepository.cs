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

    public User? GetByUsername(string username) {
        using var db = new AppDbContext();
        return db.Users.FirstOrDefault(u => u.Username == username);
    }

    public User? GetByEmail(string email) {
        using var db = new AppDbContext();
        return db.Users.FirstOrDefault(u => u.Email == email);
    }
}