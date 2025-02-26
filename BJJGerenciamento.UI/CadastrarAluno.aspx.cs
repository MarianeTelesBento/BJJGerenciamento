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
            if (!IsPostBack)
            {
                //CarregarTurmas();
                pnlHorarios.Visible = false;
            }
        }

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
            rua.Text = string.Empty;
            bairro.Text = string.Empty;
            cidade.Text = string.Empty;
            estado.Text = string.Empty;
            numeroCasa.Text = string.Empty;
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

        protected void rua_TextChanged(object sender, EventArgs e)
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

        protected void carteiraFPJJ_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddTurmas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(ddTurmas.SelectedValue))
            //{
            //    CarregarDias(int.Parse(ddTurmas.SelectedValue));
            //}
        }

        protected void cbDias_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> diasSelecionados = new List<int>();

            foreach (ListItem item in cbDias.Items)
            {
                if (item.Selected)
                {
                    diasSelecionados.Add(int.Parse(item.Value));
                }
            }

            if (diasSelecionados.Count > 0)
            {
                //CarregarHorarios(diasSelecionados, int.Parse(ddTurmas.SelectedValue));
                pnlHorarios.Visible = true;
            }
            else
            {
                pnlHorarios.Visible = false;
            }
        }
        #endregion

        protected void BuscarCep_Click(object sender, EventArgs e)
        {
            CepService cepService = new CepService();
            var ceplist = cepService.GetEndereco(cep.Text);

            rua.Text = ceplist.Rua;
            bairro.Text = ceplist.Bairro;
            cidade.Text = ceplist.Cidade;
            estado.Text = ceplist.Estado;

        }

        protected void btnProximoResponsavel_Click(object sender, EventArgs e)
        {
            pnlInformacoesPessoaisAluno.Visible = false;
            pnlInformacoesResponsavelAluno.Visible = true;
            //AlunoModels aluno = new AlunoModels
            //{
            //    IdTurma = 1,
            //    IdMatricula = 1,
            //    Nome = txtNome.Text,
            //    Sobrenome = sobrenome.Text,
            //    Telefone = telefone.Text.Replace(")", "").Replace("(", "").Replace(" ", "").Replace("-", ""),
            //    Email = email.Text,
            //    Rg = rg.Text.Replace(".", "").Replace("-", ""),
            //    Cpf = cpf.Text.Replace("-", "").Replace(".", ""),
            //    DataNascimento = dataNascimento.Text,   
            //    Cep = cep.Text.Replace("-", ""),
            //    Bairro = bairro.Text,
            //    Estado = estado.Text,
            //    Cidade = cidade.Text,
            //    Rua = rua.Text,
            //    NumeroCasa = numeroCasa.Text,
            //    CarteiraFPJJ = carteiraFPJJ.Text
            //};

            //AlunosDAL alunosRepository = new AlunosDAL();
            //alunosRepository.CadastrarDados(aluno);
            //LimparCampos();
        }

        protected void btnProximoPlano_Click(object sender, EventArgs e)
        {
            pnlInformacoesResponsavelAluno.Visible = false;
            pnlPlanoAluno.Visible = true;
        }


        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            if (pnlPlanoAluno.Visible == true)
            {
                pnlInformacoesResponsavelAluno.Visible = true;
                pnlInformacoesPessoaisAluno.Visible = false;
                pnlPlanoAluno.Visible = false;
            }
            else if (pnlInformacoesResponsavelAluno.Visible == true)
            {
                pnlInformacoesPessoaisAluno.Visible = true;
                pnlInformacoesResponsavelAluno.Visible = false;
                pnlPlanoAluno.Visible = false;
            }
                
        }
    }
}