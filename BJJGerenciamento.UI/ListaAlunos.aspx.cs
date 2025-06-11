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
        int idAluno;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
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

        protected void btnFiltro_Click(object sender, EventArgs e)
        {

            if (ddPlanos.Visible == false)
            {
                btnLimpar.Visible = true;
                btnPesquisar.Visible = true;
                TxtTermoPesquisa.Visible = true;
                ddPlanos.Visible = true;
                chkApenasAtivos.Visible = true;
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
                chkApenasAtivos.Visible = false;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {

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

            bool apenasAtivos = chkApenasAtivos.Checked;

            List<AlunoModels> alunoModels = alunosList = alunosDAL.PesquisarAlunos(termo, idPlano, apenasAtivos);

            GridView1.DataSource = alunosList;
            GridView1.DataBind();


        }

        protected void ddPlanos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            TxtTermoPesquisa.Text = string.Empty;
            ddPlanos.SelectedIndex = -1;

            AlunosDAL alunosDAL = new AlunosDAL();
            alunosList = alunosDAL.VisualizarDados();
            GridView1.DataSource = alunosList;
            GridView1.DataBind();

        }

        protected void btnDetalhes_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int index = row.RowIndex;

            modalIdMatriculaAluno.Text = row.Cells[0].Text;

            CheckBox chkStatusMatricula = (CheckBox)row.FindControl("chkStatusMatricula");
            modalStatusMatricula.Checked = chkStatusMatricula.Checked;

            modalNomeAluno.Text = row.Cells[1].Text;
            modalSobrenomeAluno.Text = row.Cells[2].Text;
            modalCpfAluno.Text = row.Cells[3].Text;
            modalTelefoneAluno.Text = row.Cells[4].Text;

            modalEmailAluno.Text = GridView1.DataKeys[index]["Email"].ToString();
            modalDataNascimentoAluno.Text = GridView1.DataKeys[index]["DataNascimento"].ToString();
            modalCepAluno.Text = GridView1.DataKeys[index]["Cep"].ToString();
            modalRuaAluno.Text = GridView1.DataKeys[index]["Rua"].ToString();
            modalBairroAluno.Text = GridView1.DataKeys[index]["Bairro"].ToString();
            modalCidadeAluno.Text = GridView1.DataKeys[index]["Cidade"].ToString();
            modalEstadoAluno.Text = GridView1.DataKeys[index]["Estado"].ToString();
            modalNumeroAluno.Text = GridView1.DataKeys[index]["NumeroCasa"].ToString();
            modalComplementoAluno.Text = GridView1.DataKeys[index]["Complemento"].ToString();
            modalCarteiraFpjjAluno.Text = GridView1.DataKeys[index]["CarteiraFPJJ"].ToString();
            modalDataMatriculaAluno.Text = GridView1.DataKeys[index]["DataMatricula"].ToString();
            ViewState["IdAlunos"] = GridView1.DataKeys[index]["IdAlunos"].ToString();

            hfIdAlunoModal.Value = GridView1.DataKeys[index]["IdAlunos"].ToString();

            DateTime dataNascimento = Convert.ToDateTime(modalDataNascimentoAluno.Text);
            int idade = DateTime.Now.Year - dataNascimento.Year;

            if (DateTime.Now < dataNascimento.AddYears(idade))
            {
                idade--;
            }

            bool maiorDeIdade = idade >= 18;

            if (maiorDeIdade)
            {
                btnDetalhesResponsavel.Visible = false;
            }
            else
            {
                btnDetalhesResponsavel.Visible = true;
            }

            /*Se tiver graduação: x se não y*/


            string script = $"<script>abrirModal();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script);
        }

        protected void btnDetalhesAluno_Click(object sender, EventArgs e)
        {
            string aba = "Aluno";
            string script = $"<script>abrirModal(); exibirAba('{aba}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowDetalhes", script);
        }

        protected void btnDetalhesResponsavel_Click(object sender, EventArgs e)
        {
            //Status Responsável
            int matriculaId = Convert.ToInt32(modalIdMatriculaAluno.Text);
            AlunosDAL alunosDAL = new AlunosDAL();
            ResponsavelModels responsavel = alunosDAL.BuscarResponsavel(matriculaId);

            modalNomeResponsavel.Text = responsavel.Nome;
            modalSobrenomeResponsavel.Text = responsavel.Sobrenome;
            modalCpfResponsavel.Text = responsavel.Cpf;
            modalTelefoneResponsavel.Text = responsavel.Telefone;
            modalEmailResponsavel.Text = responsavel.Email;
            modalDataNascimentoResponsavel.Text = responsavel.DataNascimento;
            modalCepResponsavel.Text = responsavel.Cep;
            modalRuaResponsavel.Text = responsavel.Rua;
            modalBairroResponsavel.Text = responsavel.Bairro;
            modalCidadeResponsavel.Text = responsavel.Cidade;
            modalEstadoResponsavel.Text = responsavel.Estado;
            modalNumeroResponsavel.Text = responsavel.NumeroCasa;
            modalComplementoResponsavel.Text = responsavel.Complemento;
            ModalIdResponsavel.Text = responsavel.IdResponsavel.ToString();

            string aba = "Responsavel";
            string script = $"<script>abrirModal(); exibirAba('{aba}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowDetalhes", script);
        }


        protected void btnDetalhesPlano_Click(object sender, EventArgs e)
        {
            PlanoDAL planoDal = new PlanoDAL();

            List<PlanoAlunoModels> listaPlanoAluno = planoDal.BuscarPlanoAluno(Convert.ToInt32(modalIdMatriculaAluno.Text));


            if (listaPlanoAluno != null && listaPlanoAluno.Count > 0)
            {
                hfAlunoPossuiPlano.Value = "true";

                var planosAgrupados = listaPlanoAluno
                    .GroupBy(p => p.idDetalhe)
                    .ToList();

                string htmlPlano = "";
                foreach (var grupo in planosAgrupados)
                {
                    var primeiro = grupo.First();

                    htmlPlano += $@"
                        <div class='card p-2 mb-2'>
                        <strong>Plano:</strong> {planoDal.BuscarNomePlano(Convert.ToInt32(primeiro.idDetalhe))}<br/>
                        <strong>Dias por Semana:</strong> {primeiro.qtdDias}<br/>
                        <strong>Mensalidade:</strong> R$ {primeiro.mensalidade:N2}<br/>
                        <strong>Dias e Horários:</strong><br/>
                        <ul>";

                    foreach (var plano in grupo)
                    {
                        htmlPlano += $"<li>{planoDal.BuscarDiaSemana(plano.idDia)} - {plano.horarioInicio} às {plano.horarioFim}</li>";
                    }

                    htmlPlano += "</ul></div>";
                }
                LitDadosPlano.Text = htmlPlano;

            }
            else
            {
                hfAlunoPossuiPlano.Value = "false";
                LitDadosPlano.Text = "<p>Nenhum plano encontrado para este aluno.</p>";
            }

            string aba = "Plano";
            string script = $"<script>abrirModal(); exibirAba('{aba}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowDetalhes", script);
        }

        protected void btnDetalhesGraduacao_Click(object sender, EventArgs e)
        {
            int matriculaId = Convert.ToInt32(modalIdMatriculaAluno.Text);
            GraduacaoDAL graduacaoDAL = new GraduacaoDAL();
            List<GraduacaoModels> listaGraduacoes = graduacaoDAL.BuscarGraduacao(matriculaId);

            if (listaGraduacoes != null && listaGraduacoes.Count > 0)
            {
                rptGraduacoes.DataSource = listaGraduacoes;
                rptGraduacoes.DataBind();
                rptGraduacoes.Visible = true;
                pnlNoGraduations.Visible = false;
            }
            else
            {
                rptGraduacoes.Visible = false; 
                pnlNoGraduations.Visible = true; 
            }
  
            modalNomeGraducaoHeader.Text = "Aluno: " + modalNomeAluno.Text;

            string aba = "Graduacao";
            string script = $"<script>abrirModal(); exibirAba('{aba}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowDetalhes", script);
        }

        protected void rptGraduacoes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                int idGraduacaoParaExcluir = Convert.ToInt32(e.CommandArgument);
                GraduacaoDAL graduacaoDAL = new GraduacaoDAL();
                int excluidoComSucesso = graduacaoDAL.ExcluirGraduacao(idGraduacaoParaExcluir);

                if (excluidoComSucesso > 0)
                {
                    // Reexiba as graduações para refletir a exclusão
                    int matriculaId = Convert.ToInt32(modalIdMatriculaAluno.Text);
                    List<GraduacaoModels> listaGraduacoesAtualizada = graduacaoDAL.BuscarGraduacao(matriculaId);

                    if (listaGraduacoesAtualizada != null && listaGraduacoesAtualizada.Count > 0)
                    {
                        rptGraduacoes.DataSource = listaGraduacoesAtualizada;
                        rptGraduacoes.DataBind();
                        rptGraduacoes.Visible = true;
                        pnlNoGraduations.Visible = false;
                    }
                    else
                    {
                        rptGraduacoes.Visible = false;
                        pnlNoGraduations.Visible = true;
                    }

                    string scriptSucesso = @"
                    Swal.fire({
                        icon: 'success',
                        title: 'Sucesso!',
                        text: 'Graduação excluída com sucesso!',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'OK'
                    });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "sweetalert", scriptSucesso, true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Erro ao excluir a graduação.');", true);
                }

                string aba = "Graduacao";
                string script = $"<script>abrirModal(); exibirAba('{aba}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowDetalhes", script);
            }
        }

        protected void SalvarAluno_Click(object sender, EventArgs e)
        {
            if (VerificarCampos(modalNomeAluno, modalSobrenomeAluno, modalCpfAluno, modalTelefoneAluno, modalRuaAluno, modalBairroAluno, modalCidadeAluno, modalEstadoAluno, modalNumeroAluno))
            {
                aluno.IdAlunos = Convert.ToInt32(ViewState["IdAlunos"]);
                aluno.Nome = modalNomeAluno.Text;
                aluno.Sobrenome = modalSobrenomeAluno.Text;
                aluno.Cpf = modalCpfAluno.Text;
                aluno.Telefone = modalTelefoneAluno.Text;
                aluno.Email = modalEmailAluno.Text;
                aluno.DataNascimento = modalDataNascimentoAluno.Text;
                aluno.Cep = modalCepAluno.Text;
                aluno.Rua = modalRuaAluno.Text;
                aluno.Bairro = modalBairroAluno.Text;
                aluno.Cidade = modalCidadeAluno.Text;
                aluno.Estado = modalEstadoAluno.Text;
                aluno.NumeroCasa = modalNumeroAluno.Text;
                aluno.Complemento = modalComplementoAluno.Text;
                aluno.CarteiraFPJJ = modalCarteiraFpjjAluno.Text;
                aluno.StatusMatricula = modalStatusMatricula.Checked;

                AlunosDAL alunosDAL = new AlunosDAL();

                bool funcionou = alunosDAL.AtualizarAluno(aluno);

                if (funcionou)
                {
                    string script = @"
                    Swal.fire({
                        icon: 'success',
                        title: 'Sucesso!',
                        text: 'Aluno atualizado com sucesso!',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'OK'
                    });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "sweetalert", script, true);

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

        protected void SalvarResponsavel_Click(object sender, EventArgs e)
        {

            if (VerificarCampos(modalNomeResponsavel, modalSobrenomeResponsavel, modalCpfResponsavel, modalEmailResponsavel, modalTelefoneResponsavel, modalDataNascimentoResponsavel, modalRuaResponsavel, modalBairroResponsavel, modalCidadeResponsavel, modalEstadoResponsavel, modalNumeroResponsavel))
            {
                ResponsavelModels responsavel = new ResponsavelModels()
                {
                    Nome = modalNomeResponsavel.Text,
                    Sobrenome = modalSobrenomeResponsavel.Text,
                    Cpf = modalCpfResponsavel.Text,
                    Telefone = modalTelefoneResponsavel.Text,
                    Email = modalEmailResponsavel.Text,
                    DataNascimento = modalDataNascimentoResponsavel.Text,
                    Cep = modalCepResponsavel.Text,
                    Rua = modalRuaResponsavel.Text,
                    Bairro = modalBairroResponsavel.Text,
                    Cidade = modalCidadeResponsavel.Text,
                    Estado = modalEstadoResponsavel.Text,
                    NumeroCasa = modalNumeroResponsavel.Text,
                    Complemento = modalComplementoResponsavel.Text,
                    IdResponsavel = Convert.ToInt32(ModalIdResponsavel.Text)
                };

                AlunosDAL alunosDAL = new AlunosDAL();

                bool funcionou = alunosDAL.AtualizarResponsavel(responsavel);

                if (funcionou)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Responsável atualizado com sucesso!');", true);
                    alunosList = alunosDAL.VisualizarDados();
                    GridView1.DataSource = alunosList;
                    GridView1.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Erro ao atualizar responsável. Tente novamente!');", true);
                }
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

        protected void chkApenasAtivos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkApenasAtivos.Checked)
            {
                AlunosDAL alunosDAL = new AlunosDAL();
                alunosList = alunosDAL.VisualizarDadosChamada();
            }
        }
    }
}