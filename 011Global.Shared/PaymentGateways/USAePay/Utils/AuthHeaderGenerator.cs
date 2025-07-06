using System.Security.Cryptography;
using System.Text;

namespace _011Global.Shared.PaymentGateways.USAePay.Utils;

public static class AuthHeaderGenerator
{
    public static string Generate(string seed, string apikey, string apipin)
    {
        var prehash = apikey + seed + apipin;
        var sha256Hash = ComputeSha256Hash(prehash);
        var apihash = $"s2/{seed}/{sha256Hash}";
        var authKey = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apikey}:{apihash}"));

        return authKey;
    }
    
    private static string ComputeSha256Hash(string rawData)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }
}