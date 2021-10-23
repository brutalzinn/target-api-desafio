using api_target_desafio.Responses;
using api_target_desafio.SqlConnector.Connectors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_target_desafio.Services
{
    public static class PessoaService
    {
      public static  Dictionary<string, string> tables = new Dictionary<string, string>()
      {
          {"EnderecoModel", "ende.Id,Logradouro,Bairro,Cidade,UF,CEP,Complemento" },
          {"FinanceiroModel", "fina.Id,RendaMensal" }
      };

        public static PessoaResponse RegisterPessoa(ClienteSqlConnector connector, ClienteModel pessoa)
        {
            bool QueryResult =  Task.Run(()=>connector.Insert(pessoa)).Result;
            PessoaResponse response = new PessoaResponse(false, true);
            if (!QueryResult)
            {
                response.Cadastrado = false;
                return response;
            }

            if(pessoa.Financeiro.RendaMensal >= 6000)
            {
                response.OferecerPlanoVip = true;
            }
            return response;
        }
        public static object GetPessoa(ClienteSqlConnector connector, int? id)
        {
            return Task.Run(() => connector.Read(id)).Result;
        }

        public static object GetPessoaRelation(ClienteSqlConnector connector,int? id)
        {

            return Task.Run(()=>connector.ReadRelation(tables,id)).Result;
        }

        public static object CompareDates(ClienteSqlConnector connector, DateTime start,DateTime end)
        {     
            return Task.Run(() => connector.RangeDateTime(tables, start,end)).Result;
        }

        public static object Update(ClienteSqlConnector connector, ClienteModel pessoa, int id)
        {
            return Task.Run(() => connector.Update(pessoa,id)).Result;
        }
    }
}
