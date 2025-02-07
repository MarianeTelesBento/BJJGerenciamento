using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;
using BJJGerenciamento.UI.Service;

namespace BJJGerenciamento.UI
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void matricula_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        protected void sobrenome_TextChanged(object sender, EventArgs e)
        {

        }

        protected void telefone_TextChanged(object sender, EventArgs e)
        {

        }

        protected void email_TextChanged(object sender, EventArgs e)
        {

        }

        protected void rg_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cpf_TextChanged(object sender, EventArgs e)
        {
             
        }

        protected void dataNascimento_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cep_TextChanged(object sender, EventArgs e)
        {

        }

        protected void naoCep_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void endereco_TextChanged(object sender, EventArgs e)
        {

        }

        protected void bairro_TextChanged(object sender, EventArgs e)
        {

        }

        protected void numero_TextChanged(object sender, EventArgs e)
        {

        }
        protected void cidade_TextChanged(object sender, EventArgs e)
        {

        }
        protected void estado_TextChanged(object sender, EventArgs e)
        {

        }

        protected void BuscarCep_Click(object sender, EventArgs e)
        {
            CepService cepService = new CepService();

            var ceplist = cepService.GetEndereco(cep.Text);

            endereco.Text = ceplist.Rua;
            bairro.Text = ceplist.Bairro;
            cidade.Text = ceplist.Cidade;
            estado.Text = ceplist.Estado;

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            AlunosDAL alunosRepository = new AlunosDAL();
            alunosRepository.CadastrarDados(matricula.Text, txtNome.Text, sobrenome.Text, telefone.Text, email.Text, rg.Text, cpf.Text, dataNascimento.Text, cep.Text, endereco.Text, bairro.Text, numero.Text);
        }
    }
}