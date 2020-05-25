using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public class UserModel
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Name cannot exceed 15 charachters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Password cannot exceed 50 charachters")]
        public string Password { get; set; }
    }
}
