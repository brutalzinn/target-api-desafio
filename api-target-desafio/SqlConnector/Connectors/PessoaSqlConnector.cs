using System.Collections.Generic;
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
        public override object Read(int? id)
        {
            List <PessoaModel> _List = new List <PessoaModel>();
            string isWhere = id != null ? " WHERE Id=@ID" : "";
            string commandText = $"SELECT Id, NomeCompleto, CPF, DataNascimento, EnderecoModel_Id, FinanceiroModel_Id FROM PessoaModel" + isWhere;
            Connection.Open();
            using (SqlCommand command = new SqlCommand(commandText, Connection))
            {
                if (id != null)
                {
                    command.Parameters.Add(new SqlParameter($"@ID", id));
                }
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    PessoaModel model = new PessoaModel();

                    if (id != null)
                    {
                        reader.Read();
                        model = new PessoaModel(reader.GetInt32(0), reader.GetString(1),
                            reader.GetString(2), reader.GetDateTime(3),
                            new Models.EnderecoModel(), new Models.FinanceiroModel());

                        return model;
                    }
                    while (reader.Read())
                    {

                        _List.Add(model);
                    }
                }
            }
            return _List;

        }

        public override bool Insert(object model)
        {
          if(model is PessoaModel pessoaInstance)
            {
                int enderecoModel = 0;
                int financeiroModel = 0;
                if (pessoaInstance != null && pessoaInstance.Endereco != null && pessoaInstance.Financeiro != null)
                {
                    EnderecoSqlConnector enderecoSqlConnector = new EnderecoSqlConnector();
                    FinanceiroSqlConnector financeiroSqlConnector = new FinanceiroSqlConnector();
                    enderecoSqlConnector.Config(sConnection);
                    financeiroSqlConnector.Config(sConnection);
                    enderecoModel = enderecoSqlConnector.InsertRelation(pessoaInstance.Endereco);
                    financeiroModel = financeiroSqlConnector.InsertRelation(pessoaInstance.Financeiro);
                }

                string commandText = "INSERT INTO PessoaModel (NomeCompleto,CPF,DataNascimento,EnderecoModel_Id,FinanceiroModel_Id) VALUES (@NOMECOMPLETO,@CPF,@DATANASCIMENTO,@ENDERECOMODEL,@FINANCEIROMODEL)";
                SqlCommand command = new SqlCommand(commandText, Connection);
            
                command.Parameters.Add(new SqlParameter($"@NOMECOMPLETO", pessoaInstance.NomeCompleto));
                command.Parameters.Add(new SqlParameter($"@CPF", pessoaInstance.CPF));
                command.Parameters.Add(new SqlParameter($"@DATANASCIMENTO", pessoaInstance.DataNascimento));
                command.Parameters.Add(new SqlParameter($"@ENDERECOMODEL", enderecoModel));
                command.Parameters.Add(new SqlParameter($"@FINANCEIROMODEL", financeiroModel));
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            return false;
        }

    
    }
}
