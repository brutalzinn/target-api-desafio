namespace api_target_desafio.Models
{
    public class IBGEEstadoModel
    {
        public int id { get; set; }

        public string sigla { get; set; }

        public IBGERegiaoModel regiao { get; set; }

    }
}
