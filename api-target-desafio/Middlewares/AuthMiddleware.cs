using System.Threading.Tasks;
using api_target_desafio.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;


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
            if (!context.Request.Headers.Keys.Contains("API-KEY"))
            {

                context.Response.StatusCode = 400; //Bad Request                
                await context.Response.WriteAsync("BAD REQUEST");
                return;
            }
            else
            {

                if (!AuthSchemaModel.CheckValidApiKey(context.Request.Headers["API-KEY"]))
                {
                    context.Response.StatusCode = 401; //UnAuthorized
                    await context.Response.WriteAsync("api key invalid");
                    return;
                }
            }
            await _request.Invoke(context);
        }

    }
}
