using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace api_target_desafio.Models
{
    public class FinanceiroModel : ModelBase
    {
        public FinanceiroModel(int id, decimal rendaMensal)
        {
            RendaMensal = decimal.Round(rendaMensal, 2, MidpointRounding.AwayFromZero);
            
            Id = id;
        }

        public FinanceiroModel()
        {
      
        }
        public FinanceiroModel(SqlDataReader reader)
        {
            Id = (int)reader[$"{GetNameId()}Id"];
            RendaMensal = decimal.Round((decimal)reader[$"{GetNameId()}RendaMensal"], 2, MidpointRounding.AwayFromZero); 
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
                   $"Dude, you put R$ 0 in the RendaMensal field. Fill it with some money.This is just a test. We can be like Elon Musk here.");
            }
        }
    }
}
