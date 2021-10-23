using api_target_desafio.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api_target_desafio.Models
{
    public abstract class ModelBase : IValidatableObject
    {
  
        public abstract string GetName();

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
       
    }
}
