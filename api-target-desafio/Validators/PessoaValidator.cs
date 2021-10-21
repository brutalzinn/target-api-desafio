using api_target_desafio.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_target_desafio.Validators
{
    public static class PessoaValidator 
    {
        public static object Validate(PessoaModel instance, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return "ERROR";
            }
            return null;
           
        }
    }
}
