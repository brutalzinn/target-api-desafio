using api_target_desafio.Config;
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
        }
        /// <summary>
        /// Retorna todos os estados da API do IBGE.
        /// </summary>
        /// <returns>Retorna todos os estados da API do IBGE</returns>
        [HttpGet("uf")]
        public IActionResult Uf()
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidatorException(ModelState);
            }
            return Ok(Task.Run(() => GetEstados()).Result);
        }
        /// <summary>
        /// Retorna todos os municípios de um estado específico da API do IBGE.
        /// </summary>
        /// <param name="uf">Id do estado</param>
        /// <returns>Retorna todos os municípios desse estado.</returns>
        [HttpGet("cidade/{uf}")]
        public IActionResult Distrito(string uf)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidatorException(ModelState);
            }
            return Ok(Task.Run(() => GetDistritos(uf)).Result);

        }
    }
}
