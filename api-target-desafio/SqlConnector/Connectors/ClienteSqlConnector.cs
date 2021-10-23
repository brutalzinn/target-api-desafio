﻿using api_target_desafio.Config;
using api_target_desafio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace api_target_desafio.SqlConnector.Connectors
{
    //WE NEED REFACTOR ALL QUERY BUILDERS TWO DAYS BEFORE DISPATCH THE CHALLENGER
    public class ClienteSqlConnector : SqlBase
    {

        public ClienteSqlConnector()
        {

        }
        public ClienteSqlConnector(string conn)
        {
            Config(conn);
        }
 
        public override async Task<object> Read(int? id)
        {
            List<ClienteModel> _List = new List<ClienteModel>();
            string isWhere = id != null ? " WHERE Id=@ID" : "";
            string commandText = $"SELECT Id, NomeCompleto, CPF, DataNascimento, EnderecoModel_Id, FinanceiroModel_Id FROM ClienteModel" + isWhere;
            await Connection.OpenAsync();
            using (SqlCommand command = new SqlCommand(commandText, Connection))
            {
                if (id != null)
                {
                    command.Parameters.Add(new SqlParameter($"@ID", id));
                }
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    ClienteModel model;

                    if (id != null)
                    {
                        await reader.ReadAsync();
                        model = new ClienteModel(reader.GetInt32(0), reader.GetString(1),
                            reader.GetString(2), reader.GetDateTime(3),
                            new Models.EnderecoModel(), new Models.FinanceiroModel());

                        return model;
                    }
                    while (await reader.ReadAsync())
                    {
                        model = new ClienteModel(reader.GetInt32(0), reader.GetString(1),
                            reader.GetString(2), reader.GetDateTime(3),
                            new Models.EnderecoModel(), new Models.FinanceiroModel());
                        _List.Add(model);
                    }
                }
            }
            return _List;

        }
        public override async Task<bool> Update(object body, int id)
        {

            Dictionary<string, string> tables = new Dictionary<string, string>()
            {
                {"EnderecoModel", "Logradouro,Bairro,Cidade,UF,CEP,Complemento" },
                {"FinanceiroModel", "RendaMensal" }
            };
            try
            {
                ClienteModel pessoa = (ClienteModel)await ReadRelation(tables, id);
                if (pessoa == null)
                {
                    return false;
                }
               
               // FinanceiroSqlConnector Financeiro = new FinanceiroSqlConnector(sConnection);
                EnderecoSqlConnector Endereco = new EnderecoSqlConnector(sConnection);

                if (body is ClienteModel pessoaInstance)
                {
                   // await Financeiro.Update(pessoaInstance.Financeiro, pessoa.Financeiro.Id);
                    await Endereco.Update(pessoaInstance.Endereco, pessoa.Endereco.Id);

                    Dictionary<string,string> dic = new Dictionary<string,string>();
               
                    dic.Add("NomeCompleto", pessoaInstance.NomeCompleto);
                    if (pessoaInstance.Vip != null)
                    {
                        dic.Add("VipModel_Id", pessoaInstance.Vip.Id.ToString());
                    }
                    dic.Add("CPF", pessoaInstance.CPF);
                    dic.Add("DataNascimento", pessoaInstance.DataNascimento.ToString("yyyy-MM-dd"));

                    string query = Utils.QueryBuilder(Utils.QueryBuilderEnum.UPDATE, "ClienteModel", dic, $"WHERE ID = '{pessoa.Id}'");


                    SqlCommand command = new SqlCommand(query, Connection);
                    await Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await Connection.CloseAsync();

                }
                return true;

            }
            catch (AggregateException e)
            {
                return false;
            }
        }
        public override async Task<bool> Insert(object model)
        {
            try
            {
                if (model is ClienteModel pessoaInstance)
                {
                    int enderecoModel = 0;
                    int financeiroModel = 0;
                    if (pessoaInstance != null && pessoaInstance.Endereco != null && pessoaInstance.Financeiro != null)
                    {
                        EnderecoSqlConnector enderecoSqlConnector = new EnderecoSqlConnector(sConnection);
                        FinanceiroSqlConnector financeiroSqlConnector = new FinanceiroSqlConnector(sConnection);
                        enderecoModel = await enderecoSqlConnector.InsertRelation(pessoaInstance.Endereco);
                        financeiroModel = await financeiroSqlConnector.InsertRelation(pessoaInstance.Financeiro);
                    }

                    string commandText = "INSERT INTO ClienteModel (NomeCompleto,CPF,DataNascimento,EnderecoModel_Id,FinanceiroModel_Id) VALUES (@NOMECOMPLETO,@CPF,@DATANASCIMENTO,@ENDERECOMODEL,@FINANCEIROMODEL)";
                    SqlCommand command = new SqlCommand(commandText, Connection);
                    command.Parameters.Add(new SqlParameter($"@NOMECOMPLETO", pessoaInstance.NomeCompleto));
                    command.Parameters.Add(new SqlParameter($"@CPF", pessoaInstance.CPF));
                    command.Parameters.Add(new SqlParameter($"@DATANASCIMENTO", pessoaInstance.DataNascimento));
                    command.Parameters.Add(new SqlParameter($"@ENDERECOMODEL", enderecoModel));
                    command.Parameters.Add(new SqlParameter($"@FINANCEIROMODEL", financeiroModel));
                    await Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await Connection.CloseAsync();
                    return true;
                }
                return false;
            }
            catch (AggregateException)
            {
                return false;
            }
        }
        public override async Task<object> RangeDateTime(Dictionary<string, string> tables, DateTime start, DateTime end)
        {
            try
            {
                await Connection.OpenAsync();
                string where = $"WHERE cli.DateCadastro BETWEEN '{start.ToString("yyyy - MM - dd")}' AND '{end.ToString("yyyy - MM - dd")}'";
                string select = "SELECT cli.id, NomeCompleto, CPF, DataNascimento, DateCadastro";
                string query = Utils.QueryBuilder(Utils.QueryBuilderEnum.SELECT_JOIN, "ClienteModel", tables, select, where);
                List<object> _List = new List<object>();
                ClienteModel model;
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            model = new ClienteModel();

                            model.Id = reader.GetInt32(0);
                            model.NomeCompleto = reader.GetString(1);
                            model.CPF = reader.GetString(2);
                            model.DataNascimento = reader.GetDateTime(3);
                            model.DateCadastro = reader.GetDateTime(4);

                            model.Endereco = new EnderecoModel();

                            model.Endereco.Id = reader.GetInt32(5);
                            model.Endereco.Logradouro = reader.GetString(6);
                            model.Endereco.Bairro = reader.GetString(7);
                            model.Endereco.Cidade = reader.GetString(8);
                            model.Endereco.UF = reader.GetString(9);
                            model.Endereco.CEP = reader.GetString(10);
                            model.Endereco.Complemento = reader.GetString(11);
                            model.Financeiro = new FinanceiroModel(reader.GetInt32(12), reader.GetDecimal(13));
                            _List.Add(model);
                        }
                    }
                }
                await Connection.CloseAsync();
                return _List.Count != 0  ? _List : null;
            }
            catch (AggregateException)
            {
                return null;
            }

        }

        public override async Task<object> ReadRelation(Dictionary<string, string> tables, int? id)
        {
            try
            {
                List<object> _List = new List<object>();
                string isWhere = id != null ? "WHERE cli.Id=" + id : null;
                string query = Utils.QueryBuilder(Utils.QueryBuilderEnum.SELECT_JOIN, "ClienteModel", tables, "SELECT cli.Id, NomeCompleto, CPF, DataNascimento", isWhere);
                await Connection.OpenAsync();
                ClienteModel model = null;
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        switch (id)
                        {
                            case null:
                                while (await reader.ReadAsync())
                                {
                                    model = new ClienteModel();
                                    model.Id = reader.GetInt32(0);
                                    model.NomeCompleto = reader.GetString(1);
                                    model.CPF = reader.GetString(2);
                                    model.DataNascimento = reader.GetDateTime(3);

                                    model.Endereco = new EnderecoModel();

                                    model.Endereco.Id = reader.GetInt32(4);
                                    model.Endereco.Logradouro = reader.GetString(5);
                                    model.Endereco.Bairro = reader.GetString(6);
                                    model.Endereco.Cidade = reader.GetString(7);
                                    model.Endereco.UF = reader.GetString(8);
                                    model.Endereco.CEP = reader.GetString(9);
                                    model.Endereco.Complemento = reader.GetString(10);
                                    model.Financeiro = new FinanceiroModel(reader.GetInt32(11), reader.GetDecimal(12));
                                    _List.Add(model);
                                }

                                break;

                            case not null:
                                if (await reader.ReadAsync())
                                {

                                    model = new ClienteModel();

                                    model.Id = reader.GetInt32(0);
                                    model.NomeCompleto = reader.GetString(1);
                                    model.CPF = reader.GetString(2);
                                    model.DataNascimento = reader.GetDateTime(3);

                                    model.Endereco = new EnderecoModel();

                                    model.Endereco.Id = reader.GetInt32(4);
                                    model.Endereco.Logradouro = reader.GetString(5);
                                    model.Endereco.Bairro = reader.GetString(6);
                                    model.Endereco.Cidade = reader.GetString(7);
                                    model.Endereco.UF = reader.GetString(8);
                                    model.Endereco.CEP = reader.GetString(9);
                                    model.Endereco.Complemento = reader.GetString(10);
                                    model.Financeiro = new FinanceiroModel(reader.GetInt32(11), reader.GetDecimal(12));

                                }
                                await Connection.CloseAsync();

                                return model;

                        }
                    }
                }
                await Connection.CloseAsync();
                return _List.Count != 0 ? _List : null;
            }
            catch (AggregateException)
            {
                await Connection.CloseAsync();

                return null;
            }

            // throw new CustomException($"Client with id {id} doenst exit.");

        }
    }
}
