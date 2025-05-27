using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BJJGerenciamento.UI.Models
{
	public class LoginModels
	{
        public int IdLogin { get; set; }
        public string Usuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}