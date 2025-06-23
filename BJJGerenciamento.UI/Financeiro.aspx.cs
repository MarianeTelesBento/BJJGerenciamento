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
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
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

            var agrupados = planos
                .GroupBy(p => new { p.idAlunos, p.idDetalhe })
                .Select(g => g.First())
                .ToList();

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

            if (filtro == "Vencidos")
                dados = dados.Where(p => p.Status == "Vencido");
            else if (filtro == "Proximos")
                dados = dados.Where(p => p.Status == "Próximo");

            gvFinanceiro.DataSource = dados.ToList();
            gvFinanceiro.DataBind();
        }



        private string ObterStatus(DateTime? dataVencimento)
        {
            if (dataVencimento == null)
                return "Sem data";

            DateTime hoje = DateTime.Today;

            if (dataVencimento < hoje)
                return "Vencido";
            else if ((dataVencimento - hoje)?.TotalDays <= 7)
                return "Próximo";
            else
                return "OK";
        }

        protected void gvFinanceiro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ExibirDetalhes")
            {
                int idPlanoAluno = Convert.ToInt32(e.CommandArgument);
                ExibirDetalhesPlano(idPlanoAluno);

                PlanoDAL planoDal = new PlanoDAL();
                var plano = planoDal.BuscarPlanoPorId(idPlanoAluno); // ou o método correto

                if (plano != null)
                {
                    lblNomeAluno.Text = plano.Nome + " " + plano.Sobrenome;
                    lblPlano.Text = "Plano: " + planoDal.BuscarNomePlano(plano.idDetalhe);
                    lblVencimento.Text = "Vencimento: Dia " + plano.DiaVencimento;
                    lblValor.Text = "Mensalidade: " + plano.mensalidade.ToString("C");
                    hiddenIdPlanoAluno.Value = plano.idPlanoAluno.ToString();

                  

                }
            }
        }
        protected void btnPagamentoEfetuado_Click(object sender, EventArgs e)
        {
            int idPlanoAluno = Convert.ToInt32(hiddenIdPlanoAluno.Value);
            PlanoDAL planoDal = new PlanoDAL();
            PlanoAlunoModels plano = planoDal.BuscarPlanoPorId(idPlanoAluno);

            // 1) Calcula a novaData mantendo o dia fixo de vencimento
            int diaVencimento = plano.DiaVencimento;
            DateTime dataBase = plano.DataProximaCobranca ?? DateTime.Today;

            int novoMes = dataBase.Month + 1;
            int novoAno = dataBase.Year;
            if (novoMes > 12)
            {
                novoMes = 1;
                novoAno++;
            }

            int ultimoDiaMes = DateTime.DaysInMonth(novoAno, novoMes);
            int diaCorreto = Math.Min(diaVencimento, ultimoDiaMes);
            DateTime novaData = new DateTime(novoAno, novoMes, diaCorreto);

            // 2) Executa o UPDATE e captura quantas linhas foram afetadas
            int linhasAfetadas = planoDal.AtualizarDataPagamento(idPlanoAluno, novaData);
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Linhas afetadas no UPDATE: {linhasAfetadas}");

            // 3) Recarrega o objeto do banco e loga o valor atual de DataProximaCobranca
            var atualizado = planoDal.BuscarPlanoPorId(idPlanoAluno);
            System.Diagnostics.Debug.WriteLine(
                $"[DEBUG] Valor em DataProximaCobranca no banco: {atualizado.DataProximaCobranca}"
            );

            // 4) Recarrega a grid com o filtro atual
            string filtroAtual = ddlFiltro.SelectedValue;
            CarregarMensalidades(filtroAtual);

            // 5) Fecha o modal e exibe alerta
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta",
                "alert('Pagamento registrado com sucesso!'); fecharModal();", true);
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