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
        public int IdAluno { get; set; }
        public int IdTurma { get; set; }
        public int IdMatricula { get; set; }
        public int? IdPresenca { get; set; }
        public int? IdAulaAvulsa { get; set; }
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
    }
}