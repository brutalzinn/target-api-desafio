namespace api_target_desafio.Services
{
    public static class PessoaService
    {
        public static void RegisterPessoa(PessoaSqlConnector connector, PessoaModel pessoa)
        {


            PessoaConnector.Insert(pessoa);

        }
    }
}
