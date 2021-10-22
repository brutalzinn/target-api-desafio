using api_target_desafio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace api_target_desafio.SqlConnector.Connectors
{
    public class FinanceiroSqlConnector : SqlAbstract
    {
    
        public FinanceiroSqlConnector(string conn)
        {
            Config(conn);
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

        public override async Task<bool> Update(object body,int id)
        {
            try
            {
                StringBuilder SQL_BUILDER = new StringBuilder();
                SQL_BUILDER.Append("UPDATE FinanceiroModel SET ");
                if (body is FinanceiroModel pessoaInstance)
                {
                    SQL_BUILDER.Append($"RendaMensal = {pessoaInstance.RendaMensal} ");
                }
                SQL_BUILDER.Append($"WHERE Id = {id}");
                SqlCommand command = new SqlCommand(SQL_BUILDER.ToString(), Connection);
                await Connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await Connection.CloseAsync();
                return true;
            }catch(Exception e)
            {
                return false;
            }
           
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
