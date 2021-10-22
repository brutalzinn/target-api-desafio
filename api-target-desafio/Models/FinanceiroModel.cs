using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api_target_desafio.Models
{
    public class FinanceiroModel : BaseModel
    {
        public FinanceiroModel(int id, decimal rendaMensal)
        {
            RendaMensal = decimal.Round(rendaMensal, 2, MidpointRounding.AwayFromZero);
            
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

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (RendaMensal == 0)
            {
                yield return new ValidationResult(
                   $"Dude, you put R$ 0 in the RendaMensal field. Fill it with some money.");
            }
        }
    }
}
