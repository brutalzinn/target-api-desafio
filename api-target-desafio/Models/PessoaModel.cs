using api_target_desafio.Models;
using api_target_desafio.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace api_target_desafio
{
    public class PessoaModel : BaseModel
    {
    public PessoaModel(int id, string nomeCompleto, string cPF, DateTime dataNascimento, EnderecoModel endereco, FinanceiroModel financeiro)
    {
        Id = id;
        NomeCompleto = nomeCompleto;
        CPF = cPF;
        DataNascimento = dataNascimento;
        Endereco = endereco;
        Financeiro = financeiro;
    }
    public PessoaModel()
    {

    }

    [Key]
    public int Id { get; set; }
    public string NomeCompleto { get; set; }

    public string CPF { get; set; }


    public DateTime DataNascimento { get; set; }

    public EnderecoModel Endereco { get; set; }

    public FinanceiroModel Financeiro { get; set; }
     public override string GetName()
     {
            return "PessoaModel";
     }

     public override bool Validator(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary model)
      {
           return PessoaValidator.Validate(this,model);
           
     }
    }
}
