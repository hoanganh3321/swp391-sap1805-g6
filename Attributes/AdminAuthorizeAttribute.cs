using BackEnd.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Attributes
{
    public class AdminAuthorizeAttribute : TypeFilterAttribute
    {
        public AdminAuthorizeAttribute() :
            base(typeof(AdminAuthorizationFilter))
        {
        }
    }
}
