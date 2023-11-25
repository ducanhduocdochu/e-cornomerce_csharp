using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("refresh_token_useds")]
    public class RefreshTokenUsed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        [ForeignKey("User")]
        [Column("user_id")]
        public string user_id { get; set; }

        [Required(ErrorMessage = "RefreshToken is required")]
        [StringLength(1024, MinimumLength = 6,
            ErrorMessage = "RefreshToken must be between 6 and 254 characters")]
        [Column("refresh_token")]
        public string refresh_token { get; set; }
        
        public virtual User User { get; set; }
    }

}

