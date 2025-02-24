using library.communication.Responses;
using library.exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace library.api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is LibraryException libraryException)
            {
                context.HttpContext.Response.StatusCode = (int)libraryException.GetStatusCode();

                context.Result = new ObjectResult(new ResponseErrorMessagesJson { Errors = libraryException.GetErrorMessages() });
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                context.Result = new ObjectResult(new ResponseErrorMessagesJson { Errors = ["Internal Server Error"] });
            }

        }
    }
}
