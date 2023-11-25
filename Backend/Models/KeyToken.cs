using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("key_tokens")]
    public class KeyToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        [ForeignKey("User")]
        [Column("user_id")]
        public string user_id { get; set; }

        [Required(ErrorMessage = "Public Key is required")]
        [StringLength(1024, MinimumLength = 6,
            ErrorMessage = "Public Key must be between 6 and 254 characters")]
        [Column("public_key")]
        public string public_key { get; set; }

        [Required(ErrorMessage = "Private key is required")]
        [StringLength(2048, MinimumLength = 6,
            ErrorMessage = "Private key must be between 6 and 254 characters")]
        [Column("private_key")]
        public string private_key { get; set; }

        [Required(ErrorMessage = "RefreshToken is required")]
        [StringLength(1024, MinimumLength = 6,
            ErrorMessage = "RefreshToken must be between 6 and 254 characters")]
        [Column("refresh_token")]
        public string refresh_token { get; set; }

        // Navigation property for the associated User
        public virtual User User { get; set; }
    }

}

