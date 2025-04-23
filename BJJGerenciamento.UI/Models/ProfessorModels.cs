using System;

namespace BJJGerenciamento.UI.Models
{
    public class ProfessorModels
    {
        public int IdProfessor { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNasc { get; set; }  // Data de nascimento (DateTime)
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string CarteiraFPJJ { get; set; }
        public string CarteiraCBJJ { get; set; }
        public string Numero { get; set; }   // Número da residência (obrigatório)
        public string Complemento { get; set; }  // Complemento da residência (opcional)
        public string Cidade { get; set; }   // Cidade (obrigatório)
        public string Estado { get; set; }
        public bool Ativo { get; set; }
    }
}
