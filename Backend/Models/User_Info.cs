using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("user_info")]
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        [ForeignKey("User")]
        [Column("user_id")]
        public string user_id { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(255, MinimumLength = 6,
            ErrorMessage = "Address must be between 6 and 254 characters")]
        [Column("address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Image is required")]
        [StringLength(255, MinimumLength = 6,
            ErrorMessage = "Image must be between 6 and 254 characters")]
        [Column("image")]
        public string image { get; set; }
        
        [Required(ErrorMessage = "Delivery Address is required")]
        [StringLength(255, MinimumLength = 6,
            ErrorMessage = "Delivery Address must be between 6 and 254 characters")]
        [Column("delivery_address")]
        public string delivery_address { get; set; }
        
        [Required(ErrorMessage = "Phone is required")]
        [StringLength(255, MinimumLength = 6,
            ErrorMessage = "Phone must be between 6 and 254 characters")]
        [Column("phone")]
        public string phone { get; set; }

        public virtual User User { get; set; }
    }

}

