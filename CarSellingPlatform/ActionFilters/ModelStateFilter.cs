using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CarSellingPlatform.ActionFilters
{
    public class ModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                foreach (var error in context.ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                var controller = context.Controller as Controller;
                if (controller != null)
                {
                    context.Result = controller.View(context.ActionArguments.Values.FirstOrDefault());
                }
            }
        }
    }
}
