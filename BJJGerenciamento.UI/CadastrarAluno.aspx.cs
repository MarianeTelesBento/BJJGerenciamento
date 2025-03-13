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
        public bool alunoMaiorIdade;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    //CarregarTurmas();
            //    pnlHorarios.Visible = false;
            //}
        }

        public bool cpfResponsavelExitente;
        public List<string> diasSelecionados;

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
                cpfResponsavelExitente = true;
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
        protected void ddPlanos_SelectedIndexChanged(object sender, EventArgs e)
        {
            AlunosDAL alunosDAL = new AlunosDAL();

            if (!string.IsNullOrEmpty(ddPlanos.SelectedValue))
            {
                int idPlano = int.Parse(ddPlanos.SelectedValue);
                List <KeyValuePair<int, string>> diasPlano = alunosDAL.BuscarDiasPlano(idPlano);

                cbDias.Items.Clear();

                foreach (var dia in diasPlano)
                {
                    cbDias.Items.Add(new ListItem(dia.Value, dia.Key.ToString()));
                }
            }
        }

        protected void cbDias_SelectedIndexChanged(object sender, EventArgs e)
        {
            diasSelecionados = cbDias.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Text).ToList();

            if (diasSelecionados.Count > 0)
            {
                pnlHorarios.Controls.Clear();
                pnlHorarios.Visible = true;

                AlunosDAL alunosDAL = new AlunosDAL();
                Dictionary<string, List<string>> listaHorarios = alunosDAL.BuscarHorariosPlano(diasSelecionados);

                foreach (var dia in diasSelecionados)
                {
                    Panel panelDia = new Panel();
                    panelDia.CssClass = "col-12 mb-3";

                    Label lblDia = new Label();
                    lblDia.Text = $"Selecione os horários de {dia}:";
                    lblDia.CssClass = "form-label";
                    panelDia.Controls.Add(lblDia);

                    CheckBoxList cbHorariosDia = new CheckBoxList();
                    cbHorariosDia.CssClass = "form-check";
                    cbHorariosDia.DataSource = listaHorarios[dia];
                    cbHorariosDia.DataBind();

                    panelDia.Controls.Add(cbHorariosDia);
                    pnlHorarios.Controls.Add(panelDia);
                }
            }
            else
            {
                pnlHorarios.Visible = false;
                pnlHorarios.Controls.Clear();
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
                pnlPlanoAluno.Visible = true;
                alunoMaiorIdade = true;

                AlunosDAL alunosDAL = new AlunosDAL();
                List<PlanoModels> planos = alunosDAL.BuscarPlano();

                if (planos != null && planos.Count > 0)
                {
                    ddPlanos.DataSource = planos;
                    ddPlanos.DataTextField = "Nome";
                    ddPlanos.DataValueField = "IdDetalhe";
                    ddPlanos.DataBind();
                }
            }

        }

        protected void btnProximoPlano_Click(object sender, EventArgs e)
        {
            pnlInformacoesResponsavelAluno.Visible = false;
            pnlPlanoAluno.Visible = true;

            AlunosDAL alunosDAL = new AlunosDAL();
            List<PlanoModels> planos = alunosDAL.BuscarPlano();

            if (planos != null && planos.Count > 0)
            {
                ddPlanos.DataSource = planos;
                ddPlanos.DataTextField = "Nome";
                ddPlanos.DataValueField = "IdDetalhe";
                ddPlanos.DataBind();
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            if (pnlPlanoAluno.Visible && !alunoMaiorIdade)
            {
                pnlInformacoesResponsavelAluno.Visible = true;

                pnlInformacoesPessoaisAluno.Visible = false;
                pnlPlanoAluno.Visible = false;
            }
            else if (pnlPlanoAluno.Visible && alunoMaiorIdade)
            {
                pnlInformacoesPessoaisAluno.Visible = true;

                pnlInformacoesResponsavelAluno.Visible = false;
                pnlPlanoAluno.Visible = false;
            }
            else if (pnlInformacoesResponsavelAluno.Visible)
            {
                pnlInformacoesPessoaisAluno.Visible = true;

                pnlInformacoesResponsavelAluno.Visible = false;
                pnlPlanoAluno.Visible = false;
            }

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
                        Rg = rgResponsavel.Text.Replace(".", "").Replace("-", ""),
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


            AlunoModels aluno = new AlunoModels
            {
                Nome = nomeAluno.Text,
                Sobrenome = sobrenomeAluno.Text,
                Telefone = telefoneAluno.Text.Replace(")", "").Replace("(", "").Replace(" ", "").Replace("-", ""),
                Email = emailAluno.Text,
                Rg = rgAluno.Text.Replace(".", "").Replace("-", ""),
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
                IdPlano = 1 //Iirar id do plano, no aluno
            };
             
            int idAluno = alunosRepository.CadastrarAluno(aluno);

            PlanoAlunoModels plano = new PlanoAlunoModels
            {
                idAlunos = idAluno,
                idDetalhe = int.Parse(ddPlanos.SelectedValue),    
            };

            List<KeyValuePair<int, string>> diasHorariosSelecionados = new List<KeyValuePair<int, string>>();

            foreach (ListItem diaItem in cbDias.Items)
            {
                if (diaItem.Selected)
                {
                    int idDia = int.Parse(diaItem.Value);

                    foreach (Control ctrl in pnlHorarios.Controls)
                    {
                        if (ctrl is Panel panelDia)
                        {
                            foreach (Control subCtrl in panelDia.Controls)
                            {
                                if (subCtrl is CheckBoxList cbHorariosDia)
                                {
                                    foreach (ListItem horarioItem in cbHorariosDia.Items)
                                    {
                                        if (horarioItem.Selected)
                                        {
                                            diasHorariosSelecionados.Add(new KeyValuePair<int, string>(idDia, horarioItem.Text));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (diasHorariosSelecionados.Count > 0)
            {
                AlunosDAL alunosDAL = new AlunosDAL();
                alunosDAL.CadastrarPlanoAluno(plano, diasHorariosSelecionados);
                //lblMensagem.Text = "Plano cadastrado com sucesso!";
            }
            //else
            //{
            //    lblMensagem.Text = "Selecione pelo menos um dia e horário!";
            //}


        }
    }
}