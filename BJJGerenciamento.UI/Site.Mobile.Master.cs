using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BJJGerenciamento.UI
{
    public partial class Site_Mobile : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuario = Session["UsuarioLogado"]?.ToString() ?? "Usu�rio";
                hlUsuarioLogado.Text = $"Ol�, {usuario}!";
                // Link para a p�gina de edi��o, por exemplo passando o usu�rio como par�metro
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