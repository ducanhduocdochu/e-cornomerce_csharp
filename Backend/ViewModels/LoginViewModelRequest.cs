using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class LoginViewModelRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")] 
        [StringLength(254, MinimumLength = 6,
            ErrorMessage = "Email must be between 6 and 254 characters")]
        public string? email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(24, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 24 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,24}$", ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string? password { get; set; }
    }
}

