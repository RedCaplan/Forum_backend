using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Core.Model;
using Forum.Core.Model.Enums;

namespace Forum.Web.DTO
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
