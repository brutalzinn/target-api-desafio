using api_target_desafio.Config;
using api_target_desafio.Models.Plans;
using api_target_desafio.Services;
using api_target_desafio.SqlConnector.Connectors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static api_target_desafio.Services.IBGEService;
namespace api_target_desafio.Controllers
{

    [ApiController]
    [Route("plano")]
    [Produces("application/json")]
    public class PlanoController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public string connStr = String.Empty;
        private PlanSqlConnector PlanConnector { get; set; } = new PlanSqlConnector();

        public PlanoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            Configuration = configuration;
            connStr = Configuration.GetConnectionString("app_target_api");
            PlanConnector.Config(connStr);
        }

        /// <summary>
        /// Exibir quantidade de usuários compatíveis com o benefício de VIP.
        ///  Exibir quantidade de usuários com o benefício de VIP.
        ///  Exibir quantidade de usuários que são VIPS.
        /// </summary>
        /// <returns>Retorna a quantidade de usuários que não são compatíveis com o VIP</returns>
        [HttpGet("detalhe")]
        public IActionResult PlanoInfo(int? id)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidatorException(ModelState);
            }
            return Ok(PlanService.GetPlanoInfo(PlanConnector, id));
        }

        /// <summary>
        /// Exibir detalhe de um determinado plano vip.
        /// </summary>
        /// <returns>Retorna uma lista de planos ou um único plano</returns>

        [HttpGet("vip/{id?}")]
        public IActionResult Get(int? id)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidatorException(ModelState);
            }
            return Ok(PlanService.GetPlano(PlanConnector, id));
        }
        /// <summary>
        /// Rota utilizada para o cliente confirmar o uso do plano VIP
        /// </summary>
        /// <returns>Retorna um feedback da api.</returns>
        [HttpPost("manage")]
        public IActionResult PlanoManager(PlanModel instance)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidatorException(ModelState);
            }
            return Ok(PlanService.PlanoManager(PlanConnector, instance));
        }
    }
}
