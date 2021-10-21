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
        public static object GetPessoa(PessoaSqlConnector connector, int? id)
        {
            return connector.Read(id);
        }

        public static object GetPessoaRelation(PessoaSqlConnector connector, Dictionary<string, string> tables,int? id)
        {
            return connector.ReadRelation(tables,id);
        }
    }
}
