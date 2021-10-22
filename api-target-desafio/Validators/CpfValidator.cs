using api_target_desafio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace api_target_desafio.Validators
{
    public static class CpfValidator
    {
        private static int CheckDivision(int value)
        {
            int result = value % 11;

            if (result < 2)
            {
                return 0;
            }
            else if (result >= 2)
            {
                return 11 - result;
            }
            return 0;
        }
        private static int CalcPeso(string CPF, int Peso)
        {
            int Calc = 0;
            foreach (var digit in CPF)
            {
                Calc += (int)Char.GetNumericValue(digit) * Peso;

                if (Peso == 2)
                {

                    break;
                }
                Peso--;
            }
            return Calc;
        }
        private static bool CheckCpf(int Vone, int VTwo, string CPF)
        {
            int VerifierOne = (int)Char.GetNumericValue(CPF[CPF.Length - 2]);
            int VerifierTwo = (int)Char.GetNumericValue(CPF[CPF.Length - 1]);

            if (VerifierOne == Vone && VerifierTwo == VTwo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static string Salinizator(string cpf)
        {
            return Regex.Replace(cpf, @"[^a-z0-9_]+", "");
        }
        private static bool ValidFormat(string cpf) {
            bool valid = false;
            for(var i = 0; i < cpf.Length; i++)
            {
                if (i < cpf.Length - 1 && cpf[i] == cpf[i+1])
                {
                    valid = false;
                }
                else if(i < cpf.Length - 1 && cpf[i] != cpf[i + 1])
                {
                    valid = true;
                }
                
            }

            return valid;
        }
       public static bool Validate(string cpf)
        {
            string CPFOriginal = Salinizator(cpf);
            int index = CPFOriginal.Length - 2;
            if (index < 0)
                return false;
            string CPF = CPFOriginal.Substring(0, CPFOriginal.Length - 2);

            if (!ValidFormat(CPF))
            {
                return false;
            }
            int Calc;
            int FDigit;
            int SDigit;
            Calc = CalcPeso(CPF, 10);
            FDigit = CheckDivision(Calc);
            Calc = CalcPeso($"{CPF}{FDigit}", 11);
            SDigit = CheckDivision(Calc);
            return CheckCpf(FDigit, SDigit, CPFOriginal);
        }
      
    }
}
