﻿using api_target_desafio.Responses;
using api_target_desafio.SqlConnector.Connectors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_target_desafio.Services
{
    public static class PessoaService
    {

        public static PessoaResponse RegisterPessoa(PessoaSqlConnector connector, PessoaModel pessoa)
        {
            bool QueryResult =  Task.Run(()=>connector.Insert(pessoa)).Result;
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
            return Task.Run(() => connector.Read(id)).Result;
        }

        public static object GetPessoaRelation(PessoaSqlConnector connector,int? id)
        {
            Dictionary<string, string> tables = new Dictionary<string, string>();

            tables.Add("FinanceiroModel", "ende.Id,Logradouro,Bairro,Cidade,UF,CEP,Complemento");
            tables.Add("EnderecoModel", "RendaMensal");
            return Task.Run(()=>connector.ReadRelation(tables,id)).Result;
        }

        public static object CompareDates(PessoaSqlConnector connector, DateTime start,DateTime end)
        {
            Dictionary<string, string> tables = new Dictionary<string, string>();

            tables.Add("FinanceiroModel", "ende.Id,Logradouro,Bairro,Cidade,UF,CEP,Complemento");
            tables.Add("EnderecoModel", "RendaMensal");
            return Task.Run(() => connector.RangeDateTime(tables, start,end)).Result;
        }
    }
}
