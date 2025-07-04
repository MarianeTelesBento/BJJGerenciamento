﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;

namespace BJJGerenciamento.UI.Models
{
    public class AlunoModels : EnderecoModels
    {
        public int IdAlunos { get; set; }
        public int? IdResponsavel { get; set; }
        public string NomeCompleto {
            get => $"{Nome} {Sobrenome}"; 
        }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public string DataNascimento { get; set; }
        public string CarteiraFPJJ { get; set; }
        public int IdMatricula { get; set; }
        public bool StatusMatricula { get; set; }
        public string DataMatricula { get; set; }
        public int TotalPresencas { get; set; }
    }
    public class HoraModel
    {
        public int IdHora { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public TimeSpan HorarioFim { get; set; }
        public bool Ativa { get; set; }

        public string Descricao => $"{HorarioInicio:hh\\:mm} - {HorarioFim:hh\\:mm}";
    }

}