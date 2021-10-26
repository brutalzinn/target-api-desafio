using api_target_desafio.Config;
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
    [Route("cliente")]
    [Produces("application/json")]

    public class ClienteController : ControllerBase
    {
        // TODO: WE NEED TO REFACTOR THIS CONTROLLER TO USE SERVICES.
        public IConfiguration Configuration { get; }
        public string connStr = String.Empty;

        private ClienteSqlConnector PessoaConnector { get; set; } = new ClienteSqlConnector();
        public  ClienteController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            Configuration = configuration;
            connStr = Configuration.GetConnectionString("app_target_api");
            PessoaConnector.Config(connStr);
        }

        /// <summary>
        /// Cadastro de cliente.
        /// </summary>
        /// <response code="200">Retorna um feedback sobre o cliente cadastrado.</response>
        /// <remarks>
        /// Exemplo de resposta(200) - cliente com renda superior ou igual a 6000 reais:
        ///
        ///     POST /user
        ///     {
        ///        "oferecerPlanoVip": true,
        ///        "cadastrado": "true",
        ///        "error": false
        ///     }
        ///     
        ///    Cliente com renda inferior a 6000 reais
        ///
        ///     POST /user
        ///     {
        ///        "oferecerPlanoVip": false,
        ///        "cadastrado": "true",
        ///        "error": false
        ///     }
        ///
        ///     Cliente não conseguiu efetuar o cadastro.
        /// Exemplo de resposta(400) :
        ///
        ///     POST /user
        ///     {
        ///        "oferecerPlanoVip": false,
        ///        "cadastrado": "false",
        ///        "error": true
        ///     }
        ///
        /// </remarks>
        /// <response code="400">Retorna um feedback sobre um cliente que não conseguiu efetuar o cadastro.</response>    
        /// <returns>Um novo cliente cadastrado</returns>
        [HttpPost]
        public IActionResult Post(ClienteModel pessoa)
        {                  
           var ServiceQuery = ClienteService.RegisterPessoa(PessoaConnector, pessoa);
            if (!ModelState.IsValid)
            {
                throw new ModelValidatorException(ModelState);
            }
            switch (ServiceQuery.Error)
            {
                case true:
                    return BadRequest(ServiceQuery);
                case false:
                    return Ok(ServiceQuery);
            }
        }




        /// <summary>
        /// Listar clientes ou exibir apenas um único cliente.
        /// </summary>
        /// <response code="200">Retorna uma lista de clientes ou um cliente único.</response>
        /// <remarks>
        ///Exemplo de resposta(200) - cliente renda superior ou igual a 6000 reais:
        ///
        ///    GET /user/1
        ///    
        ///    {
        ///    "id": 1,
        ///    "nomeCompleto": "Fernando Paes",
        ///    "cpf": "177.811.940-98",
        ///    "dataNascimento": "2019-01-06T02:00:00Z",
        ///    "dateCadastro": "0001-01-01T02:00:00Z",
        ///    "dateModificado": "0001-01-01T02:00:00Z",
        ///    "endereco": {
        ///        "id": 1,
        ///        "logradouro": "ARSO 23 Alameda 4",
        ///        "bairro": "Plano Diretor Sul",
        ///        "cidade": "Palmas",
        ///        "uf": "TO",
        ///        "cep": "770153-14",
        ///        "complemento": "Complemento"
        ///     },
        ///     "financeiro": {
        ///         "id": 1,
        ///        "rendaMensal": 350.50
        ///      }
        ///      }
        ///
        ///    GET /user
        ///    
        ///    [{
        ///    "id": 1,
        ///    "nomeCompleto": "Fernando Paes",
        ///    "cpf": "177.811.940-98",
        ///    "dataNascimento": "2019-01-06T02:00:00Z",
        ///    "dateCadastro": "0001-01-01T02:00:00Z",
        ///    "dateModificado": "0001-01-01T02:00:00Z",
        ///    "endereco": {
        ///        "id": 1,
        ///        "logradouro": "ARSO 23 Alameda 4",
        ///        "bairro": "Plano Diretor Sul",
        ///        "cidade": "Palmas",
        ///        "uf": "TO",
        ///        "cep": "770153-14",
        ///        "complemento": "Complemento"
        ///     },
        ///     "financeiro": {
        ///         "id": 1,
        ///        "rendaMensal": 350.50
        ///      }
        ///      }]
        /// 
        /// </remarks>
        /// <param name="id?">Identificador único do cliente</param>
        /// <response code="400">Retorna um feedback sobre um cliente que não conseguiu efetuar o cadastro.</response>    
        /// <returns>Um novo cliente cadastrado</returns>
        [HttpGet("{id?}")]

        public IActionResult Get(int? id)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidatorException(ModelState);
            }
            return Ok(ClienteService.GetPessoa(PessoaConnector, id));

        }

        /// <summary>
        /// Atualização de cliente.
        /// </summary>
        /// <remarks>
        ///
        /// 
        ///
        /// </remarks>
        /// <response code="400">Retorna um feedback sobre um cliente que não conseguiu efetuar a atualização.</response>    
        [HttpPut("{id}")]
        public IActionResult Update(ClienteModel pessoa,int id)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidatorException(ModelState);
            }
            return Ok(ClienteService.Update(PessoaConnector, pessoa, id));

        }


        /// <summary>
        /// Listar clientes cadastrados por um período de datas
        /// </summary>
        /// <returns>Uma lista de clientes filtrados por data de cadastro.</returns>
        [HttpGet("date/{datestart}/{dateend}")]
        public IActionResult CompareDate(DateTime datestart, DateTime dateend)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidatorException(ModelState);
            }
            return Ok(ClienteService.CompareDates(PessoaConnector, datestart, dateend));
        }

     



    }
}