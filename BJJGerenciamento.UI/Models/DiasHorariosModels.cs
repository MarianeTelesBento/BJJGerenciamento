using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BJJGerenciamento.UI.Models
{
	public class DiasHorariosModels
	{
        public class DiaSemanaModel
        {
            public int IdDia { get; set; }
            public string Dia { get; set; }
        }

        public class HorarioModel
        {
            public int IdHora { get; set; }
            public TimeSpan HorarioInicio { get; set; }
            public TimeSpan HorarioFim { get; set; }
        }
    }
}