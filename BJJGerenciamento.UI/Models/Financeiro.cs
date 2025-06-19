using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BJJGerenciamento.UI.Models
{
    public class Financeiro
    {
        public class FinanceiroDTO
        {
            public int idPlanoAluno { get; set; }
            public string NomeCompleto { get; set; }
            public int DiaVencimento { get; set; }
            public decimal Mensalidade { get; set; }
            public string Status { get; set; }
        }

    }
}