using api_target_desafio.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace api_target_desafio.SqlConnector.Connectors
{
    public class FinanceiroSqlConnector : SqlAbstract
    {
        public override void CheckValidation()
        {

            throw new System.NotImplementedException();
        }

        public FinanceiroSqlConnector()
        {

        }
        public override int InsertRelation(object model)
        {

            if (model is FinanceiroModel financeiroInstance)
            {
                string commandText = "INSERT INTO FinanceiroModel (RendaMensal) output INSERTED.Id VALUES (@RENDAMENSAL)";
                SqlCommand command = new SqlCommand(commandText, Connection);

                command.Parameters.Add(new SqlParameter($"@RENDAMENSAL", financeiroInstance.RendaMensal));
        
                Connection.Open();
                int modified = (int)command.ExecuteScalar();

                Connection.Close();
                return modified;
            }
            return 0;
        }

        public override bool Insert(object model)
        {

            if (model is EnderecoModel enderecoInstance)
            {
                string commandText = "INSERT INTO EnderecoModel (Logradouro,Bairro,Cidade,UF,CEP,Complemento) VALUES (@LOGRADOURO,@BAIRRO,@CIDADE,@UF,@CEP,@COMPLEMENTO)";
                SqlCommand command = new SqlCommand(commandText, Connection);

                command.Parameters.Add(new SqlParameter($"@LOGRADOURO", enderecoInstance.Logradouro));
                command.Parameters.Add(new SqlParameter($"@BAIRRO", enderecoInstance.Bairro));
                command.Parameters.Add(new SqlParameter($"@CIDADE", enderecoInstance.Cidade));
                command.Parameters.Add(new SqlParameter($"@UF", enderecoInstance.UF));
                command.Parameters.Add(new SqlParameter($"@CEP", enderecoInstance.CEP));
                command.Parameters.Add(new SqlParameter($"@COMPLEMENTO", enderecoInstance.Complemento));
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            return false;
        }
    }
}
