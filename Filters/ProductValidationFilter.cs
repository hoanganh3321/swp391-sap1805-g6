namespace BackEnd.Filters
{
    using BackEnd.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;

    
    public class ProductValidationFilter : ActionFilterAttribute
    {
        //OnActionExecuting : được gọi trước khi hành động controller được thực hiện. 
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("product", out object productObj ))
            {
                var product = productObj as Product;

                if (product == null || !ValidateProductBeforeAdding(product))
                {
                    context.Result = new BadRequestObjectResult("Product data is not valid.");
                    return;
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("Product data is required.");
                return;
            }

            base.OnActionExecuting(context);
        }

        private bool ValidateProductBeforeAdding(Product product)
        {
            // Check if ProductName is not null or empty
            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                return false;
            }

            // Check if Price is greater than or equal to zero
            if (product.Price < 0)
            {
                return false;
            }

            // Check if Quantity is greater than or equal to zero
            if (product.Quantity < 0)
            {
                return false;
            }

            // Add more validation rules here as needed...

            return true;
        }
    }
}
