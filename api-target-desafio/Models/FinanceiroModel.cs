using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace api_target_desafio.Models
{
    public class FinanceiroModel : BaseModel
    {
        public FinanceiroModel(int id, decimal rendaMensal)
        {
            RendaMensal = rendaMensal;
            Id = id;
        }
        public FinanceiroModel()
        {
           
        }
        public int Id { get; set; }
        public decimal RendaMensal { get; set; }
        public override string GetName()
        {
            return "FinanceiroModel";
        }

     
    }
}
