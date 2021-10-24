using System.Text.Json;

namespace api_target_desafio.Responses
{

    public class VipInfo : ResponseBase
    {
        public VipInfo(int canBeVips, int vips, int nonCanBeVips)
        {
            CanBeVips = canBeVips;
            Vips = vips;
            NonCanBeVips = nonCanBeVips;
        }

        public int CanBeVips { get; set; }

        public int Vips { get; set; }

        public int NonCanBeVips { get; set; }
        public override string GetResponse()
        {
            return JsonSerializer.Serialize(new { CanBeVips, Vips, NonCanBeVips });
        }
    }



}
