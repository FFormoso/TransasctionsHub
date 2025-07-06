using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _011Global.Shared.Interfaces;
using _011Global.Shared.Patterns;
using _011Global.Shared.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace _011Global.Shared.Services;

public class TokenService : ITokenService
{
    public Result<ClaimsPrincipal> ValidateToken(string token, string secretKey)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key
            }, out SecurityToken validatedToken);

            return Result<ClaimsPrincipal>.Success(principal);
        }
        catch (SecurityTokenExpiredException)
        {
            return Result<ClaimsPrincipal>.Failure("expired token");
        }
        catch
        {
            return Result<ClaimsPrincipal>.Failure("invalid token");
        }
    }
}