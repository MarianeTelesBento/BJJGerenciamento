using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;

namespace BJJGerenciamento.UI
{
    public partial class ListaTurmas : Page
    {
        PlanoDAL planoDAL = new PlanoDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarTurmas();
                CarregarDiasSemana();
                phHorariosEdit.Controls.Clear();
            }
        }

        private void CarregarTurmas()
        {
            List<PlanoModels> turmas = planoDAL.ListarTurmas();
            GridViewTurmas.DataSource = turmas;
            GridViewTurmas.DataBind();
        }

        private void CarregarDiasSemana()
        {
            var dias = planoDAL.BuscarDiasSemana();
            cblDiasEdit.DataSource = dias;
            cblDiasEdit.DataTextField = "Value";
            cblDiasEdit.DataValueField = "Key";
            cblDiasEdit.DataBind();

            cblDiasEdit.AutoPostBack = true;
            cblDiasEdit.SelectedIndexChanged += cblDiasEdit_SelectedIndexChanged;
        }

        private void CarregarHorariosPorDia(List<int> diasSelecionados)
        {
            phHorariosEdit.Controls.Clear();

            if (diasSelecionados == null || diasSelecionados.Count == 0)
                return;

            var todosHorarios = planoDAL.BuscarHorarios();
            var todosDias = planoDAL.BuscarDiasSemana();

            foreach (int idDia in diasSelecionados)
            {
                var diaNome = todosDias.FirstOrDefault(d => d.Key == idDia).Value ?? "Dia desconhecido";
                phHorariosEdit.Controls.Add(new LiteralControl($"<strong>{diaNome}</strong><br />"));

                var horariosDoDia = planoDAL.BuscarHorariosPorDia(idDia).Select(h => h.Key).ToList();

                foreach (var h in todosHorarios)
                {
                    CheckBox cb = new CheckBox
                    {
                        ID = $"chkHorario_{idDia}_{h.Key}",
                        Text = h.Value,
                        CssClass = "form-check-input",
                        Checked = horariosDoDia.Contains(h.Key)
                    };
                    cb.InputAttributes["style"] = "margin-right:10px;";

                    phHorariosEdit.Controls.Add(cb);
                    phHorariosEdit.Controls.Add(new LiteralControl("<br />"));
                }
                phHorariosEdit.Controls.Add(new LiteralControl("<hr />"));
            }
        }

        protected void btnDetalhes_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            int idPlano = Convert.ToInt32(GridViewTurmas.DataKeys[row.RowIndex]["IdPlano"]);

            PlanoModels turma = planoDAL.ObterTurmaPorId(idPlano);

            modalIdPlano.Text = turma.IdPlano.ToString();
            modalNome.Text = turma.Nome;
            modalQtdDias.Text = turma.QtdDias.ToString();
            modalMensalidade.Text = turma.Mensalidade.ToString("F2");
            modalAtivo.Checked = turma.Ativo;

            var diasSelecionados = planoDAL.ListarDiasDoPlano(idPlano);
            for (int i = 0; i < cblDiasEdit.Items.Count; i++)
            {
                int idDia = Convert.ToInt32(cblDiasEdit.Items[i].Value);
                cblDiasEdit.Items[i].Selected = diasSelecionados.Contains(idDia);
            }

            CarregarHorariosPorDia(diasSelecionados);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModal", "abrirModal();", true);
        }

        protected void SalvarTurma_Click(object sender, EventArgs e)
        {
            PlanoModels turmaAtualizada = new PlanoModels
            {
                IdPlano = int.Parse(modalIdPlano.Text),
                Nome = modalNome.Text,
                QtdDias = int.Parse(modalQtdDias.Text),
                Mensalidade = decimal.Parse(modalMensalidade.Text),
                Ativo = modalAtivo.Checked
            };

            planoDAL.AtualizarTurma(turmaAtualizada);

            List<int> diasSelecionados = new List<int>();
            foreach (ListItem item in cblDiasEdit.Items)
            {
                if (item.Selected)
                {
                    diasSelecionados.Add(int.Parse(item.Value));
                }
            }
            planoDAL.AtualizarDiasDoPlano(turmaAtualizada.IdPlano, diasSelecionados);

            List<(int IdDia, int IdHora)> horariosSelecionados = new List<(int, int)>();

            foreach (ListItem itemDia in cblDiasEdit.Items)
            {
                if (itemDia.Selected)
                {
                    int idDia = int.Parse(itemDia.Value);

                    foreach (Control ctrl in phHorariosEdit.Controls)
                    {
                        if (ctrl is CheckBox cb)
                        {
                            string[] parts = cb.ID.Split('_');
                            if (parts.Length == 3 && int.TryParse(parts[1], out int diaCheck) && int.TryParse(parts[2], out int idHora))
                            {
                                if (diaCheck == idDia && cb.Checked)
                                {
                                    horariosSelecionados.Add((idDia, idHora));
                                }
                            }
                        }
                    }
                }
            }

            planoDAL.AtualizarHorariosDoPlanoComDias(turmaAtualizada.IdPlano, horariosSelecionados);


            CarregarTurmas();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "fecharModal", "fecharModal();", true);
        }

        protected void cblDiasEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> diasSelecionados = new List<int>();
            foreach (ListItem item in cblDiasEdit.Items)
            {
                if (item.Selected)
                    diasSelecionados.Add(int.Parse(item.Value));
            }

            CarregarHorariosPorDia(diasSelecionados);
        }

        protected void GridViewTurmas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Método não utilizado
        }
    }
}
