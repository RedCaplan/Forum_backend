using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Web.DTO
{
    public class RegisterDTO:LoginDTO
    {
        #region Properties

        private DateTime _birthDay { get; set; }

        [Required(ErrorMessage = "Provide at least a username")]
        [MinLength(3, ErrorMessage = "The username has to be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "The username has to be maximum 20 characters long")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please provide a birthday")]
        [DataType(DataType.Date)]
        public DateTime BirthDay {
            get => _birthDay;
            set {
                if (value.Year < 1901)
                {
                    throw new Exception("Birthday can't be smaller then 1900");
                }

                if (value.CompareTo(DateTime.Now) >= 0)
                {
                    throw new Exception("Your birthday needs to be in the past.");
                }

                _birthDay = value;
            }
        }

        #endregion
    }
}
