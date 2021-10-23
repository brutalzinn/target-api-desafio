using api_target_desafio.Models;
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
                return false;
            }
            catch (AggregateException)
            {
              
                return false;
            }

        }
        public static async Task<object> Count(SqlConnection Connection, string Condition, string ModelName)
        {
            string query = $"SELECT Count(*) as count FROM {ModelName} {Condition}";
            try
            {
                await Connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return reader.GetInt32(0);
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error related.");
                return 0;
            }

        }

    }
}
