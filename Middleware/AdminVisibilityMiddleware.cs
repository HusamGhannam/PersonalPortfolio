using System.Security.Claims;

namespace PersonalPortfolio.Middleware;

/// <summary>
/// Hides the /Admin panel from non-admin users by returning 404 instead of
/// redirecting to login or showing a 403. The admin panel is invisible unless
/// you are an authenticated Admin.
/// </summary>
public class AdminVisibilityMiddleware
{
    private readonly RequestDelegate _next;

    public AdminVisibilityMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;

        if (path.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase))
        {
            var user = context.User;

            if (user.Identity?.IsAuthenticated == true && !user.IsInRole("Admin"))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }
        }

        await _next(context);
    }
}
