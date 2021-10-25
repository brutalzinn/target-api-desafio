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
using api_target_desafio.Validators;

namespace api_target_desafio.Services
{
    public static class VIACepService
    {
        public static async Task<CEPModel> GetCep(string cep)
        {
            try
            {
                HttpClient client = new HttpClient();
                cep = CepValidator.Salinizator(cep);
                HttpResponseMessage response = await client.GetAsync($"http://viacep.com.br/ws/{cep}/json/");
                var json = await response.Content.ReadAsStringAsync();
                CEPModel result = JsonSerializer.Deserialize<CEPModel>(json);
                return result;

            }
            catch (Exception)
            {
                return null;
            }
        }
       
    }
}
