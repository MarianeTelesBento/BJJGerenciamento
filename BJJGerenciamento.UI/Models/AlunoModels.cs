﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;

namespace BJJGerenciamento.UI.Models
{
    public class AlunoModels
    {
        public int IdAlunos { get; set; }
        public int IdTurma { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public bool EstadoMatricula { get; set; }
        public string Telefone { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
    }
}