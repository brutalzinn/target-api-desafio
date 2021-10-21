using api_target_desafio.SqlConnector.Connectors;
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

        // TODO: WE NEED TO REFACTOR THIS CONTROLLER TO USE SERVICES.


        public IConfiguration Configuration { get; }
        private PessoaSqlConnector PessoaConnector { get; set; } = new PessoaSqlConnector();

        public string connStr = String.Empty;
        public  PessoaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            Configuration = configuration;
            connStr = Configuration.GetConnectionString("app_target_api");
            PessoaConnector.Config(connStr);


        }



        [HttpPost]
        public string Post(PessoaModel pessoa)
        {
           PessoaConnector.Insert(pessoa);

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