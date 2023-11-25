using System;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
// using Backend.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MySqlConnector;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBAppContext _context;
        private readonly IConfiguration _config;

        public UserRepository(DBAppContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<User?> GetById(string user_id)
        {
            return await _context.User.FindAsync(user_id);
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.username == username);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.email == email);
        }

         public async Task<UserInfo?> GetInfoById(string user_id)
        {
            return await _context.UserInfo.FindAsync(user_id);
        }

        public async Task<User?> Create(string username, string hashedPassword, string email, bool status, bool verify, bool isUser, bool isShop, bool isAdmin)
        {
            string user_id = Guid.NewGuid().ToString();
            string sql = "CALL CreateUser(@UserId,@UserName, @Email, @Password, @Status, @Verify, @IsUser, @IsShop, @IsAdmin)";
            IEnumerable<User> result = await _context.User.FromSqlRaw(sql,
                new MySqlParameter("@UserId", user_id),
                new MySqlParameter("@UserName", username),
                new MySqlParameter("@Email", email),
                new MySqlParameter("@Password", hashedPassword),
                new MySqlParameter("@Status", status),
                new MySqlParameter("@Verify", verify),
                new MySqlParameter("@IsUser", isUser),
                new MySqlParameter("@IsShop", isShop),
                new MySqlParameter("@IsAdmin", isAdmin)).ToListAsync();  

            User? user = result.FirstOrDefault();
            return user;
        }
    }

}

