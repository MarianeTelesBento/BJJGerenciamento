using BJJGerenciamento.UI.DAL;
using System;
using System.Web.UI;

namespace BJJGerenciamento.UI
{
    public partial class EditarUsuario : System.Web.UI.Page
    {
        private LoginDAL loginDal = new LoginDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                string usuario = Request.QueryString["usuario"];
                if (!string.IsNullOrEmpty(usuario))
                {
                    CarregarDadosUsuario(usuario);
                }
                else
                {
                    Response.Redirect("EditarUsuario.aspx");
                }
            }
        }

        private void CarregarDadosUsuario(string usuario)
        {
            var usuarioModel = loginDal.ObterUsuario(usuario);
            if (usuarioModel != null)
            {
                // Preenche os TextBoxes com os dados do usuário
                txtUsuario.Text = usuarioModel.Usuario;
                txtEmail.Text = usuarioModel.Email;
                txtSenha.Text = usuarioModel.Senha;
                // Armazena o IdLogin em ViewState para usar no update
                ViewState["IdLogin"] = usuarioModel.IdLogin;
            }
            else
            {
                lblMensagem.Text = "Usuário não encontrado.";
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ViewState["IdLogin"] != null)
            {
                int idLogin = (int)ViewState["IdLogin"];
                string novoEmail = txtEmail.Text.Trim();
                string novaSenha = txtSenha.Text.Trim();

                if (string.IsNullOrEmpty(novoEmail) || string.IsNullOrEmpty(novaSenha))
                {
                    lblMensagem.CssClass = "text-danger";
                    lblMensagem.Text = "Todos os campos são obrigatórios.";
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(novoEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    lblMensagem.CssClass = "text-danger";
                    lblMensagem.Text = "E-mail inválido.";
                    return;
                }

                if (novaSenha.Length < 6)
                {
                    lblMensagem.CssClass = "text-danger";
                    lblMensagem.Text = "A senha deve conter pelo menos 6 caracteres.";
                    return;
                }

                bool sucesso = loginDal.AtualizarUsuario(idLogin, novoEmail, novaSenha);
                if (sucesso)
                {
                    lblMensagem.CssClass = "text-success";
                    lblMensagem.Text = "Dados atualizados com sucesso.";
                }
                else
                {
                    lblMensagem.CssClass = "text-danger";
                    lblMensagem.Text = "Erro ao atualizar os dados.";
                }
            }
            else
            {
                lblMensagem.Text = "Falha ao obter dados do usuário para atualização.";
            }
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx"); 
        }
    }
}
