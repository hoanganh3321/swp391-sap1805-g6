using BackEnd.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Attributes
{
    public class JwtAuthorizationAttribute : TypeFilterAttribute  // ỦY QUYỀN CHO ATTRIBUTE MÀ KHÔNG CẦN PHẢI GỌI TRỰC TIẾP TỪ FILTER
    {

        public JwtAuthorizationAttribute() : 
            base(typeof(JwtAuthorizationFilter))
        {
        }
    }
}
