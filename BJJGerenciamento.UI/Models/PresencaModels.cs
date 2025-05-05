using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BJJGerenciamento.UI.Models
{
	public class PresencaModels
	{
		public PresencaModels() {
            Data = DateTime.Now;
            StatusPresenca = true;
        }
        public int IdPresenca { get; set; }
        public int IdMatricula { get; set; }
        public int IdProfessor { get; set; }
        public bool StatusPresenca { get; set; }
        public int IdSala { get; set; }
        public DateTime Data { get; set; }


      public PresencaModels(int idPresenca, int idMatricula, int idProfessor, bool statusPresenca, int idSala)
      {
            IdPresenca = idPresenca;
            IdMatricula = idMatricula;
            IdProfessor = idProfessor;
            StatusPresenca = statusPresenca;
            IdSala = idSala;
            Data = DateTime.Now;
            StatusPresenca = true;
        }   

    }
}