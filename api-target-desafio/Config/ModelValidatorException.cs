using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
namespace api_target_desafio.Config
{
    public class ModelValidatorException : Exception
    {
      
            public ModelStateDictionary Model { get; private set; }

            public List<string> Errors { get; private set; }


        public ModelValidatorException()
                { }

            public ModelValidatorException(ModelStateDictionary model)
            {
            foreach (var item in model.Values)
            {
                foreach (var error in item.Errors) {
                    Errors.Add(error.ErrorMessage);
               }
            }
                Model = model;
            }
    }
}
