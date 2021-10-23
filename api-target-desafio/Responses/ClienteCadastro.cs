using System.Text.Json;

namespace api_target_desafio.Responses
{
    public class ClienteCadastro : ResponseBase
    {
        public ClienteCadastro(bool oferecerPlanoVip, bool cadastrado)
        {
            OferecerPlanoVip = oferecerPlanoVip;
            Cadastrado = cadastrado;
        }
        public ClienteCadastro()
        {

        }
        public bool OferecerPlanoVip { get; set; }
        public bool Cadastrado { get; set; }


        public bool Error { get => !Cadastrado; }

        public override string GetResponse()
        {
          return JsonSerializer.Serialize(new { Cadastrado, Error, OferecerPlanoVip });
        }
    }
}
