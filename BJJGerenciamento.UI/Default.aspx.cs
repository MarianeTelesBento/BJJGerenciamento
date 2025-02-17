using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;

namespace BJJGerenciamento.UI
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AlunosDAL alunosDAL = new AlunosDAL();
                GridView1.DataSource = alunosDAL.VisualizarDados();
                GridView1.DataBind();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnDetalhes_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string nome = row.Cells[1].Text;
            string cpf = row.Cells[2].Text;
            string email = row.Cells[3].Text;

            string script = $"<script>abrirModal('{nome}', '{cpf}', '{email}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script);
        }
    }
}