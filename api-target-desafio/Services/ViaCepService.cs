using api_target_desafio.Models;
using api_target_desafio.Models.IBGE;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using api_target_desafio.Models.VIACEP;
using System.Diagnostics;

namespace api_target_desafio.Services
{
    public static class VIACepService
    {

        private static string Salinizator(string cpf)
        {
            return Regex.Replace(cpf, @"[^a-z0-9_]+", "");
        }

      

        public static async Task<CEPModel> GetCep(string cep)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync($"http://viacep.com.br/ws/{Salinizator(cep)}/json/");
                var json = await response.Content.ReadAsStringAsync();
                CEPModel result = JsonSerializer.Deserialize<CEPModel>(json);
                return result;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static async Task<bool> CheckCep(EnderecoModel endereco)
        {
            CEPModel ViaCepModel = await GetCep(endereco.CEP);
            if (ViaCepModel == null || endereco == null)
            {
                return false;
            }
            Debug.WriteLine($"{ViaCepModel.logradouro}-{ViaCepModel.cep}-{ViaCepModel.bairro}-{ViaCepModel.uf}");
            if (ViaCepModel == null)
            {
                return false;
            }
            if (ViaCepModel.logradouro == endereco.Logradouro && Salinizator(ViaCepModel.cep) == Salinizator(endereco.CEP) && ViaCepModel.bairro == endereco.Bairro && ViaCepModel.uf == endereco.UF && endereco.Cidade == ViaCepModel.localidade)
            {
                return true;
            }
            return false;

        }
    }
}
