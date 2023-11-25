using System.Security.Claims;
using Backend.Models;
using Backend.Repositories;
using Backend.Services;

namespace Backend.Controllers.Middleware
{
    public class Authentication
    {
        private readonly RequestDelegate _next;

        public Authentication(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                // Lấy dịch vụ IKeyTokenRepository từ scope
                var _keyTokenRepository = scope.ServiceProvider.GetRequiredService<IKeyTokenRepository>();
                var _keyTokenService = scope.ServiceProvider.GetRequiredService<IKeyTokenService>();

                // Logic xử lý trước khi request đến controller hoặc endpoint
                string user_id = context.Request.Headers["client_id"];
                if (user_id == null) throw new ArgumentException("Invalid request");

                KeyToken keyStore = await _keyTokenRepository.GetById(user_id);

                if (keyStore == null)
                {
                    throw new ArgumentException("Not Found KeyStore");
                }

                string refreshToken = context.Request.Headers["refresh_token"];
                if (refreshToken != null)
                {
           
                    if (!_keyTokenService.VerifyToken(refreshToken, keyStore.private_key, out ClaimsPrincipal decodeUser))
                    {
                        throw new ArgumentException("Token is invalid");
                    }
                    _keyTokenService.DecodeToken(refreshToken, keyStore.private_key, out string userIdClaim, out string emailClaim);
                    if (user_id != userIdClaim)
                    {
                        throw new ArgumentException("Invalid user_id");
                    }
                    context.Items["keyStore"] = keyStore;
                    context.Items["user"] = decodeUser;
                    context.Items["refreshToken"] = refreshToken;
                    await _next(context);
                }

                string accessToken = context.Request.Headers["access_token"];
                if (accessToken == null)
                {
                    throw new ArgumentException("Invalid Request");
                }

                if (!_keyTokenService.VerifyToken(accessToken, keyStore.public_key, out ClaimsPrincipal _decodeUser))
                {
                    throw new ArgumentException("Token is invalid");
                }
                _keyTokenService.DecodeToken(accessToken, keyStore.public_key, out string _userIdClaim, out string _emailClaim);
                if (user_id != _userIdClaim)
                {
                    throw new ArgumentException("Invalid user_id");
                }
                context.Items["user_id"] = user_id;
                context.Items["refresh_token"] = keyStore.refresh_token;
                await _next(context);
            }
        }
    }
}