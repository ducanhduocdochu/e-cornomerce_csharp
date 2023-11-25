// MyMiddleware.cs

namespace Backend.Controllers.Middleware
{
    public class Permisson
    {
        private readonly RequestDelegate _next;

        public Permisson(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Logic xử lý trước khi request đến controller hoặc endpoint
            
            Console.WriteLine("Đức Anh đẹp trai");

            await _next(context);
        }
    }
}