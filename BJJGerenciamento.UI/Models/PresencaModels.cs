using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BJJGerenciamento.UI.Models
{
	public class PresencaModels
	{
		public PresencaModels() { }
        public int IdPresenca { get; set; }
        public int IdAluno { get; set; }
        public int IdProfessor { get; set; }
        public bool StatusPresenca { get; set; }
        public int IdSala { get; set; }
        public DateTime Data { get; set; }


      public PresencaModels(int idPresenca, int idAluno, int idProfessor, bool statusPresenca, int idSala, DateTime data)
      {
            IdPresenca = idPresenca;
            IdAluno = idAluno;
            IdProfessor = idProfessor;
            StatusPresenca = statusPresenca;
            IdSala = idSala;
            Data = data;
      }   

    }
}