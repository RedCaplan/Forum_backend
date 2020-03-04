using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Filters.Attributes
{
    public class AuthorizeGroupAttribute : TypeFilterAttribute
    {
        public AuthorizeGroupAttribute(params string[] claim) : base(typeof(AuthorizeGroupFilter))
        {
            Arguments = new object[] { claim };
        }
    }
}
