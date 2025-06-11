using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;

namespace BJJGerenciamento.UI
{
    public partial class CadastrarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }

        }
        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text.Trim();
            string confirmarSenha = txtConfirmarSenha.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(confirmarSenha))
            {
                lblMensagem.Text = "Todos os campos são obrigatórios.";
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblMensagem.Text = "E-mail inválido.";
                return;
            }

            if (senha != confirmarSenha)
            {
                lblMensagem.Text = "As senhas não coincidem.";
                return;
            }

            if (senha.Length < 6)
            {
                lblMensagem.Text = "A senha deve conter pelo menos 6 caracteres.";
                return;
            }        

            try
            {
                LoginDAL loginDAL = new LoginDAL();

                if (loginDAL.UsuarioExiste(usuario, email))
                {
                    lblMensagem.CssClass = "text-danger";
                    lblMensagem.Text = "Já existe um usuário com esse nome ou e-mail.";
                    return;
                }

                loginDAL.CadastrarUsuario(usuario, email, senha);

                lblMensagem.CssClass = "text-success";
                lblMensagem.Text = $"Usuário {usuario} cadastrado com sucesso!";
                LimparCampos();
            }
            catch (Exception ex)
            {
                lblMensagem.CssClass = "text-danger"; 
                lblMensagem.Text = "Erro ao cadastrar usuário: " + ex.Message;
            }
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        private void LimparCampos()
        {
            txtUsuario.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSenha.Text = string.Empty;
            lblMensagem.Text = string.Empty;
        }
    }
}