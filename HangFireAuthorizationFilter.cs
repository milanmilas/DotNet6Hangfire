using Hangfire.Dashboard;

public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

        // Allow all authenticated users to see the Dashboard (potentially dangerous).
        var IsAuthenticated = httpContext.User.Identity?.IsAuthenticated ?? false;
        var id = httpContext.User.Claims.Where(x => x.Type == "id").FirstOrDefault();
        return true;

    }
}