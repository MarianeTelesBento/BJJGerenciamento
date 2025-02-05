using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using RestSharp;

using BJJGerenciamento.UI.Models;

namespace BJJGerenciamento.UI.Service
{
    public class CepService
    {
        private readonly string _apiKey = "17736|zxEDycFm2NHmnr5Q3v4oK5MDZOTRBdSA";
        private readonly RestClient _client = new RestClient("https://api.invertexto.com/v1/");

        public EnderecoModels GetEndereco(string cep)
        {
            var request = new RestRequest($"/cep/{cep}?token={_apiKey}", Method.Get);
            request.AddHeader("Accept", "application/json");

            var response = _client.Get(request);
            if (response.IsSuccessful)
            {
                var apiResponse = JsonConvert.DeserializeObject<EnderecoModels>(response.Content);
                return apiResponse;
            }

            throw new Exception($"Erro ao tentar obter dados: {response.StatusCode} - {response.Content}");

        }
    }
}