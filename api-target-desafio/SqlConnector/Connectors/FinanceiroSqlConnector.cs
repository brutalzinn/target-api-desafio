using api_target_desafio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace api_target_desafio.SqlConnector.Connectors
{
    public class FinanceiroSqlConnector : SqlBase
    {
           
        public FinanceiroSqlConnector()
        {

        }
            public FinanceiroSqlConnector(string conn)
        {
            Config(conn);
        }
        private static Dictionary<string, string> tables = new Dictionary<string, string>()
            {
                {"EnderecoModel", "Logradouro,Bairro,Cidade,UF,CEP,Complemento" },
                {"FinanceiroModel", "RendaMensal" }
            };


        public override async Task<int> InsertRelation(object model)
        {

            if (model is FinanceiroModel financeiroInstance)
            {
                string commandText = "INSERT INTO FinanceiroModel (RendaMensal) output INSERTED.Id VALUES (@RENDAMENSAL)";
                SqlCommand command = new SqlCommand(commandText, Connection);

                command.Parameters.Add(new SqlParameter($"@RENDAMENSAL", financeiroInstance.RendaMensal));

                await Connection.OpenAsync();
                int modified = (int)command.ExecuteScalar();

                await Connection.CloseAsync();
                return modified;
            }
            return 0;
        }

        public override async Task<bool> Update(object body, int id)
        {
            try
            {
                StringBuilder SQL_BUILDER = new StringBuilder();
                SQL_BUILDER.Append("UPDATE FinanceiroModel SET ");
                if (body is FinanceiroModel pessoaInstance)
                {
                    SQL_BUILDER.Append($"RendaMensal = {pessoaInstance.RendaMensal} ");
                }
                SQL_BUILDER.Append($"WHERE Id = {id}");
                SqlCommand command = new SqlCommand(SQL_BUILDER.ToString(), Connection);
                await Connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await Connection.CloseAsync();
                return true;
            }
            catch (AggregateException)
            {
                return false;
            }

        }

        public  async Task<object> CompareMin(decimal min, decimal? max)
        {

            string where = max != null ? $"WHERE RendaMensal BETWEEN {min} AND {max}" : $"WHERE RendaMensal >= {min}";
            string query = QueryBuilder.Query(QueryBuilder.QueryBuilderEnum.SELECT_JOIN, "ClienteModel", tables, $"Id, NomeCompleto, CPF, DataNascimento, DateCadastro", where);

            List<object> _List = new List<object>();
            ClienteModel model = null;
            try
            {
                await Connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            model = new ClienteModel(reader);
                            model.Endereco = new EnderecoModel(reader);
                            model.Financeiro = new FinanceiroModel(reader);
                            _List.Add(model);
                        }


                    }
                }
                await Connection.CloseAsync();

                return _List.Count != 0 ? _List : null;

            }
            catch (AggregateException)
            {
                return null;
            }
        }
        public override async Task<bool> Insert(object model)
        {
            if (model is EnderecoModel enderecoInstance)
            {
                string commandText = "INSERT INTO EnderecoModel (Logradouro,Bairro,Cidade,UF,CEP,Complemento) VALUES (@LOGRADOURO,@BAIRRO,@CIDADE,@UF,@CEP,@COMPLEMENTO)";
                SqlCommand command = new SqlCommand(commandText, Connection);
                command.Parameters.Add(new SqlParameter($"@LOGRADOURO", enderecoInstance.Logradouro));
                command.Parameters.Add(new SqlParameter($"@BAIRRO", enderecoInstance.Bairro));
                command.Parameters.Add(new SqlParameter($"@CIDADE", enderecoInstance.Cidade));
                command.Parameters.Add(new SqlParameter($"@UF", enderecoInstance.UF));
                command.Parameters.Add(new SqlParameter($"@CEP", enderecoInstance.CEP));
                command.Parameters.Add(new SqlParameter($"@COMPLEMENTO", enderecoInstance.Complemento));
                await Connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await Connection.CloseAsync();
                return true;
            }
            return false;
        }

        
    }
}
