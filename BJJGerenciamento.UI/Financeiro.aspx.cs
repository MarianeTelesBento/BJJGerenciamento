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
            if (!IsPostBack)
            {
                CarregarMensalidades("Todos");
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
            List<PlanoAlunoModels> planos = planoDAL.BuscarTodosPlanosComAlunos();

            // Agrupar por aluno + tipo de plano
            var planosAgrupados = planos
                .GroupBy(p => new { p.idAlunos, p.idDetalhe })
                .Select(g => g.First())
                .ToList();

            var dadosFinanceiros = planosAgrupados
                .Select(p => new
                {
                    idPlanoAluno = p.idPlanoAluno,
                    NomeCompleto = p.Nome + " " + p.Sobrenome,
                    DiaVencimento = p.DiaVencimento,
                    Mensalidade = p.mensalidade,
                    Status = ObterStatus(p.DiaVencimento)
                });

            // Filtro
            if (filtro == "Vencidos")
                dadosFinanceiros = dadosFinanceiros.Where(p => p.Status == "Vencido");
            else if (filtro == "Proximos")
                dadosFinanceiros = dadosFinanceiros.Where(p => p.Status == "Próximo");

            gvFinanceiro.DataSource = dadosFinanceiros.ToList();
            gvFinanceiro.DataBind();
        }




        private string ObterStatus(int diaVencimento)
        {
            DateTime hoje = DateTime.Today;
            int ano = hoje.Year;
            int mes = hoje.Month;

            // Se o dia de vencimento já passou, o próximo vencimento é no próximo mês
            DateTime dataVencimento;

            if (diaVencimento < hoje.Day)
            {
                // Avança para o próximo mês
                mes++;
                if (mes > 12)
                {
                    mes = 1;
                    ano++;
                }
            }

            int ultimoDia = DateTime.DaysInMonth(ano, mes);
            int diaCorreto = Math.Min(diaVencimento, ultimoDia);

            dataVencimento = new DateTime(ano, mes, diaCorreto);

            // Agora compara a data de vencimento com hoje
            if (dataVencimento < hoje)
                return "Vencido";
            else if ((dataVencimento - hoje).TotalDays <= 7)
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

            int dia = plano.DiaVencimento;
            DateTime novaData = DateTime.Today.AddMonths(1);

            int ultimoDiaMes = DateTime.DaysInMonth(novaData.Year, novaData.Month);
            int novoDia = Math.Min(dia, ultimoDiaMes);

            planoDal.AtualizarDataPagamento(idPlanoAluno, novoDia);

            // Recarrega mantendo o filtro atual
            string filtroAtual = ddlFiltro.SelectedValue;
            CarregarMensalidades(filtroAtual);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertaFechar", "alert('Pagamento registrado com sucesso!'); fecharModal();", true);
        }


        protected void gvFinanceiro_SelectedIndexChanged(object sender, EventArgs e)
        {
          

        }

        protected void btnDetalhesPlano_Click(object sender, EventArgs e)
        {
            string script = "<script>abrirModal();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowDetalhes", script);
        }
    }
}