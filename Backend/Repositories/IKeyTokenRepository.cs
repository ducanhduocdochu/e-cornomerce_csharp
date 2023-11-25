using System;
using Backend.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Backend.Repositories
{
    public interface IKeyTokenRepository
    {
        Task<KeyToken?> GetById(string user_id);
        Task<KeyToken> CreateKeyToken(string user_id, string publicKey, string privateKey, string refresh_token);
        Task<KeyToken> UpdateKeyToken(string user_id, string publicKey, string privateKey, string refresh_token);

        Task<string> RemoveKeyById(string user_id);

        Task<RefreshTokenUsed> AddRefreshTokenUsed(string user_id, string refresh_token);
    }
}

