using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Core.Common
{
    public class PaginationRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
