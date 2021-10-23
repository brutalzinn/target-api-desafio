using System.Text.Json;

namespace api_target_desafio.Responses
{
  
        public class ClienteUpdate : ResponseBase
        {
            public ClienteUpdate(bool success)
            {
                Updated = success;
            }
            public ClienteUpdate()
            {

            }

            public bool Updated { get; set; }

            public bool Error { get => !Updated; }

            public override string GetResponse()
            {
            return JsonSerializer.Serialize(new {  Error, Updated });
             }
    }
    


}
