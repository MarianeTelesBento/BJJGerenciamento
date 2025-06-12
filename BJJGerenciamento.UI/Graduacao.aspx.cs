using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BJJGerenciamento.UI
{
	public partial class Graduacao : System.Web.UI.Page
	{

        public List<AlunoModels> alunosList = new List<AlunoModels>();
        public AlunoModels aluno = new AlunoModels();

        protected void Page_Load(object sender, EventArgs e)
        {
            int idMatriculaUrl = -1;

            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                AlunosDAL alunosDAL = new AlunosDAL();
                alunosList = alunosDAL.VisualizarAlunosPresencas();
                GridView1.DataSource = alunosList;
                GridView1.DataBind();

                if (Request.QueryString["idMatricula"] != null)
                {
                    if (int.TryParse(Request.QueryString["idMatricula"], out idMatriculaUrl))
                    {
                        string script = $"<script type='text/javascript'>highlightAndScrollToRow({idMatriculaUrl});</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "HighlightRow", script, false);
                    }
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (GridView1.DataKeys[e.Row.RowIndex] != null)
                {
                    string idMatricula = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                    e.Row.Attributes["data-matricula-id"] = idMatricula;
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnFiltro_Click(object sender, EventArgs e)
        {

            if (ddPlanos.Visible == false)
            {
                btnLimpar.Visible = true;
                btnPesquisar.Visible = true;
                TxtTermoPesquisa.Visible = true;
                ddPlanos.Visible = true;
                PlanoDAL planoDAL = new PlanoDAL();
                List<PlanoModels> planos = planoDAL.BuscarPlano();

                if (planos != null && planos.Count > 0)
                {
                    ddPlanos.DataSource = planos;
                    ddPlanos.DataTextField = "Nome";
                    ddPlanos.DataValueField = "idPlano";
                    ddPlanos.DataBind();

                    ddPlanos.Items.Insert(0, new ListItem("-- Selecione uma turma --", "-1"));
                }
            }
            else
            {
                btnLimpar.Visible = false;
                btnPesquisar.Visible = false;
                TxtTermoPesquisa.Visible = false;
                ddPlanos.Visible = false;
                ddHorarios.Visible = false;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {

            AlunosDAL alunosDAL = new AlunosDAL();

            string termo = TxtTermoPesquisa.Text;
            int? idPlano = null;
            int? idHora = null;

            if (ddPlanos.SelectedValue != "-1" && int.TryParse(ddPlanos.SelectedValue, out int idPlanoConvertido))
                idPlano = idPlanoConvertido;

            if (ddHorarios.SelectedValue != "-1" && int.TryParse(ddHorarios.SelectedValue, out int idHoraConvertido))
                idHora = idHoraConvertido;

            List<AlunoModels> alunoModels = alunosList = alunosDAL.PesquisarAlunosPresencas(termo, idPlano, idHora);

            GridView1.DataSource = alunosList;
            GridView1.DataBind();

        }

        protected void ddPlanos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idPlanoSelecionado;

            if (int.TryParse(ddPlanos.SelectedValue, out idPlanoSelecionado))
            {
                CarregarHorariosPorPlano(idPlanoSelecionado);
            }
        }

        protected void SalvarAluno_Click(object sender, EventArgs e)
        {

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            TxtTermoPesquisa.Text = string.Empty;
            ddPlanos.SelectedIndex = -1;

            AlunosDAL alunosDAL = new AlunosDAL();
            alunosList = alunosDAL.VisualizarAlunosPresencas();
            GridView1.DataSource = alunosList;
            GridView1.DataBind();

        }

        protected void btnGraduar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            GridViewRow row = (GridViewRow)btn.NamingContainer;

            modalMatricula.Text = row.Cells[0].Text;
            modalNome.Text  = row.Cells[1].Text;
            modalSobrenome.Text = row.Cells[2].Text;

            string script = "<script>abrirModal();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowDetalhes", script);
        }


        protected void btnSalvarGraduacao_Click(object sender, EventArgs e)
        {
            GraduacaoDAL graduacaoDAL = new GraduacaoDAL();
            int cadastroRealizado = graduacaoDAL.CadastrarGraduacao(new GraduacaoModels
                                    {
                                        idMatricula = Convert.ToInt32(modalMatricula.Text),
                                        observacao = modalObservacaoAluno.Text,
                                        dataGraduacao = DateTime.Now
                                    });

            if (cadastroRealizado > 0)
            {
                string script = @"
                Swal.fire({
                    icon: 'success',
                    title: 'Sucesso!',
                    text: 'Nova graduação registrada com sucesso!',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'OK'
                });";
                ScriptManager.RegisterStartupScript(this, GetType(), "sweetalert", script, true);


                AlunosDAL alunosDAL = new AlunosDAL();
                alunosList = alunosDAL.VisualizarAlunosPresencas();
                GridView1.DataSource = alunosList;
                GridView1.DataBind();
            }

        }

        private void CarregarHorariosPorPlano(int idPlano)
        {
            AlunosDAL horaDAL = new AlunosDAL();
            var horarios = horaDAL.GetHorariosPorPlano(idPlano);

            ddHorarios.Items.Clear();
            ddHorarios.Items.Add(new ListItem("-- Todos os horários --", "-1"));

            foreach (var hora in horarios)
            {
                string texto = $"{hora.HorarioInicio:hh\\:mm} às {hora.HorarioFim:hh\\:mm}";
                ddHorarios.Items.Add(new ListItem(texto, hora.IdHora.ToString()));
            }

            ddHorarios.Visible = true;
        }

    }
}
