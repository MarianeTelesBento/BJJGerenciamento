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
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");

            }

            CarregarDiasSemana();

            if (!IsPostBack)
            {
                CarregarTurmas();
                phHorariosEdit.Controls.Clear();
            }
            else
            {
                // 🔹 SOMENTE recarrega os horários se dias ainda existirem selecionados
                if (int.TryParse(modalIdPlano.Text, out int idPlano) && idPlano > 0)
                {
                    var diasSelecionados = cblDiasEdit.Items.Cast<ListItem>()
                                            .Where(i => i.Selected)
                                            .Select(i => int.Parse(i.Value))
                                            .ToList();
                    CarregarHorariosPorDia(diasSelecionados, idPlano);
                }
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
            if (cblDiasEdit.Items.Count == 0) // Evita sobrescrever seleção no postback
            {
                var dias = planoDAL.BuscarDiasSemana();
                cblDiasEdit.DataSource = dias;
                cblDiasEdit.DataTextField = "Value";
                cblDiasEdit.DataValueField = "Key";
                cblDiasEdit.DataBind();
            }

            cblDiasEdit.AutoPostBack = true;
        }

        private void CarregarHorariosPorDia(List<int> diasSelecionados, int idPlano = 0)
        {
            // 🔸 Salvar os horários já marcados antes de limpar
            var horariosSelecionadosAnteriores = new HashSet<string>();
            foreach (Control ctrl in phHorariosEdit.Controls)
            {
                if (ctrl is CheckBox cb && cb.Checked)
                {
                    horariosSelecionadosAnteriores.Add(cb.ID);
                }
            }

            // 🔸 Limpa os controles
            phHorariosEdit.Controls.Clear();

            if (diasSelecionados == null || diasSelecionados.Count == 0)
                return;

            var todosHorarios = planoDAL.BuscarHorarios();
            var todosDias = planoDAL.BuscarDiasSemana();

            var horariosDoPlano = idPlano > 0
                ? planoDAL.BuscarHorariosPorPlano(idPlano)
                : new Dictionary<int, List<PlanoHorarioModels>>();

            foreach (int idDia in diasSelecionados)
            {
                var diaNome = todosDias.FirstOrDefault(d => d.Key == idDia).Value ?? "Dia desconhecido";
                phHorariosEdit.Controls.Add(new LiteralControl($"<strong>{diaNome}</strong><br />"));

                var horariosMarcadosDoPlano = horariosDoPlano.ContainsKey(idDia)
                    ? horariosDoPlano[idDia].Select(h => h.idHora).ToList()
                    : new List<int>();

                foreach (var h in todosHorarios)
                {
                    string checkboxId = $"chkHorario_{idDia}_{h.Key}";

                    CheckBox cb = new CheckBox
                    {
                        ID = checkboxId,
                        Text = h.Value,
                        CssClass = "form-check-input",
                        Checked =
                            horariosMarcadosDoPlano.Contains(h.Key) || 
                            horariosSelecionadosAnteriores.Contains(checkboxId) 
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
            modalAtivo.Checked = turma.Ativo;
           
            // 🔹 Marcar dias salvos no banco
            var diasSelecionados = planoDAL.ListarDiasDoPlano(idPlano);
            foreach (ListItem item in cblDiasEdit.Items)
            {
                item.Selected = diasSelecionados.Contains(int.Parse(item.Value));
            }

            // 🔹 Carregar e marcar os horários corretos
            CarregarHorariosPorDia(diasSelecionados, idPlano);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModal", "abrirModal();", true);
        }

        protected void SalvarTurma_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(modalIdPlano.Text, out int idPlano))
                idPlano = 0;


            PlanoModels turmaAtualizada = new PlanoModels
            {
                IdPlano = idPlano,
                Nome = modalNome.Text.Trim(),
                Ativo = modalAtivo.Checked,
             
                QtdDias = cblDiasEdit.Items.Cast<ListItem>().Count(i => i.Selected)



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
                            if (parts.Length == 3 &&
                                int.TryParse(parts[1], out int diaCheck) &&
                                int.TryParse(parts[2], out int idHora))
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
        }
    }
}
