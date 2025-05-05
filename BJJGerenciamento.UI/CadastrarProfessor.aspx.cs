using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using BJJGerenciamento.UI.Models;
using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Service;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BJJGerenciamento.UI
{
    public partial class CadastrarProfessor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            // Verifica se os validadores de servidor passaram
            if (!Page.IsValid)
            {
                return; // Se algum validador falhar, não faz nada
            }

            // Valida Nome e Sobrenome (apenas letras e espaços)
            if (!Regex.IsMatch(txtNome.Text, @"^[a-zA-Zà-úÀ-Ú\s]+$") || !Regex.IsMatch(txtSobrenome.Text, @"^[a-zA-Zà-úÀ-Ú\s]+$"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "erro", "alert('Nome ou Sobrenome inválido. Somente letras são permitidas.');", true);
                return;
            }

            // Valida CPF
            if (!CpfValido(txtCpf.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "erro", "alert('CPF inválido.');", true);
                return;
            }

            // Valida Telefone
            if (!Regex.IsMatch(txtTelefone.Text, @"^\(\d{2}\)\s9\d{4}-\d{4}$"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "erro", "alert('Telefone inválido. O formato correto é (XX) 9XXXX-XXXX.');", true);
                return;
            }

            // Valida Número (apenas números e um limite de 5 caracteres)
            if (!Regex.IsMatch(txtNumero.Text, @"^\d{1,5}$"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "erro", "alert('Número inválido. Somente números são permitidos e o limite é de 5 caracteres.');", true);
                return;
            }

            // Valida Email (formato de e-mail válido)
            if (!Regex.IsMatch(txtEmail.Text, @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "erro", "alert('Email inválido.');", true);
                return;
            }
            if (ProfessorDAL.CpfJaCadastrado(txtCpf.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "erro", "alert('CPF já cadastrado.');", true);
                return; // Impede o cadastro se o CPF já existir
            }


            try
            {
                ProfessorModels professor = new ProfessorModels
                {
                    Nome = txtNome.Text,
                    Sobrenome = txtSobrenome.Text,
                    DataNasc = Convert.ToDateTime(txtDataNascimento.Text),
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
                professorDAL.CadastrarProfessor();

                // Mensagem de sucesso
                LimparCampos();

                ScriptManager.RegisterStartupScript(HttpContext.Current.Handler as Page,
                    typeof(Page),
                    "sucesso",
                    "alert('Professor cadastrado com sucesso!'); window.location.href='ListaProfessores.aspx';",
                    true);
            }
            catch (Exception ex)
            {
                // Aqui você pode exibir um erro global, mas sem duplicar as mensagens de validação
                ScriptManager.RegisterStartupScript(this, GetType(), "erro", $"alert('Erro ao cadastrar professor: {ex.Message}');", true);
            }
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

        protected void buscarCepResponsavel_Click(object sender, EventArgs e)
        {
            CepService cepService = new CepService();
            var ceplist = cepService.GetEndereco(txtCep.Text);
            txtRua.Text = ceplist.Rua;
            txtBairro.Text = ceplist.Bairro;
            txtCidade.Text = ceplist.Cidade;
            txtEstado.Text = ceplist.Estado;
        }

        private bool CpfValido(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "").Trim();

            if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }

        protected void cepResponsavel_TextChanged(object sender, EventArgs e)
        {
        }

        protected void cvIdade_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Tenta converter a data de nascimento para um tipo DateTime
            DateTime dataNascimento;
            if (DateTime.TryParse(args.Value, out dataNascimento))
            {
                // Verifica se a pessoa tem 18 anos ou mais
                int idade = DateTime.Now.Year - dataNascimento.Year;
                if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
                    idade--;

                // Valida se a idade é maior ou igual a 18 anos
                args.IsValid = idade >= 18;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }
}
