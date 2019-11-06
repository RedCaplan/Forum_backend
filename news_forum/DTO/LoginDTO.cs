using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace news_forum.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Emailadress is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(10, ErrorMessage = "Password needs to be at least 10 characters")]
        public string Password { get; set; }
    }
}
