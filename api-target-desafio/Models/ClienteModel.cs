using api_target_desafio.Models;
using api_target_desafio.Models.Plans;
using api_target_desafio.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api_target_desafio
{
    public class ClienteModel : IValidatableObject
    {
    public ClienteModel(int id, string nomeCompleto, string cPF, DateTime dataNascimento, EnderecoModel endereco, FinanceiroModel financeiro)
    {
        Id = id;
        NomeCompleto = nomeCompleto;
        CPF = cPF;
        DataNascimento = dataNascimento;
        Endereco = endereco;
        Financeiro = financeiro;
    }
    public ClienteModel()
    {

    }

    [Key]
    public int Id { get; set; }
    
    public string NomeCompleto { get; set; }
    

    public string CPF { get; set; }


    public DateTime DataNascimento { get; set; }

    public DateTime DateCadastro { get; set; }


    public DateTime DateModificado { get; set; }

    public VipModel Vip { get; set; }


    public EnderecoModel Endereco { get; set; }

    public FinanceiroModel Financeiro { get; set; }
  

     public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
      {
            //fun moment to validate fields

            if (!CpfValidator.Validate(CPF))
            {
                yield return new ValidationResult(
                   $"INVALID CPF. PLEASE, REWRITE IT."); 
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
            if(NomeCompleto.Length < 8)
            {
                yield return new ValidationResult(
                  $"PLEASE, PUT YOUR FULL NAME.");
            }
            if(DateTime.Today.Year - DataNascimento.Year  > 120)
            {
                yield return new ValidationResult(
                  $"YOU ARE VERY OLD, DUDE! {DateTime.Today.Year - DataNascimento.Year} YEARS?! Are you a Android?");
            }

            if (DateTime.Today.Year - DataNascimento.Year <= 5)
            {
                yield return new ValidationResult(
                  $"OHHH MY GOD!, ARE YOU A BABY?! {DateTime.Today.Year - DataNascimento.Year} YEARS?! Dude, this system cannot be used by you.");
            }


        }
    }
}
