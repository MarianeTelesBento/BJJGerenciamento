using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
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
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                CarregarAdesoes();
                CarregarTurmas();
                CarregarFrequencias();
            }

            string eventTarget = Request["__EVENTTARGET"];
            string eventArgument = Request["__EVENTARGUMENT"];
            if (eventTarget == "buscarAdesao" && int.TryParse(eventArgument, out int id))
            {
                CarregarDadosParaEditar(id);
            }
        }

        // Método auxiliar para exibir o status VIP na GridView
        public string GetVipText(object isVip, object valorVip)
        {
            if (isVip != DBNull.Value && (bool)isVip)
            {
                return $"Sim (R$ {(valorVip != DBNull.Value ? string.Format("{0:N2}", valorVip) : "N/A")})";
            }
            return "Não";
        }

        // Método auxiliar para exibir as frequências na GridView
        public string GetFrequenciasTexto(object frequencias)
        {
            if (frequencias is List<FrequenciaAdesaoModels> lista)
            {
                var frequenciasValidas = lista.Where(f => f.Mensalidade > 0).OrderBy(f => f.QtdDiasPermitidos).ToList();
                return string.Join(", ", frequenciasValidas.Select(f => $"{f.QtdDiasPermitidos}x: R$ {f.Mensalidade:N2}"));
            }
            return "N/A";
        }

        private void CarregarAdesoes()
        {
            var lista = dal.ListarAdesoesComFrequencias();
            gridAdesoes.DataSource = lista;
            gridAdesoes.DataBind();
        }

        private void CarregarTurmas()
        {
            var turmas = dal.ListarTurmasAdesao();
            chkListTurmas.DataSource = turmas;
            chkListTurmas.DataTextField = "Nome";
            chkListTurmas.DataValueField = "IdPlano";
            chkListTurmas.DataBind();
            CarregarFrequencias();
        }

        private void CarregarFrequencias()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Dias");

            for (int i = 1; i <= 7; i++)
            {
                DataRow row = dt.NewRow();
                row["Dias"] = i;
                dt.Rows.Add(row);
            }

            chkFrequencias.DataSource = dt;
            chkFrequencias.DataTextField = "Dias";
            chkFrequencias.DataValueField = "Dias";
            chkFrequencias.DataBind();

            rptFrequencias.DataSource = dt;
            rptFrequencias.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            bool isVip = chkIsVip.Checked;
            decimal? valorVip = null;
            if (isVip && !string.IsNullOrWhiteSpace(txtValorVip.Text))
            {
                decimal tempValorVip;
                // CORREÇÃO APLICADA AQUI
                string valorVipLimpo = txtValorVip.Text.Replace(".", "").Replace(",", ".");
                if (decimal.TryParse(valorVipLimpo, NumberStyles.Any, CultureInfo.InvariantCulture, out tempValorVip))
                {
                    valorVip = tempValorVip;
                }
            }

            var adesao = new AdesaoModels
            {
                NomeAdesao = txtNomeAdesao.Text.Trim(),
                IsVip = isVip,
                ValorVip = valorVip ?? 0,
                Frequencias = new List<FrequenciaAdesaoModels>(),
                IdsPlanos = new List<int>()
            };

            bool peloMenosUmaFrequenciaValida = false;
            foreach (ListItem item in chkFrequencias.Items)
            {
                if (item.Selected && int.TryParse(item.Value, out int qtdDias))
                {
                    var itemRepeater = rptFrequencias.Items.Cast<RepeaterItem>().FirstOrDefault(r => ((HiddenField)r.FindControl("hdnFrequenciaId")).Value == qtdDias.ToString());
                    if (itemRepeater != null)
                    {
                        string valorTexto = ((TextBox)itemRepeater.FindControl("txtValorFrequencia")).Text;
                        if (!string.IsNullOrWhiteSpace(valorTexto))
                        {
                            decimal mensalidade;
                            // CORREÇÃO APLICADA AQUI
                            string valorMensalidadeLimpo = valorTexto.Replace(".", "").Replace(",", ".");
                            if (decimal.TryParse(valorMensalidadeLimpo, NumberStyles.Any, CultureInfo.InvariantCulture, out mensalidade) && mensalidade > 0)
                            {
                                adesao.Frequencias.Add(new FrequenciaAdesaoModels
                                {
                                    QtdDiasPermitidos = qtdDias,
                                    Mensalidade = mensalidade
                                });
                                peloMenosUmaFrequenciaValida = true;
                            }
                        }
                    }
                }
            }

            if (!peloMenosUmaFrequenciaValida && !isVip)
            {
                lblMensagem.Text = "Selecione ao menos uma frequência válida e preencha a mensalidade, ou ative o plano VIP.";
                return;
            }

            foreach (ListItem item in chkListTurmas.Items)
            {
                if (item.Selected && int.TryParse(item.Value, out int idTurma))
                {
                    adesao.IdsPlanos.Add(idTurma);
                }
            }
            dal.InserirAdesaoComFrequencias(adesao);
            lblMensagem.Text = "Adesão cadastrada com sucesso!";
            CarregarAdesoes();
            txtNomeAdesao.Text = "";
            chkIsVip.Checked = false;
            txtValorVip.Text = "";
            chkFrequencias.ClearSelection();
            chkListTurmas.ClearSelection();


        }

        protected void gridAdesoes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int idAdesao = Convert.ToInt32(e.CommandArgument);
                CarregarDadosParaEditar(idAdesao);
            }
            else if (e.CommandName == "Excluir")
            {
                int idAdesao = Convert.ToInt32(e.CommandArgument);
                dal.ExcluirAdesao(idAdesao);
                CarregarAdesoes();
                lblMensagem.Text = "Adesão excluída com sucesso!";
            }
        }
        protected void btnConfirmarExclusao_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hdnIdAdesaoExcluir.Value);
            string scriptFecharModal = "$('#modalConfirmarExclusao').modal('hide');";

            if (dal.VerificarAdesaoEmUso(id))
            {
                string mensagemErro = "Esta adesão não pode ser excluída, pois existem alunos cadastrados nela.";

                // MUDANÇA 1: O alert() vem ANTES de fechar o modal.
                string scriptErro = $"alert('{mensagemErro}'); {scriptFecharModal}";

                
                ScriptManager.RegisterStartupScript((Control)sender, this.GetType(), "ErroExclusao", scriptErro, true);
            }
            else
            {
                try
                {
                    dal.ExcluirAdesao(id);
                    CarregarAdesoes();
                    lblMensagem.Text = "Adesão excluída com sucesso!";
                    lblMensagem.CssClass = "text-success d-block mb-4";

                    // Aplicando as mesmas mudanças aqui para consistência
                    ScriptManager.RegisterStartupScript((Control)sender, this.GetType(), "FecharModalSucesso", scriptFecharModal, true);
                }
                catch (Exception ex)
                {
                    // E aqui também...
                    string mensagemErroInesperado = $"Ocorreu um erro inesperado: {ex.Message.Replace("'", "\\'").Replace("\r\n", " ")}";
                    string scriptErroInesperado = $"alert('{mensagemErroInesperado}'); {scriptFecharModal}";
                    ScriptManager.RegisterStartupScript((Control)sender, this.GetType(), "ErroInesperado", scriptErroInesperado, true);
                }
            }
        }
        private void CarregarDadosParaEditar(int idAdesao)
        {
            // Mantendo o padrão de instanciar o DAL localmente, como no seu código original.
            PlanoDAL planoDAL = new PlanoDAL();
            AdesaoModels adesao = planoDAL.BuscarAdesaoPorId(idAdesao);

            if (adesao == null) return;

            // 1. Preencher campos simples
            hdnIdAdesaoEditar.Value = adesao.IdAdesao.ToString();
            txtNomeAdesaoEditar.Text = adesao.NomeAdesao;
            chkIsVipEditar.Checked = adesao.IsVip;
            txtVipEditar.Text = adesao.ValorVip > 0 ? adesao.ValorVip.ToString("N2") : "";


            // 2. Carregar e selecionar as turmas (usando planoDAL.ListarTurmas() como no seu original)
            var turmas = planoDAL.ListarTurmas(); // Mantido conforme seu método original.
            chkListTurmasEditar.DataSource = turmas;
            chkListTurmasEditar.DataTextField = "Nome";
            chkListTurmasEditar.DataValueField = "IdPlano";
            chkListTurmasEditar.DataBind();

            foreach (ListItem item in chkListTurmasEditar.Items)
            {
                item.Selected = adesao.IdsPlanos != null && adesao.IdsPlanos.Contains(int.Parse(item.Value));
            }

            CarregarFrequenciasEditar(); 

            // Desmarcar seleções antigas
            foreach (ListItem item in chkFrequenciasEditar.Items)
            {
                item.Selected = false;
            }

            // Iterar sobre os itens do Repeater para preencher os valores via C#
            foreach (RepeaterItem itemRepeater in rptFrequenciasEditar.Items)
            {
                var txtValor = (TextBox)itemRepeater.FindControl("txtValorFrequencia");
                var hdnId = (HiddenField)itemRepeater.FindControl("hdnFrequenciaId");
                var hdnDbId = (HiddenField)itemRepeater.FindControl("hdnFrequenciaDbId");

                int dias = int.Parse(hdnId.Value);
                var frequenciaExistente = adesao.Frequencias.FirstOrDefault(f => f.QtdDiasPermitidos == dias);

                if (frequenciaExistente != null)
                {
                    // Marca o checkbox e preenche o textbox e hidden fields
                    chkFrequenciasEditar.Items.FindByValue(dias.ToString()).Selected = true;
                    txtValor.Text = frequenciaExistente.Mensalidade.ToString("N2");
                    hdnDbId.Value = frequenciaExistente.IdFrequencia.ToString();
                }
                else
                {
                    // Limpa os campos se não houver dados
                    txtValor.Text = "";
                    hdnDbId.Value = "0";
                }
            }

            // 4. Registrar o script para ABRIR O MODAL
            string script = "var modal = new bootstrap.Modal(document.getElementById('modalEditarAdesao')); modal.show(); aplicarMascaras();";
            ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalEditar", script, true);
        }

        private void CarregarFrequenciasEditar()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Dias");

            for (int i = 1; i <= 7; i++)
            {
                DataRow row = dt.NewRow();
                row["Dias"] = i;
                dt.Rows.Add(row);
            }

            chkFrequenciasEditar.DataSource = dt;
            chkFrequenciasEditar.DataTextField = "Dias";
            chkFrequenciasEditar.DataValueField = "Dias";
            chkFrequenciasEditar.DataBind();

            rptFrequenciasEditar.DataSource = dt;
            rptFrequenciasEditar.DataBind();
        }
        protected void btnSalvarEdicao_Click(object sender, EventArgs e)
        {
            int idAdesao = int.Parse(hdnIdAdesaoEditar.Value);
            string nome = txtNomeAdesaoEditar.Text;

            bool isVip = chkIsVipEditar.Checked;
            decimal? valorVip = null;
            if (isVip && !string.IsNullOrWhiteSpace(txtVipEditar.Text))
            {
                decimal tempValorVip;
                // GARANTINDO A CORREÇÃO AQUI
                string valorVipLimpo = txtVipEditar.Text.Replace(".", "").Replace(",", ".");
                if (decimal.TryParse(valorVipLimpo, NumberStyles.Any, CultureInfo.InvariantCulture, out tempValorVip))
                {
                    valorVip = tempValorVip;
                }
            }

            List<FrequenciaAdesaoModels> frequencias = new List<FrequenciaAdesaoModels>();
            foreach (RepeaterItem itemRepeater in rptFrequenciasEditar.Items)
            {
                var chkFrequencia = chkFrequenciasEditar.Items.FindByValue(((HiddenField)itemRepeater.FindControl("hdnFrequenciaId")).Value);
                if (chkFrequencia != null && chkFrequencia.Selected)
                {
                    int qtdDias = int.Parse(chkFrequencia.Value);
                    string valorTexto = ((TextBox)itemRepeater.FindControl("txtValorFrequencia")).Text;
                    string idFreqStr = ((HiddenField)itemRepeater.FindControl("hdnFrequenciaDbId")).Value;
                    decimal mensalidade;

                    // GARANTINDO A CORREÇÃO AQUI
                    string valorMensalidadeLimpo = valorTexto.Replace(".", "").Replace(",", ".");
                    if (decimal.TryParse(valorMensalidadeLimpo, NumberStyles.Any, CultureInfo.InvariantCulture, out mensalidade))
                    {
                        frequencias.Add(new FrequenciaAdesaoModels
                        {
                            IdFrequencia = int.TryParse(idFreqStr, out int parsedIdFreq) ? parsedIdFreq : 0,
                            QtdDiasPermitidos = qtdDias,
                            Mensalidade = mensalidade
                        });
                    }
                }
            }

            // O resto do seu método continua igual...
            List<int> idsPlanos = chkListTurmasEditar.Items
               .Cast<ListItem>()
               .Where(i => i.Selected)
               .Select(i => int.Parse(i.Value))
               .ToList();

            AdesaoModels adesao = new AdesaoModels
            {
                IdAdesao = idAdesao,
                NomeAdesao = nome,
                IsVip = isVip,
                ValorVip = valorVip ?? 0,
                Frequencias = frequencias,
                IdsPlanos = idsPlanos
            };

            dal.AtualizarAdesao(adesao);
            ScriptManager.RegisterStartupScript(this, GetType(), "FecharModal", "$('#modalEditarAdesao').modal('hide');", true);
            CarregarAdesoes();
            lblMensagem.Text = "Adesão atualizada com sucesso!";
        }

    }
}

