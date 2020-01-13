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
