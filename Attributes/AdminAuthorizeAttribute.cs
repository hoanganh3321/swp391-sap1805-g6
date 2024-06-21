using BackEnd.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Attributes
{
    public class AdminAuthorizeAttribute : TypeFilterAttribute // ỦY QUYỀN CHO ATTRIBUTE MÀ KHÔNG CẦN PHẢI GỌI TRỰC TIẾP TỪ FILTER
    {
        public AdminAuthorizeAttribute() :
            base(typeof(AdminAuthorizationFilter))
        {
        }
    }
}
