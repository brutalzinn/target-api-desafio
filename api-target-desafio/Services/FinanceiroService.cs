using api_target_desafio.SqlConnector.Connectors;
using System.Threading.Tasks;

namespace api_target_desafio.Services
{
    public static class FinanceiroService
    {

        public static object CompareMin(FinanceiroSqlConnector connector,decimal min, decimal? max)
        {
            string query = max != null ? $"WHERE RendaMensal BETWEEN {min} AND {max}" : $"WHERE RendaMensal >= {min}";
            return Task.Run(() => connector.ReadCustom(query)).Result;
        }
        public static object GetVips(FinanceiroSqlConnector connector)
        {
            int count = (int)Task.Run(() => connector.Count("WHERE RendaMensal >= 6000")).Result;
            var result = new { Vips = count };
            return result;
        }
    }
}
