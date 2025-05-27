using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;

namespace BJJGerenciamento.UI
{
    public partial class CadastrarTurmas : System.Web.UI.Page
    {
        PlanoDAL planoDAL = new PlanoDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
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
            // Salvar seleções atuais
            var horarios = planoDAL.BuscarHorarios();

            // Dicionário para salvar os horários selecionados: chave = "idDia_idHora"
            var horariosSelecionados = new HashSet<string>();

            foreach (Control ctrl in phHorarios.Controls)
            {
                if (ctrl is Panel panel)
                {
                    foreach (Control subCtrl in panel.Controls)
                    {
                        if (subCtrl is CheckBox chk)
                        {
                            if (chk.Checked)
                            {
                                horariosSelecionados.Add(chk.ID); // Exemplo: "chk_1_3"
                            }
                        }
                    }
                }
            }

            // Limpar e recriar controles
            phHorarios.Controls.Clear();

            foreach (ListItem diaItem in cblDias.Items)
            {
                if (diaItem.Selected)
                {
                    int idDia = int.Parse(diaItem.Value);

                    // Título do dia
                    phHorarios.Controls.Add(new Literal { Text = $"<span class='dia-horario-titulo'>{diaItem.Text}</span>" });

                    Panel container = new Panel();
                    container.CssClass = "horarios-container";

                    foreach (var horario in horarios)
                    {
                        string chkId = $"chk_{idDia}_{horario.Key}";

                        CheckBox chk = new CheckBox
                        {
                            ID = chkId,
                            Text = horario.Value,
                            CssClass = "horario-item",
                            Checked = horariosSelecionados.Contains(chkId) // Mantém a seleção
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

            lblMensagem.Visible = false;
            lblMensagem.Text = "";

            // Validação do nome da turma
            if (string.IsNullOrWhiteSpace(nomePlano))
            {
                lblMensagem.Text = "Informe o nome da turma.";
                lblMensagem.Visible = true;
                return;
            }

            // Validação dos dias
            if (cblDias.SelectedItem == null)
            {
                lblMensagem.Text = "Selecione pelo menos um dia.";
                lblMensagem.Visible = true;
                return;
            }

            // Validação da mensalidade
            if (string.IsNullOrWhiteSpace(mensalidadeStr) ||
            !decimal.TryParse(mensalidadeStr, out decimal valor) ||
            valor <= 0)
            {
                lblMensagem.Text = "Por favor, informe uma sugestão de valor válida (o Valor não pode ser 0,00).";
                lblMensagem.Visible = true;
                return;
            }

            // Cria o novo plano e pega o ID gerado
            int idPlano = planoDAL.CriarNovoPlano(nomePlano);
            int diasSelecionados = 0;

            List<string> diasSemHorarios = new List<string>();

            // Percorre os dias selecionados
            foreach (ListItem diaItem in cblDias.Items)
            {
                if (diaItem.Selected)
                {
                    int idDia = int.Parse(diaItem.Value);
                    bool temHorarioSelecionado = false;

                    // Percorre os checkboxes de horário dentro dos panels
                    foreach (Control ctrl in phHorarios.Controls)
                    {
                        if (ctrl is Panel panel)
                        {
                            foreach (Control subCtrl in panel.Controls)
                            {
                                if (subCtrl is CheckBox chk && chk.ID.StartsWith($"chk_{idDia}_"))
                                {
                                    if (chk.Checked)
                                    {
                                        temHorarioSelecionado = true;
                                        int idHora = int.Parse(chk.ID.Split('_')[2]);
                                        planoDAL.VincularPlanoHorario(idPlano, idDia, idHora);
                                    }
                                }
                            }
                        }
                    }

                    if (!temHorarioSelecionado)
                    {
                        // Guarda os dias que não tiveram horário selecionado
                        diasSemHorarios.Add(diaItem.Text);
                    }
                    else
                    {
                        // Salva o vínculo do dia
                        planoDAL.VincularPlanoADia(idPlano, idDia);
                        diasSelecionados++;
                    }
                }
            }

            // Se houver dias sem horário, exibe a mensagem e para o processo
            if (diasSemHorarios.Count > 0)
            {
                lblMensagem.Text = "Os seguintes dias não possuem horários selecionados: " + string.Join(", ", diasSemHorarios) + ".";
                lblMensagem.Visible = true;
                return;
            }

            // Salva o valor do plano
            planoDAL.SalvarValorPlano(idPlano, diasSelecionados, valor);

            // Exibe alerta de sucesso e faz o redirect para a lista
            ClientScript.RegisterStartupScript(this.GetType(), "alert", @"
        alert('Turma cadastrada com sucesso!');
        window.location.href='ListaTurmas.aspx';
    ", true);
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
