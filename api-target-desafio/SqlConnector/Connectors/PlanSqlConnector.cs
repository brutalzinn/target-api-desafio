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
      

        public override async Task<bool> Insert(object model)
        {

            try
            {
                if (model is VipModel vipInstance)
                {
                    string commandText = "INSERT INTO VipModel (Nome, Preco, Descricao) VALUES (@NOME,@PRECO,@DESCRICAO)";
                    SqlCommand command = new SqlCommand(commandText, Connection);
                    command.Parameters.Add(new SqlParameter($"@NOME", vipInstance.Name));
                    command.Parameters.Add(new SqlParameter($"@PRECO", vipInstance.Preco));
                    command.Parameters.Add(new SqlParameter($"@DESCRICAO", vipInstance.Descricao));
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
        public override async Task<bool> Query(string query)
        {
            try
            {
                SqlCommand command = new SqlCommand(query, Connection);
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

        public async Task<bool> ClienteProporcionalPlans(string plan,string query)
        {
            try
            {
                SqlCommand command = new SqlCommand(query, Connection);
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
                                model = new VipModel(reader);            
                            }
                            await Connection.CloseAsync();
                            return model;
                        case not null:
                        while (reader.Read())
                        {
                            model = new VipModel(reader);
                            _List.Add(model);
                        }
                            await Connection.CloseAsync();
                            break;
                    }           
                }
                
            }
            return _List;
        }

       
    }
}
