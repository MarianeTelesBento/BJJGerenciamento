using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;

namespace BJJGerenciamento.UI
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();

            if (Session["usuarioLogado"] != null)
            {
                Response.Redirect("~/Home.aspx");
            }

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string senha = txtSenha.Text.Trim();

            LoginDAL dal = new LoginDAL();

            if (dal.ValidarLogin(usuario, senha))
            {
                Session["UsuarioLogado"] = usuario;
                Response.Redirect("Home.aspx"); // <-- ou PaginaInicial.aspx, dependendo do nome
            }
            else
            {
                lblMensagem.Visible = true;
                lblMensagem.Text = "Usuário ou senha inválidos.";
            }
        }

    }
}