using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        [Column("user_id")]
        public string user_id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(255, MinimumLength = 6,
            ErrorMessage = "Username must be between 6 and 24 characters")]
        [Column("username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")] 
        [StringLength(255, MinimumLength = 6,
            ErrorMessage = "Email must be between 6 and 254 characters")]
        [Column("email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 24 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,24}$", ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [Column("password")]
        public string password { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Column("status")]
        public bool status { get; set; }

        [Required(ErrorMessage = "Verify is required")]
        [Column("verify")]
        public bool verify { get; set; }

        [Required(ErrorMessage = "IsUser is required")]
        [Column("is_user")]
        public bool is_user { get; set; }

        [Required(ErrorMessage = "IsShop is required")]
        [Column("is_shop")]
        public bool is_shop { get; set; }

        [Required(ErrorMessage = "IsAdmin is required")]
        [Column("is_admin")]
        public bool is_admin { get; set; }

        // Foreign Key
        public virtual KeyToken KeyToken { get; set; }
        public virtual UserInfo UserInfo { get; set; }
         public virtual List<RefreshTokenUsed> RefreshTokensUsed { get; set; }
    }
}

