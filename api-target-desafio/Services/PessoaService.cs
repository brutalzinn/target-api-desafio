using api_target_desafio.Config;
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
                object result = Task.Run(() => connector.ReadRelation(tables, id)).Result;
                if (result == null)
                {
                string error = id != null ? $"Cant find client with id {id}" : $"Cant find any clients.";
                    throw new SqlServiceException(System.Net.HttpStatusCode.NotFound, error);
                }
            return result;
        }

        public static object CompareDates(ClienteSqlConnector connector, DateTime start,DateTime end)
        {
            object result = Task.Run(() => connector.RangeDateTime(tables, start,end)).Result;

            if (result == null)
            {
                throw new SqlServiceException(System.Net.HttpStatusCode.NotFound, "Cant find any clients.");
            }
            return result;
        }

        public static object Update(ClienteSqlConnector connector, ClienteModel pessoa, int id)
        {
            bool result = Task.Run(() => connector.Update(pessoa,id)).Result;
            if (!result)
            {
                throw new SqlServiceException(System.Net.HttpStatusCode.NotFound, $"Client not found. Cant update client with id {id}.");
            }
            else
            {

            }
        }
    }
}
