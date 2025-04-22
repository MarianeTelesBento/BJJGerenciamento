using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;

namespace BJJGerenciamento.UI.Models
{
    public class PlanoHorarioModels 
    {
        public int idHora { get; set; }
        public string horarioInicio { get; set; }
        public string horarioFim { get; set; }
    }
}