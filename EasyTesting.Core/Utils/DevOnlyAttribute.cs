using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace EasyTesting.Core.Utils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DevOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var env = context.HttpContext.RequestServices.GetService(typeof(IHostEnvironment)) as IHostEnvironment;

            if (env == null || !env.IsDevelopment())
            {
                context.Result = new NotFoundResult();
            }

            base.OnActionExecuting(context);
        }
    }

}
