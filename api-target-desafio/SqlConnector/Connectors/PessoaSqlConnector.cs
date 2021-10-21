using System.Data.SqlClient;
using System.Diagnostics;

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
                Debug.WriteLine(pessoaInstance.Endereco.Logradouro);
                int enderecoModel = 0;
                if (pessoaInstance != null && pessoaInstance.Endereco != null)
                {
                    EnderecoSqlConnector enderecoSqlConnector = new EnderecoSqlConnector();
                    enderecoSqlConnector.Config(sConnection);
                    enderecoModel = enderecoSqlConnector.InsertRelation(pessoaInstance.Endereco);
                }

                string commandText = "INSERT INTO PessoaModel (NomeCompleto,CPF,DataNascimento,EnderecoModel_Id) VALUES (@NOMECOMPLETO,@CPF,@DATANASCIMENTO,@ENDERECOMODEL)";
                SqlCommand command = new SqlCommand(commandText, Connection);
            
                command.Parameters.Add(new SqlParameter($"@NOMECOMPLETO", pessoaInstance.NomeCompleto));
                command.Parameters.Add(new SqlParameter($"@CPF", pessoaInstance.CPF));
                command.Parameters.Add(new SqlParameter($"@DATANASCIMENTO", pessoaInstance.DataNascimento));
                command.Parameters.Add(new SqlParameter($"@ENDERECOMODEL", enderecoModel));

                Connection.Open();
                command.ExecuteNonQuery();

                Connection.Close();

              

                return true;
            }
            return false;
        }
    }
}
