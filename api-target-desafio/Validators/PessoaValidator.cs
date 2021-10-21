using api_target_desafio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace api_target_desafio.Validators
{
    public static class PessoaValidator
    {
        //private PessoaModel PessoaModel { get; set; }
        //public PessoaValidator(PessoaModel instance)
        //{
        //    PessoaModel = instance;
        //}
        private  static BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        public static bool Validate(PessoaModel instance,Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            List<string> modelNameList = new List<string>();
            var fieldValues = instance.GetType().GetFields(bindingFlags).ToList();
            foreach (var fields in fieldValues)
            {
                MatchCollection matches = Regex.Matches(fields.Name, @"<(.*)>");
                foreach (Match match in matches)
                {
                    if(match.Groups[1].ToString() != "Id")
                    modelNameList.Add(match.Groups[1].ToString());
                }
            }
            bool isValid = true;
            foreach(var teste in modelState.Values)
            {
                Debug.WriteLine($"STATE:{teste}");
            }
            foreach(string name in modelNameList)
            {
                isValid = modelState.ContainsKey(name);
                Debug.WriteLine($"{isValid} = {name}");
            }

            return isValid;


            //if (!modelState.IsValid)
            //{ 
            //    return false;
            //}
            //return true;
           
        }
    }
}
