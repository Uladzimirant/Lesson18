using Microsoft.AspNetCore.Mvc.Filters;
using Lesson18.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Lesson6.ProductsClassRealization;
using Lesson16.Controllers;

namespace Lesson18
{
    public class IncorrectOperationExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is IncorrectOperationException)
            {
                context.Result = ((context.Exception as IncorrectOperationException)?.Controller as ProductController)
                    ?.View("IncorrectOperation", context.Exception);
                context.ExceptionHandled = true;
            }
        }
    }
}
