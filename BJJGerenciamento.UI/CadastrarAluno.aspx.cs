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
    public partial class CadastrarAluno : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                pnlInformacoesPessoaisAluno.Visible = true;
                pnlInformacoesResponsavelAluno.Visible = false;
                pnlConfirmarAluno.Visible = false;
                btnVoltar.Visible = false;
                Session["AlunoMaiorIdade"] = null; 
            }
            else
            {

                if (pnlInformacoesPessoaisAluno.Visible)
                {
                    btnVoltar.Visible = false;
                }
                else
                {
                    btnVoltar.Visible = true;
                }
            }
        }

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

        #region Métodos de Salvamento e Carregamento de Dados na Session

        private void SalvarDadosAlunoNaSession()
        {
            Session["NomeAluno"] = nomeAluno.Text;
            Session["SobrenomeAluno"] = sobrenomeAluno.Text;
            Session["CpfAluno"] = cpfAluno.Text;
            Session["DataNascimentoAluno"] = dataNascimentoAluno.Text;
            Session["TelefoneAluno"] = telefoneAluno.Text;
            Session["CarteiraFPJJAluno"] = carteiraFPJJAluno.Text;
            Session["EmailAluno"] = emailAluno.Text;
            Session["CepAluno"] = cepAluno.Text;
            Session["CidadeAluno"] = cidadeAluno.Text;
            Session["BairroAluno"] = bairroAluno.Text;
            Session["RuaAluno"] = ruaAluno.Text;
            Session["NumeroCasaAluno"] = numeroCasaAluno.Text;
            Session["EstadoAluno"] = estadoAluno.Text;
            Session["ComplementoAluno"] = complementoAluno.Text;
        }

        private void SalvarDadosResponsavelNaSession()
        {
            Session["NomeResponsavel"] = nomeResponsavel.Text;
            Session["SobrenomeResponsavel"] = sobrenomeResponsavel.Text;
            Session["CpfResponsavel"] = cpfResponsavel.Text;
            Session["DataNascimentoResponsavel"] = dataNascimentoResponsavel.Text;
            Session["TelefoneResponsavel"] = telefoneResponsavel.Text;
            Session["EmailResponsavel"] = emailResponsavel.Text;
            Session["CepResponsavel"] = cepResponsavel.Text;
            Session["CidadeResponsavel"] = cidadeResponsavel.Text;
            Session["BairroResponsavel"] = bairroResponsavel.Text;
            Session["RuaResponsavel"] = ruaResponsavel.Text;
            Session["NumeroCasaResponsavel"] = numeroCasaResponsavel.Text;
            Session["ComplementoResponsavel"] = complementoResponsavel.Text;
            Session["EstadoResponsavel"] = estadoResponsavel.Text;
        }

        private void CarregarDadosConfirmacao()
        {
            // Dados do Aluno
            lblNomeCompletoAlunoConfirm.Text = $"{Session["NomeAluno"]} {Session["SobrenomeAluno"]}";
            lblCpfAlunoConfirm.Text = Session["CpfAluno"] as string;
            lblDataNascimentoAlunoConfirm.Text = Session["DataNascimentoAluno"] as string;
            lblTelefoneAlunoConfirm.Text = Session["TelefoneAluno"] as string;
            lblCarteiraFpjjAlunoConfirm.Text = Session["CarteiraFPJJAluno"] as string;
            lblEmailAlunoConfirm.Text = Session["EmailAluno"] as string;
            lblEnderecoAlunoConfirm.Text = $"{Session["RuaAluno"]}, {Session["NumeroCasaAluno"]} - {Session["BairroAluno"]}, {Session["CidadeAluno"]} - {Session["EstadoAluno"]} CEP: {Session["CepAluno"]}{(string.IsNullOrEmpty(Session["ComplementoAluno"] as string) ? "" : " " + Session["ComplementoAluno"])}";

            // Dados do Responsável (apenas se o aluno não for maior de idade)
            if (Session["AlunoMaiorIdade"] != null && !(bool)Session["AlunoMaiorIdade"])
            {
                lblNomeCompletoResponsavelConfirm.Text = $"{Session["NomeResponsavel"]} {Session["SobrenomeResponsavel"]}";
                lblCpfResponsavelConfirm.Text = Session["CpfResponsavel"] as string;
                lblDataNascimentoResponsavelConfirm.Text = Session["DataNascimentoResponsavel"] as string;
                lblTelefoneResponsavelConfirm.Text = Session["TelefoneResponsavel"] as string;
                lblEmailResponsavelConfirm.Text = Session["EmailResponsavel"] as string;
                lblEnderecoResponsavelConfirm.Text = $"{Session["RuaResponsavel"]}, {Session["NumeroCasaResponsavel"]} - {Session["BairroResponsavel"]}, {Session["CidadeResponsavel"]} - {Session["EstadoResponsavel"]} CEP: {Session["CepResponsavel"]}{(string.IsNullOrEmpty(Session["ComplementoResponsavel"] as string) ? "" : " " + Session["ComplementoResponsavel"])}";
            }
            else
            {
                // Se o aluno for maior de idade, esconde a seção do responsável na confirmação.
                // Isso pressupõe que você tem uma forma de esconder o card de informações do responsável no ASPX
                // ou apenas deixar os labels vazios como está. Se for para esconder o card, você precisaria de um Panel extra no ASPX
                // envolvendo as informações do responsável na tela de confirmação.
                // Ex: pnlInfoResponsavelConfirmacao.Visible = false;
                lblNomeCompletoResponsavelConfirm.Text = "Não aplicável";
                lblCpfResponsavelConfirm.Text = "Não aplicável";
                lblDataNascimentoResponsavelConfirm.Text = "Não aplicável";
                lblTelefoneResponsavelConfirm.Text = "Não aplicável";
                lblEmailResponsavelConfirm.Text = "Não aplicável";
                lblEnderecoResponsavelConfirm.Text = "Não aplicável";
            }
        }
        #endregion

        #region Eventos TextChanged
        protected void cpfAluno_TextChanged(object sender, EventArgs e)
        {
            AlunosDAL alunosDAL = new AlunosDAL();
            if (alunosDAL.BuscarCpfAluno(cpfAluno.Text.Trim().Replace("-", "").Replace(".", "")) != null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Aluno já cadastrando na base de dados');", true);
                cpfAluno.Text = string.Empty;
            }
        }

        protected void cpfResponsavel_TextChanged(object sender, EventArgs e)
        {
            AlunosDAL alunosDAL = new AlunosDAL();
            ResponsavelModels responsavel = alunosDAL.BuscarCpfResponsavel(cpfResponsavel.Text.Trim().Replace("-", "").Replace(".", ""));

            if (responsavel != null)
            {
                // cpfResponsavelExistente = true; // Não é mais uma variável de instância, remova isso.
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Responsável já cadastrado na base de dados, preenchendo informações.');", true);

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

        // Você pode remover os TextChanged vazios para campos que não fazem nada
        // Ex: protected void nomeAluno_TextChanged(object sender, EventArgs e) { }
        #endregion

        #region Eventos de Botões de Navegação

        protected void buscarCepAluno_Click(object sender, EventArgs e)
        {
            CepService cepService = new CepService();
            var ceplist = cepService.GetEndereco(cepAluno.Text);

            if (ceplist != null)
            {
                ruaAluno.Text = ceplist.Rua;
                bairroAluno.Text = ceplist.Bairro;
                cidadeAluno.Text = ceplist.Cidade;
                estadoAluno.Text = ceplist.Estado;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('CEP não encontrado ou inválido!');", true);
            }
        }

        protected void buscarCepResponsavel_Click(object sender, EventArgs e)
        {
            CepService cepService = new CepService();
            var ceplist = cepService.GetEndereco(cepResponsavel.Text);

            if (ceplist != null)
            {
                ruaResponsavel.Text = ceplist.Rua;
                bairroResponsavel.Text = ceplist.Bairro;
                cidadeResponsavel.Text = ceplist.Cidade;
                estadoResponsavel.Text = ceplist.Estado;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('CEP não encontrado ou inválido!');", true);
            }
        }

        protected void btnProximoResponsavel_Click(object sender, EventArgs e)
        {
            if (VerificarCampos(nomeAluno, sobrenomeAluno, telefoneAluno, emailAluno, cpfAluno, dataNascimentoAluno, cepAluno, ruaAluno, bairroAluno, cidadeAluno, estadoAluno, numeroCasaAluno))
            {
                SalvarDadosAlunoNaSession(); 

                DateTime dataNascimento;
                if (!DateTime.TryParse(dataNascimentoAluno.Text, out dataNascimento))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Data de Nascimento do Aluno inválida!');", true);
                    return;
                }

                int idade = DateTime.Now.Year - dataNascimento.Year;
                if (DateTime.Now < dataNascimento.AddYears(idade))
                {
                    idade--;
                }

                pnlInformacoesPessoaisAluno.Visible = false; 

                if (idade < 18) 
                {
                    pnlInformacoesResponsavelAluno.Visible = true;
                    pnlConfirmarAluno.Visible = false;
                    Session["AlunoMaiorIdade"] = false;
                }
                else 
                {
                    pnlInformacoesResponsavelAluno.Visible = false; 
                    pnlConfirmarAluno.Visible = true;
                    Session["AlunoMaiorIdade"] = true;
                    CarregarDadosConfirmacao(); 
                }
                btnVoltar.Visible = true;
            }
        }

        protected void btnProximoConfimar_Click(object sender, EventArgs e)
        {
            if (VerificarCampos(nomeResponsavel, sobrenomeResponsavel, telefoneResponsavel, emailResponsavel, cpfResponsavel, dataNascimentoResponsavel, cepResponsavel, ruaResponsavel, bairroResponsavel, cidadeResponsavel, estadoResponsavel, numeroCasaResponsavel))
            {
                SalvarDadosResponsavelNaSession();

                DateTime dataNascimento;
                if (!DateTime.TryParse(dataNascimentoResponsavel.Text, out dataNascimento))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Data de Nascimento do Responsável inválida!');", true);
                    return;
                }

                int idade = DateTime.Now.Year - dataNascimento.Year;
                if (DateTime.Now < dataNascimento.AddYears(idade))
                {
                    idade--;
                }

                if (idade < 18)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('O responsável deve ser maior de idade!');", true);
                }
                else
                {
                    pnlInformacoesResponsavelAluno.Visible = false;
                    pnlConfirmarAluno.Visible = true;
                    btnVoltar.Visible = true;
                    CarregarDadosConfirmacao();
                }
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            if (pnlConfirmarAluno.Visible)
            {
                bool alunoMaiorIdade = Session["AlunoMaiorIdade"] != null && (bool)Session["AlunoMaiorIdade"];

                pnlConfirmarAluno.Visible = false;

                if (alunoMaiorIdade)
                {
                    pnlInformacoesPessoaisAluno.Visible = true;
                    btnVoltar.Visible = false;
                }
                else
                {
                    pnlInformacoesResponsavelAluno.Visible = true;
                }
            }
            else if (pnlInformacoesResponsavelAluno.Visible)
            {
                pnlInformacoesPessoaisAluno.Visible = true;
                pnlInformacoesResponsavelAluno.Visible = false;
                btnVoltar.Visible = false; 
            }
        }

        protected void btnEnviarInformacoes_Click(object sender, EventArgs e)
        {
            AlunosDAL alunosRepository = new AlunosDAL();
            int? idResponsavel = null;

            bool alunoMaiorIdade = Session["AlunoMaiorIdade"] != null && (bool)Session["AlunoMaiorIdade"];

            if (!alunoMaiorIdade)
            {
                string cpfResponsavelFormatado = (Session["CpfResponsavel"] as string)?.Replace("-", "").Replace(".", "").Trim();
                ResponsavelModels responsavel = alunosRepository.BuscarCpfResponsavel(cpfResponsavelFormatado);

                if (responsavel != null)
                {
                    idResponsavel = responsavel.IdResponsavel;
                }
                else
                {
                    responsavel = new ResponsavelModels
                    {
                        Nome = Session["NomeResponsavel"] as string,
                        Sobrenome = Session["SobrenomeResponsavel"] as string,
                        Telefone = (Session["TelefoneResponsavel"] as string)?.Replace(")", "").Replace("(", "").Replace(" ", "").Replace("-", ""),
                        Email = Session["EmailResponsavel"] as string,
                        Cpf = cpfResponsavelFormatado,
                        DataNascimento = Session["DataNascimentoResponsavel"] as string,
                        Cep = (Session["CepResponsavel"] as string)?.Replace("-", ""),
                        Bairro = Session["BairroResponsavel"] as string,
                        Estado = Session["EstadoResponsavel"] as string,
                        Cidade = Session["CidadeResponsavel"] as string,
                        Rua = Session["RuaResponsavel"] as string,
                        NumeroCasa = Session["NumeroCasaResponsavel"] as string,
                        Complemento = Session["ComplementoResponsavel"] as string
                    };
                    idResponsavel = (int?)alunosRepository.CadastrarResponsavel(responsavel);
                }
            }

            int idMatricula = alunosRepository.CadastrarMatricula(DateTime.Now, true);

            AlunoModels aluno = new AlunoModels
            {
                Nome = Session["NomeAluno"] as string,
                Sobrenome = Session["SobrenomeAluno"] as string,
                Telefone = (Session["TelefoneAluno"] as string)?.Replace(")", "").Replace("(", "").Replace(" ", "").Replace("-", ""),
                Email = Session["EmailAluno"] as string,
                Cpf = (Session["CpfAluno"] as string)?.Replace("-", "").Replace(".", ""),
                DataNascimento = Session["DataNascimentoAluno"] as string,
                Cep = (Session["CepAluno"] as string)?.Replace("-", ""),
                Bairro = Session["BairroAluno"] as string,
                Estado = Session["EstadoAluno"] as string,
                Cidade = Session["CidadeAluno"] as string,
                Rua = Session["RuaAluno"] as string,
                NumeroCasa = Session["NumeroCasaAluno"] as string,
                CarteiraFPJJ = Session["CarteiraFPJJAluno"] as string,
                Complemento = Session["ComplementoAluno"] as string,
                IdResponsavel = idResponsavel,
                IdMatricula = idMatricula
            };

            int idAluno = alunosRepository.CadastrarAluno(aluno);


            Session.Remove("NomeAluno");
            Session.Remove("SobrenomeAluno");

            if (idAluno > 0)
            {
                string script = $@"
                    Swal.fire({{
                        icon: 'success',
                        title: 'Sucesso!',
                        text: 'Aluno cadastrado! Você será redirecionado...',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'OK'
                    }}).then((result) => {{
                        if (result.isConfirmed) {{
                            window.location.href = 'CadastrarPlano.aspx?idAluno={idAluno}';
                        }}
                    }});";


                ScriptManager.RegisterStartupScript(this, GetType(), "sweetalert", script, true);
            }
            else
            {
                Response.Write("<script>alert('Erro ao cadastrar aluno.');</script>");
            }
        }
    }
}
#endregion