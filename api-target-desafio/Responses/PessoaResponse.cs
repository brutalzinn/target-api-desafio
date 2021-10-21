namespace api_target_desafio.Responses
{
    public class PessoaResponse
    {
        public PessoaResponse(bool oferecerPlanoVip, bool cadastrado)
        {
            OferecerPlanoVip = oferecerPlanoVip;
            Cadastrado = cadastrado;
        }

        public bool OferecerPlanoVip { get; set; }
        public bool Cadastrado { get; set; }

        public bool Error { get => !Cadastrado; }

    }
}
