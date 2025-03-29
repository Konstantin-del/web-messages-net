using Microsoft.AspNetCore.Authorization;
using Messages.Web.Models;

namespace Messages.Web.utils
{
    public class CastomAuthorizeAttribute : AuthorizeAttribute
    {
        public CastomAuthorizeAttribute(UserRole[] roles)
        {
            Roles = string.Join(",", roles.Select(n => (int)n).ToArray());
        }
    }
}
