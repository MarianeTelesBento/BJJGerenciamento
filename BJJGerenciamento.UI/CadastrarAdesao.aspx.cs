using System;
using System.Collections.Generic;
using System.Data;
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
                string eventTarget = Request["__EVENTTARGET"];
                string eventArgument = Request["__EVENTARGUMENT"];
                if (eventTarget == "buscarAdesao" && int.TryParse(eventArgument, out int id))
                {
                    CarregarDadosParaEditar(id);
                }
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
            chkListTurmas.DataTextField = "Nome";      // ou "NomeTurma"
            chkListTurmas.DataValueField = "IdPlano";  // ou "IdTurma"
            chkListTurmas.DataBind();
        }

        private void CarregarFrequencias()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Dias");

            for (int i = 1; i <= 5; i++)
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
                Frequencias = new List<FrequenciaAdesaoModels>(),
                IdsPlanos = new List<int>()
            };

            bool peloMenosUmaFrequenciaValida = false;
            decimal maiorMensalidade = 0;

            foreach (ListItem item in chkFrequencias.Items)
            {
                if (item.Selected && int.TryParse(item.Value, out int qtdDias))
                {
                    string valorTexto = Request.Form["valor_" + qtdDias];

                    if (decimal.TryParse(valorTexto, out decimal mensalidade) && mensalidade > 0)
                    {
                        adesao.Frequencias.Add(new FrequenciaAdesaoModels
                        {
                            QtdDiasPermitidos = qtdDias,
                            Mensalidade = mensalidade
                        });

                        // Captura a maior mensalidade, caso você use isso em TBAdesao
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

            // Definir o máximo de dias selecionado
            adesao.QtdDiasPermitidos = adesao.Frequencias.Max(f => f.QtdDiasPermitidos);

            // Se TBAdesao também tem uma coluna Mensalidade, descomente abaixo
            // adesao.Mensalidade = maiorMensalidade;

            // Captura planos/turmas selecionados
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
                int index = Convert.ToInt32(e.CommandArgument);
                int idAdesao = Convert.ToInt32(gridAdesoes.DataKeys[index].Value);

                CarregarDadosParaEditar(idAdesao);

                // Exibe o modal depois de carregar os dados
                ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "var modal = new bootstrap.Modal(document.getElementById('modalEditarAdesao')); modal.show();", true);

            }
            else if (e.CommandName == "Excluir")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idAdesao = Convert.ToInt32(gridAdesoes.DataKeys[index].Value);

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
            for (int i = 1; i <= 6; i++)
            {
                string valorStr = Request.Form["valor_edit_" + i];
                string idFreqStr = Request.Form["id_freq_edit_" + i]; // NOVO: Captura o ID da frequência

                if (decimal.TryParse(valorStr, out decimal mensalidade))
                {
                    // Apenas adiciona se a mensalidade for maior que zero ou se houver um ID existente
                    // Isso evita salvar frequências vazias se o usuário limpar o campo
                    if (mensalidade > 0 || (int.TryParse(idFreqStr, out int existingIdFreq) && existingIdFreq > 0))
                    {
                        frequencias.Add(new FrequenciaAdesaoModels
                        {
                            IdFrequencia = int.TryParse(idFreqStr, out int parsedIdFreq) ? parsedIdFreq : 0, // Converte para int ou 0 se for novo
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

            PlanoDAL dal = new PlanoDAL(); // Certifique-se de que esta é a instância correta (AdesaoDAL, não PlanoDAL, se você separou)
            dal.AtualizarAdesao(adesao);

            ScriptManager.RegisterStartupScript(this, GetType(), "FecharModal", "$('#modalEditarAdesao').modal('hide');", true);
            CarregarAdesoes(); // Recarrega a grid para mostrar as atualizações
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

            for (int i = 1; i <= 6; i++)
            {
                var freq = adesao.Frequencias?.FirstOrDefault(f => f.QtdDiasPermitidos == i);
                string val = "";
                string idFreq = "0"; // Valor padrão para novo ou inexistente

                if (freq != null)
                {
                    val = freq.Mensalidade.ToString("N2").Replace(",", ".");
                    idFreq = freq.IdFrequencia.ToString(); // Captura o ID da frequência
                }
                scriptBuilder.AppendLine($"document.getElementById('valor_edit_{i}').value = '{val}';");
                scriptBuilder.AppendLine($"document.getElementById('id_freq_edit_{i}').value = '{idFreq}';"); // Preenche o hidden field com o ID
            }

            scriptBuilder.AppendLine("var modal = new bootstrap.Modal(document.getElementById('modalEditarAdesao')); modal.show();");
            ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalEditar", scriptBuilder.ToString(), true);

            // Carrega e marca as turmas (essa parte parece estar OK)
            var turmas = planoDAL.ListarTurmas(); // Supondo que ListarTurmas retorna todos os planos
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

