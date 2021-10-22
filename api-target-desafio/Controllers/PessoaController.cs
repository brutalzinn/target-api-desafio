using api_target_desafio.Responses;
using api_target_desafio.Services;
using api_target_desafio.SqlConnector.Connectors;
using api_target_desafio.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace api_target_desafio.Controllers
{

    [ApiController]
    [Route("user")]
    [Produces("application/json")]

    public class PessoaController : ControllerBase
    {
        // TODO: WE NEED TO REFACTOR THIS CONTROLLER TO USE SERVICES.
        public IConfiguration Configuration { get; }
        public string connStr = String.Empty;

        private PessoaSqlConnector PessoaConnector { get; set; } = new PessoaSqlConnector();
        public  PessoaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            Configuration = configuration;
            connStr = Configuration.GetConnectionString("app_target_api");
            PessoaConnector.Config(connStr);
        }
        [HttpPost]
    
        public IActionResult Post(PessoaModel pessoa)
        {                  
           var ServiceQuery = PessoaService.RegisterPessoa(PessoaConnector, pessoa);
            switch (ServiceQuery.Error)
            {
                case true:
                    return BadRequest(ServiceQuery);
                case false:
                    return Ok(ServiceQuery);
            }
        }

        [HttpGet("{id?}")]
        public IActionResult Get(int? id)
        {
         return Ok(PessoaService.GetPessoaRelation(PessoaConnector, id));

        }

        [HttpPut("{id}")]
        public IActionResult Update(PessoaModel pessoa,int id)
        {
          
            return Ok(PessoaService.Update(PessoaConnector, pessoa, id));

        }

        [HttpGet("list/{datestart}/{dateend}")]
        public IActionResult CompareDate(DateTime datestart, DateTime dateend)
        {
            return Ok(PessoaService.CompareDates(PessoaConnector, datestart, dateend));
    

        }



    }
}