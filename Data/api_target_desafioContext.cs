using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api_target_desafio;

namespace api_target_desafio.Data
{
    public class api_target_desafioContext : DbContext
    {
        public api_target_desafioContext (DbContextOptions<api_target_desafioContext> options)
            : base(options)
        {
        }

        public DbSet<api_target_desafio.PessoaModel> PessoaModel { get; set; }
    }
}
