﻿using api_target_desafio.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace api_target_desafio.SqlConnector.Connectors
{
    public class FinanceiroSqlConnector : SqlAbstract
    {
    
        public FinanceiroSqlConnector()
        {

        }
        public override async Task<int> InsertRelation(object model)
        {

            if (model is FinanceiroModel financeiroInstance)
            {
                string commandText = "INSERT INTO FinanceiroModel (RendaMensal) output INSERTED.Id VALUES (@RENDAMENSAL)";
                SqlCommand command = new SqlCommand(commandText, Connection);

                command.Parameters.Add(new SqlParameter($"@RENDAMENSAL", financeiroInstance.RendaMensal));
        
                await Connection.OpenAsync();
                int modified = (int)command.ExecuteScalar();

               await Connection.CloseAsync();
                return modified;
            }
            return 0;
        }

        public override async Task<bool> Insert(object model)
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
                await Connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await  Connection.CloseAsync();
                return true;
            }
            return false;
        }

      

      
    }
}
