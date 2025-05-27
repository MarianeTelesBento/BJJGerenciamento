using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        }
        //protected void btnSair_Click(object sender, EventArgs e)
        //{
        //    Session.Clear();
        //    Response.Redirect("Login.aspx");
        //}
    }
}