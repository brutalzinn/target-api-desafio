using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace api_target_desafio.Controllers
{
    [ApiController]
    [Route("user")]
    public class PessoaController : ControllerBase
    {
       
        [HttpPost]
        public string Post()
        {
            Debug.WriteLine("TESTE");
            return "teste post";
        }


        [HttpGet]
        public string Get()
        {
            Debug.WriteLine("TESTE");
            return "teste get";
        }
    }
}
