using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
	public class LoginViewModelResponse
	{                 
        public string user_id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(24, MinimumLength = 6,
            ErrorMessage = "Username must be between 6 and 24 characters")]
        public string? username { get; set; }

        public string access_token { get; set; }

        [Required(ErrorMessage = "RefreshToken is required")]
        [StringLength(254, MinimumLength = 6,
            ErrorMessage = "RefreshToken must be between 6 and 254 characters")]
        public string refresh_token { get; set; }

        public LoginViewModelResponse(string userId, string userName, string accessToken, string refreshToken){
            user_id = userId;
            username = userName;
            access_token = accessToken;
            refresh_token = refreshToken;
        }
    }
}

