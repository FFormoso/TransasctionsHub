using System.Security.Claims;
using _011Global.Shared.Patterns;

namespace _011Global.Shared.Interfaces;

public interface ITokenService
{
    Result<ClaimsPrincipal> ValidateToken(string token, string secretKey);
}