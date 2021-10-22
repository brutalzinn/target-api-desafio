using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static api_target_desafio.Services.IBGEService;
namespace api_target_desafio.Controllers
{

    [ApiController]
    [Route("geo")]
    [Produces("application/json")]
    public class IBGEController : ControllerBase
    {
        public IConfiguration Configuration { get; }


        public IBGEController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            Configuration = configuration;
           // connStr = Configuration.GetConnectionString("app_target_api");
          
        }

        [HttpGet("uf")]
        public IActionResult Uf()
        {
            return Ok(Task.Run(() => GetEstados()).Result);
        }

        [HttpGet("cidade/{uf}")]
        public IActionResult Distrito(string uf)
        {
            return Ok(Task.Run(() => GetDistritos(uf)).Result);

        }
    }
}
