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
    public class PlanController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public string connStr = String.Empty;
        private PlanSqlConnector PlanConnector { get; set; } = new PlanSqlConnector();

        public PlanController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            Configuration = configuration;
            connStr = Configuration.GetConnectionString("app_target_api");
            PlanConnector.Config(connStr);
        }

        /// <summary>
        /// Exibir quantidade de usuários compatíveis com o benefício de VIP.
        /// </summary>
        /// <returns>Retorna a quantidade de usuários compatíveis com o VIP</returns>
        [HttpGet("detail/vip")]
        public IActionResult VipDetail()
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidatorException(ModelState);
            }
            return Ok(PlanService.VipDetail(PlanConnector));
        }
        /// <summary>
        /// Rota utilizada para o cliente confirmar o uso do plano VIP
        /// </summary>
        /// <returns>Uma lista de clientes filtrados por data de cadastro.</returns>
        [HttpPost("manage")]
        public IActionResult VipManager(PlanModel instance)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidatorException(ModelState);
            }
            return Ok(PlanService.VipManager(PlanConnector, instance));
        }
    }
}
