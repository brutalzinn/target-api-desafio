using System.Threading.Tasks;
using api_target_desafio.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

//THIS CLASS IS BASED ON https://mithunvp.com/write-custom-asp-net-core-middleware-web-api/
//THANKS FOR THIS AWESOME ARTICLE :)
// 21/10/2021
namespace api_target_desafio.Middlewares
{

    public class AuthMiddleware
    {
        private readonly RequestDelegate _request;

        public static AuthSchema AuthSchemaModel { get; set; } = new AuthSchema();
        public AuthMiddleware(RequestDelegate request)
        {
            _request = request;
        }
        public async Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            if (!context.Request.Headers.Keys.Contains("API-KEY"))
            {

                context.Response.StatusCode = 400; //Bad Request

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    error = "BAD REQUEST. PROVIDE API-KEY HEADER."
                }));
                return;
            }
            else
            {

                if (!AuthSchemaModel.CheckValidApiKey(context.Request.Headers["API-KEY"]))
                {
                    context.Response.StatusCode = 401; //UnAuthorized
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        error = "BAD REQUEST. API KEY INVALID"
                    }));


                    return;
                }
            }
            await _request.Invoke(context);
        }

    }
}
