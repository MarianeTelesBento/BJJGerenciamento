using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;
using BJJGerenciamento.UI.Service;

namespace BJJGerenciamento.UI
{
    public partial class CadastrarAluno : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool cpfResponsavelExistente;
        public bool alunoMaiorIdade;


        public void LimparCamposAluno()
        {
            nomeAluno.Text = string.Empty;
            sobrenomeAluno.Text = string.Empty;
            telefoneAluno.Text = string.Empty;
            emailAluno.Text = string.Empty;
            cpfAluno.Text = string.Empty;
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

        public static bool VerificarCampos(params TextBox[] campos)
        {
            if (campos.Any(campo => string.IsNullOrWhiteSpace(campo.Text)))
            {
                ScriptManager.RegisterStartupScript(HttpContext.Current.Handler as Page,
                    typeof(Page),
                    "alerta",
                    "alert('Preencha todos os campos obrigatórios!');",
                    true);
                return false;
            }
            return true;
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
                cpfResponsavelExistente = true;
                Response.Write("<script>alert('Responsavel já cadastrando na base de dados');</script>");

                nomeResponsavel.Text = responsavel.Nome;
                sobrenomeResponsavel.Text = responsavel.Sobrenome;
                telefoneResponsavel.Text = responsavel.Telefone;
                emailResponsavel.Text = responsavel.Email;
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
            if (VerificarCampos(cpfAluno, nomeAluno, sobrenomeAluno, telefoneAluno, dataNascimentoAluno, ruaAluno, bairroAluno, cidadeAluno, estadoAluno, numeroCasaAluno))
            {
                int maiorIdade = 18;

                DateTime dataNascimento = DateTime.Parse(dataNascimentoAluno.Text);

                int idade = DateTime.Now.Year - dataNascimento.Year;

                if (DateTime.Now < dataNascimento.AddYears(idade))
                {
                    idade--;
                }

                pnlInformacoesPessoaisAluno.Visible = false;

                if (idade < maiorIdade || (idade == maiorIdade && DateTime.Now < dataNascimento.AddYears(18)))
                {
                    pnlInformacoesResponsavelAluno.Visible = true;
                }
                else
                {
                    pnlConfirmarAluno.Visible = true;
                    alunoMaiorIdade = true;

                }
            }
            
        }

        protected void btnProximoPlano_Click(object sender, EventArgs e)
        {
            //btnVoltar.Visible = true;
            if (VerificarCampos(cpfResponsavel, nomeResponsavel, sobrenomeResponsavel, telefoneResponsavel, dataNascimentoResponsavel, ruaResponsavel, bairroResponsavel, cidadeResponsavel, estadoResponsavel, numeroCasaResponsavel))
            {
                int maiorIdade = 18;

                DateTime dataNascimento = DateTime.Parse(dataNascimentoResponsavel.Text);

                int idade = DateTime.Now.Year - dataNascimento.Year;

                if (DateTime.Now < dataNascimento.AddYears(idade))
                {
                    idade--;
                }

                if (idade < maiorIdade || (idade == maiorIdade && DateTime.Now < dataNascimento.AddYears(18)))
                {
                    ScriptManager.RegisterStartupScript(HttpContext.Current.Handler as Page,
                    typeof(Page),
                    "alerta",
                    "alert('O responsável deve ser maior de idade');",
                    true);
                }
                else
                {

                    pnlInformacoesResponsavelAluno.Visible = false;
                    pnlConfirmarAluno.Visible = true;

                }
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            if (pnlConfirmarAluno.Visible) //Alteração do nome do pnl
            {
                if (!alunoMaiorIdade && pnlInformacoesResponsavelAluno.Visible)
                {
                    pnlInformacoesResponsavelAluno.Visible = true;
                    pnlInformacoesPessoaisAluno.Visible = false;
                }
                else
                {
                    pnlInformacoesPessoaisAluno.Visible = true;
                    pnlInformacoesResponsavelAluno.Visible = false;
                }

                pnlConfirmarAluno.Visible = false;
            }
            else if (pnlInformacoesResponsavelAluno.Visible)
            {
                pnlInformacoesPessoaisAluno.Visible = true;
                pnlInformacoesResponsavelAluno.Visible = false;
            }
            //else if (pnlInformacoesPessoaisAluno.Visible)
            //{
            //    pnlInformacoesPessoaisAluno.Visible = false;
            //    pnlConfirmarAluno.Visible = true;
            //}

        }

        protected void btnEnviarInformacoes_Click(object sender, EventArgs e)
        {

            AlunosDAL alunosRepository = new AlunosDAL();

            int? idResponsavel;

            if (alunoMaiorIdade)
            {
                idResponsavel = null;
            }
            else
            {
                ResponsavelModels responsavel = alunosRepository.BuscarCpfResponsavel(cpfResponsavel.Text.Replace("-", "").Replace(".", "").Trim());

                if (responsavel != null)
                {
                    idResponsavel = responsavel.IdResponsavel;
                }
                else
                {
                    responsavel = new ResponsavelModels
                    {
                        Nome = nomeResponsavel.Text,
                        Sobrenome = sobrenomeResponsavel.Text,
                        Telefone = telefoneResponsavel.Text.Replace(")", "").Replace("(", "").Replace(" ", "").Replace("-", ""),
                        Email = emailResponsavel.Text,
                        Cpf = cpfResponsavel.Text.Replace("-", "").Replace(".", ""),
                        DataNascimento = dataNascimentoResponsavel.Text,
                        Cep = cepResponsavel.Text.Replace("-", ""),
                        Bairro = bairroResponsavel.Text,
                        Estado = estadoResponsavel.Text,
                        Cidade = cidadeResponsavel.Text,
                        Rua = ruaResponsavel.Text,
                        NumeroCasa = numeroCasaResponsavel.Text,
                        Complemento = complementoResponsavel.Text
                    };

                    idResponsavel = (int?)alunosRepository.CadastrarResponsavel(responsavel);
                }
            }

            int idMatricula = alunosRepository.CadastrarMatricula(DateTime.Now, true);

            AlunoModels aluno = new AlunoModels
            {
                Nome = nomeAluno.Text,
                Sobrenome = sobrenomeAluno.Text,
                Telefone = telefoneAluno.Text.Replace(")", "").Replace("(", "").Replace(" ", "").Replace("-", ""),
                Email = emailAluno.Text,
                Cpf = cpfAluno.Text.Replace("-", "").Replace(".", ""),
                DataNascimento = dataNascimentoAluno.Text,
                Cep = cepAluno.Text.Replace("-", ""),
                Bairro = bairroAluno.Text,
                Estado = estadoAluno.Text,
                Cidade = cidadeAluno.Text,
                Rua = ruaAluno.Text,
                NumeroCasa = numeroCasaAluno.Text,
                CarteiraFPJJ = carteiraFPJJAluno.Text,
                Complemento = complementoAluno.Text,
                IdResponsavel = idResponsavel,
                IdPlano = 1,
                IdMatricula = idMatricula
            };

            int idAluno = alunosRepository.CadastrarAluno(aluno);
            if (idAluno >= 0)
            {
                ScriptManager.RegisterStartupScript(HttpContext.Current.Handler as Page,
                    typeof(Page),
                    "sucesso",
                    "alert('Aluno Salvo com sucesso'); window.location.href='ListaAlunos.aspx';",
                    true);
            }
            
            Server.Transfer($"CadastrarPlano.aspx?idAluno={idAluno}"); 
        }

    }
}