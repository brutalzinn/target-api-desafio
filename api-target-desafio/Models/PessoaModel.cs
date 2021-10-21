using api_target_desafio.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace api_target_desafio
{
    public class PessoaModel
    {
        //public PessoaModel()
        //{
        //    Endereco = new EnderecoModel();
        //    Financeiro = new FinanceiroModel();
        //}
    [Key]
    public int Id { get; set; }
    public string NomeCompleto { get; set; }

    public string CPF { get; set; }


    public DateTime DataNascimento { get; set; }

    public EnderecoModel Endereco { get; set; }

    public FinanceiroModel Financeiro { get; set; }


    }
}
