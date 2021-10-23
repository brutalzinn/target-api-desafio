using api_target_desafio.Models.Plans;
using api_target_desafio.SqlConnector;
using api_target_desafio.SqlConnector.Connectors;
using System.Threading.Tasks;

namespace api_target_desafio.Services
{
    public static class PlanService
    {



        public static VipModel VipDetail(PlanSqlConnector instance)
        {
            VipModel _vip = new VipModel("Vip",50M);

            int VipModelExists = Task.Run(() => Utils.Count(instance.Config(), "","VipModel")).Result;
            if (VipModelExists == 0)
            {
                Task.Run(() => instance.Insert(_vip));
            }
            return _vip;
        }

        public static VipModel VipManager()
        {
          //  VipModel _vip = new VipModel("Vip");

            return null;
        }

    }
}
