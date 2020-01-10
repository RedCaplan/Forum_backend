using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Forum.Model.EFClasses;

namespace Forum.DTO
{
    public class UserAccountDTO
    {
        #region Properties

        [Required]
        public string ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public byte[] Avatar { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public List<UserGroup> UserGroups { get; set; }

        #endregion
    }
}
