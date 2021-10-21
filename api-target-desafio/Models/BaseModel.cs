using api_target_desafio.Validators;

namespace api_target_desafio.Models
{
    public abstract class BaseModel
    {
       

        public abstract string GetName();

        public abstract bool Validator(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary model);

    }
}
