﻿using api_target_desafio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace api_target_desafio.SqlConnector.Connectors
{        
    
    
    //WE NEED REFACTOR ALL QUERY BUILDERS TWO DAYS BEFORE DISPATCH THE CHALLENGER

    public class PessoaSqlConnector : SqlAbstract
    {
  
        public PessoaSqlConnector()
        {

        }
        public bool CheckRenda()
        {
            return false;
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
            try
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
            }catch(Exception e)
            {
                return false;
            }
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
            string isWhere = id != null ? "WHERE pes.Id=" +id : "";
            string commandText = _SQL_NAMES + " FROM PessoaModel AS pes " + _SQL_JOIN + isWhere;
            Debug.WriteLine($"SQL:{commandText}");
            Connection.Open();
            PessoaModel model;
            using (SqlCommand command = new SqlCommand(commandText, Connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    switch (id)
                {
                    case null:
                            while (reader.Read())
                            {
                                    model = new PessoaModel();

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
                                    model.Financeiro = new FinanceiroModel(reader.GetDecimal(11));                                                      
                                    _List.Add(model);
                            }

                            break;

                    case not null:
                       
                            reader.Read();

                            model = new PessoaModel();

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
                            model.Financeiro = new FinanceiroModel(reader.GetDecimal(11)); 
                            
                            return model;

                }
                
              
                    

                 
                }
            }
            return _List;

        }
    }
}
