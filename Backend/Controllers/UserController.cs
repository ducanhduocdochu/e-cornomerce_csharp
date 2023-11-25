using System;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;
using Backend.ViewModels;
using Backend.Controllers.Filter;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        //https://localhost:port/api/user/register
        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterViewModelRequest payload)
        {
            try
            {
                User response = await _userService.Register(payload);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        //https://localhost:port/api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModelRequest payload)
        {
            try
            {
                LoginViewModelResponse response = await _userService.Login(payload);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        //https://localhost:port/api/user/logout
        [HttpPost("logout")]
        [MiddlewareFilter(typeof(AuthenticationFilter))]
        public async Task<IActionResult> Logout()
        {
            try
            {
                // KeyToken keyStore = (KeyToken)HttpContext.Items["keyStore"];

                // string response = await _userService.Logout(keyStore.user_id, keyStore.refresh_token);
                // return Ok(response);

                // Lấy thông tin từ Items
                string userIdObj = (string)HttpContext.Items["user_id"];
                string refreshTokenObj =  (string)HttpContext.Items["refresh_token"];

                string response = await _userService.Logout(userIdObj, refreshTokenObj);
                return Ok(response);
 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
