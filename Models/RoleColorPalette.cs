namespace ProjectManagementSystem.Models;

internal static class RoleColorPalette
{
    private static readonly string[] Colors = {
        "#F94144", "#F3722C", "#F8961E", "#F9C74F",
        "#90BE6D", "#43AA8B", "#577590", "#277DA1",
        "#9D4EDD", "#FF5DA2", "#4D908E", "#6A4C93"
    };

    public static Avalonia.Media.Color GetBaseColor(string seed)
    {
        var index = System.Math.Abs(seed.ToLowerInvariant().GetHashCode()) % Colors.Length;
        return Avalonia.Media.Color.Parse(Colors[index]);
    }
}