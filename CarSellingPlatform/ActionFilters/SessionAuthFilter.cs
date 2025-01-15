using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CarSellingPlatform.ActionFilters
{
    public class SessionAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("loggedUserId") == null)
                context.Result =
                    new RedirectToActionResult("Login", "Home", null);
        }
    }
}
