using api_target_desafio.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace api_target_desafio.SqlConnector.Connectors
{
     public class EnderecoSqlConnector : SqlAbstract
    {
      
        public EnderecoSqlConnector()
        {

        }
        public override int InsertRelation(object model)
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
                Connection.Open();
                int modified = (int)command.ExecuteScalar();

                Connection.Close();
                return modified;
            }
            return 0;
        }

        public override bool Insert(object model)
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
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            return false;
        }


        public override object Read(int? id)
        {
            List<EnderecoModel> _List = new List<EnderecoModel>();
            string isWhere = id != null ? " WHERE Id=@ID" : "";
            string commandText = $"SELECT Id, Logradouro, Bairro, Cidade, UF, CEP, Complemento FROM EnderecoModel" + isWhere;
            Connection.Open();
            using (SqlCommand command = new SqlCommand(commandText, Connection))
            {
                if (id != null)
                {
                    command.Parameters.Add(new SqlParameter($"@ID", id));
                }
                using (SqlDataReader reader = command.ExecuteReader())
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

        public override object ReadRelation(Dictionary<string,string> tables, int? id)
        {
            throw new System.NotImplementedException();
        }
    }
}
