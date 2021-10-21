using System.Data.SqlClient;

namespace api_target_desafio.SqlConnector.Connectors
{
     public class PessoaSqlConnector : SqlAbstract
    {
        public override void CheckValidation()
        {
            
            throw new System.NotImplementedException();
        }

        public PessoaSqlConnector()
        {

        }
     

        public override bool Insert(object model)
        {

          if(model is PessoaModel pessoaInstance)
            {
                string commandText = "INSERT INTO PessoaModel (NomeCompleto,CPF,DataNascimento) VALUES (@NOMECOMPLETO,@CPF,@DATANASCIMENTO)";
                SqlCommand command = new SqlCommand(commandText, Connection);

                command.Parameters.Add(new SqlParameter($"@NOMECOMPLETO", pessoaInstance.NomeCompleto));
                command.Parameters.Add(new SqlParameter($"@CPF", pessoaInstance.CPF));
                command.Parameters.Add(new SqlParameter($"@DATANASCIMENTO", pessoaInstance.DataNascimento));
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            return false;
        }
    }
}
