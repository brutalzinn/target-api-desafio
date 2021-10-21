using api_target_desafio.Responses;
using api_target_desafio.SqlConnector.Connectors;
using System.Collections.Generic;

namespace api_target_desafio.Services
{
    public static class PessoaService
    {

        public static PessoaResponse RegisterPessoa(PessoaSqlConnector connector, PessoaModel pessoa)
        {
            bool QueryResult = connector.Insert(pessoa);
            PessoaResponse response = new PessoaResponse(false, true);
            if (!QueryResult)
            {
                response.Cadastrado = false;
                return response;
            }

            if(pessoa.Financeiro.RendaMensal >= 6000M)
            {
                response.OferecerPlanoVip = true;
            }
            return response;
        }
        public static object GetPessoa(PessoaSqlConnector connector, int? id)
        {
            return connector.Read(id);
        }

        public static object GetPessoaRelation(PessoaSqlConnector connector,int? id)
        {
            Dictionary<string, string> tables = new Dictionary<string, string>();

            tables.Add("FinanceiroModel", "ende.Id,Logradouro,Bairro,Cidade,UF,CEP,Complemento");
            tables.Add("EnderecoModel", "RendaMensal");
            return connector.ReadRelation(tables,id);
        }
    }
}
