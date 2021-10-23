﻿using api_target_desafio.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static api_target_desafio.Services.IBGEService;
namespace api_target_desafio.Controllers
{

    [ApiController]
    [Route("plan")]
    [Produces("application/json")]
    public class PlanController : ControllerBase
    {
        public IConfiguration Configuration { get; }


        public PlanController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Exibir quantidade de usuários compatíveis com o benefício de VIP.
        /// </summary>
        /// <returns>Retorna a quantidade de usuários compatíveis com o VIP</returns>
        [HttpGet("detail/vip")]
        public IActionResult Uf()
        {
            return Ok(PlanService.VipDetail());
        }

    }
}
