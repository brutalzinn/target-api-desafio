//THIS CLASS IS BASED ON https://mithunvp.com/write-custom-asp-net-core-middleware-web-api/
//THANKS FOR THIS AWESOME ARTICLE :)
// robertocpaes-21/10/2021

using System.Collections.Generic;

namespace api_target_desafio.Authentication
{
    public class AuthSchema 
    {
   
        public AuthSchema()
        {
        }

        public  bool CheckValidApiKey(string key)
        {
            var KeyList = new List<string>
            {
                "28236d8ec201df516d0f6472d516d72d",
                "38236d8ec201df516d0f6472d516d72c",
                "48236d8ec201df516d0f6472d516d72b"
            };
            if (KeyList.Contains(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }







    }
}
