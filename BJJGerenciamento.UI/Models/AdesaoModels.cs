using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BJJGerenciamento.UI.Models
{
    public class AdesaoModels
    {
        public int IdAdesao { get; set; }
        public string NomeAdesao { get; set; }
        public int QtdDiasPermitidos { get; set; }
        public decimal Mensalidade { get; set; }
        public List<int> IdsPlanos
        {
            get; set;
        } = new List<int>();    
    }
}