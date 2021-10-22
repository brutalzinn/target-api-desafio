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

        [HttpGet("renda/{min}/{max?}")]
        public IActionResult CompareMinimum(decimal min, decimal? max)
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
