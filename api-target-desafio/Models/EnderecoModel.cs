using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace api_target_desafio.Models
{
    public class EnderecoModel : BaseModel
    {
        public EnderecoModel(int id = 0, string logradouro = "", string bairro = "", string cidade = "", string uF = "", string cEP = "", string complemento= "")
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

        public override bool Validator(ModelStateDictionary model)
        {
            throw new System.NotImplementedException();
        }
    }
}
