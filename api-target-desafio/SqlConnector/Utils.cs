﻿using api_target_desafio.Models;
using api_target_desafio.Models.Plans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace api_target_desafio.SqlConnector
{
    public  static class Utils
    {
        private static Dictionary<string, string> tables = new Dictionary<string, string>()
            {
                {"EnderecoModel", "ende.Id,Logradouro,Bairro,Cidade,UF,CEP,Complemento" },
                {"FinanceiroModel", "fina.Id,RendaMensal" }
            };
        public enum QueryBuilderEnum
        {
            UPDATE = 0,
            SELECT = 1,
            INSERT = 2,
            SELECT_JOIN = 3
        }

       

        public static string QueryBuilder(QueryBuilderEnum type, string ModelName, Dictionary<string, string> columns, string select = null, string condition = null)  
        {
            StringBuilder SQL_BUILDER = new StringBuilder();

            switch (type) {
// QUERY UPDATE
                case QueryBuilderEnum.UPDATE:
                    SQL_BUILDER.Append($"UPDATE {ModelName} SET ");
                    foreach (var item in columns)
                    {
                        SQL_BUILDER.Append($"{item.Key} = '{item.Value}',");
                    }
                    SQL_BUILDER.Remove(SQL_BUILDER.Length - 1, 1);
                    if (condition != null)
                    {
                        SQL_BUILDER.Append($" {condition}");
                    }
                    break;
//  QUERY FOR INSERT
                case QueryBuilderEnum.INSERT:
                    SQL_BUILDER.Append($"INSERT INTO {ModelName} (");
                    foreach (var item in columns)
                    {
                        SQL_BUILDER.Append($"{item.Key},");
                    }
                    SQL_BUILDER.Remove(SQL_BUILDER.Length - 1, 1);
                    SQL_BUILDER.Append(") VALUES (");
                    foreach (var item in columns)
                    {
                        SQL_BUILDER.Append($"{item.Value},");
                    }
                    SQL_BUILDER.Append(")");
                    break;
// QUERY FOR SELECT
                case QueryBuilderEnum.SELECT:
                    SQL_BUILDER.Append($"SELECT ");
                    foreach (var item in columns)
                    {
                        SQL_BUILDER.Append($"{item.Key},");
                    }
                    SQL_BUILDER.Remove(SQL_BUILDER.Length - 1, 1);
                    SQL_BUILDER.Append($" FROM {ModelName}");
                    if (condition != null)
                    {
                        SQL_BUILDER.Append($" {condition}");
                    }
                    break;
// QUERY SELECT JOIN


                case QueryBuilderEnum.SELECT_JOIN:
                    StringBuilder _SQL_NAMES = new StringBuilder();
                    string uniqueModel = ModelName.ToLower().Substring(0, 3);
                    _SQL_NAMES.Append($"{select},");
                    foreach (var item in columns)
                    {
                        string name = item.Key.ToLower().Substring(0, 5);
                        SQL_BUILDER.Append($" INNER JOIN {item.Key} AS {name} ON {name}.Id = {uniqueModel}.{item.Key}_Id");
                        _SQL_NAMES.Append($"{name}.Id, {item.Value},");
                    }
                    _SQL_NAMES.Remove(_SQL_NAMES.Length - 1, 1);             
                    string query = $"{_SQL_NAMES} FROM {ModelName} AS {uniqueModel} {SQL_BUILDER}";
                    SQL_BUILDER.Clear();
                    SQL_BUILDER.Append(query);
                    if (condition != null)
                    {
                        SQL_BUILDER.Append($" {condition}");
                    }
                    break;
            }
            Debug.WriteLine($"BUILDERJOIN {SQL_BUILDER}");

            return SQL_BUILDER.ToString();
        }
        public static async Task<object> ReadCustom(SqlConnection Connection, string Condition, string ModelName)
        {

            StringBuilder _SQL_JOIN = new StringBuilder();
            StringBuilder _SQL_NAMES = new StringBuilder();
            _SQL_NAMES.Append("SELECT pes.id, NomeCompleto, CPF, DataNascimento,");
            foreach (var item in tables)
            {
                string name = item.Key.ToLower().Substring(0, 4);
                _SQL_JOIN.Append($"INNER JOIN {item.Key} AS {name} ON {name}.Id = pes.{item.Key}_Id ");
                _SQL_NAMES.Append($"{item.Value},");
            }
            _SQL_NAMES.Remove(_SQL_NAMES.Length - 1, 1);
            List<object> _List = new List<object>();
            string commandText = $"{_SQL_NAMES} FROM {ModelName} AS pes " + _SQL_JOIN + " " + Condition;
            await Connection.OpenAsync();
            ClienteModel model;
            using (SqlCommand command = new SqlCommand(commandText, Connection))
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


                }
            }
            return _List;

        }
        public static async Task<bool> TableExists(SqlConnection Connection,string ModelName)
        {
            string query = $"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '@ModelName') SELECT 1 ELSE SELECT 0";
            try
            {
                await Connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return Convert.ToBoolean(reader.GetInt32(0));
                        }
                    }
                }
                await Connection.CloseAsync();

                return false;
            }
            catch (AggregateException)
            {
                await Connection.CloseAsync();

                return false;
            }

        }

        public static bool isOpen(SqlConnection Connection)
        {
            return (Connection.State == System.Data.ConnectionState.Open);
        }
        public static async Task<VipModel> SelectAnyVipPlan(SqlConnection Connection)
        {
            try
            {

            string query = $"SELECT TOP 1 Id, Name, Preco, Descricao FROM VipModel ORDER BY NEWID()";
           
                VipModel vipModel = null;
                using (var con = new SqlConnection(Connection.ConnectionString))
                {
                    await con.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, con))
                    {

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                vipModel = new VipModel(reader.GetString(1), reader.GetDecimal(2));

                                vipModel.Id = reader.GetInt32(0);
                                vipModel.Descricao = reader.GetString(3);
                            }
                        }
                    }
                }

                return vipModel;
            }
            catch (AggregateException)
            {
              

                return null;
            }

        }
        public static async Task<object> Count(SqlConnection Connection, string Condition, string ModelName)
        {
            string query = $"SELECT Count(*) as count FROM {ModelName} {Condition}";
        
            try
            {
                using (var con = new SqlConnection(Connection.ConnectionString))
                {
                    await con.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, con))
                    {

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return reader.GetInt32(0);
                            }
                        }

                    }
                }
                return 0;
            }
            catch (AggregateException)
            {

                Debug.WriteLine("Error related.");
                return 0;
            }

        }

    }
}
