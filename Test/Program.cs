using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TokenExample
{
    public class TokenService
    {
        private static string secretKey = "MIIBCgKCAQEAvp/OKFLfwIrCeiQX6wnqoquuip6TvRNU22CtBexJvu3+PMtey2jjWX1cWuhrITpDmOAxa2a8S3ry66vW9ykKmE/lP1gosenjXJ3BuXfMButhwxhrm0x2NZbp9N8VK5lekgvsLaES/13opk10ab6T3kTCisdCFiTqQaJcP+vfYNHvimmYUpQd7/H8hgn5WlIyUeVO0ncKtI8SAZJWgvZFlKYVHYjJzPk88DVkyllr+Jr3sAULkUiwcmMjfNlmcKUnPZ5ZtLq8fEk+VPz2N9G5hV2GR049oVr8ccmtqCf8EIBqZksG0Zi4MNVlTo3lXcWB/ATEjDDGpmLRi5j22javLQIDAQAB"; // Replace with your actual secret key

        public static string GenerateToken(string userId, string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey.PadRight(32))); // Pad the key to ensure it is 256 bits
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourIssuer",
                audience: "yourAudience",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        public static bool TryValidateAndDecryptToken(string token, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = null;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
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

        public static void DecodeToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey.PadRight(32))); // Pad the key to ensure it is 256 bits
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidIssuer = "yourIssuer",
                ValidAudience = "yourAudience"
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
                var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var emailClaim = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;

                Console.WriteLine($"Decoded UserId: {userIdClaim}");
                Console.WriteLine($"Decoded Email: {emailClaim}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token decoding failed: {ex.Message}");
            }
        }

        public static void Main()
        {
            // Test GenerateToken
            var userId = "c9b81213-736d-4a24-b23c-a5aded9833da";
            var email = "tducanh263@gmail.com";
            var generatedToken = GenerateToken(userId, email);
            Console.WriteLine($"Generated Token: {generatedToken}");

            // Test TryValidateAndDecryptToken
            if (TryValidateAndDecryptToken(generatedToken, out var claimsPrincipal))
            {
                Console.WriteLine("Token is valid.");
                foreach (var claim in claimsPrincipal.Claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }
            }
            else
            {
                Console.WriteLine("Token is invalid.");
            }

            // Decode the token and retrieve UserId and Email
            DecodeToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImM5YjgxMjEzLTczNmQtNGEyNC1iMjNjLWE1YWRlZDk4MzNkYSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InRkdWNhbmgyNjNAZ21haWwuY29tIiwiZXhwIjoxNzAwOTA1NzUzLCJpc3MiOiJ5b3VySXNzdWVyIiwiYXVkIjoieW91ckF1ZGllbmNlIn0.7_see16OyHigabxUuLTRlH3WcVLP1S0cEI2J8n4ngH0");
        }
    }
}
