using Backend.Controllers.Middleware;
namespace Backend.Controllers.Filter
{
    public class AuthenticationFilter
    {
        public void Configure(IApplicationBuilder appBuilder)
        {
            // Note the AuthencitaionMiddleware here is your Basic Authentication Middleware,
            // not the middleware from the Microsoft.AspNetCore.Authentication;
            appBuilder.UseMiddleware<Authentication>();
        }
    }
}