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


        public static object GetPlanoInfo(PlanSqlConnector instance, int? id)
        {
            object result = Task.Run(() => instance.GetPlanoInfo()).Result;
            if (result == null)
            {
                string error = id != null ? $"Cant find Plan with id {id}" : $"Cant find any Plans.";
                throw new SqlServiceException(System.Net.HttpStatusCode.NotFound, error);
            }
            return result;
        }


        public static object GetPlano(PlanSqlConnector instance, int? id)
        {
            object result =  Task.Run(() => instance.Read(id)).Result;
            if (result == null)
            {
                string error = id != null ? $"Cant find Plan with id {id}" : $"Cant find any Plans.";
                throw new SqlServiceException(System.Net.HttpStatusCode.NotFound, error);
            }
            return result;
        }
        // need refactor here - 25/10/2021
        public static object PlanoManager(PlanSqlConnector instance, PlanModel cliente)
        {
            int ClientelExists = (int)Task.Run(() => GenericUtils.Count(instance.Config(), $"WHERE Id = '{cliente.Cliente_Id}'", "ClienteModel")).Result;
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
