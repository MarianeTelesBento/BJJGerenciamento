using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;

namespace BJJGerenciamento.UI.Models
{
    public class PlanoAlunoModels 
    {
        public int idPlanoAluno { get; set; }
        public int idAlunos { get; set; }
        public int idDia { get; set; }
        public int idHorario { get; set; }
        public int idDetalhe { get; set; }
        public int idPlanoAlunoValor { get; set; }
        public int qtdDias { get; set; }
        public decimal mensalidade { get; set; }
        public string horarioInicio { get; set; }
        public string horarioFim { get; set; }
        public bool passeLivre { get; set; }
        public int DiaVencimento { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime? DataProximaCobranca { get; set; }



    }
}