using api_target_desafio.Models;
using api_target_desafio.Models.IBGE;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace api_target_desafio.Services
{
    public static class IBGEService
    {
        public static async Task<List<IBGEEstadoModel>> GetEstados()
        {
            try
            {
                HttpClient client = new HttpClient();
             
                HttpResponseMessage response = await client.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/estados");
                var json = await response.Content.ReadAsStringAsync();
               List<IBGEEstadoModel> result = JsonSerializer.Deserialize<List<IBGEEstadoModel>>(json);
                return result;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<List<IBGEMunicipioModel>> GetDistritos(string estado)
        {
            try
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/estados/"+ estado + "/municipios");
                var json = await response.Content.ReadAsStringAsync();
                List<IBGEMunicipioModel> result = JsonSerializer.Deserialize<List<IBGEMunicipioModel>>(json);
                return result;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
