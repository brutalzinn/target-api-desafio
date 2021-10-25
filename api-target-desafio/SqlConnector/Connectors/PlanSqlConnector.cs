using api_target_desafio.Models;
using api_target_desafio.Models.Plans;
using api_target_desafio.Responses;
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

        public async Task<VipModel> SelectAnyVipPlan()
        {
            try
            {
                string query = $"SELECT TOP 1 Id, Nome, Preco, Descricao FROM VipModel ORDER BY NEWID()";

                VipModel vipModel = null;
               
                    await Connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, Connection))
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
                await Connection.CloseAsync();

                return vipModel;
            }
            catch (AggregateException)
            {


                return null;
            }
        }
        public async Task<object> GetPlanoInfo()
        {
            try
            {
                string CanBeVips = "SELECT cli.id FROM ClienteModel as cli INNER JOIN FinanceiroModel as fin ON fin.Id = cli.FinanceiroModel_Id WHERE fin.RendaMensal >= 6000";
                string Vips = "SELECT Id FROM ClienteModel WHERE VipModel_Id is not null";
                string NonCanBeVips = "SELECT cli.id FROM ClienteModel as cli INNER JOIN FinanceiroModel as fin ON fin.Id = cli.FinanceiroModel_Id WHERE fin.RendaMensal < 6000";
                string query = QueryBuilder.QueryConcat(CanBeVips, Vips, NonCanBeVips);
                VipInfo vipModel = null;
                
                await Connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        vipModel = new VipInfo();

                        while (await reader.ReadAsync())
                        {
                            vipModel.CanBeVipsList.Add((int)reader["Id"]);
                        }
                        reader.NextResult();
                        while (await reader.ReadAsync())
                        {
                            vipModel.VipsList.Add((int)reader["Id"]);
                        }
                        reader.NextResult();
                        while (await reader.ReadAsync())
                        {
                           vipModel.NonCanBeVipsList.Add((int)reader["Id"]);
                        }
                    }
                }
                await Connection.CloseAsync();

                vipModel.NonCanBeVips = vipModel.NonCanBeVipsList.Count;
                vipModel.Vips = vipModel.VipsList.Count;
                vipModel.CanBeVips = vipModel.CanBeVipsList.Count;
                return vipModel;
            }
            catch (AggregateException)
            {


                return null;
            }
        }

        public override async Task<bool> Insert(object model)
        {

            try
            {
                if (model is VipModel vipInstance)
                {
                    string commandText = "INSERT INTO VipModel (Nome, Preco, Descricao) VALUES (@NOME,@PRECO,@DESCRICAO)";
                    SqlCommand command = new SqlCommand(commandText, Connection);
                    command.Parameters.Add(new SqlParameter($"@NOME", vipInstance.Nome));
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
   

   
        public override async Task<bool> Update(object body, int id)
        {
            try
            {
                StringBuilder SQL_BUILDER = new StringBuilder();
                SQL_BUILDER.Append("UPDATE VipModel SET ");
                if (body is VipModel vipModelInstance)
                {
                    SQL_BUILDER.Append($"Name = '{vipModelInstance.Nome}',");
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
            string commandText = $"SELECT Id, Nome, Preco, Descricao FROM VipModel" + isWhere;
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
