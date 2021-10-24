using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace api_target_desafio.Models.Plans
{
    public class VipModel
    {

        public int Id {  get; set; } 
        public string Name { get; set; }

        public decimal Preco { get; set; } = 50M;

        public string Descricao { get; set; }
        public VipModel(SqlDataReader reader)
        {
            Id = (int)reader["Id"];
            Name = reader["Name"].ToString();
            Preco = decimal.Round((decimal)reader["Preco"], 2, MidpointRounding.AwayFromZero);  
            Descricao = reader["Descricao"].ToString();

        }

        public VipModel(string name, decimal preco)
        {
            Name = name;
            Descricao = GetDescricao();
            Preco = decimal.Round(preco, 2, MidpointRounding.AwayFromZero);

        }
        //this is just for funny. Please, dont consider this part :)
        public string RandomEmpresa()
        {
            List<string> empresas = new List<string> { "Cosmolitana", "Google","Microsoft","RobbitWorks","LuthorCorp","RenatoCorp","YuriCorp"};
            int index = new Random().Next(0, empresas.Count);
            return empresas[index];

        }
        //this is just for funny. Please, dont consider this part :)

        public string GetDescricao()
        {
          List<string> descricao = new List<string> {"Você poderá ter um robô que avisará sobre tendências de investimento", 
              $"Compre ações na empresa {RandomEmpresa()}.", "Você poderá investir no banco tal" };

            int index =  new Random().Next(0, descricao.Count);
            return descricao[index];

        }
    }
}
