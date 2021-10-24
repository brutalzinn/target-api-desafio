namespace api_target_desafio.Models.Errors
{
    public class CepModelError
    {
        public CepModelError(bool logradouro, bool bairro, bool uf, bool cidade)
        {
            Logradouro = logradouro;
            Bairro = bairro;
            Uf = uf;
            Cidade = cidade;
        }

        public  bool Logradouro { get; set;}

        public bool Bairro { get; set; }

        public bool Uf { get; set; }

        public bool Cidade { get; set; }

        public bool Validade()
        {
            return Logradouro && Bairro && Uf && Cidade;
        }
    }
}
