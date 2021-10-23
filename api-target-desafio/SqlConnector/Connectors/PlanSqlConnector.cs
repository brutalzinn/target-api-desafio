using api_target_desafio.Models;
using api_target_desafio.Models.Plans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace api_target_desafio.SqlConnector.Connectors
{
     public class PlanSqlConnector : SqlBase
    {
        public PlanSqlConnector()
        {
        }
        public PlanSqlConnector(string conn)
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
                string commandText = "INSERT INTO VipModel (Name, Preco, Descricao) VALUES (@NAME,@PRECO,@DESCRICAO)";
                SqlCommand command = new SqlCommand(commandText, Connection);         
                command.Parameters.Add(new SqlParameter($"@NAME", enderecoInstance.Logradouro));
                command.Parameters.Add(new SqlParameter($"@PRECO", enderecoInstance.Bairro));
                command.Parameters.Add(new SqlParameter($"@DESCRICAO", enderecoInstance.Cidade));
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
                SQL_BUILDER.Append("UPDATE VipModel SET ");
                if (body is VipModel vipModelInstance)
                {
                    SQL_BUILDER.Append($"Name = '{vipModelInstance.Name}',");
                    SQL_BUILDER.Append($"Preco = '{vipModelInstance.Preco}',");
                    SQL_BUILDER.Append($"Descricao = '{vipModelInstance.Descricao}' ");
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
            List<VipModel> _List = new List<VipModel>();
            string isWhere = id != null ? " WHERE Id=@ID" : "";
            string commandText = $"SELECT Id, Name, Preco, Descricao FROM VipModel" + isWhere;
            await Connection.OpenAsync();
            using (SqlCommand command = new SqlCommand(commandText, Connection))
            {
                if (id != null)
                {
                    command.Parameters.Add(new SqlParameter($"@ID", id));
                }
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    VipModel model = null;           
                    switch (id)
                    {
                        case null:
                            if (reader.Read()) {
                                model = new VipModel();
                                model.Id = reader.GetInt32(0);
                                model.Name = reader.GetString(1);
                                model.Preco = reader.GetDecimal(2);
                                model.Descricao = reader.GetString(3);
                            }
                            return model;
                        case not null:
                        while (reader.Read())
                        {
                            model = new VipModel();
                            model.Id = reader.GetInt32(0);
                            model.Name = reader.GetString(1);
                            model.Preco = reader.GetDecimal(2);
                            model.Descricao = reader.GetString(3);
                            _List.Add(model);
                        }
                         break;
                    }           
                }
                
            }
            return _List;
        }

       
    }
}
