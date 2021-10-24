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

            return Task.Run(() => connector.CompareMin(min,max)).Result;
        }
        public static object GetVips(FinanceiroSqlConnector connector)
        {
            int count = (int)Task.Run(() => GenericUtils.Count(connector.Config(),"WHERE RendaMensal >= 6000","FinanceiroModel")).Result;
            var result = new { Vips = count };
            return result;
        }
    }
}
