using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Filters.Attributes
{
    public class AuthorizeGroupAttribute : TypeFilterAttribute
    {
        public AuthorizeGroupAttribute(params string[] claim) : base(typeof(AuthorizeGroupFilter))
        {
            Arguments = new object[] { claim };
        }
    }
}
