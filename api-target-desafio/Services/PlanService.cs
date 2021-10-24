using api_target_desafio.Config;
using api_target_desafio.Models.Plans;
using api_target_desafio.Responses;
using api_target_desafio.SqlConnector;
using api_target_desafio.SqlConnector.Connectors;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace api_target_desafio.Services
{
    public static class PlanService
    {



        public static VipModel VipDetail(PlanSqlConnector instance)
        {
            int VipModelExists = (int)Task.Run(() => Utils.Count(instance.Config(), "", "VipModel")).Result;
            if (VipModelExists == 0)
            {
                for (var i = 1; i < 5; i++)
                {
                    VipModel _vip = new VipModel($"Vip {i}", 50M);

                    if (Task.Run(() => instance.Insert(_vip)).Result)
                    {
                        Debug.WriteLine("TRUE");
                    }
                }
            }

            // VipInfo vipInfo = new VipInfo();

            // return Task.Run(() => instance.Insert(_vip)).Result)
            return null;
        }

        public static object VipManager(PlanSqlConnector instance, PlanModel cliente)
        {
            //  VipModel _vip = new VipModel("Vip");

            int ClientelExists = (int)Task.Run(() => Utils.Count(instance.Config(), $"WHERE ID = '{cliente.Cliente_Id}'", "ClienteModel")).Result;
            if(ClientelExists == 0)
            {
                throw new SqlServiceException(System.Net.HttpStatusCode.NotFound, $"Client not found. Cant set vip to client with id {cliente.Cliente_Id}.");
            }
            bool result = false;
            if (cliente.Aceito) {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("VipModel_Id", cliente.Vip_Id.ToString());

                string query = QueryBuilder.Query(QueryBuilder.QueryBuilderEnum.UPDATE, "ClienteModel", dic, $"WHERE ID = '{cliente.Cliente_Id}'");

                result = Task.Run(() => instance.Query(query)).Result;
            }
            var _vipResponse = new VipUpdate(result, "Thanks for accept us vip plan.");

            return _vipResponse;
        }

    }
}
