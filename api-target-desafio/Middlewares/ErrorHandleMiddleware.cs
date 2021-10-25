using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace api_target_desafio.Config
{
    //this is a global middleware example to handle erros that i read on article https://jasonwatmore.com/post/2020/10/02/aspnet-core-31-global-error-handler-tutorial
    // thanks to Jason Watmore for this cool article.
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var Response = context.Response;
                List<string> ErrorList = new List<string>();
                Response.ContentType = "application/json";

                switch (error)
                {
                //sqlServiceClassException error
                    case SqlServiceException:                    
                        Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ModelValidatorException e:
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        ErrorList = e.Errors;
                        break;
                    default:
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                if(error is not ModelValidatorException && error is not SqlServiceException)
                {
                    ErrorList.Add(error?.Message);
                }

                var result = JsonSerializer.Serialize(new { Error = ErrorList });
                await Response.WriteAsync(result);
            }
        }
    }
}
