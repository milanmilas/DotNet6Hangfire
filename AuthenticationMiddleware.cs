using System.Security.Claims;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var claimsIdentity = new ClaimsIdentity("CI_CUSTOM");
        claimsIdentity.AddClaim(new Claim("id", "milan.milas"));
        context.User.AddIdentity(claimsIdentity);
        await _next(context);
    }
}