// using System;
// using System.Security.Cryptography;

// namespace Backend.Utils
// {
//     public class TokenGenerator
//     {
//         public static void GenerateKeys(string user_id, string email, string privateKey, string publicKey, out string access_token, out string refresh_token)
//         {
//             var claims = new List<Claim>
//         {
//             new Claim(ClaimTypes.NameIdentifier, userId),
//             new Claim(ClaimTypes.Email, email)
//         };

//         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
//         var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

//         var token = new JwtSecurityToken(
//             issuer: "yourIssuer", // Replace with your issuer
//             audience: "yourAudience", // Replace with your audience
//             claims: claims,
//             expires: DateTime.Now.AddDays(1), // Set token expiration as needed
//             signingCredentials: creds);

//         return new JwtSecurityTokenHandler().WriteToken(token);
//             access_token = ;
//             refresh_token = ;
//         }
//     }
// }