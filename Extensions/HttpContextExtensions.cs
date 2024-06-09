using System;
using System.Security.Claims;
namespace BackEnd.Extensions
{
    public  static class HttpContextExtensions
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
                if (context.Items.TryGetValue("adminId", out var adminId))
                {
                    return (int?) adminId;
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
