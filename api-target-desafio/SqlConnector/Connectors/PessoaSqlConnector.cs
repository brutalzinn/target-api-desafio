using api_target_desafio.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace api_target_desafio.SqlConnector.Connectors
{        
    
    
    //WE NEED REFACTOR ALL QUERY BUILDERS TWO DAYS BEFORE DISPATCH THE CHALLENGER

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
                    PessoaModel model;

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
                        model = new PessoaModel(reader.GetInt32(0), reader.GetString(1),
                            reader.GetString(2), reader.GetDateTime(3),
                            new Models.EnderecoModel(), new Models.FinanceiroModel());
                        _List.Add(model);
                    }
                }
            }
            return _List;

        }

        public override bool Insert(object model)
        {
            if (model is PessoaModel pessoaInstance)
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


        public override object ReadRelation(Dictionary<string,string> tables, int? id)
        {

            StringBuilder _SQL_JOIN = new StringBuilder();
            StringBuilder _SQL_NAMES = new StringBuilder();
            _SQL_NAMES.Append("SELECT pes.id, NomeCompleto, CPF, DataNascimento, ");
            foreach (var item in tables)
            {
                string name = item.Key.ToLower().Substring(0, 4);
                _SQL_JOIN.Append($"INNER JOIN {item.Key} AS {name} ON {name}.Id = pes.Id ");
                _SQL_NAMES.Append($"{item.Value},");

            }
            _SQL_NAMES.Remove(_SQL_NAMES.Length - 1, 1);
            List<object> _List = new List<object>();
            string isWhere = id != null ? "WHERE Id=@ID" : "";
            string commandText = _SQL_NAMES + " FROM PessoaModel AS pes " + _SQL_JOIN + isWhere;
            Connection.Open();
            using (SqlCommand command = new SqlCommand(commandText, Connection))
            {
              
                if (id != null)
                {
                    command.Parameters.Add(new SqlParameter($"@ID", id));
                }
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    PessoaModel model;

                    if (id != null)
                    {
                        reader.Read();
                       
                        model = new PessoaModel(reader.GetInt32(0), reader.GetString(1),
               reader.GetString(2), reader.GetDateTime(3), new EnderecoModel(reader.GetInt32(4), reader.GetString(5),
               reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10)), new FinanceiroModel(reader.GetInt32(11)));

                        return model;
                    }
                    while (reader.Read())
                    {
                        model = new PessoaModel(reader.GetInt32(0), reader.GetString(1),
               reader.GetString(2), reader.GetDateTime(3), new EnderecoModel(reader.GetInt32(4),reader.GetString(5),
               reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10)), new FinanceiroModel(reader.GetInt32(11)));

                        _List.Add(model);

                        
                    }
                }
            }
            return _List;

        }
    }
}
