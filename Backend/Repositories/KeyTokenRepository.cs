using Microsoft.EntityFrameworkCore;
using Backend.Models;
using MySqlConnector;

namespace Backend.Repositories
{
    public class KeyTokenRepository : IKeyTokenRepository
    {
        private readonly DBAppContext _context;

        public async Task<KeyToken?> GetById(string user_id)
        {
            return await _context.KeyToken.FindAsync(user_id);
        }

        public KeyTokenRepository(DBAppContext context)
        {
            _context = context;
        }

        public async Task<KeyToken> CreateKeyToken(string user_id, string publicKey, string privateKey, string refresh_token)
        {
            string sql = "CALL CreateKeyToken(@UserId, @PublicKey, @PrivateKey, @RefreshToken)";
            IEnumerable<KeyToken> result = await _context.KeyToken.FromSqlRaw(sql,
                new MySqlParameter("@UserId", user_id),
                new MySqlParameter("@PublicKey", publicKey),
                new MySqlParameter("@PrivateKey", privateKey),
                new MySqlParameter("@RefreshToken", refresh_token)).ToListAsync();

            KeyToken? keyToken = result.FirstOrDefault();
            return keyToken;
        }

        public async Task<KeyToken> UpdateKeyToken(string user_id, string publicKey, string privateKey, string refresh_token)
        {
            string sql = "CALL UpdateKeyToken(@UserId, @PublicKey, @PrivateKey, @RefreshToken)";
            IEnumerable<KeyToken> result = await _context.KeyToken.FromSqlRaw(sql,
                new MySqlParameter("@UserId", user_id),
                new MySqlParameter("@PublicKey", publicKey),
                new MySqlParameter("@PrivateKey", privateKey),
                new MySqlParameter("@RefreshToken", refresh_token)).ToListAsync();

            KeyToken? keyToken = result.FirstOrDefault();
            return keyToken;
        }
        public async Task<string> RemoveKeyById(string user_id)
        {
            string sql = "CALL DeleteKeyToken(@UserId)";
            // IEnumerable<> result = await _context.KeyToken.FromSqlRaw(sql,
            //     new MySqlParameter("@UserId", user_id)).ToListAsync();
            // KeyToken? keyToken = result.FirstOrDefault();
            // return keyToken.user_id;

            // Assuming the stored procedure returns a single string value
            IEnumerable<KeyToken> result = await _context.KeyToken
                .FromSqlRaw(sql, new MySqlParameter("@UserId", user_id))
                .ToListAsync();

            // Get the first (and presumably only) item from the result
            string userId = result.FirstOrDefault().user_id;

            return userId;

            // IEnumerable<string> result = await _context.KeyToken
            //     .FromSqlRaw(sql, new MySqlParameter("@UserId", user_id))
            //     .Select(k => k.user_id)
            //     .ToListAsync();

            // Get the first (and presumably only) item from the result
            // string userId = result.FirstOrDefault();

            return userId;
        }

        public async Task<RefreshTokenUsed> AddRefreshTokenUsed(string user_id, string refresh_token)
        {
            string sql = "CALL AddTokenUsed(@UserId, @RefreshToken)";
            IEnumerable<RefreshTokenUsed> result = await _context.RefreshTokenUsed.FromSqlRaw(sql,
                new MySqlParameter("@UserId", user_id),
                new MySqlParameter("@RefreshToken", refresh_token)).ToListAsync();
            // throw new Exception(result.FirstOrDefault().refresh_token);
            RefreshTokenUsed? refreshTokenUsed = result.FirstOrDefault();
            return refreshTokenUsed;
        }
    }
}

