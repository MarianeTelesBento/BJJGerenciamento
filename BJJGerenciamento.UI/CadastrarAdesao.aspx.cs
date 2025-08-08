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
            if (!IsPostBack)
            {
                CarregarAdesoes();
                CarregarTurmas();
                CarregarFrequencias();
            }
            // ALTERAÇÃO: Lógica para tratar o postback de edição.
            // O OnClientClick do botão de edição agora dispara um postback com o ID.
            string eventTarget = Request["__EVENTTARGET"];
            string eventArgument = Request["__EVENTARGUMENT"];
            if (eventTarget == "buscarAdesao" && int.TryParse(eventArgument, out int id))
            {
                CarregarDadosParaEditar(id);
            }
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
            chkListTurmas.DataTextField = "Nome";      // ou "NomeTurma"
            chkListTurmas.DataValueField = "IdPlano";  // ou "IdTurma"
            chkListTurmas.DataBind();
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
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var adesao = new AdesaoModels
            {
                NomeAdesao = txtNomeAdesao.Text.Trim(),
                Frequencias = new List<FrequenciaAdesaoModels>(), // CORRIGIDO: Tipo do modelo
                IdsPlanos = new List<int>()
            };

            bool peloMenosUmaFrequenciaValida = false;
            decimal maiorMensalidade = 0;

            foreach (ListItem item in chkFrequencias.Items)
            {
                if (item.Selected && int.TryParse(item.Value, out int qtdDias))
                {
                    string valorTexto = Request.Form["valor_" + qtdDias];

                    // ALTERAÇÃO: Usar InvariantCulture para garantir a leitura correta
                    if (decimal.TryParse(valorTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal mensalidade) && mensalidade > 0)
                    {
                        adesao.Frequencias.Add(new FrequenciaAdesaoModels
                        {
                            IdFrequencia = 0, // IdFrequencia é 0 para novas inserções
                            QtdDiasPermitidos = qtdDias,
                            Mensalidade = mensalidade
                        });
                        if (mensalidade > maiorMensalidade)
                            maiorMensalidade = mensalidade;
                        peloMenosUmaFrequenciaValida = true;
                    }
                }
            }

            if (!peloMenosUmaFrequenciaValida)
            {
                lblMensagem.Text = "Selecione ao menos uma frequência válida e preencha a mensalidade.";
                return;
            }

            // CORRIGIDO: A propriedade QtdDiasPermitidos não existe em AdesaoModels
            // AdesaoModels precisa apenas da lista de frequências
            //adesao.QtdDiasPermitidos = adesao.Frequencias.Max(f => f.QtdDiasPermitidos);

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
        }

        protected void gridAdesoes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                // ALTERAÇÃO: O CommandArgument é o ID, não o índice.
                int idAdesao = Convert.ToInt32(e.CommandArgument);
                CarregarDadosParaEditar(idAdesao);
                // O modal é aberto no CarregarDadosParaEditar através do script
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
            dal.ExcluirAdesao(id);
            CarregarAdesoes();
            lblMensagem.Text = "Adesão excluída com sucesso!";
        }

        protected void btnSalvarEdicao_Click(object sender, EventArgs e)
        {
            int idAdesao = int.Parse(hdnIdAdesaoEditar.Value);
            string nome = txtNomeAdesaoEditar.Text;

            List<FrequenciaAdesaoModels> frequencias = new List<FrequenciaAdesaoModels>();
            for (int i = 1; i <= 7; i++)
            {
                string valorStr = Request.Form["valor_edit_" + i];
                string idFreqStr = Request.Form["id_freq_edit_" + i];

                // ALTERAÇÃO: Usar InvariantCulture para ler o valor formatado com ponto decimal
                if (decimal.TryParse(valorStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal mensalidade))
                {
                    if (mensalidade > 0 || (int.TryParse(idFreqStr, out int existingIdFreq) && existingIdFreq > 0))
                    {
                        frequencias.Add(new FrequenciaAdesaoModels
                        {
                            IdFrequencia = int.TryParse(idFreqStr, out int parsedIdFreq) ? parsedIdFreq : 0,
                            QtdDiasPermitidos = i,
                            Mensalidade = mensalidade
                        });
                    }
                }
            }

            List<int> idsPlanos = chkListTurmasEditar.Items
              .Cast<ListItem>()
              .Where(i => i.Selected)
              .Select(i => int.Parse(i.Value))
              .ToList();

            AdesaoModels adesao = new AdesaoModels
            {
                IdAdesao = idAdesao,
                NomeAdesao = nome,
                Frequencias = frequencias,
                IdsPlanos = idsPlanos
            };

            dal.AtualizarAdesao(adesao);

            ScriptManager.RegisterStartupScript(this, GetType(), "FecharModal", "$('#modalEditarAdesao').modal('hide');", true);
            CarregarAdesoes();
        }

        private void CarregarDadosParaEditar(int idAdesao)
        {
            PlanoDAL planoDAL = new PlanoDAL();
            AdesaoModels adesao = planoDAL.BuscarAdesaoPorId(idAdesao);

            if (adesao == null)
                return;

            txtNomeAdesaoEditar.Text = adesao.NomeAdesao;
            hdnIdAdesaoEditar.Value = adesao.IdAdesao.ToString();

            StringBuilder scriptBuilder = new StringBuilder();

            for (int i = 1; i <= 7; i++)
            {
                var freq = adesao.Frequencias?.FirstOrDefault(f => f.QtdDiasPermitidos == i);
                string val = "";
                string idFreq = "0";

                if (freq != null)
                {
                    // ALTERAÇÃO: Usar CultureInfo.InvariantCulture para formatar
                    val = freq.Mensalidade.ToString("F2", CultureInfo.InvariantCulture);
                    idFreq = freq.IdFrequencia.ToString();
                }
                scriptBuilder.AppendLine($"document.getElementById('valor_edit_{i}').value = '{val}';");
                scriptBuilder.AppendLine($"document.getElementById('id_freq_edit_{i}').value = '{idFreq}';");
            }

            scriptBuilder.AppendLine("var modal = new bootstrap.Modal(document.getElementById('modalEditarAdesao')); modal.show();");
            ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalEditar", scriptBuilder.ToString(), true);

            var turmas = planoDAL.ListarTurmas();
            chkListTurmasEditar.DataSource = turmas;
            chkListTurmasEditar.DataTextField = "Nome";
            chkListTurmasEditar.DataValueField = "IdPlano";
            chkListTurmasEditar.DataBind();

            foreach (ListItem item in chkListTurmasEditar.Items)
            {
                if (adesao.IdsPlanos != null && adesao.IdsPlanos.Contains(int.Parse(item.Value)))
                {
                    item.Selected = true;
                }
            }
        }
    }
}

