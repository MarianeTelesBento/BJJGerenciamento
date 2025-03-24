using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;

namespace BJJGerenciamento.UI
{
    public partial class _Default : Page
    {
        public List<AlunoModels> alunosList = new List<AlunoModels>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AlunosDAL alunosDAL = new AlunosDAL();
                alunosList = alunosDAL.VisualizarDados();
                GridView1.DataSource = alunosList;
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

            modalNome.Text = row.Cells[1].Text;
            modalCpf.Text = row.Cells[2].Text;
            modalTelefone.Text = row.Cells[3].Text;
            modalEstadoMatricula.Text = row.Cells[4].Text;

            modalTurma.Text = ((HiddenField)row.FindControl("hfIdTurma")).Value;
            modalEmail.Text = ((HiddenField)row.FindControl("hfEmail")).Value;
            modalRg.Text = ((HiddenField)row.FindControl("hfRg")).Value;
            modalDataNascimento.Text = ((HiddenField)row.FindControl("hfDataNascimento")).Value;
            modalCep.Text = ((HiddenField)row.FindControl("hfCep")).Value;
            modalRua.Text = ((HiddenField)row.FindControl("hfRua")).Value;
            modalBairro.Text = ((HiddenField)row.FindControl("hfBairro")).Value;
            modalCidade.Text = ((HiddenField)row.FindControl("hfCidade")).Value;
            modalEstado.Text = ((HiddenField)row.FindControl("hfEstado")).Value;
            modalNumero.Text = ((HiddenField)row.FindControl("hfNumero")).Value;

            string script = $"<script>abrirModal();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script);
        }

        protected void BtnSalvarAluno_Click(object sender, EventArgs e)
        {
            AlunosDAL alunosDAL = new AlunosDAL();

        }
    }
}