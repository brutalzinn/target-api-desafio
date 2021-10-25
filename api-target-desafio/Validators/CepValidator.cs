using api_target_desafio.Models;
using api_target_desafio.Models.Errors;
using api_target_desafio.Models.VIACEP;
using api_target_desafio.Services;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace api_target_desafio.Validators
{
    public static class CepValidator { 
    
        public static string Salinizator(string cpf)
        {
            return Regex.Replace(cpf, @"[^a-z0-9_]+", "");
        }

        public static string SalinitizeAccents(string logradouro)
        {
            logradouro = logradouro.ToLower();
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
            for (var i = 0; i < logradouro.Length; i++)
            {
                int t = comAcentos.IndexOf(logradouro[i]);
                if (t > 0)
                {
                    logradouro = logradouro.Replace(logradouro[i], semAcentos[t]);
                }
            }
            return logradouro;
        }
        private static CepModelError Validade(CEPModel viacep, EnderecoModel Endereco)
        {
            viacep.logradouro = SalinitizeAccents(viacep.logradouro);
            Endereco.Logradouro = SalinitizeAccents(Endereco.Logradouro);
            viacep.bairro = SalinitizeAccents(viacep.bairro);
            Endereco.Bairro = SalinitizeAccents(Endereco.Bairro);
            viacep.uf = SalinitizeAccents(viacep.uf);
            Endereco.UF = SalinitizeAccents(Endereco.UF);
            viacep.localidade = SalinitizeAccents(viacep.localidade);
            Endereco.Cidade = SalinitizeAccents(Endereco.Cidade);

            bool logradouro = viacep.logradouro == Endereco.Logradouro;
            bool bairro = viacep.bairro == Endereco.Bairro;
            bool uf = viacep.uf == Endereco.UF;
            bool cidade = viacep.localidade == Endereco.Cidade;
            return new CepModelError(logradouro,bairro,uf,cidade);

        }

        public static async Task<object> CheckCep(EnderecoModel endereco)
        {
            if(endereco == null)
            {
                return null;
            }
            CEPModel ViaCepModel = await VIACepService.GetCep(endereco.CEP);
            if (ViaCepModel == null || ViaCepModel.erro)
            {
                return null;
            }

            return Validade(ViaCepModel, endereco);

        }
    }
}
