using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("api_keys")]
    public class ApiKey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Key is required")]
        [StringLength(255, MinimumLength = 6,
            ErrorMessage = "Key must be between 6 and 100 characters")]
        [Column("key")]
        public string? Key { get; set; }

        public bool Status { get; set; }

        //navigation
        // public ICollection<WatchList>? WatchLists { get; set; }
    }
}

