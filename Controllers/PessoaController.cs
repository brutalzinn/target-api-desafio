using api_target_desafio.Utils;
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
        public SqlConnector SqlConnector { get; set; }

        public string connStr = String.Empty;
        public  PessoaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            Configuration = configuration;
           
           
             connStr = Configuration.GetConnectionString("app_target_api");
            SqlConnector = new SqlConnector(new PessoaModel(), connStr);

        }



        [HttpPost]
        public async Task<string> Post(PessoaModel pessoa)
        {



            SqlConnector.Insert(pessoa);
;




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