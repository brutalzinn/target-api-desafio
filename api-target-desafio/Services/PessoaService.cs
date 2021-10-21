using api_target_desafio.SqlConnector.Connectors;
using System.Collections.Generic;

namespace api_target_desafio.Services
{
    public static class PessoaService
    {

        public static void RegisterPessoa(PessoaSqlConnector connector, PessoaModel pessoa)
        {
            connector.Insert(pessoa);
        }
        public static List<PessoaModel> GetPessoa(PessoaSqlConnector connector, int? id)
        {
            return connector.Read(id);
        }
    }
}
