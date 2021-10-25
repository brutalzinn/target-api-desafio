using api_target_desafio.Config;
using api_target_desafio.SqlConnector;
using api_target_desafio.SqlConnector.Connectors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_target_desafio.Services
{
    public static class FinanceiroService
    {
     

        public static object CompareMin(FinanceiroSqlConnector connector,decimal min, decimal? max)
        {
            object result = Task.Run(() => connector.CompareMin(min, max)).Result;         
            if (result == null)
            {
                throw new SqlServiceException(System.Net.HttpStatusCode.NotFound, "Cant find any clients.");
            }
            return result;
        }
    
    }
}
