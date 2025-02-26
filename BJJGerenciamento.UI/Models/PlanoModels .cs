using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;

namespace BJJGerenciamento.UI.Models
{
    public class PlanoModels 
    {
        public int IdPlano { get; set; }
        public string Nome { get; set; }
        public int QtdDias { get; set; }
        public decimal Mensalidade { get; set; }
    }
}