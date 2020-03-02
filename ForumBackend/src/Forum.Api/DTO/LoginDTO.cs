using System.ComponentModel.DataAnnotations;

namespace Forum.Web.DTO
{
    public class LoginDTO
    {
        #region Properties

        [Required(ErrorMessage = "Emailadress is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(10, ErrorMessage = "Password needs to be at least 10 characters")]
        public string Password { get; set; }

        #endregion
    }
}
