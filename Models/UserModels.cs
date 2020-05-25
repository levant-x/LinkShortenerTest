using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public class UserBaseModel
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Name length cannot exceed 15 charachters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Password length cannot exceed 50 charachters")]
        public string Password { get; set; }
    }

    public class UserRegisterModel : UserBaseModel
    {
        [MaxLength(30, ErrorMessage = "Email length cannot exceed 30 charachters")]
        public string Email { get; set; }
    }
}
