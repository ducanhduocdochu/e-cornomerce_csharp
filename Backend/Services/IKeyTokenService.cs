using System.Security.Claims;

namespace Backend.Services
{
    public interface IKeyTokenService
    {
        void CreateTokenPair(string userId, string email, string publicKey, string privateKey, out string accessToken, out string refreshToken);

        bool VerifyToken(string token, string key, out ClaimsPrincipal claimsPrincipal);

        void DecodeToken(string token, string key, out string userIdClaim, out string emailClaim);
    }
}
