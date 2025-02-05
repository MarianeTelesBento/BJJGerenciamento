using System;
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

        Nome varchar(25) NOT NULL,
    Sobrenome varchar(50) NOT NULL,
    EstadoMatricula bit NOT NULL,
	Telefone varchar(15) NOT NULL,
    Rg varchar(10) NOT NULL,
    Cpf varchar(11) NOT NULL,
    DataNascimento date NOT NULL,
	CEP varchar(8) NULL,
	Endereco varchar(100) NULL,
	Bairro varchar(50) NULL,
	Numero varchar(10) NOT NULL
    }
}