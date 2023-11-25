using System;
using Backend.Models;

namespace Backend.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(string user_id);
        Task<User?> GetByUsername(string username);
        Task<User?> GetByEmail(string email);

        Task<UserInfo?> GetInfoById(string user_id);

        Task<User?> Create(string username, string hashedPassword, string email, bool status, bool verify, bool isUser,  bool isShop, bool isAdmin);
    }
}

