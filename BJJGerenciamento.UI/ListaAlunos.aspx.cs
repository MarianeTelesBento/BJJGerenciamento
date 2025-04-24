using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public static bool VerificarCampos(params TextBox[] campos)
        {
            if (campos.Any(campo => string.IsNullOrWhiteSpace(campo.Text)))
            {
                ScriptManager.RegisterStartupScript(HttpContext.Current.Handler as Page,
                    typeof(Page),
                    "alerta",
                    "alert('Preencha todos os campos obrigatórios!');",
                    true);
                return false;
            }
            return true;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnDetalhes_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int index = row.RowIndex;

            modalIdMatricula.Text = row.Cells[0].Text;

            CheckBox chkStatusMatricula = (CheckBox)row.FindControl("chkStatusMatricula");
            modalStatusMatricula.Checked = chkStatusMatricula.Checked;

            modalNome.Text = row.Cells[1].Text;
            modalSobrenome.Text = row.Cells[2].Text;
            modalCpf.Text = row.Cells[3].Text;
            modalTelefone.Text = row.Cells[4].Text;

            modalEmail.Text = GridView1.DataKeys[index]["Email"].ToString();
            modalDataNascimento.Text = GridView1.DataKeys[index]["DataNascimento"].ToString();
            modalCep.Text = GridView1.DataKeys[index]["Cep"].ToString();
            modalRua.Text = GridView1.DataKeys[index]["Rua"].ToString();
            modalBairro.Text = GridView1.DataKeys[index]["Bairro"].ToString();
            modalCidade.Text = GridView1.DataKeys[index]["Cidade"].ToString();
            modalEstado.Text = GridView1.DataKeys[index]["Estado"].ToString();
            modalNumero.Text = GridView1.DataKeys[index]["NumeroCasa"].ToString();
            modalComplemento.Text = GridView1.DataKeys[index]["Complemento"].ToString();
            modalCarteiraFpjj.Text = GridView1.DataKeys[index]["CarteiraFPJJ"].ToString();
            modalDataMatricula.Text = GridView1.DataKeys[index]["DataMatricula"].ToString();
            ViewState["IdAlunos"] = GridView1.DataKeys[index]["IdAlunos"].ToString();

            string script = $"<script>abrirModal();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script);
        }

        protected void SalvarAluno_Click(object sender, EventArgs e)
        {
            if (VerificarCampos(modalNome, modalSobrenome, modalCpf, modalTelefone, modalRua, modalBairro, modalCidade, modalEstado, modalNumero))
            {
                aluno.IdAlunos = Convert.ToInt32(ViewState["IdAlunos"]);
                aluno.Nome = modalNome.Text;
                aluno.Sobrenome = modalSobrenome.Text;
                aluno.Cpf = modalCpf.Text;
                aluno.Telefone = modalTelefone.Text;
                aluno.Email = modalEmail.Text;
                aluno.DataNascimento = modalDataNascimento.Text;
                aluno.Cep = modalCep.Text;
                aluno.Rua = modalRua.Text;
                aluno.Bairro = modalBairro.Text;
                aluno.Cidade = modalCidade.Text;
                aluno.Estado = modalEstado.Text;
                aluno.NumeroCasa = modalNumero.Text;
                aluno.Complemento = modalComplemento.Text;
                aluno.CarteiraFPJJ = modalCarteiraFpjj.Text;
                aluno.StatusMatricula = modalStatusMatricula.Checked;

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
}