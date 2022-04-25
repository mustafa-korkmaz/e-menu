using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.Middlewares.Validations
{
    public class ModelStateValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;

            if (!modelState.IsValid)
            {
                var errorMessage = modelState.Values.First(p => p.ValidationState == ModelValidationState.Invalid)
                    .Errors.First().ErrorMessage;

                context.Result = new ContentResult
                {
                    Content = errorMessage,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            base.OnActionExecuting(context);
        }
    }
}
