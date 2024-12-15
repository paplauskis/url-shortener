using System.Text;

namespace url_shortener.Helpers;

public static class ShortUrlGenerator
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        
    public static string Generate(int entityCount)
    {
        var length = CalculateLength(entityCount);
        var random = new Random();
        var result = new StringBuilder(length);
        
        for (int i = 0; i < length; i++)
        { 
            result.Append(Chars[random.Next(Chars.Length)]);
        }
        
        return result.ToString();
    }

    private static int CalculateLength(int entityCount)
    {
        if (entityCount <= Math.Pow(Chars.Length, 2) / 2) return 2;

        if (entityCount <= Math.Pow(Chars.Length, 3) / 2) return 3;

        return 4;
    }
}