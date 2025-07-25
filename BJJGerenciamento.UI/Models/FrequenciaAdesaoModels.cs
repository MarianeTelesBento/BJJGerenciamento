using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BJJGerenciamento.UI.Models
{


    public class FrequenciaAdesaoModels
    {
        public int IdFrequencia { get; set; }
        public int IdAdesao { get; set; }
        public int QtdDiasPermitidos { get; set; }
        public decimal Mensalidade { get; set; }
    }
}