﻿using System;
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
        public void LimparCampos()
        {
            txtNome.Text = string.Empty;
            sobrenome.Text = string.Empty;
            telefone.Text = string.Empty;
            email.Text = string.Empty;
            cpf.Text = string.Empty;
            rg.Text = string.Empty;
            dataNascimento.Text = string.Empty;
            cep.Text = string.Empty;
            endereco.Text = string.Empty;
            bairro.Text = string.Empty;
            cidade.Text = string.Empty;
            estado.Text = string.Empty;
            numeroCasa.Text = string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
#region TextChanged
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
            AlunosDAL alunosDAL = new AlunosDAL();
            if (alunosDAL.BuscarCpfAluno(cpf.Text.Trim()) != null)
            {
                Response.Write("<script>alert('Aluno já cadastrando na base de dados');</script>");
                cpf.Text = string.Empty;
            }
            
        }

        protected void dataNascimento_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cep_TextChanged(object sender, EventArgs e)
        {

        }

        protected void naoCep_CheckedChanged(object sender, EventArgs e)
        {
            /*if (naoCep.Checked)
            {
                cep.Enabled = false;
            }*/
        }

        protected void endereco_TextChanged(object sender, EventArgs e)
        {

        }

        protected void bairro_TextChanged(object sender, EventArgs e)
        {

        }
        protected void cidade_TextChanged(object sender, EventArgs e)
        {

        }
        protected void estado_TextChanged(object sender, EventArgs e)
        {

        }

        protected void numeroCasa_TextChanged(object sender, EventArgs e)
        {

        }
#endregion

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
            alunosRepository.CadastrarDados(txtNome.Text, sobrenome.Text, telefone.Text, email.Text, rg.Text, cpf.Text, dataNascimento.Text, cep.Text, endereco.Text, bairro.Text, cidade.Text, estado.Text, numeroCasa.Text);
            LimparCampos();
        }
    }
}