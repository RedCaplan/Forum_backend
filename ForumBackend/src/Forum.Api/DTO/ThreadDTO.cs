using System;
using Forum.DAL.Models.Enums;

namespace Forum.Api.DTO
{
    public class ThreadDTO
    {
        #region Properties
        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public string LatinName { get; set; }

        public string CreatedBy { get; set; }

        public string CategoryName { get; set; }

        public Status Status { get; set; }

        #endregion
    }
}
