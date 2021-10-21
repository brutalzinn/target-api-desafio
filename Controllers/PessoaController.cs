using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace api_target_desafio.Controllers
{
    [ApiController]
    [Route("user")]
    public class PessoaController : ControllerBase
    {


        public IConfiguration Configuration { get; }

        public string connStr = String.Empty;
        public  PessoaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            Configuration = configuration;
           
           
             connStr = Configuration.GetConnectionString("app_target_api");
            
        }
  


        [HttpPost]
        public async Task<string> Post(PessoaModel pessoa)
        {
            string commandText = "INSERT INTO PessoaModel (NomeCompleto,CPF,DataNascimento) VALUES (@NOMECOMPLETO,@CPF,@DATANASCIMENTO)";
            using (var connection = new SqlConnection(connStr))
            {
          
               
                    using (var command = new SqlCommand(commandText, connection))
                    {
                        try
                        {
                            command.Parameters.Add(new SqlParameter("@NOMECOMPLETO", pessoa.NomeCompleto));
                            command.Parameters.Add(new SqlParameter("@CPF", pessoa.CPF));
                            command.Parameters.Add(new SqlParameter("@DATANASCIMENTO", pessoa.DataNascimento));
                            await connection.OpenAsync();
                            await command.ExecuteNonQueryAsync();
                        }
                        catch (Exception e)
                        {
                            return "ERROR";
                        }


                        }
                
                           
                
            }
            return "OK";
               
        }


        [HttpGet]
        public async Task<string> Get(PessoaModel pessoa)
        {
            Debug.WriteLine("TESTE");
           
            return "";
        }
    }
}