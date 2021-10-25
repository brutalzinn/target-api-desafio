using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json;

namespace api_target_desafio.Responses
{

    public class VipInfo : ResponseBase
    {
        public VipInfo()
        {
    
        }

     
        public List<int> CanBeVipsList { get; set; } = new List<int>();
        public List<int> VipsList { get; set; } = new List<int>();

        public List<int> NonCanBeVipsList { get; set; } = new List<int>();


        public int CountCanBeVips { get; set; }

        public int CountVips { get; set; }

        public int CountNonCanBeVips { get; set; }
        public override object GetResponse()
        {
            CountNonCanBeVips = NonCanBeVipsList.Count;
            CountVips = VipsList.Count;
            CountCanBeVips = CanBeVipsList.Count;
            return new { NonCanBeVipsList, VipsList, CanBeVipsList,CountCanBeVips, CountVips, CountNonCanBeVips };
        }
    }



}
