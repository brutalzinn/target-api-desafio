using api_target_desafio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace api_target_desafio.SqlConnector.Connectors
{
     public class EnderecoSqlConnector : SqlBase
    {
      
        public EnderecoSqlConnector(string conn)
        {
            Config(conn);
        }
        public override async Task<int> InsertRelation(object model)
        {

            if (model is EnderecoModel enderecoInstance)
            {
                string commandText = "INSERT INTO EnderecoModel (Logradouro,Bairro,Cidade,UF,CEP,Complemento) output INSERTED.Id VALUES (@LOGRADOURO,@BAIRRO,@CIDADE,@UF,@CEP,@COMPLEMENTO)";
                SqlCommand command = new SqlCommand(commandText, Connection);

                command.Parameters.Add(new SqlParameter($"@LOGRADOURO", enderecoInstance.Logradouro));
                command.Parameters.Add(new SqlParameter($"@BAIRRO", enderecoInstance.Bairro));
                command.Parameters.Add(new SqlParameter($"@CIDADE", enderecoInstance.Cidade));
                command.Parameters.Add(new SqlParameter($"@UF", enderecoInstance.UF));
                command.Parameters.Add(new SqlParameter($"@CEP", enderecoInstance.CEP));
                command.Parameters.Add(new SqlParameter($"@COMPLEMENTO", enderecoInstance.Complemento));
                await Connection.OpenAsync();
                int modified =  (int) await command.ExecuteScalarAsync();

                 await Connection.CloseAsync();
                return modified;
            }
            return 0;
        }

        public override async Task<bool> Insert(object model)
        {

          if(model is EnderecoModel enderecoInstance)
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

        public override async Task<bool> Update(object body, int id)
        {
            try
            {
                StringBuilder SQL_BUILDER = new StringBuilder();
                SQL_BUILDER.Append("UPDATE EnderecoModel SET ");
                if (body is EnderecoModel enderecoInstance)
                {
                    SQL_BUILDER.Append($"UF = '{enderecoInstance.UF}',");
                    SQL_BUILDER.Append($"Cidade = '{enderecoInstance.Cidade}',");
                    SQL_BUILDER.Append($"Bairro = '{enderecoInstance.Bairro}',");
                    SQL_BUILDER.Append($"CEP = '{enderecoInstance.CEP}',");
                    SQL_BUILDER.Append($"Complemento = '{enderecoInstance.Complemento}',");
                    SQL_BUILDER.Append($"Logradouro = '{enderecoInstance.Logradouro}' ");
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
        public override async Task<object> Read(int? id)
        {
            List<EnderecoModel> _List = new List<EnderecoModel>();
            string isWhere = id != null ? " WHERE Id=@ID" : "";
            string commandText = $"SELECT Id, Logradouro, Bairro, Cidade, UF, CEP, Complemento FROM EnderecoModel" + isWhere;
            await Connection.OpenAsync();
            using (SqlCommand command = new SqlCommand(commandText, Connection))
            {
                if (id != null)
                {
                    command.Parameters.Add(new SqlParameter($"@ID", id));
                }
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    EnderecoModel model = new EnderecoModel();
                   
                    if (id != null)
                    {

                        reader.Read();
                        model = new EnderecoModel(reader.GetInt32(0), reader.GetString(1),
                        reader.GetString(2), reader.GetString(3),
                        reader.GetString(4), reader.GetString(4), reader.GetString(5));
                        return model;
                    }

                    while (reader.Read())
                    {
                       
                            _List.Add(model);
                    }
                }
                
            }
            return _List;
        }

       
    }
}
