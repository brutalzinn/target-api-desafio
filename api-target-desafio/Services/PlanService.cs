using api_target_desafio.Models.Plans;
using api_target_desafio.SqlConnector;
using api_target_desafio.SqlConnector.Connectors;
using System.Diagnostics;
using System.Threading.Tasks;

namespace api_target_desafio.Services
{
    public static class PlanService
    {



        public static VipModel VipDetail(PlanSqlConnector instance)
        {
            VipModel _vip = null;
            int VipModelExists = (int)Task.Run(() => Utils.Count(instance.Config(), "", "VipModel")).Result;
            Debug.WriteLine($"VIPS:{VipModelExists}");
            if (VipModelExists == 0)
            {
                for (var i = 1; i < 5; i++)
                {
                    _vip = new VipModel($"Vip {i}", 50M);

                    if ((bool)Task.Run(() => instance.Insert(_vip)).Result)
                    {
                        Debug.WriteLine("TRUE");
                    }
                }
            }
            _vip = Task.Run(() => Utils.SelectAnyVipPlan(instance.Config())).Result;
            return _vip; //Task.Run(() => Utils.SelectAnyVipPlan(instance.Config())).Result;
        }

        public static VipModel VipManager()
        {
          //  VipModel _vip = new VipModel("Vip");

            return null;
        }

    }
}
