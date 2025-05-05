using BJJGerenciamento.UI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;

namespace BJJGerenciamento.UI
{
	public partial class CadastrarTurmas : System.Web.UI.Page
	{
        PlanoDAL planoDAL = new PlanoDAL();
        protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                // Carregar planos no DropDownList
                CarregarPlanos();

                // Carregar dias da semana no CheckBoxList
                CarregarDiasSemana();
            }
        }

        private void CarregarPlanos()
        {
            var planos = planoDAL.BuscarPlano(); // Buscar planos da DAL
            ddlPlano.DataSource = planos;
            ddlPlano.DataTextField = "Nome";
            ddlPlano.DataValueField = "IdPlano";
            ddlPlano.DataBind();
        }

        private void CarregarDiasSemana()
        {
            // Você pode carregar os dias diretamente na tabela TBDiasSemana ou definir como manualmente
            var diasSemana = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "Segunda"),
                new KeyValuePair<int, string>(2, "Terça"),
                new KeyValuePair<int, string>(3, "Quarta"),
                new KeyValuePair<int, string>(4, "Quinta"),
                new KeyValuePair<int, string>(5, "Sexta"),
                new KeyValuePair<int, string>(6, "Sábado"),
                new KeyValuePair<int, string>(7, "Domingo")
            };

            cblDias.DataSource = diasSemana;
            cblDias.DataTextField = "Value";
            cblDias.DataValueField = "Key";
            cblDias.DataBind();
        }

        // Quando os dias forem selecionados, você atualiza os horários
        protected void cblDias_SelectedIndexChanged(object sender, EventArgs e)
        {
            var diasSelecionados = new List<int>();
            foreach (ListItem item in cblDias.Items)
            {
                if (item.Selected)
                {
                    diasSelecionados.Add(int.Parse(item.Value));
                }
            }

            // Atualizar horários com base nos dias selecionados
            CarregarHorarios(diasSelecionados);
            AtualizarMensalidade(diasSelecionados.Count); // Atualizar mensalidade
        }

        private void CarregarHorarios(List<int> diasSelecionados)
        {
            phHorarios.Controls.Clear();

            foreach (var dia in diasSelecionados)
            {
                var horarios = planoDAL.BuscarHorariosPlano(new KeyValuePair<int, string>(dia, ""), int.Parse(ddlPlano.SelectedValue));

                foreach (var horario in horarios)
                {
                    var checkBox = new CheckBox
                    {
                        Text = $"{horario.Key} - {horario.Value}",
                        ID = $"chkHorario_{dia}_{horario.Key}"
                    };
                    phHorarios.Controls.Add(checkBox);
                    phHorarios.Controls.Add(new Literal { Text = "<br />" });
                }
            }
        }

        private void AtualizarMensalidade(int quantidadeDias)
        {
            if (ddlPlano.SelectedValue != null)
            {
                var planoId = int.Parse(ddlPlano.SelectedValue);
                var mensalidade = planoDAL.BuscarMensalidade(planoId, quantidadeDias);
                txtMensalidade.Text = mensalidade.ToString("C2");
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            // Coletar os dados e salvar no banco
            var idPlano = int.Parse(ddlPlano.SelectedValue);
            var diasSelecionados = new List<int>();
            foreach (ListItem item in cblDias.Items)
            {
                if (item.Selected)
                {
                    diasSelecionados.Add(int.Parse(item.Value));
                }
            }

            // Salvar os dados usando o método da DAL
            foreach (var dia in diasSelecionados)
            {
                // Chamar método da DAL para salvar a turma
                planoDAL.CadastrarPlanoAlunoValor(decimal.Parse(txtMensalidade.Text));
            }

            // Exibir mensagem de sucesso
            Response.Redirect("CadastrarAluno.aspx");
        }

    }
}