namespace BLH.ApproveIQ.API.Headers;

public class SelectedDistrictIdHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public SelectedDistrictIdHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Example: Log or inspect the headers
        var headers = context.Request.Headers;

        // Check if a specific header exists, e.g., Authorization header
        if (headers.ContainsKey("X-Selected-District-Id"))
        {
            var headerObj = headers["X-Selected-District-Id"];
            context.Items["SelectedDistrictId"] = headerObj.Count > 0 ? headerObj[0] : "";
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }
}
