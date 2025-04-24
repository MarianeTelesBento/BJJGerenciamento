using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.Models;  
using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Service;

namespace BJJGerenciamento.UI
{
	public partial class CadastrarProfessor : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            ProfessorModels professor = new ProfessorModels
            {
                Nome = txtNome.Text,
                Sobrenome = txtSobrenome.Text,
                DataNasc = Convert.ToDateTime(txtDataNascimento.Text), // Certifique-se que a data está no formato correto
                Cpf = txtCpf.Text,
                Telefone = txtTelefone.Text,
                Email = txtEmail.Text,
                CEP = txtCep.Text.Replace("-", ""),
                Rua = txtRua.Text,
                Bairro = txtBairro.Text,
                CarteiraFPJJ = txtFpjj.Text,
                CarteiraCBJJ = txtCBJJ.Text,
                Numero = txtNumero.Text,
                Complemento = txtComplemento.Text,
                Cidade = txtCidade.Text,
                Estado = txtEstado.Text
            };

            ProfessorDAL professorDAL = new ProfessorDAL(professor);

            int cadastroFuncionando = professorDAL.CadastrarProfessor();

            if (cadastroFuncionando > 0)
            {
                ScriptManager.RegisterStartupScript(HttpContext.Current.Handler as Page,
                typeof(Page),
                "alerta",
                "alert('alert('Professor cadastrado com sucesso!'); window.location.href='ListaProfessores.aspx';",
                true);
            }
            else
            {
                Response.Write("<script>alert('Erro ao cadastrar professor.');</script>");
            }


            LimparCampos();

        }   
        private void LimparCampos()
        {
            txtNome.Text = "";
            txtSobrenome.Text = "";
            txtDataNascimento.Text = "";
            txtCpf.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            txtCep.Text = "";
            txtRua.Text = "";
            txtBairro.Text = "";
            txtNumero.Text = "";
            txtComplemento.Text = "";
            txtFpjj.Text = "";
            txtCBJJ.Text = "";
            txtCidade.Text = "";
            txtEstado.Text = "";
        }

        public static bool VerificarCampos(params TextBox[] campos)
        {
            if (campos.Any(campo => string.IsNullOrWhiteSpace(campo.Text)))
            {
                ScriptManager.RegisterStartupScript(HttpContext.Current.Handler as Page,
                    typeof(Page),
                    "alerta",
                    "alert('Preencha todos os campos');",
                    true);
                return false;
            }
            return true;
        }

        protected void cepResponsavel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void buscarCepResponsavel_Click(object sender, EventArgs e)
        {
            CepService cepService = new CepService();
            var ceplist = cepService.GetEndereco(txtCep.Text);

            txtRua.Text = ceplist.Rua;
            txtBairro.Text = ceplist.Bairro;
            txtCidade.Text = ceplist.Cidade;
            txtEstado.Text = ceplist.Estado;
        }
    }
}