using BJJGerenciamento.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;

namespace BJJGerenciamento.UI
{
	public partial class Home : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                int ativos = AlunosDAL.ObterQuantidadeAtivos();
                int inativos = AlunosDAL.ObterQuantidadeInativos();
                List<AlunoModels> aniversariantes = AlunosDAL.ObterAniversariantesDoMes();

                lblAtivos.Text = ativos.ToString();
                lblInativos.Text = inativos.ToString();
                rptAniversariantes.DataSource = aniversariantes;
                rptAniversariantes.DataBind();
            }

        }
        
    }
}