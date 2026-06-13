using System.Linq;

namespace ProjectManagementSystem.Helpers;

public class UserValidator {
    
    private static readonly char[] SpecialChars = ['@', '!', '#', '$', '&', '%', '*', '?'];

    public static bool ValidateEmail(string email) {
        return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
    }
    
    public static string? ValidatePassword(string password) {
        string result = string.Empty;
        
        if (password.Length < 8) result += "Password must be at least 8 characters.\n";
        if (!password.Any(char.IsDigit)) result += "Password must contain at least one digit.\n";
        if (!password.Any(char.IsUpper)) result += "Password must contain at least one uppercase letter.\n";
        if (!password.Any(char.IsLower)) result += "Password must contain at least one lowercase letter.\n";
        if (!password.Any(c => SpecialChars.Contains(c))) result += "Password must contain at least one special character (!@#$%&?*).";

        return result;
    }
}