using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BJJGerenciamento.UI
{
    public partial class Chamada : System.Web.UI.Page
    {
        public List<AlunoModels> alunosList = new List<AlunoModels>();
        public AlunoModels aluno = new AlunoModels();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.Columns[6].Visible = false;
                AlunosDAL alunosDAL = new AlunosDAL();
                alunosList = alunosDAL.VisualizarDados();
                GridView1.DataSource = alunosList;
                GridView1.DataBind();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            AlunosDAL alunosDAL = new AlunosDAL();
            alunosList = alunosDAL.PesquisarAlunos(TxtTermoPesquisa.Text);
            GridView1.DataSource = alunosList;
            GridView1.DataBind();
        }

        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            if (ddPlanos.Visible == false)
            {
                ddPlanos.Visible = true;
                PlanoDAL planoDAL = new PlanoDAL();
                List<PlanoModels> planos = planoDAL.BuscarPlano();

                if (planos != null && planos.Count > 0)
                {
                    ddPlanos.DataSource = planos;
                    ddPlanos.DataTextField = "Nome";
                    ddPlanos.DataValueField = "idPlano";
                    ddPlanos.DataBind();

                    ddPlanos.Items.Insert(0, new ListItem("-- Selecione uma turma --", ""));
                }
            }
            else
            {
                ddPlanos.Visible = false;
            }
        }

        protected void ddPlanos_SelectedIndexChanged(object sender, EventArgs e)
        {//Ajustar
            AlunosDAL alunosDal = new AlunosDAL();

            if (ddPlanos.SelectedValue != "-1")
            {
                int idPlano;
                if (int.TryParse(ddPlanos.SelectedValue, out idPlano))
                {
                    List<AlunoModels> listaAlunos = alunosDal.PesquisarAlunosTurma(idPlano);
                    GridView1.DataSource = listaAlunos;
                    GridView1.DataBind();
                }

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
                ddProfessores.Items.Insert(0, new ListItem("-- Selecione um professor --", ""));
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
                ddSalas.Items.Insert(0, new ListItem("-- Selecione uma sala --", ""));
            }
        }

        protected void btnSalvarChamada_Click(object sender, EventArgs e)
        {
            PresencaModels presencaModel = new PresencaModels();

            if (ddProfessores.SelectedValue == "-1" || ddSalas.SelectedValue == "-1")
            {
                // Exibir mensagem de erro ou aviso
                return;
            }

            presencaModel.IdProfessor = Convert.ToInt32(ddProfessores.SelectedValue);
            presencaModel.IdSala = Convert.ToInt32(ddSalas.SelectedValue);

            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox checkBox = (CheckBox)row.FindControl("chkPresente");
                if (checkBox != null && checkBox.Checked)
                {
                    presencaModel.IdMatricula = Convert.ToInt32(row.Cells[0].Text.Trim());

                    PresencaDAL presencaDAL = new PresencaDAL();
                    presencaDAL.RegistrarPresenca(presencaModel);
                }
            }

            ddProfessores.Visible = false;
            ddSalas.Visible = false;
            btnSalvarChamada.Visible = false;
        }
    }
}