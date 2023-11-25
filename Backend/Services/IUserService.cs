using System;
using Backend.Models;
using Backend.ViewModels;

namespace Backend.Services
{
    public interface IUserService
    {
        Task<User> Register(RegisterViewModelRequest payload);
        //jwt string
        Task<LoginViewModelResponse> Login(LoginViewModelRequest payload);
        Task<string> Logout(string user_id, string refresh_token);
    }
}

