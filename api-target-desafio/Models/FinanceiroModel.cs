using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace api_target_desafio.Models
{
    public class FinanceiroModel : BaseModel
    {
        public FinanceiroModel(decimal rendaMensal)
        {
            RendaMensal = rendaMensal;
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

        public override bool Validator(ModelStateDictionary model)
        {
            throw new System.NotImplementedException();
        }
    }
}
