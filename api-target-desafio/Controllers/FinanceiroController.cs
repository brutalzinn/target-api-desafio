using api_target_desafio.Services;
using api_target_desafio.SqlConnector.Connectors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace api_target_desafio.Controllers
{

    [ApiController]
    [Route("financeiro")]
    [Produces("application/json")]
    public class FinanceiroController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public string connStr = String.Empty;

        private FinanceiroSqlConnector FinanceiroConnector { get; set; } = new FinanceiroSqlConnector();
        public FinanceiroController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            Configuration = configuration;
            connStr = Configuration.GetConnectionString("app_target_api");
            FinanceiroConnector.Config(connStr);
        }


        /// <summary>
        /// Retorna todos os clientes com base em um valor de RendaMensal mínimo ou ambos ao mesmo tempo.
        /// </summary>
        /// <param name="min">Renda mínima</param>
        /// <param name="max" required="false">Renda Máxima(opcional)</param>
        /// <returns>Retorna todos os clientes com base nos valores de mínimo
        /// e máximo de RendaMensal ou apenas com base no valor mínimo de RendaMensal.</returns>
        [HttpGet("renda/{min}/{max?}")]
        public IActionResult CompareMinimum(decimal min, decimal? max = null)
        {
            return Ok(FinanceiroService.CompareMin(FinanceiroConnector, min,max));
        }

        [HttpGet("vips/count")]
        public IActionResult VipsCount()
        {
            return Ok(FinanceiroService.GetVips(FinanceiroConnector));
        }

    }
}
