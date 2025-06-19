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
            PlanoDAL planoDal = new PlanoDAL();

            // Agora busca todos os planos de todos os alunos
            List<PlanoAlunoModels> listaPlanos = planoDal.BuscarTodosPlanosAlunos();

            DateTime hoje = DateTime.Today;
            int diasPraVencer = 7;

            var listaComDataVencimento = listaPlanos.Select(p => new
            {
                Plano = p,
                DataVencimentoReal = ObterDataVencimentoReal(p.DiaVencimento)
            }).ToList();

            // Agrupar por aluno (evita duplicações visuais)
            var vencidas = listaComDataVencimento
                .Where(x => x.DataVencimentoReal < hoje)
                .GroupBy(x => x.Plano.idAlunos)
                .Select(g => g.First().Plano)
                .ToList();

            var proximasVencer = listaComDataVencimento
                .Where(x => x.DataVencimentoReal >= hoje && x.DataVencimentoReal <= hoje.AddDays(diasPraVencer))
                .GroupBy(x => x.Plano.idAlunos)
                .Select(g => g.First().Plano)
                .ToList();

            rptMensalidadesVencidas.DataSource = vencidas;
            rptMensalidadesVencidas.DataBind();

            rptMensalidadesProximas.DataSource = proximasVencer;
            rptMensalidadesProximas.DataBind();
        }



    }
}