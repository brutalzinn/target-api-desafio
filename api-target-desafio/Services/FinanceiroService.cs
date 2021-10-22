using api_target_desafio.SqlConnector;
using api_target_desafio.SqlConnector.Connectors;
using System.Threading.Tasks;

namespace api_target_desafio.Services
{
    public static class FinanceiroService
    {

        public static object CompareMin(FinanceiroSqlConnector connector,decimal min, decimal? max)
        {
            string query = max != null ? $"WHERE RendaMensal BETWEEN {min} AND {max}" : $"WHERE RendaMensal >= {min}";
            return Task.Run(() => Utils.ReadCustom(connector.Config(),query,"PessoaModel")).Result;
        }
        public static object GetVips(FinanceiroSqlConnector connector)
        {
            int count = (int)Task.Run(() => Utils.Count(connector.Config(),"WHERE RendaMensal >= 6000","FinanceiroModel")).Result;
            var result = new { Vips = count };
            return result;
        }
    }
}
