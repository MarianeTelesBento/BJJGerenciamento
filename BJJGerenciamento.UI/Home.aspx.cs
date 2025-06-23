using BJJGerenciamento.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;

namespace BJJGerenciamento.UI
{
	public partial class Home : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }


            if (!IsPostBack)
            {
                CarregarMensalidades();
                int ativos = AlunosDAL.ObterQuantidadeAtivos();
                int inativos = AlunosDAL.ObterQuantidadeInativos();
                List<AlunoModels> aniversariantes = AlunosDAL.ObterAniversariantesDoMes();
           


                lblAtivos.Text = ativos.ToString();
                lblInativos.Text = inativos.ToString();
                rptAniversariantes.DataSource = aniversariantes;
                rptAniversariantes.DataBind();
            }

        }
        private DateTime ObterDataVencimentoReal(int diaVencimento)
        {
            DateTime hoje = DateTime.Today;
            int ano = hoje.Year;
            int mes = hoje.Month;

            if (diaVencimento < hoje.Day)
            {
                mes++;
                if (mes > 12)
                {
                    mes = 1;
                    ano++;
                }
            }

            int ultimoDiaMes = DateTime.DaysInMonth(ano, mes);
            int diaFinal = Math.Min(diaVencimento, ultimoDiaMes);

            return new DateTime(ano, mes, diaFinal);
        }
        private void CarregarMensalidades()
        {
            var planoDal = new PlanoDAL();
            var listaPlanos = planoDal.BuscarTodosPlanosComAlunos();

            DateTime hoje = DateTime.Today;
            int diasPraVencer = 7;

            // Projeta cada plano usando só DataProximaCobranca
            var todosDados = listaPlanos
                .Where(p => p.DataProximaCobranca.HasValue) // DESCARTA NULLs
                .Select(p => new
                {
                    p.idPlanoAluno,
                    NomeCompleto = p.Nome + " " + p.Sobrenome,
                    DataReal = p.DataProximaCobranca.Value,
                    Mensalidade = p.mensalidade
                })
            // Agrupa por plano
            .GroupBy(x => x.NomeCompleto)

                .Select(g => g.First())
                .ToList();

            // Vencidas: DataReal < hoje
            var vencidas = todosDados
                .Where(x => x.DataReal.Date < hoje)
                .ToList();

            // Próximas: hoje ≤ DataReal ≤ hoje+7
            var proximas = todosDados
                .Where(x => x.DataReal.Date >= hoje && x.DataReal.Date <= hoje.AddDays(diasPraVencer))
                .ToList();

            gvVencidas.DataSource = vencidas;
            gvVencidas.DataBind();

            gvProximas.DataSource = proximas;
            gvProximas.DataBind();
        }








    }
}