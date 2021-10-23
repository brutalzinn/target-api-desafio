using api_target_desafio.Config;
using api_target_desafio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace api_target_desafio.SqlConnector.Connectors
{
    //WE NEED REFACTOR ALL QUERY BUILDERS TWO DAYS BEFORE DISPATCH THE CHALLENGER
    public class ClienteSqlConnector : SqlAbstract
    {

        public ClienteSqlConnector()
        {

        }
        public ClienteSqlConnector(string conn)
        {
            Config(conn);
        }
        private Dictionary<string, string> tables = new Dictionary<string, string>()
            {
                {"EnderecoModel", "ende.Id,Logradouro,Bairro,Cidade,UF,CEP,Complemento" },
                {"FinanceiroModel", "fina.Id,RendaMensal" }
            };
        public override async Task<object> Read(int? id)
        {
            List<ClienteModel> _List = new List<ClienteModel>();
            string isWhere = id != null ? " WHERE Id=@ID" : "";
            string commandText = $"SELECT Id, NomeCompleto, CPF, DataNascimento, EnderecoModel_Id, FinanceiroModel_Id FROM PessoaModel" + isWhere;
            await Connection.OpenAsync();
            using (SqlCommand command = new SqlCommand(commandText, Connection))
            {
                if (id != null)
                {
                    command.Parameters.Add(new SqlParameter($"@ID", id));
                }
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    ClienteModel model;

                    if (id != null)
                    {
                        await reader.ReadAsync();
                        model = new ClienteModel(reader.GetInt32(0), reader.GetString(1),
                            reader.GetString(2), reader.GetDateTime(3),
                            new Models.EnderecoModel(), new Models.FinanceiroModel());

                        return model;
                    }
                    while (await reader.ReadAsync())
                    {
                        model = new ClienteModel(reader.GetInt32(0), reader.GetString(1),
                            reader.GetString(2), reader.GetDateTime(3),
                            new Models.EnderecoModel(), new Models.FinanceiroModel());
                        _List.Add(model);
                    }
                }
            }
            return _List;

        }
        public override async Task<bool> Update(object body, int id)
        {
            try
            {
                ClienteModel pessoa = (ClienteModel)await ReadRelation(tables, id);
                Debug.WriteLine($"TESTING WITH {pessoa.NomeCompleto}-Financeiro:{pessoa.Financeiro.RendaMensal}");
                FinanceiroSqlConnector Financeiro = new FinanceiroSqlConnector(sConnection);
                EnderecoSqlConnector Endereco = new EnderecoSqlConnector(sConnection);

                if (body is ClienteModel pessoaInstance)
                {
                    await Financeiro.Update(pessoaInstance.Financeiro, pessoa.Financeiro.Id);
                    await Endereco.Update(pessoaInstance.Endereco, pessoa.Endereco.Id);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public override async Task<bool> Insert(object model)
        {
            try
            {
                if (model is ClienteModel pessoaInstance)
                {
                    int enderecoModel = 0;
                    int financeiroModel = 0;
                    if (pessoaInstance != null && pessoaInstance.Endereco != null && pessoaInstance.Financeiro != null)
                    {
                        EnderecoSqlConnector enderecoSqlConnector = new EnderecoSqlConnector(sConnection);
                        FinanceiroSqlConnector financeiroSqlConnector = new FinanceiroSqlConnector(sConnection);
                        enderecoModel = await enderecoSqlConnector.InsertRelation(pessoaInstance.Endereco);
                        financeiroModel = await financeiroSqlConnector.InsertRelation(pessoaInstance.Financeiro);
                    }

                    string commandText = "INSERT INTO PessoaModel (NomeCompleto,CPF,DataNascimento,EnderecoModel_Id,FinanceiroModel_Id) VALUES (@NOMECOMPLETO,@CPF,@DATANASCIMENTO,@ENDERECOMODEL,@FINANCEIROMODEL)";
                    SqlCommand command = new SqlCommand(commandText, Connection);

                    command.Parameters.Add(new SqlParameter($"@NOMECOMPLETO", pessoaInstance.NomeCompleto));
                    command.Parameters.Add(new SqlParameter($"@CPF", pessoaInstance.CPF));
                    command.Parameters.Add(new SqlParameter($"@DATANASCIMENTO", pessoaInstance.DataNascimento));
                    command.Parameters.Add(new SqlParameter($"@ENDERECOMODEL", enderecoModel));
                    command.Parameters.Add(new SqlParameter($"@FINANCEIROMODEL", financeiroModel));
                    await Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    await Connection.CloseAsync();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public override async Task<object> RangeDateTime(Dictionary<string, string> tables, DateTime start, DateTime end)
        {
            await Connection.OpenAsync();
            StringBuilder _SQL_JOIN = new StringBuilder();
            StringBuilder _SQL_NAMES = new StringBuilder();
            _SQL_NAMES.Append("SELECT pes.id, NomeCompleto, CPF, DataNascimento, pes.DateCadastro,");
            foreach (var item in tables)
            {
                string name = item.Key.ToLower().Substring(0, 4);
                _SQL_JOIN.Append($"INNER JOIN {item.Key} AS {name} ON {name}.Id = pes.{item.Key}_Id ");
                _SQL_NAMES.Append($"{item.Value},");
            }
            _SQL_JOIN.Append($"WHERE pes.DateCadastro BETWEEN '{start.ToString("yyyy-MM-dd")}' AND '{end.ToString("yyyy-MM-dd")}'");
            _SQL_NAMES.Remove(_SQL_NAMES.Length - 1, 1);
            List<object> _List = new List<object>();
            string commandText = _SQL_NAMES + " FROM PessoaModel AS pes " + _SQL_JOIN;
            ClienteModel model;
            using (SqlCommand command = new SqlCommand(commandText, Connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        model = new ClienteModel();

                        model.Id = reader.GetInt32(0);
                        model.NomeCompleto = reader.GetString(1);
                        model.CPF = reader.GetString(2);
                        model.DataNascimento = reader.GetDateTime(3);
                        model.DateCadastro = reader.GetDateTime(4);

                        model.Endereco = new EnderecoModel();

                        model.Endereco.Id = reader.GetInt32(5);
                        model.Endereco.Logradouro = reader.GetString(6);
                        model.Endereco.Bairro = reader.GetString(7);
                        model.Endereco.Cidade = reader.GetString(8);
                        model.Endereco.UF = reader.GetString(9);
                        model.Endereco.CEP = reader.GetString(10);
                        model.Endereco.Complemento = reader.GetString(11);
                        model.Financeiro = new FinanceiroModel(reader.GetInt32(12), reader.GetDecimal(13));
                        _List.Add(model);
                    }
                }
            }
            return _List;


        }

        public override async Task<object> ReadRelation(Dictionary<string, string> tables, int? id)
        {
           
                StringBuilder _SQL_JOIN = new StringBuilder();
                StringBuilder _SQL_NAMES = new StringBuilder();
                _SQL_NAMES.Append("SELECT pes.id, NomeCompleto, CPF, DataNascimento,");
                foreach (var item in tables)
                {
                    string name = item.Key.ToLower().Substring(0, 4);
                    _SQL_JOIN.Append($"INNER JOIN {item.Key} AS {name} ON {name}.Id = pes.{item.Key}_Id ");
                    _SQL_NAMES.Append($"{item.Value},");

                }
                _SQL_NAMES.Remove(_SQL_NAMES.Length - 1, 1);
                List<object> _List = new List<object>();
                string isWhere = id != null ? "WHERE pes.Id=" + id : "";
                string commandText = _SQL_NAMES + " FROM PessoaModel AS pes " + _SQL_JOIN + isWhere;
                Debug.WriteLine($"SQL:{commandText}");
                await Connection.OpenAsync();
                ClienteModel model = null;
                using (SqlCommand command = new SqlCommand(commandText, Connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        switch (id)
                        {
                            case null:
                                while (await reader.ReadAsync())
                                {
                                    model = new ClienteModel();
                                    model.Id = reader.GetInt32(0);
                                    model.NomeCompleto = reader.GetString(1);
                                    model.CPF = reader.GetString(2);
                                    model.DataNascimento = reader.GetDateTime(3);

                                    model.Endereco = new EnderecoModel();

                                    model.Endereco.Id = reader.GetInt32(4);
                                    model.Endereco.Logradouro = reader.GetString(5);
                                    model.Endereco.Bairro = reader.GetString(6);
                                    model.Endereco.Cidade = reader.GetString(7);
                                    model.Endereco.UF = reader.GetString(8);
                                    model.Endereco.CEP = reader.GetString(9);
                                    model.Endereco.Complemento = reader.GetString(10);
                                    model.Financeiro = new FinanceiroModel(reader.GetInt32(11), reader.GetDecimal(12));
                                    _List.Add(model);
                                }

                                break;

                            case not null:

                            if (await reader.ReadAsync())
                            {

                                model = new ClienteModel();

                                model.Id = reader.GetInt32(0);
                                model.NomeCompleto = reader.GetString(1);
                                model.CPF = reader.GetString(2);
                                model.DataNascimento = reader.GetDateTime(3);

                                model.Endereco = new EnderecoModel();

                                model.Endereco.Id = reader.GetInt32(4);
                                model.Endereco.Logradouro = reader.GetString(5);
                                model.Endereco.Bairro = reader.GetString(6);
                                model.Endereco.Cidade = reader.GetString(7);
                                model.Endereco.UF = reader.GetString(8);
                                model.Endereco.CEP = reader.GetString(9);
                                model.Endereco.Complemento = reader.GetString(10);
                                model.Financeiro = new FinanceiroModel(reader.GetInt32(11), reader.GetDecimal(12));

                            }
                            else
                            {
                                throw new SqlServiceException($"Client with id {id} doenst exit.");
                            }
                            return model;

                    }
                }
                }
                return _List;
            
                
               // throw new CustomException($"Client with id {id} doenst exit.");
            
        }
    }
}
