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
            phHorarios.Controls.Clear();
            var horarios = planoDAL.BuscarHorarios();

            foreach (ListItem diaItem in cblDias.Items)
            {
                if (diaItem.Selected)
                {
                    int idDia = int.Parse(diaItem.Value);

                    // Título do dia
                    phHorarios.Controls.Add(new Literal { Text = $"<span class='dia-horario-titulo'>{diaItem.Text}</span>" });

                    // Container para os horários
                    Panel container = new Panel();
                    container.CssClass = "horarios-container";

                    foreach (var horario in horarios)
                    {
                        // Criando CheckBox individual
                        CheckBox chk = new CheckBox
                        {
                            ID = $"chk_{idDia}_{horario.Key}",
                            Text = horario.Value,
                            CssClass = "horario-item"
                        };

                        container.Controls.Add(chk);
                    }

                    phHorarios.Controls.Add(container);
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

            decimal mensalidade = diasSelecionados.Count * 30; // Exemplo de cálculo automático
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

            phHorarios.Controls.Add(new Literal { Text = $"<span class='dia-horario-titulo'>{diaItem.Text}</span>" });

            Panel container = new Panel();
            container.CssClass = "horarios-container";

            foreach (var horario in horarios)
            {
                CheckBox chk = new CheckBox
                {
                    ID = $"chk_{idDia}_{horario.Key}",
                    Text = horario.Value,
                    CssClass = "horario-item"
                };

                container.Controls.Add(chk);
            }

            phHorarios.Controls.Add(container);
        }
    }
}


    }
}
