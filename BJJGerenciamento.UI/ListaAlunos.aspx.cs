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
        public AlunoModels aluno = new AlunoModels();

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

            modalIdAluno.Text = row.Cells[0].Text;
            modalNome.Text = row.Cells[1].Text;
            modalSobrenome.Text = row.Cells[2].Text;
            modalCpf.Text = row.Cells[3].Text;
            modalTelefone.Text = row.Cells[4].Text;

            modalEmail.Text = ((HiddenField)row.FindControl("hfEmail")).Value;
            modalRg.Text = ((HiddenField)row.FindControl("hfRg")).Value;
            modalDataNascimento.Text = ((HiddenField)row.FindControl("hfDataNascimento")).Value;
            modalCep.Text = ((HiddenField)row.FindControl("hfCep")).Value;
            modalRua.Text = ((HiddenField)row.FindControl("hfRua")).Value;
            modalBairro.Text = ((HiddenField)row.FindControl("hfBairro")).Value;
            modalCidade.Text = ((HiddenField)row.FindControl("hfCidade")).Value;
            modalEstado.Text = ((HiddenField)row.FindControl("hfEstado")).Value;
            modalNumero.Text = ((HiddenField)row.FindControl("hfNumero")).Value;
            modalComplemento.Text = ((HiddenField)row.FindControl("hfComplemento")).Value;
            modalCarteiraFpjj.Text = ((HiddenField)row.FindControl("hfCarteiraFpjj")).Value;

            string script = $"<script>abrirModal();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script);
        }

        protected void SalvarAluno_Click(object sender, EventArgs e)
        {
            aluno.IdAlunos = Convert.ToInt32(modalIdAluno.Text);
            aluno.Nome = modalNome.Text;
            aluno.Sobrenome = modalSobrenome.Text;
            aluno.Cpf = modalCpf.Text;
            aluno.Telefone = modalTelefone.Text;
            aluno.Email = modalEmail.Text;
            aluno.Rg = modalRg.Text;
            aluno.DataNascimento = modalDataNascimento.Text;
            aluno.Cep = modalCep.Text;
            aluno.Rua = modalRua.Text;
            aluno.Bairro = modalBairro.Text;
            aluno.Cidade = modalCidade.Text;
            aluno.Estado = modalEstado.Text;
            aluno.NumeroCasa = modalNumero.Text;
            aluno.Complemento = modalComplemento.Text;
            aluno.CarteiraFPJJ = modalCarteiraFpjj.Text;

            AlunosDAL alunosDAL = new AlunosDAL();

            bool funcionou = alunosDAL.AtualizarAluno(aluno);

            if (funcionou)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Aluno atualizado com sucesso!');", true);
                alunosList = alunosDAL.VisualizarDados();
                GridView1.DataSource = alunosList;
                GridView1.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Erro ao atualizar aluno. Tente novamente!');", true);
            }
        }
    }
}