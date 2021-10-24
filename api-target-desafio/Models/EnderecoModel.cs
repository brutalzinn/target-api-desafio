using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api_target_desafio.Services;
using System.Threading.Tasks;
using api_target_desafio.Validators;
using api_target_desafio.Models.Errors;

namespace api_target_desafio.Models
{
    public class EnderecoModel : ModelBase
    {
        public EnderecoModel(int id = 0, string logradouro = "", string bairro = "", string cidade = "", string uF = "", string cEP = "", string complemento = "")
        {

            Id = id;
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            UF = uF;
            CEP = cEP;
            Complemento = complemento;
        }
        public EnderecoModel()
        {

        }

        [Key]
        public int Id { get; set; }

        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string UF { get; set; }

        public string CEP { get; set; }

        public string Complemento { get; set; }

        public override string GetName()
        {
            return "EnderecoModel";
        }
       
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //fun moment to validate fields
            CepModelError cepModelError = Task.Run(() => CepValidator.CheckCep(this)).Result;
            if (!cepModelError.Validade())
            {
                string result = "";
                if (!cepModelError.Cidade)
                {
                    result = "Error on cidade field";
                }
                if (!cepModelError.Logradouro)
                {
                    result = "Error on Logradouro field";
                }
                if (!cepModelError.Uf)
                {
                    result = "Error on UF field";
                }
                if (!cepModelError.Bairro)
                {
                    result = "Error on Bairro field";
                }
                yield return new ValidationResult(result);
            }

        }
     }
}
