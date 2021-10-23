using System.Text.Json;

namespace api_target_desafio.Responses
{

    public class VipUpdate : ResponseBase
    {

        public VipUpdate(bool success, string _message)
        {
            Updated = success;
            CoolMessage = _message;
        }
        public VipUpdate()
        {

        }
        private string CoolMessage { get; set; }

        public string Message { get => Updated ? CoolMessage : "Ops... its ok. You cannot accept us vip plan :("; }
        public bool Updated { get; set; }

        public bool Error { get => !Updated; }

        public override string GetResponse()
        {
            return JsonSerializer.Serialize(new { Error, Updated, Message });
        }
    }



}
