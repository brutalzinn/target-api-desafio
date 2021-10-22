using api_target_desafio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace api_target_desafio.SqlConnector.Connectors
{
    public class FinanceiroSqlConnector : SqlAbstract
    {
              private Dictionary<string, string> tables = new Dictionary<string, string>()
            {
                {"EnderecoModel", "ende.Id,Logradouro,Bairro,Cidade,UF,CEP,Complemento" },
                {"FinanceiroModel", "fina.Id,RendaMensal" }
            };
        public FinanceiroSqlConnector()
        {

        }
            public FinanceiroSqlConnector(string conn)
        {
            Config(conn);
        }
        public  async Task<object> ReadCustom(string condition)
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
            string commandText = _SQL_NAMES + " FROM PessoaModel AS pes " + _SQL_JOIN + " " + condition;
            await Connection.OpenAsync();
            PessoaModel model;
            using (SqlCommand command = new SqlCommand(commandText, Connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                  
                            while (await reader.ReadAsync())
                            {
                                model = new PessoaModel();
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
            catch (Exception e)
            {
                return false;
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

        //public async Task<object> ReadCustom(string query)
        //{
        //    List<object> list = new List<object>();

        //    await Connection.OpenAsync();
        //    using (SqlCommand command = new SqlCommand(query, Connection))
        //    {
        //        using (SqlDataReader reader = await command.ExecuteReaderAsync())
        //        {
        //            while (await reader.ReadAsync())
        //            {

        //                for (var i = 0; i < reader.FieldCount; i++)
        //                {
        //                    list.Add(reader[i]);
        //                }
        //            }
        //        }
        //    }
           
        //    await Connection.CloseAsync();

        //    return list;
        //}
    }
}
