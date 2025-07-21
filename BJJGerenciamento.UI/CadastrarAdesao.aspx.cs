using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;

namespace BJJGerenciamento.UI
{
    public partial class CadastrarAdesao : System.Web.UI.Page
    {
        PlanoDAL dal = new PlanoDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarAdesoes();
                CarregarTurmas();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            AdesaoModels adesao = new AdesaoModels();
            adesao.NomeAdesao = txtNomeAdesao.Text;
            adesao.QtdDiasPermitidos = int.Parse(txtQtdDias.Text);
            adesao.Mensalidade = decimal.Parse(txtMensalidade.Text);

            adesao.IdsPlanos = new List<int>();
            foreach (ListItem item in chkTurmas.Items)
            {
                if (item.Selected)
                    adesao.IdsPlanos.Add(int.Parse(item.Value));
            }

            dal.InserirAdesao(adesao);
            lblMensagem.Text = "Adesão cadastrada com sucesso!";
            CarregarAdesoes();
        }

        private void CarregarAdesoes()
        {
            gridAdesoes.DataSource = dal.ListarAdesoes();
            gridAdesoes.DataBind();
        }

        private void CarregarTurmas()
        {
            chkTurmas.DataSource = dal.ListarTurmasAdesao();
            chkTurmas.DataTextField = "Nome";
            chkTurmas.DataValueField = "IdPlano";
            chkTurmas.DataBind();
        }

        protected void gridAdesoes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idAdesao = Convert.ToInt32(gridAdesoes.DataKeys[index].Value);

                dal.ExcluirAdesao(idAdesao);
                CarregarAdesoes();
            }
        }
    }
}
