using _011Global.Shared.Interfaces;
using _011Global.Shared.Patterns;
using _011Global.Shared.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _011Global.Shared.Filters
{
    public class AuthorizationFilterAttribute() : TypeFilterAttribute(typeof(AuthorizationFilter));

    public class AuthorizationFilter(ITokenService tokenService, AppSettingsManagerBase appSettings, int[]? permissions = null) : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                context.Result = new UnauthorizedObjectResult(Result.Failure("Missing tokens"));
                return;
            }

            try
            {
                var result = tokenService.ValidateToken(token, appSettings.AccessTokenSecretKey);
                
                if (!result.IsSuccess)
                {
                    context.Result = new UnauthorizedObjectResult(Result.Failure(result.ErrorMessage));
                    return;
                }

                context.HttpContext.User = result.Value;
            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
