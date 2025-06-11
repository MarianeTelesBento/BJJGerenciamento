using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BJJGerenciamento.UI
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["UsuarioLogado"]?.ToString() ?? "Usuário";
                hlUsuarioLogado.Text = $"Olá, {usuario}!";
                // Link para a página de edição, por exemplo passando o usuário como parâmetro
                hlUsuarioLogado.NavigateUrl = $"~/EditarUsuario.aspx?usuario={usuario}";

                hlEditarUsuario.NavigateUrl = $"~/EditarUsuario.aspx?usuario={usuario}";
            }
        }
        protected void btnSair_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }

}