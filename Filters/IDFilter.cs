using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using swp391_sap1805_g6.Reporitories;

namespace swp391_sap1805_g6.Filters
{
    public class IDProFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var proId = context.ActionArguments["id"] as int?;
            var war = context.ActionArguments["id"] as int?;
            if (proId.HasValue||war.HasValue)
            {
                if (proId.Value < 0 || war.Value < 0)
                {
                    context.ModelState.AddModelError("Id", " Id must >0");
                    var problemDetail = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };
                    context.Result = new BadRequestObjectResult(problemDetail);
                }
                else if (!ProductRepo.ProsExist(proId.Value))
                {
                    context.ModelState.AddModelError("Id", "Id dont exist");
                    var problemDetail = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound,
                    };
                    context.Result = new NotFoundObjectResult(problemDetail);
                }
            }
        }


    }
}
