using BackEnd.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Attributes
{
    public class StaffAuthorizeAttribute : TypeFilterAttribute  // ỦY QUYỀN CHO ATTRIBUTE MÀ KHÔNG CẦN PHẢI GỌI TRỰC TIẾP TỪ FILTER
    {
        public StaffAuthorizeAttribute() :
          base(typeof(StaffAuthorizerFilter))
        {
        }
    }
}
