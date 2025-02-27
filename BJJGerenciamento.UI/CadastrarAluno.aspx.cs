using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
            //if (!IsPostBack)
            //{
            //    //CarregarTurmas();
            //    pnlHorarios.Visible = false;
            //}
        }

        public void LimparCamposAluno()
        {
            nomeAluno.Text = string.Empty;
            sobrenomeAluno.Text = string.Empty;
            telefoneAluno.Text = string.Empty;
            emailAluno.Text = string.Empty;
            cpfAluno.Text = string.Empty;
            rgAluno.Text = string.Empty;
            dataNascimentoAluno.Text = string.Empty;
            cepAluno.Text = string.Empty;
            ruaAluno.Text = string.Empty;
            bairroAluno.Text = string.Empty;
            cidadeAluno.Text = string.Empty;
            estadoAluno.Text = string.Empty;
            numeroCasaAluno.Text = string.Empty;
            complementoAluno.Text = string.Empty;
            carteiraFPJJAluno.Text = string.Empty;
        }

        #region TextChangedAluno
                protected void nomeAluno_TextChanged(object sender, EventArgs e)
                {

                }

                protected void sobrenomeAluno_TextChanged(object sender, EventArgs e)
                {

                }

                protected void telefoneAluno_TextChanged(object sender, EventArgs e)
                {

                }

                protected void emailAluno_TextChanged(object sender, EventArgs e)
                {

                }

                protected void rgAluno_TextChanged(object sender, EventArgs e)
                {

                }

                protected void cpfAluno_TextChanged(object sender, EventArgs e)
                {
                    AlunosDAL alunosDAL = new AlunosDAL();
                    if (alunosDAL.BuscarCpfAluno(cpfAluno.Text.Trim().Replace("-", "").Replace(".", "")) != null)
                    {
                        Response.Write("<script>alert('Aluno já cadastrando na base de dados');</script>");
                        cpfAluno.Text = string.Empty;
                    }

                }

                protected void dataNascimentoAluno_TextChanged(object sender, EventArgs e)
                {

                }

                protected void cepAluno_TextChanged(object sender, EventArgs e)
                {

                }

                protected void ruaAluno_TextChanged(object sender, EventArgs e)
                {

                }

                protected void bairroAluno_TextChanged(object sender, EventArgs e)
                {

                }

                protected void cidadeAluno_TextChanged(object sender, EventArgs e)
                {

                }

                protected void estadoAluno_TextChanged(object sender, EventArgs e)
                {

                }

                protected void numeroCasaAluno_TextChanged(object sender, EventArgs e)
                {

                }

                protected void carteiraFPJJAluno_TextChanged(object sender, EventArgs e)
                {

                }

                protected void complementoAluno_TextChanged(object sender, EventArgs e)
                {

                }
        #endregion

        #region TextChangedResponsavel
        protected void nomeResponsavel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void sobrenomeResponsavel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void telefoneResponsavel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void emailResponsavel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void rgResponsavel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cpfResponsavel_TextChanged(object sender, EventArgs e)
        {
            AlunosDAL alunosDAL = new AlunosDAL();
            ResponsavelModels responsavel = alunosDAL.BuscarCpfResponsavel(cpfResponsavel.Text.Trim().Replace("-", "").Replace(".", ""));

            if (responsavel != null)
            {
                Response.Write("<script>alert('Responsavel já cadastrando na base de dados');</script>");

                nomeResponsavel.Text = responsavel.Nome;
                sobrenomeResponsavel.Text = responsavel.Sobrenome;
                telefoneResponsavel.Text = responsavel.Telefone;
                emailResponsavel.Text = responsavel.Email;
                rgResponsavel.Text = responsavel.Rg;
                cpfResponsavel.Text = responsavel.Cpf;
                dataNascimentoResponsavel.Text = DateTime.Parse(responsavel.DataNascimento).ToString("yyyy-MM-dd");
                cepResponsavel.Text = responsavel.Cep;
                ruaResponsavel.Text = responsavel.Rua;
                bairroResponsavel.Text = responsavel.Bairro;
                cidadeResponsavel.Text = responsavel.Cidade;
                estadoResponsavel.Text = responsavel.Estado;
                numeroCasaResponsavel.Text = responsavel.NumeroCasa;
                complementoResponsavel.Text = responsavel.Complemento;
            }
        }

        protected void dataNascimentoResponsavel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cepResponsavel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ruaResponsavel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void bairroResponsavel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cidadeResponsavel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void estadoResponsavel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void numeroCasaResponsavel_TextChanged(object sender, EventArgs e)
        {

        }

        protected void complementoResponsavel_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region TextChangedPlano
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

        protected void buscarCepAluno_Click(object sender, EventArgs e)
        {
            CepService cepService = new CepService();
            var ceplist = cepService.GetEndereco(cepAluno.Text);

            ruaAluno.Text = ceplist.Rua;
            bairroAluno.Text = ceplist.Bairro;
            cidadeAluno.Text = ceplist.Cidade;
            estadoAluno.Text = ceplist.Estado;

        }

        protected void buscarCepResponsavel_Click(object sender, EventArgs e)
        {
            CepService cepService = new CepService();
            var ceplist = cepService.GetEndereco(cepResponsavel.Text);

            ruaResponsavel.Text = ceplist.Rua;
            bairroResponsavel.Text = ceplist.Bairro;
            cidadeResponsavel.Text = ceplist.Cidade;
            estadoResponsavel.Text = ceplist.Estado;

        }

        protected void btnProximoResponsavel_Click(object sender, EventArgs e)
        {
            pnlInformacoesPessoaisAluno.Visible = false;
            pnlInformacoesResponsavelAluno.Visible = true;

            //AlunoModels aluno = new AlunoModels
            //{
            //    Nome = nomeAluno.Text,
            //    Sobrenome = sobrenomeAluno.Text,
            //    Telefone = telefoneAluno.Text.Replace(")", "").Replace("(", "").Replace(" ", "").Replace("-", ""),
            //    Email = emailAluno.Text,
            //    Rg = rgAluno.Text.Replace(".", "").Replace("-", ""),
            //    Cpf = cpfAluno.Text.Replace("-", "").Replace(".", ""),
            //    DataNascimento = dataNascimentoAluno.Text,
            //    Cep = cepAluno.Text.Replace("-", ""),
            //    Bairro = bairroAluno.Text,
            //    Estado = estadoAluno.Text,
            //    Cidade = cidadeAluno.Text,
            //    Rua = ruaAluno.Text,
            //    NumeroCasa = numeroCasaAluno.Text,
            //    CarteiraFPJJ = carteiraFPJJAluno.Text,
            //    Complemento = complementoAluno.Text
            //};

            //AlunosDAL alunosRepository = new AlunosDAL();
            //alunosRepository.CadastrarDados(aluno);
            //LimparCamposAluno;
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