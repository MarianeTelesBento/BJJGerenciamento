using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BJJGerenciamento.UI
{
    public partial class GerenciarSalas : System.Web.UI.Page
    {
        private SalaDAL salaDAL = new SalaDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                CarregarSalas();
            }
        }

        protected void btnGerarNumeroSala_Click(object sender, EventArgs e)
        {
            int numeroSala = salaDAL.ObterProximoNumeroSala();
            lblNumeroSala.Text = "Número da próxima sala: " + numeroSala;
            btnConfirmarAdicao.Visible = true;
        }

        protected void btnConfirmarAdicao_Click(object sender, EventArgs e)
        {
            int numeroSala = Convert.ToInt32(lblNumeroSala.Text.Split(':')[1].Trim());

            salaDAL.AdicionarSala(numeroSala);

            lblMensagem.Text = "Sala " + numeroSala + " adicionada com sucesso!";
            btnConfirmarAdicao.Visible = false;
            lblNumeroSala.Text = "";

            CarregarSalas();
        }

        private void CarregarSalas()
        {
            List<SalaModel> salas = salaDAL.ObterSalas(); // lista todas, ativas e inativas
            gvSalas.DataSource = salas;
            gvSalas.DataBind();
            gvSalas.Visible = salas.Count > 0;
        }

        protected void btnToggleAtiva_Click(object sender, EventArgs e)
        {
            LinkButton btnToggle = (LinkButton)sender;
            string[] argumentos = btnToggle.CommandArgument.Split('|');
            int idSala = Convert.ToInt32(argumentos[0]);
            bool statusAtual = Convert.ToBoolean(argumentos[1]);

            bool novoStatus = !statusAtual;
            salaDAL.DefinirStatusSala(idSala, novoStatus);

            lblMensagem.Text = $"Sala {(novoStatus ? "ativada" : "desativada")} com sucesso!";
            CarregarSalas();
        }

        // Método para excluir a sala
        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            LinkButton btnExcluir = (LinkButton)sender;
            int idSala = Convert.ToInt32(btnExcluir.CommandArgument);

            // Excluir a sala
            salaDAL.ExcluirSala(idSala);

            lblMensagem.Text = "Sala excluída com sucesso!";
            CarregarSalas();
        }
    }
}
