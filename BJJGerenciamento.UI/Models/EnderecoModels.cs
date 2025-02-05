using Newtonsoft.Json;

namespace BJJGerenciamento.UI.Models
{
    public class EnderecoModels
    {
        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("logradouro")]
        public string Endereco { get; set; }

        [JsonProperty("neighborhood")]
        public string Bairro { get; set; }

        [JsonProperty("numero")]
        public string Numero { get; set; }

        [JsonProperty("state")]
        public string Estado { get; set; }

        [JsonProperty("city")]
        public string Cidade { get; set; }

        [JsonProperty("street")]
        public string Rua { get; set; }

        [JsonProperty("complement")]
        public string Complemento { get; set; }

        [JsonProperty("ibge")]
        public string IBGE { get; set; }
    }
}
