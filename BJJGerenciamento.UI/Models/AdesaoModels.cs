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
        public List<int> IdsPlanos { get; set; } = new List<int>();

        
        public List<FrequenciaAdesaoModels> Frequencias { get; set; } = new List<FrequenciaAdesaoModels>();

        public string FrequenciasTexto
        {
            get
            {
                if (Frequencias == null || Frequencias.Count == 0)
                    return "";

                return string.Join(", ", Frequencias.Select(f => $"{f.QtdDiasPermitidos}x: R$ {f.Mensalidade.ToString("N2")}"));
            }
        }
        public bool IsVip { get; set; }
        public decimal ValorVip { get; set; }


    }

}