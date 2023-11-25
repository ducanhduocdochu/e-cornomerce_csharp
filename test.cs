// LogoutMiddleware.cs
public class LogoutMiddleware
{
    private readonly RequestDelegate _next;

    public LogoutMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserService userService)
    {
        // Kiểm tra logic để xác định có sử dụng middleware cho logout hay không
        if (ShouldUseMiddlewareForLogout(context))
        {
            // Logic xử lý middleware (nếu cần)
            // ...

            // Chuyển điều khiển cho middleware tiếp theo trong pipeline
            await _next(context);
        }
        else
        {
            // Không sử dụng middleware, chuyển trực tiếp đến xử lý của controller
            await userService.Logout(context);
        }
    }

    private bool ShouldUseMiddlewareForLogout(HttpContext context)
    {
        // Thêm logic để xác định khi nào sử dụng middleware cho logout
        // Ví dụ: Kiểm tra xem request có phải là logout không
        return context.Request.Path.StartsWithSegments("/api/user/logout");
    }
}
