using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using news_forum.Model.EFClasses;

namespace news_forum.DTO
{
    public class UserAccountDTO
    {
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
    }
}
