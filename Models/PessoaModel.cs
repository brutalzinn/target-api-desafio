using System;
using System.ComponentModel.DataAnnotations;

namespace api_target_desafio
{
    public class PessoaModel
    {
    public int Id { get; set; }
    public string NomeCompleto { get; set; }

    public string CPF { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }

    }
}
