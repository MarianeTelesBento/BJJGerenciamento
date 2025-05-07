using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;

namespace BJJGerenciamento.UI
{
    public partial class CadastrarTurmas : System.Web.UI.Page
    {
        PlanoDAL planoDAL = new PlanoDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarDiasSemana();
            }
            else
            {
                RecarregarHorariosSelecionados();
            }
        }

        private void CarregarDiasSemana()
        {
            var dias = planoDAL.BuscarDiasSemana();
            cblDias.DataSource = dias;
            cblDias.DataTextField = "Value";
            cblDias.DataValueField = "Key";
            cblDias.DataBind();
        }

        protected void cblDias_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Limpa o PlaceHolder
            phHorarios.Controls.Clear();

            // Carrega todos os horários do banco
            var horarios = planoDAL.BuscarHorarios();

            foreach (ListItem diaItem in cblDias.Items)
            {
                if (diaItem.Selected)
                {
                    int idDia = int.Parse(diaItem.Value);

                    // Cria um título para o dia
                    phHorarios.Controls.Add(new Literal { Text = $"<strong>{diaItem.Text}</strong><br />" });

                    // Cria o CheckBoxList dos horários
                    CheckBoxList cblHorarios = new CheckBoxList
                    {
                        ID = $"cblHorarios_{idDia}",
                        RepeatDirection = RepeatDirection.Horizontal
                    };

                    cblHorarios.DataSource = horarios;
                    cblHorarios.DataTextField = "Value";
                    cblHorarios.DataValueField = "Key";
                    cblHorarios.DataBind();

                    phHorarios.Controls.Add(cblHorarios);
                    phHorarios.Controls.Add(new Literal { Text = "<br />" });
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string nomePlano = txtNomeNovoPlano.Text.Trim();
            string mensalidadeStr = txtMensalidade.Text.Trim();

            if (string.IsNullOrWhiteSpace(nomePlano))
            {
                // Exiba erro de validação
                return;
            }

            if (cblDias.SelectedItem == null)
            {
                // Exiba erro de validação
                return;
            }

            int idPlano = planoDAL.CriarNovoPlano(nomePlano);
            int diasSelecionados = 0;

            foreach (ListItem diaItem in cblDias.Items)
            {
                if (diaItem.Selected)
                {
                    int idDia = int.Parse(diaItem.Value);
                    planoDAL.VincularPlanoADia(idPlano, idDia);
                    diasSelecionados++;

                    // Recupera o CheckBoxList dos horários para este dia
                    var cblHorarios = phHorarios.FindControl($"cblHorarios_{idDia}") as CheckBoxList;
                    if (cblHorarios != null)
                    {
                        foreach (ListItem horarioItem in cblHorarios.Items)
                        {
                            if (horarioItem.Selected)
                            {
                                int idHora = int.Parse(horarioItem.Value);
                                planoDAL.VincularPlanoHorario(idPlano, idDia, idHora);
                            }
                        }
                    }
                }
            }

            decimal valor = decimal.Parse(txtMensalidade.Text);
            planoDAL.SalvarValorPlano(idPlano, diasSelecionados, valor);

        }

        protected void btnCalcularMensalidade_Click(object sender, EventArgs e)
        {
            var diasSelecionados = ObterDiasSelecionados();
            if (diasSelecionados.Count == 0)
            {
                txtMensalidade.Text = "0.00";
                return;
            }

            decimal mensalidade = diasSelecionados.Count * 100; // Exemplo de cálculo automático
            txtMensalidade.Text = mensalidade.ToString("F2");
        }

        private List<int> ObterDiasSelecionados()
        {
            List<int> diasSelecionados = new List<int>();
            foreach (ListItem item in cblDias.Items)
            {
                if (item.Selected)
                {
                    diasSelecionados.Add(int.Parse(item.Value));
                }
            }
            return diasSelecionados;
        }

        private void RecarregarHorariosSelecionados()
        {
            phHorarios.Controls.Clear();
            var horarios = planoDAL.BuscarHorarios();

            foreach (ListItem diaItem in cblDias.Items)
            {
                if (diaItem.Selected)
                {
                    int idDia = int.Parse(diaItem.Value);

                    phHorarios.Controls.Add(new Literal { Text = $"<strong>{diaItem.Text}</strong><br />" });

                    CheckBoxList cblHorarios = new CheckBoxList
                    {
                        ID = $"cblHorarios_{idDia}",
                        RepeatDirection = RepeatDirection.Horizontal
                    };

                    cblHorarios.DataSource = horarios;
                    cblHorarios.DataTextField = "Value";
                    cblHorarios.DataValueField = "Key";
                    cblHorarios.DataBind();

                    phHorarios.Controls.Add(cblHorarios);
                    phHorarios.Controls.Add(new Literal { Text = "<br />" });
                }
            }
        }

    }
}
