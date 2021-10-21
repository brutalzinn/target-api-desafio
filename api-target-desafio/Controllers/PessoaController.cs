using api_target_desafio.Services;
using api_target_desafio.SqlConnector.Connectors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private PessoaSqlConnector PessoaConnector { get; set; } = new PessoaSqlConnector();

        public string connStr = String.Empty;
        public  PessoaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            Configuration = configuration;
            connStr = Configuration.GetConnectionString("app_target_api");
            PessoaConnector.Config(connStr);


        }



        [HttpPost]
        public IActionResult Post(PessoaModel pessoa)
        {
            if (!ModelState.IsValid)
            {
               return BadRequest(new { Cadastrado = false, });
                // return Page();
            }

            PessoaConnector.CheckValidation();
    

           return Ok(PessoaService.RegisterPessoa(PessoaConnector, pessoa));
            //Response.StatusCode = StatusCodes.Status400BadRequest;




        }

        [HttpGet("{id?}")]
        public IActionResult Get(int? id)
        {           
            return Ok(PessoaService.GetPessoa(PessoaConnector, id));
        }

        [HttpGet("relation/{id?}")]
        public IActionResult RelationGet(int? id)
        {
   
            return Ok(PessoaService.GetPessoaRelation(PessoaConnector,id));
        }





    }
}