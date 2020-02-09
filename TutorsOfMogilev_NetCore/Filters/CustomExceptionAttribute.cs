using Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace TutorsOfMogilev_NetCore.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _env;

        public CustomExceptionFilter(IHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            string exceptionStack = context.Exception.StackTrace;
            string exceptionMessage = context.Exception.Message;

            ExceptionHandler.HandleError(context.Exception, actionName);

            context.Result = new ContentResult
            {
                StatusCode = 500,
                Content = _env.IsDevelopment()
                    ? $"В методе {actionName} возникло исключение: \n {exceptionMessage} \n {exceptionStack}"
                    : $"{exceptionMessage}"
            };

            context.ExceptionHandled = true;
        }
    }
}