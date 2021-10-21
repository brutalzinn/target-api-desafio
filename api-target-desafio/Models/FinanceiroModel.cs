namespace api_target_desafio.Models
{
    public class FinanceiroModel
    {
        public FinanceiroModel(int rendaMensal)
        {
            RendaMensal = rendaMensal;
        }
        public FinanceiroModel()
        {
           
        }
        public int Id { get; set; }
        public decimal RendaMensal { get; set; }

    }
}
