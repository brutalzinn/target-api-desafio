namespace api_target_desafio.Responses
{
    public class PessoaResponse
    {
        public PessoaResponse(bool oferecerPlanoVip, bool cadastrado)
        {
            OferecerPlanoVip = oferecerPlanoVip;
            Cadastrado = cadastrado;
        }
        public PessoaResponse()
        {

        }
        public bool OferecerPlanoVip { get; set; }
        public bool Cadastrado { get; set; }

        public string Text { get; set; }

        public bool Error { get => !Cadastrado; }

    }
}
