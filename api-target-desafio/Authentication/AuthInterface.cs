using System.Collections.Generic;

//THIS CLASS IS BASED ON https://mithunvp.com/write-custom-asp-net-core-middleware-web-api/
//THANKS FOR THIS AWESOME ARTICLE :)
// 21/10/2021
namespace api_target_desafio.Authentication
{
    public interface AuthInterface
    {
     
        bool CheckValidApiKey(string key);
      
   

    }
}
