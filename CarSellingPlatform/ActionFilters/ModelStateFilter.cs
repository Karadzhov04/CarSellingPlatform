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
                    Console.WriteLine(error.ErrorMessage); // Или използвайте логиране
                }

                var controller = context.Controller as Controller;
                if (controller != null)
                {
                    // Ако контролерът е валиден, върни същата View с модела
                    context.Result = controller.View(context.ActionArguments.Values.FirstOrDefault());
                }
            }
        }
    }
}
