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
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                //sqlServiceClassException error
                    case SqlServiceException e:                    
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { error = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
