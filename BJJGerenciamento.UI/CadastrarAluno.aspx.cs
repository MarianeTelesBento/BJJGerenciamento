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
        // Variáveis de instância para controle de fluxo (serão ajustadas para Session)
        // public bool cpfResponsavelExistente; // Não é estritamente necessário como variável de instância, pode ser verificada no fluxo
        // public bool alunoMaiorIdade; // Mover para Session para persistência entre postbacks

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                // Garante que apenas o primeiro painel esteja visível ao carregar a página pela primeira vez
                pnlInformacoesPessoaisAluno.Visible = true;
                pnlInformacoesResponsavelAluno.Visible = false;
                pnlConfirmarAluno.Visible = false;
                btnVoltar.Visible = false;
                Session["AlunoMaiorIdade"] = null; // Reinicia a flag de idade do aluno
            }
            else
            {
                // No postback, verifica qual painel estava visível antes do clique
                // e mantém a visibilidade para não piscar a tela ou esconder o painel atual
                // Isso é importante para a funcionalidade do "Voltar" e "Próximo"
                if (pnlInformacoesPessoaisAluno.Visible)
                {
                    // Se o aluno está no painel de informações pessoais, o botão voltar não deve aparecer
                    btnVoltar.Visible = false;
                }
                else
                {
                    // Para os outros painéis, o botão voltar deve estar visível
                    btnVoltar.Visible = true;
                }
            }
        }

        public void LimparCamposAluno()
        {
            // Sua lógica de limpar campos, mantenha-a aqui se usada em algum lugar.
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

        #region Eventos TextChanged (mantidos para sua lógica de validação/busca)
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
                SalvarDadosAlunoNaSession(); // Salva os dados do aluno

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

                pnlInformacoesPessoaisAluno.Visible = false; // Esconde o painel atual

                if (idade < 18) // Aluno menor de idade
                {
                    pnlInformacoesResponsavelAluno.Visible = true;
                    pnlConfirmarAluno.Visible = false;
                    Session["AlunoMaiorIdade"] = false;
                }
                else // Aluno maior de idade
                {
                    pnlInformacoesResponsavelAluno.Visible = false; // Garante que o painel do responsável esteja escondido
                    pnlConfirmarAluno.Visible = true;
                    Session["AlunoMaiorIdade"] = true;
                    CarregarDadosConfirmacao(); // Carrega os dados diretamente para o painel de confirmação
                }
                btnVoltar.Visible = true;
            }
        }

        protected void btnProximoPlano_Click(object sender, EventArgs e)
        {
            if (VerificarCampos(nomeResponsavel, sobrenomeResponsavel, telefoneResponsavel, emailResponsavel, cpfResponsavel, dataNascimentoResponsavel, cepResponsavel, ruaResponsavel, bairroResponsavel, cidadeResponsavel, estadoResponsavel, numeroCasaResponsavel))
            {
                SalvarDadosResponsavelNaSession(); // Salva os dados do responsável

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
                    CarregarDadosConfirmacao(); // Carrega os dados para o painel de confirmação
                }
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            if (pnlConfirmarAluno.Visible)
            {
                // Se estamos na tela de confirmação, decide para qual painel voltar
                bool alunoMaiorIdade = Session["AlunoMaiorIdade"] != null && (bool)Session["AlunoMaiorIdade"];

                pnlConfirmarAluno.Visible = false;

                if (alunoMaiorIdade)
                {
                    // Se o aluno é maior de idade, volta para o painel de informações do aluno
                    pnlInformacoesPessoaisAluno.Visible = true;
                    btnVoltar.Visible = false; // Não há mais para onde voltar
                }
                else
                {
                    // Se o aluno é menor de idade, volta para o painel de informações do responsável
                    pnlInformacoesResponsavelAluno.Visible = true;
                }
            }
            else if (pnlInformacoesResponsavelAluno.Visible)
            {
                // Se estamos no painel do responsável, volta para o painel do aluno
                pnlInformacoesPessoaisAluno.Visible = true;
                pnlInformacoesResponsavelAluno.Visible = false;
                btnVoltar.Visible = false; // Não há mais para onde voltar
            }
            // else if (pnlInformacoesPessoaisAluno.Visible) - Não precisa de um else if aqui, pois é o primeiro passo
            // Se estiver no primeiro painel, o botão "Voltar" já deve estar invisível.
        }

        protected void btnEnviarInformacoes_Click(object sender, EventArgs e)
        {
            AlunosDAL alunosRepository = new AlunosDAL();
            int? idResponsavel = null;

            bool alunoMaiorIdade = Session["AlunoMaiorIdade"] != null && (bool)Session["AlunoMaiorIdade"];

            if (!alunoMaiorIdade) // Se o aluno for menor de idade, precisa do responsável
            {
                string cpfResponsavelFormatado = (Session["CpfResponsavel"] as string)?.Replace("-", "").Replace(".", "").Trim();
                ResponsavelModels responsavel = alunosRepository.BuscarCpfResponsavel(cpfResponsavelFormatado);

                if (responsavel != null)
                {
                    idResponsavel = responsavel.IdResponsavel;
                }
                else
                {
                    // Cria um novo responsável com os dados da Session
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

            // Cria o objeto AlunoModels com os dados da Session
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
                IdPlano = 1, // Assumindo que 1 é um valor padrão ou virá de outra etapa
                IdMatricula = idMatricula
            };

            int idAluno = alunosRepository.CadastrarAluno(aluno);

            // Limpa a session após o cadastro, se desejar
            Session.Remove("NomeAluno");
            Session.Remove("SobrenomeAluno");
            // ... remova todas as outras variáveis de Session que você usou

            ScriptManager.RegisterStartupScript(this, GetType(), "Sucesso", "alert('Aluno cadastrado com sucesso!');", true);
            Server.Transfer($"CadastrarPlano.aspx?idAluno={idAluno}");
        }
    }
}
#endregion