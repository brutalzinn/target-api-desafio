using api_target_desafio.Models;
using api_target_desafio.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api_target_desafio
{
    public class PessoaModel : IValidatableObject
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
  

     public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
      {

            if (!CpfValidator.Validate(CPF))
            {
                yield return new ValidationResult(
                   $"INVALID CPF. PLEASE, REWRITE."); 
            }
            if (Financeiro == null)
            {
                yield return new ValidationResult(
                  $"NO FINANCES DATA DETECTED ON YOUR BODY REQUEST.");
            }
            if (Endereco == null)
            {
                yield return new ValidationResult(
                  $"NO GEOLOCALIZATION DATA DETECTED ON YOUR BODY REQUEST.");
            }
            if (Endereco == null)
            {
                yield return new ValidationResult(
                  $"NO GEOLOCALIZATION DATA DETECTED ON YOUR BODY REQUEST.");
            }
            if(NomeCompleto.Length < 8)
            {
                yield return new ValidationResult(
                  $"PLEASE, PUT YOUR FULL NAME.");
            }

        }
    }
}
