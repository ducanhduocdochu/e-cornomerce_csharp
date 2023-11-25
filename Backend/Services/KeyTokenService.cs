using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Backend.Services
{
    public class KeyTokenService : IKeyTokenService
    {
        public KeyTokenService()
        {
        }

        public void CreateTokenPair(string userId, string email, string publicKey, string privateKey, out string accessToken, out string refreshToken)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email)
            };

            var key_accessToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(publicKey.PadRight(32))); // Pad the key to ensure it is 256 bits
            var creds_accessToken = new SigningCredentials(key_accessToken, SecurityAlgorithms.HmacSha256);

            var _accessToken = new JwtSecurityToken(
                issuer: "yourIssuer",
                audience: "yourAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds_accessToken
            );

            var key_refreshToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey.PadRight(32))); // Pad the key to ensure it is 256 bits
            var creds_refreshToken = new SigningCredentials(key_refreshToken, SecurityAlgorithms.HmacSha256);

            var _refreshToken = new JwtSecurityToken(
                issuer: "yourIssuer",
                audience: "yourAudience",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds_refreshToken
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            accessToken = tokenHandler.WriteToken(_accessToken);
            refreshToken = tokenHandler.WriteToken(_refreshToken);
        }

        public bool VerifyToken(string token, string key, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = null;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidIssuer = "yourIssuer",
                ValidAudience = "yourAudience"
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DecodeToken(string token, string key, out string userIdClaim, out string emailClaim)
        {
            var _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key.PadRight(32))); // Pad the key to ensure it is 256 bits
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key,
                ValidIssuer = "yourIssuer",
                ValidAudience = "yourAudience"
            };
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
            userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            emailClaim = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
