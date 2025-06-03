using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BJJGerenciamento.UI
{
    public partial class ListaHorario : Page
    {
        HorarioDAL horarioDAL = new HorarioDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                pnlCadastro.Visible = false;
                CarregarGrid();
            }
        }

        private void CarregarGrid()
        {
            try
            {
                var listaRaw = horarioDAL.ListarTodos();

                var listaFormatada = listaRaw.Select(h => new
                {
                    h.IdHora,
                    HorarioInicioStr = h.HorarioInicio.ToString(@"hh\:mm"),
                    HorarioFimStr = h.HorarioFim.ToString(@"hh\:mm"),
                    h.Ativa
                }).ToList();

                gvHorario.DataSource = listaFormatada;
                gvHorario.DataBind();

                lblMensagem.Text = "";
            }
            catch (Exception ex)
            {
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                lblMensagem.Text = "Erro ao carregar horários: " + ex.Message;
            }
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            pnlCadastro.Visible = true;
            lblId.Text = "0";
            txtHorarioInicio.Text = "";
            txtHorarioFim.Text = "";
            lblMensagemCadastro.Text = "";

            // Define como novo no JavaScript
            btnSalvar.OnClientClick = "return confirmarSalvar(true);";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlCadastro.Visible = false;
            lblMensagemCadastro.Text = "";
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            lblMensagemCadastro.ForeColor = System.Drawing.Color.Red;
            lblMensagemCadastro.Text = "";

            // Validação dos horários
            if (!TimeSpan.TryParse(txtHorarioInicio.Text.Trim(), out TimeSpan inicio))
            {
                lblMensagemCadastro.Text = "Horário Início inválido. Use o formato HH:mm.";
                return;
            }
            if (!TimeSpan.TryParse(txtHorarioFim.Text.Trim(), out TimeSpan fim))
            {
                lblMensagemCadastro.Text = "Horário Fim inválido. Use o formato HH:mm.";
                return;
            }
            if (fim <= inicio)
            {
                lblMensagemCadastro.Text = "Horário Fim deve ser maior que Horário Início.";
                return;
            }

            try
            {
                if (lblId.Text == "0")
                {
                    HoraModels novoHora = new HoraModels
                    {
                        HorarioInicio = inicio,
                        HorarioFim = fim,
                        Ativa = true
                    };
                    horarioDAL.Inserir(novoHora);
                    lblMensagem.ForeColor = System.Drawing.Color.Green;
                    lblMensagem.Text = "Horário inserido com sucesso!";
                }
                else
                {
                    HoraModels horaAlterada = new HoraModels
                    {
                        IdHora = Convert.ToInt32(lblId.Text),
                        HorarioInicio = inicio,
                        HorarioFim = fim,
                        Ativa = true
                    };
                    horarioDAL.Atualizar(horaAlterada);
                    lblMensagem.ForeColor = System.Drawing.Color.Green;
                    lblMensagem.Text = "Horário atualizado com sucesso!";
                }

                pnlCadastro.Visible = false;
                CarregarGrid();
            }
            catch (Exception ex)
            {
                lblMensagemCadastro.Text = "Erro ao salvar horário: " + ex.Message;
            }
        }

        protected void gvHorario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int idHora = Convert.ToInt32(e.CommandArgument);
                HoraModels hora = horarioDAL.ListarTodos().Find(h => h.IdHora == idHora);
                if (hora != null)
                {
                    pnlCadastro.Visible = true;
                    lblId.Text = hora.IdHora.ToString();
                    txtHorarioInicio.Text = hora.HorarioInicio.ToString(@"hh\:mm");
                    txtHorarioFim.Text = hora.HorarioFim.ToString(@"hh\:mm");
                    lblMensagemCadastro.Text = "";

                    // Define como edição no JavaScript
                    btnSalvar.OnClientClick = "return confirmarSalvar(false);";
                }
            }
            else if (e.CommandName == "ToggleStatus")
            {
                int idHora = Convert.ToInt32(e.CommandArgument);
                HoraModels hora = horarioDAL.ListarTodos().Find(h => h.IdHora == idHora);
                if (hora != null)
                {
                    try
                    {
                        horarioDAL.AlterarStatus(idHora, !hora.Ativa);
                        lblMensagem.ForeColor = System.Drawing.Color.Green;
                        lblMensagem.Text = $"Horário {(hora.Ativa ? "desativado" : "ativado")} com sucesso!";
                        CarregarGrid();
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.ForeColor = System.Drawing.Color.Red;
                        lblMensagem.Text = "Erro ao alterar status: " + ex.Message;
                    }
                }
            }
        }
    }
}
