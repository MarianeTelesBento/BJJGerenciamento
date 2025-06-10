using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BJJGerenciamento.UI
{

    public partial class Chamada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
              
                GridView1.Columns[6].Visible = false;
                AlunosDAL alunosDAL = new AlunosDAL();
                alunosList = alunosDAL.VisualizarDadosChamada();
                GridView1.DataSource = alunosList;
                GridView1.DataBind();

                RestaurarChecks(IdsMarcados);

            }
        }

        private List<int> IdsMarcados
        {
            get
            {
                if (Session["IdsMarcados"] == null)
                    Session["IdsMarcados"] = new List<int>();
                return (List<int>)Session["IdsMarcados"];
            }
            set
            {
                Session["IdsMarcados"] = value;
            }
        }

        public List<AlunoModels> alunosList = new List<AlunoModels>();
        public AlunoModels aluno = new AlunoModels();
       

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            IdsMarcados = ObterIdsAlunosSelecionados();

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
                    IdsMarcados = ObterIdsAlunosSelecionados();

                    ddPlanos.DataSource = planos;
                    ddPlanos.DataTextField = "Nome";
                    ddPlanos.DataValueField = "idPlano";
                    ddPlanos.DataBind();

                    RestaurarChecks(IdsMarcados);

                    ddPlanos.Items.Insert(0, new ListItem("-- Selecione uma turma --", "-1"));
                }
            }
            else
            {
                btnLimpar.Visible = false;
                btnPesquisar.Visible = false;
                TxtTermoPesquisa.Visible = false;
                ddPlanos.Visible = false;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            IdsMarcados = ObterIdsAlunosSelecionados();

            AlunosDAL alunosDAL = new AlunosDAL();

            string termo = TxtTermoPesquisa.Text;
            int? idPlano = null;

            if (ddPlanos.SelectedValue != "-1")
            {
                if (int.TryParse(ddPlanos.SelectedValue, out int idPlanoConvertido))
                {
                    idPlano = idPlanoConvertido;
                }
            }
            List<AlunoModels> alunoModels = alunosList = alunosDAL.PesquisarAlunos(termo, idPlano);

            GridView1.DataSource = alunosList;
            GridView1.DataBind();

            RestaurarChecks(IdsMarcados);

        }

        protected void ddPlanos_SelectedIndexChanged(object sender, EventArgs e)
        {
             int idPlanoSelecionado;

            if (int.TryParse(ddPlanos.SelectedValue, out idPlanoSelecionado))
            {
                // Carrega os horários da turma (plano) selecionado
                CarregarHorariosPorPlano(idPlanoSelecionado);
            }
        }

        protected void SalvarAluno_Click(object sender, EventArgs e)
        {
        }

        protected void btnChamada_Click(object sender, EventArgs e)
        {
            if(ddProfessores.Visible == true)
            {
                GridView1.Columns[6].Visible = false;
                ddProfessores.Visible = false;
                ddSalas.Visible = false;
                btnSalvarChamada.Visible = false;
                return;
            }

            GridView1.Columns[6].Visible = true; 
            ddProfessores.Visible = true;
            ddSalas.Visible = true;
            btnSalvarChamada.Visible = true;

            ProfessorDAL professorDAL = new ProfessorDAL();
            List<ProfessorModels> professores = professorDAL.ListarProfessores();
            if (professores != null && professores.Count > 0)
            {
                ddProfessores.DataSource = professores;
                ddProfessores.DataTextField = "Nome";
                ddProfessores.DataValueField = "IdProfessor";
                ddProfessores.DataBind();
                ddProfessores.Items.Insert(0, new ListItem("-- Selecione um professor --", "-1"));
            }

            SalaDAL salaDAL = new SalaDAL();
            List<SalaModel> salas = salaDAL.ObterSalas();
            List<SalaModel> salasAtivas = salas.Where(s => s.Ativa == true).ToList();

            if (salas != null && salas.Count > 0)
            {
                ddSalas.DataSource = salasAtivas;
                ddSalas.DataTextField = "NumeroSala";
                ddSalas.DataValueField = "IdSala";
                ddSalas.DataBind();
                ddSalas.Items.Insert(0, new ListItem("-- Selecione uma sala --", "-1"));
            }
        }

        protected void btnSalvarChamada_Click(object sender, EventArgs e)
        {
            PresencaModels presencaModel = new PresencaModels();

            if (ddProfessores.SelectedValue == "-1" || ddSalas.SelectedValue == "-1")
            {
                Response.Write("<script>alert('Selecione uma turma');</script>");
                return;
            }

            presencaModel.IdProfessor = Convert.ToInt32(ddProfessores.SelectedValue);
            presencaModel.IdSala = Convert.ToInt32(ddSalas.SelectedValue);

            PresencaDAL presencaDAL = new PresencaDAL();

            int cadastroRealizado = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox checkBox = (CheckBox)row.FindControl("chkPresente");
                if (checkBox != null && checkBox.Checked)
                {
                    presencaModel.IdMatricula = Convert.ToInt32(row.Cells[0].Text.Trim());
                    cadastroRealizado += presencaDAL.RegistrarPresenca(presencaModel);
                }
            }

            if (cadastroRealizado > 0)
            {
                string script = @"
                    Swal.fire({
                        icon: 'success',
                        title: 'Sucesso!',
                        text: 'Chamada registrada com sucesso!',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'OK'
                    });";
                ScriptManager.RegisterStartupScript(this, GetType(), "sweetalert", script, true);
            }
            else
            {
                Response.Write("<script>alert('Nenhuma presença registrada.');</script>");
            }

            IdsMarcados = new List<int>();
            ddProfessores.Visible = false;
            ddSalas.Visible = false;
            btnSalvarChamada.Visible = false;
            GridView1.Columns[6].Visible = false;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            TxtTermoPesquisa.Text = string.Empty;
            ddPlanos.SelectedIndex = -1;

            IdsMarcados = ObterIdsAlunosSelecionados();

            AlunosDAL alunosDAL = new AlunosDAL();

            alunosList = alunosDAL.VisualizarDados();
            GridView1.DataSource = alunosList;
            GridView1.DataBind();
            ddHorarios.Visible = false;

            RestaurarChecks(IdsMarcados);
        }

        private List<int> ObterIdsAlunosSelecionados()
        {
            List<int> idsSelecionados = new List<int>();

            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox checkBox = row.FindControl("chkPresente") as CheckBox;
                if (checkBox != null && checkBox.Checked)
                {
                    int idMatricula = Convert.ToInt32(row.Cells[0].Text.Trim());
                    idsSelecionados.Add(idMatricula);
                }
            }

            return idsSelecionados;
        }

        private void RestaurarChecks(List<int> idsSelecionados)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                int idMatricula = Convert.ToInt32(row.Cells[0].Text.Trim());
                CheckBox checkBox = row.FindControl("chkPresente") as CheckBox;
                if (checkBox != null && idsSelecionados.Contains(idMatricula))
                {
                    checkBox.Checked = true;
                }
            }
        }

        protected void ddHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {

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