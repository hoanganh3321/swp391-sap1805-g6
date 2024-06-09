using BackEnd.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Attributes
{
    public class StaffAuthorizeAttribute : TypeFilterAttribute
    {
        public StaffAuthorizeAttribute() :
          base(typeof(StaffAuthorizerFilter))
        {
        }
    }
}
