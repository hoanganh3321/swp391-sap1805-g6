using System;
using System.Security.Claims;
namespace BackEnd.Extensions
{
    //REFERENCE NGUYEN DUC HOANG BKHN https://www.facebook.com/nguyen.duc.hoang.bk
    public static class HttpContextExtensions // phương thức mở rộng thêm chức năng mới vào các lớp hiện có mà không cần kế thừa 
                                               // có thể sử dụng nó ở bất kỳ đâu có HttpContext
    {
        public static int? GetCustomerId(this HttpContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }    
                if (context.Items.TryGetValue("customerId", out var customerId))
                {
                    return (int?)customerId;
                }
                return null;
            }

            public static int? GetAdminId(this HttpContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }
            if (context.Items.TryGetValue("AdminID", out var AdminID))
                {
                    return (int?)AdminID;
                }
                return null;
            }

            public static int? GetOrderId(this HttpContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                if (context.Items.TryGetValue("OrderID", out var orderId))
                {
                    return (int?)orderId;
                }

                return null;
            }

            public static int? GetStaffId(this HttpContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }
                if (context.Items.TryGetValue("StaffID", out var StaffID))
                {
                    return (int?)StaffID;
                }
                return null;
            }

    }
}
