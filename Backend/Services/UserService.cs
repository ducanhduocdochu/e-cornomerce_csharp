using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;
using Backend.ViewModels;
using Backend.Utils;
// using Backend.ViewModels;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IKeyTokenRepository _keyTokenRepository;
        private readonly IConfiguration _configuration;

        public UserService(IKeyTokenRepository keyTokenRepository,IUserRepository userRepository, IConfiguration configuration)
        {
            _keyTokenRepository = keyTokenRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginViewModelResponse> Login(LoginViewModelRequest payload) {
            // Kiểm tra tồn tại
            User? user = await _userRepository.GetByEmail(payload.email);
            if (user == null){
                throw new ArgumentException("Email does not exist");
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(payload.password, user.password);

            if (!isPasswordCorrect)
            {
                throw new ArgumentException("Authentication error");
            }

            // Generate Key
            string publicKey, privateKey;
            KeyGenerator.GenerateKeys(out publicKey, out privateKey);
            if (privateKey == null || publicKey == null){
                throw new ArgumentException("Authentication error");
            }

            // Create Token
            string access_token, refresh_token;
            KeyTokenService objKeyTokenService = new KeyTokenService();
            objKeyTokenService.CreateTokenPair(user.user_id, user.email, publicKey, privateKey, out access_token, out refresh_token);

            if (access_token == null || refresh_token == null){
                throw new ArgumentException("Authentication error");
            }
            KeyToken _keyToken = await _keyTokenRepository.GetById(user.user_id);
            if (_keyToken == null){
                KeyToken keyToken = await _keyTokenRepository.CreateKeyToken(user.user_id, publicKey, privateKey, refresh_token);
            }else {
                KeyToken keyToken = await _keyTokenRepository.UpdateKeyToken(user.user_id, publicKey, privateKey, refresh_token);
            }

            return new LoginViewModelResponse(user.user_id, user.username, access_token, refresh_token);
            
        }
        public async Task<User> Register(RegisterViewModelRequest payload)
        {

            // Kiểm tra tồn tại
            User? user = await _userRepository.GetByEmail(payload.email);
            if (user != null){
                throw new ArgumentException("Email already exist");
            }
            // Hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(payload.password, BCrypt.Net.BCrypt.GenerateSalt(Int32.Parse(_configuration["HassPassword:SecretKey"])));
            
            // Setup giá trị mặc định
            bool status = true;
            bool verify = false;
            bool isUser = true;
            bool isShop = false;
            bool isAdmin = false;

            // Create newUser in database
            User newUserInDatabase = await _userRepository.Create(payload.username, hashedPassword, payload.email, status, verify, isUser, isShop, isAdmin);
            if (newUserInDatabase == null){
                throw new ArgumentException("Create database fail");
            }
            return newUserInDatabase;
        }

        public async Task<string> Logout(string user_id, string refresh_token){
            RefreshTokenUsed refreshTokenUsed = await _keyTokenRepository.AddRefreshTokenUsed(user_id, refresh_token);
            if (refreshTokenUsed == null){
                throw new ArgumentException("Create database fail");
            }
            return await _keyTokenRepository.RemoveKeyById(user_id);
        }
    }
}

