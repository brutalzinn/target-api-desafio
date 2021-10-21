using System.ComponentModel.DataAnnotations;

namespace api_target_desafio.Models
{
    public class EnderecoModel
    {
        public EnderecoModel(int id, string logradouro, string bairro, string cidade, string uF, string cEP, string complemento)
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


    }
}
