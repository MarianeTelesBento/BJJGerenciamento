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
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                lblMensagem.Text = "Todos os campos são obrigatórios.";
                return;
            }
            LoginDAL loginDAL = new LoginDAL();
            try
            {
                loginDAL.CadastrarUsuario(usuario, email, senha);
                lblMensagem.Text = "Usuário cadastrado com sucesso!";
                txtUsuario.Text = "";
                txtEmail.Text = "";
                txtSenha.Text = "";
                LimparCampos();
            }

            catch (Exception ex)
            {
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