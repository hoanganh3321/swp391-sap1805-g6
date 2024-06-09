using BackEnd.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Attributes
{
    public class JwtAuthorizationAttribute : TypeFilterAttribute
    {

        public JwtAuthorizationAttribute() : 
            base(typeof(JwtAuthorizationFilter))
        {
        }
    }
}
