using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;

namespace BJJGerenciamento.UI.Models
{
    public class MatriculaModels
    {
        public int IdMatricula { get; set; }
        public int IdPlano { get; set; }
        public bool StatusMatricula { get; set; }
        public DateTime DataMatricula { get; set; }

    }
}