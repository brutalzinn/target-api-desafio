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


        public int CanBeVips { get; set; }

        public int Vips { get; set; }

        public int NonCanBeVips { get; set; }
        public override string GetResponse()
        {
            return JsonSerializer.Serialize(new { CanBeVips, Vips, NonCanBeVips });
        }
    }



}
