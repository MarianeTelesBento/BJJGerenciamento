using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;
using static BJJGerenciamento.UI.Models.Financeiro;


namespace BJJGerenciamento.UI
{
    public partial class Financeiro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["UsuarioLogado"] == null)
            //{
            //    Response.Redirect("Login.aspx");
            //}
            if (!IsPostBack)
            {
                if (Request.QueryString["idPlanoAluno"] != null)
                {
                    int id = int.Parse(Request.QueryString["idPlanoAluno"]);
                    hiddenIdPlanoAluno.Value = id.ToString();

                    // Exibe imediatamente o modal com os detalhes
                    ExibirDetalhesPlano(id);
                }

                // Carrega a lista principal
                CarregarMensalidades();
            }
         

        }
        protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtroSelecionado = ddlFiltro.SelectedValue;
            CarregarMensalidades(filtroSelecionado);
        }

        private void CarregarMensalidades(string filtro = "Todos")
        {
            PlanoDAL planoDAL = new PlanoDAL();
            var planos = planoDAL.BuscarTodosPlanosComAlunos();

            // Agrupar por Aluno + Adesão, pegando SEMPRE o registro com a DataProximaCobranca mais recente
            var agrupados = planos
                .GroupBy(p => new { p.idAlunos, p.IdAdesao })
                .Select(g => g.OrderByDescending(x => x.DataProximaCobranca).First())
                .ToList();

            // Monta os dados para o GridView
            var dados = agrupados.Select(p => new
            {
                p.idPlanoAluno,
                NomeCompleto = p.Nome + " " + p.Sobrenome,
                DataVencimento = p.DataProximaCobranca.HasValue
                    ? p.DataProximaCobranca.Value.ToString("dd/MM/yyyy")
                    : "",
                p.mensalidade,
                Status = ObterStatus(p.DataProximaCobranca)
            });

            // Aplica o filtro escolhido
            if (filtro == "Vencidos")
                dados = dados.Where(p => p.Status == "Vencido");
            else if (filtro == "Proximos")
                dados = dados.Where(p => p.Status == "Próximo");

            // Carrega no Grid
            gvFinanceiro.DataSource = dados.ToList();
            gvFinanceiro.DataBind();
        }

        private string ObterStatus(DateTime? dataVencimento)
        {
            if (dataVencimento == null)
                return "Sem data";

            DateTime hoje = DateTime.Today;
            double dias = (dataVencimento.Value - hoje).TotalDays;

            if (dias < 0)
                return "Vencido";
            else if (dias <= 7)
                return "Próximo";
            else
                return "OK";
        }






        protected void gvFinanceiro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ExibirDetalhes")
            {
                int idPlanoAluno = Convert.ToInt32(e.CommandArgument);

                PlanoDAL planoDal = new PlanoDAL();
                var plano = planoDal.BuscarPlanoPorId(idPlanoAluno);

                if (plano != null)
                {
                    lblNomeAluno.Text = plano.Nome + " " + plano.Sobrenome;
                    lblPlano.Text = "Turma: " + planoDal.BuscarNomePlano(plano.idDetalhe);
                    lblAdesao.Text = "Adesão: " + plano.NomeAdesao; // <-- LINHA ADICIONADA
                    lblVencimento.Text = "Vencimento: Dia " + plano.DiaVencimento;
                    lblValor.Text = "Mensalidade: " + plano.mensalidade.ToString("C");
                    hiddenIdPlanoAluno.Value = plano.idPlanoAluno.ToString();
                }
            }    }
        protected void btnPagamentoEfetuado_Click(object sender, EventArgs e)
        {
            int idPlanoAluno = Convert.ToInt32(hiddenIdPlanoAluno.Value);

            PlanoDAL planoDal = new PlanoDAL();
            PlanoAlunoModels plano = planoDal.BuscarPlanoPorId(idPlanoAluno);
            if (plano == null)
            {
                return;
            }

            int idDoAlunoParaAtualizar = plano.idAlunos;

            DateTime? dataMaisRecente = planoDal.BuscarDataMaisRecentePorAluno(idDoAlunoParaAtualizar);

            DateTime dataReferencia = (dataMaisRecente.HasValue && dataMaisRecente.Value > DateTime.Today)
                                      ? dataMaisRecente.Value
                                      : DateTime.Today;

            DateTime proximoMesReferencia = dataReferencia.AddMonths(1);

            int diaVencimento = plano.DiaVencimento;
            int ultimoDiaDoMes = DateTime.DaysInMonth(proximoMesReferencia.Year, proximoMesReferencia.Month);
            int diaCorreto = Math.Min(diaVencimento, ultimoDiaDoMes);

            DateTime novaData = new DateTime(proximoMesReferencia.Year, proximoMesReferencia.Month, diaCorreto);

            planoDal.AtualizarApenasDataCobranca(idDoAlunoParaAtualizar, novaData);

            CarregarMensalidades();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "FecharModal", "$('#modalDetalhes').modal('hide');", true);
        }
        protected void gvFinanceiro_SelectedIndexChanged(object sender, EventArgs e)
        {
          

        }

        protected void btnDetalhesPlano_Click(object sender, EventArgs e)
        {
            string script = "<script>abrirModal();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowDetalhes", script);
        }
        private void ExibirDetalhesPlano(int idPlanoAluno)
        {
            // Instancia o DAL
            PlanoDAL planoDal = new PlanoDAL();

            // Busca o plano
            PlanoAlunoModels plano = planoDal.BuscarPlanoPorId(idPlanoAluno);
            if (plano == null) return;

            // Preenche os labels do modal
            lblNomeAluno.Text = plano.Nome + " " + plano.Sobrenome;
            lblPlano.Text = "Plano: " + planoDal.BuscarNomePlano(plano.idDetalhe);
            lblVencimento.Text = "Vencimento: Dia " + plano.DiaVencimento;
            lblValor.Text = "Mensalidade: " + plano.mensalidade.ToString("C");

            // Guarda o id no hidden
            hiddenIdPlanoAluno.Value = idPlanoAluno.ToString();

            // Abre o modal via ScriptManager
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "Pop",
                "$('#modalDetalhes').modal('show');",
                true
            );
        }



    }
}