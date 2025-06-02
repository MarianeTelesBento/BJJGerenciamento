using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;

namespace BJJGerenciamento.UI
{
    public partial class _Default : Page
    {
        public List<AlunoModels> alunosList = new List<AlunoModels>();
        public AlunoModels aluno = new AlunoModels();
        int idAluno;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                AlunosDAL alunosDAL = new AlunosDAL();
                alunosList = alunosDAL.VisualizarDados();
                GridView1.DataSource = alunosList;
                GridView1.DataBind();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnFiltro_Click(object sender, EventArgs e)
        {

            if (ddPlanos.Visible == false)
            {
                btnLimpar.Visible = true;
                btnPesquisar.Visible = true;
                TxtTermoPesquisa.Visible = true;
                ddPlanos.Visible = true;
                PlanoDAL planoDAL = new PlanoDAL();
                List<PlanoModels> planos = planoDAL.BuscarPlano();

                if (planos != null && planos.Count > 0)
                {
                    ddPlanos.DataSource = planos;
                    ddPlanos.DataTextField = "Nome";
                    ddPlanos.DataValueField = "idPlano";
                    ddPlanos.DataBind();

                    ddPlanos.Items.Insert(0, new ListItem("-- Selecione uma turma --", "-1"));
                }
            }
            else
            {
                btnLimpar.Visible = false;
                btnPesquisar.Visible = false;
                TxtTermoPesquisa.Visible = false;
                ddPlanos.Visible = false;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {

            AlunosDAL alunosDAL = new AlunosDAL();

            string termo = TxtTermoPesquisa.Text;
            int? idPlano = null;

            if (ddPlanos.SelectedValue != "-1")
            {
                if (int.TryParse(ddPlanos.SelectedValue, out int idPlanoConvertido))
                {
                    idPlano = idPlanoConvertido;
                }
            }
            List<AlunoModels> alunoModels = alunosList = alunosDAL.PesquisarAlunos(termo, idPlano);

            GridView1.DataSource = alunosList;
            GridView1.DataBind();


        }

        protected void ddPlanos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Ao mudar, aparecer um combo com os dias
            //Ao mudar o combo de dias, aparecer um combo com os horários
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            TxtTermoPesquisa.Text = string.Empty;
            ddPlanos.SelectedIndex = -1;

            AlunosDAL alunosDAL = new AlunosDAL();
            alunosList = alunosDAL.VisualizarDados();
            GridView1.DataSource = alunosList;
            GridView1.DataBind();

        }

        protected void btnDetalhes_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int index = row.RowIndex;

            modalIdMatriculaAluno.Text = row.Cells[0].Text;

            CheckBox chkStatusMatricula = (CheckBox)row.FindControl("chkStatusMatricula");
            modalStatusMatricula.Checked = chkStatusMatricula.Checked;

            modalNomeAluno.Text = row.Cells[1].Text;
            modalSobrenomeAluno.Text = row.Cells[2].Text;
            modalCpfAluno.Text = row.Cells[3].Text;
            modalTelefoneAluno.Text = row.Cells[4].Text;

            modalEmailAluno.Text = GridView1.DataKeys[index]["Email"].ToString();
            modalDataNascimentoAluno.Text = GridView1.DataKeys[index]["DataNascimento"].ToString();
            modalCepAluno.Text = GridView1.DataKeys[index]["Cep"].ToString();
            modalRuaAluno.Text = GridView1.DataKeys[index]["Rua"].ToString();
            modalBairroAluno.Text = GridView1.DataKeys[index]["Bairro"].ToString();
            modalCidadeAluno.Text = GridView1.DataKeys[index]["Cidade"].ToString();
            modalEstadoAluno.Text = GridView1.DataKeys[index]["Estado"].ToString();
            modalNumeroAluno.Text = GridView1.DataKeys[index]["NumeroCasa"].ToString();
            modalComplementoAluno.Text = GridView1.DataKeys[index]["Complemento"].ToString();
            modalCarteiraFpjjAluno.Text = GridView1.DataKeys[index]["CarteiraFPJJ"].ToString();
            modalDataMatriculaAluno.Text = GridView1.DataKeys[index]["DataMatricula"].ToString();
            ViewState["IdAlunos"] = GridView1.DataKeys[index]["IdAlunos"].ToString();

            DateTime dataNascimento = Convert.ToDateTime(modalDataNascimentoAluno.Text);
            int idade = DateTime.Now.Year - dataNascimento.Year;

            if (DateTime.Now < dataNascimento.AddYears(idade))
            {
                idade--;
            }

            bool maiorDeIdade = idade >= 18;

            if (maiorDeIdade)
            {
                btnDetalhesResponsavel.Visible = false;
            }
            else
            {
                btnDetalhesResponsavel.Visible = true;
            }


                string script = $"<script>abrirModal();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script);
        }

        protected void btnDetalhesAluno_Click(object sender, EventArgs e)
        {
            string aba = "Aluno";
            string script = $"<script>abrirModal(); exibirAba('{aba}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowDetalhes", script);
        }

        protected void btnDetalhesResponsavel_Click(object sender, EventArgs e)
        {
            //Status Responsável
            int matriculaId = Convert.ToInt32(modalIdMatriculaAluno.Text);
            AlunosDAL alunosDAL = new AlunosDAL();
            ResponsavelModels responsavel =  alunosDAL.BuscarResponsavel(matriculaId);

            modalNomeResponsavel.Text = responsavel.Nome;
            modalSobrenomeResponsavel.Text = responsavel.Sobrenome;
            modalCpfResponsavel.Text = responsavel.Cpf;
            modalTelefoneResponsavel.Text = responsavel.Telefone;
            modalEmailResponsavel.Text = responsavel.Email;
            modalDataNascimentoResponsavel.Text = responsavel.DataNascimento;
            modalCepResponsavel.Text = responsavel.Cep;
            modalRuaResponsavel.Text = responsavel.Rua;
            modalBairroResponsavel.Text = responsavel.Bairro;
            modalCidadeResponsavel.Text = responsavel.Cidade;
            modalEstadoResponsavel.Text = responsavel.Estado;
            modalNumeroResponsavel.Text = responsavel.NumeroCasa;
            modalComplementoResponsavel.Text = responsavel.Complemento;
            ModalIdResponsavel.Text = responsavel.IdResponsavel.ToString();

            string aba = "Responsavel";
            string script = $"<script>abrirModal(); exibirAba('{aba}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowDetalhes", script);
        }

        protected void btnDetalhesPlano_Click(object sender, EventArgs e)
        {
            PlanoDAL planoDAL = new PlanoDAL();
            List<PlanoModels> planos = planoDAL.BuscarPlano();

            if (planos != null && planos.Count > 0)
            {
                ddPlanosModal.DataSource = planos;
                ddPlanosModal.DataTextField = "Nome";
                ddPlanosModal.DataValueField = "idPlano";
                ddPlanosModal.DataBind();

                ddPlanosModal.Items.Insert(0, new ListItem("-- Selecione uma turma --", ""));
            }

            string aba = "Plano";
            string script = $"<script>abrirModal(); exibirAba('{aba}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowDetalhes", script);
        }


        protected void SalvarAluno_Click(object sender, EventArgs e)
        {
            if (VerificarCampos(modalNomeAluno, modalSobrenomeAluno, modalCpfAluno, modalTelefoneAluno, modalRuaAluno, modalBairroAluno, modalCidadeAluno, modalEstadoAluno, modalNumeroAluno))
            {
                aluno.IdAlunos = Convert.ToInt32(ViewState["IdAlunos"]);
                aluno.Nome = modalNomeAluno.Text;
                aluno.Sobrenome = modalSobrenomeAluno.Text;
                aluno.Cpf = modalCpfAluno.Text;
                aluno.Telefone = modalTelefoneAluno.Text;
                aluno.Email = modalEmailAluno.Text;
                aluno.DataNascimento = modalDataNascimentoAluno.Text;
                aluno.Cep = modalCepAluno.Text;
                aluno.Rua = modalRuaAluno.Text;
                aluno.Bairro = modalBairroAluno.Text;
                aluno.Cidade = modalCidadeAluno.Text;
                aluno.Estado = modalEstadoAluno.Text;
                aluno.NumeroCasa = modalNumeroAluno.Text;
                aluno.Complemento = modalComplementoAluno.Text;
                aluno.CarteiraFPJJ = modalCarteiraFpjjAluno.Text;
                aluno.StatusMatricula = modalStatusMatricula.Checked;

                AlunosDAL alunosDAL = new AlunosDAL();

                bool funcionou = alunosDAL.AtualizarAluno(aluno);

                if (funcionou)
                {
                    string script = @"
                    Swal.fire({
                        icon: 'success',
                        title: 'Sucesso!',
                        text: 'Aluno atualizado com sucesso!',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'OK'
                    });";
                    ScriptManager.RegisterStartupScript(this, GetType(), "sweetalert", script, true);

                    alunosList = alunosDAL.VisualizarDados();
                    GridView1.DataSource = alunosList;
                    GridView1.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Erro ao atualizar aluno. Tente novamente!');", true);
                }
            }
        }

        protected void SalvarResponsavel_Click(object sender, EventArgs e)
        {

            if (VerificarCampos(modalNomeResponsavel, modalSobrenomeResponsavel, modalCpfResponsavel, modalEmailResponsavel, modalTelefoneResponsavel, modalDataNascimentoResponsavel, modalRuaResponsavel, modalBairroResponsavel, modalCidadeResponsavel, modalEstadoResponsavel, modalNumeroResponsavel))
            {
                ResponsavelModels responsavel = new ResponsavelModels()
                {
                    Nome = modalNomeResponsavel.Text,
                    Sobrenome = modalSobrenomeResponsavel.Text,
                    Cpf = modalCpfResponsavel.Text,
                    Telefone = modalTelefoneResponsavel.Text,
                    Email = modalEmailResponsavel.Text,
                    DataNascimento = modalDataNascimentoResponsavel.Text,
                    Cep = modalCepResponsavel.Text,
                    Rua = modalRuaResponsavel.Text,
                    Bairro = modalBairroResponsavel.Text,
                    Cidade = modalCidadeResponsavel.Text,
                    Estado = modalEstadoResponsavel.Text,
                    NumeroCasa = modalNumeroResponsavel.Text,
                    Complemento = modalComplementoResponsavel.Text,
                    IdResponsavel = Convert.ToInt32(ModalIdResponsavel.Text)
                };

                AlunosDAL alunosDAL = new AlunosDAL();

                bool funcionou = alunosDAL.AtualizarResponsavel(responsavel);

                if (funcionou)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Responsável atualizado com sucesso!');", true);
                    alunosList = alunosDAL.VisualizarDados();
                    GridView1.DataSource = alunosList;
                    GridView1.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Erro ao atualizar responsável. Tente novamente!');", true);
                }
            }

        }


        protected void ddPlanosModal_SelectedIndexChanged(object sender, EventArgs e)
        {
            //pnlSegunda.Visible = false;
            //pnlTerca.Visible = false;
            //pnlQuarta.Visible = false;
            //pnlQuinta.Visible = false;
            //pnlSexta.Visible = false;

            //cbHorariosSegunda.Items.Clear();
            //cbHorariosTerca.Items.Clear();
            //cbHorariosQuarta.Items.Clear();
            //cbHorariosQuinta.Items.Clear();
            //cbHorariosSexta.Items.Clear();

            //PlanoDAL planoDal = new PlanoDAL();

            //if (ddPlanos.SelectedValue != "-1")
            //{
            //    int idPlano;
            //    if (int.TryParse(ddPlanos.SelectedValue, out idPlano))
            //    {
            //        List<KeyValuePair<int, string>> diasPlano = planoDal.BuscarDiasPlano(idPlano);

            //        cbDias.Items.Clear();

            //        foreach (var dia in diasPlano)
            //        {
            //            cbDias.Items.Add(new ListItem(dia.Value, dia.Key.ToString()));
            //        }
            //    }
            //    else
            //    {
            //        cbDias.Items.Clear();
            //    }
            //}
            //else
            //{
            //    cbDias.Items.Clear();
            //}
        }

        protected void cbDias_SelectedIndexChanged(object sender, EventArgs e)
        {
            //pnlSegunda.Visible = false;
            //pnlTerca.Visible = false;
            //pnlQuarta.Visible = false;
            //pnlQuinta.Visible = false;
            //pnlSexta.Visible = false;

            //cbHorariosSegunda.Items.Clear();
            //cbHorariosTerca.Items.Clear();
            //cbHorariosQuarta.Items.Clear();
            //cbHorariosQuinta.Items.Clear();
            //cbHorariosSexta.Items.Clear();

            //foreach (ListItem item in cbDias.Items)
            //{
            //    if (item.Selected)
            //    {
            //        string nomeDia = item.Text.Trim();
            //        int idDia = int.Parse(item.Value);

            //        PlanoDAL planoDal = new PlanoDAL();
            //        Dictionary<string, List<PlanoHorarioModels>> listaHorarios = planoDal.BuscarHorariosPlano(new KeyValuePair<int, String>(idDia, nomeDia), Convert.ToInt32(ddPlanos.SelectedValue));

            //        switch (nomeDia)
            //        {
            //            case "Segunda":
            //                pnlSegunda.Visible = true;
            //                if (listaHorarios.ContainsKey("Segunda"))
            //                {
            //                    foreach (var horario in listaHorarios["Segunda"])
            //                    {
            //                        cbHorariosSegunda.Items.Add(new ListItem($"{horario.horarioInicio} - {horario.horarioFim}", horario.idHora.ToString()
            //                            ));
            //                    }
            //                }
            //                break;
            //            case "Terça":
            //                pnlTerca.Visible = true;
            //                if (listaHorarios.ContainsKey("Terça"))
            //                {
            //                    foreach (var horario in listaHorarios["Terça"])
            //                    {
            //                        cbHorariosTerca.Items.Add(new ListItem($"{horario.horarioInicio} - {horario.horarioFim}", horario.idHora.ToString()
            //                            ));
            //                    }
            //                }
            //                break;
            //            case "Quarta":
            //                pnlQuarta.Visible = true;
            //                if (listaHorarios.ContainsKey("Quarta"))
            //                {
            //                    foreach (var horario in listaHorarios["Quarta"])
            //                    {
            //                        cbHorariosQuarta.Items.Add(new ListItem($"{horario.horarioInicio} - {horario.horarioFim}", horario.idHora.ToString()
            //                            ));
            //                    }
            //                }
            //                break;
            //            case "Quinta":
            //                pnlQuinta.Visible = true;

            //                if (listaHorarios.ContainsKey("Quinta"))
            //                {
            //                    foreach (var horario in listaHorarios["Quinta"])
            //                    {
            //                        cbHorariosQuinta.Items.Add(new ListItem(
            //                            $"{horario.horarioInicio} - {horario.horarioFim}",
            //                            horario.idHora.ToString()
            //                        ));
            //                    }
            //                }
            //                break;
            //            case "Sexta":
            //                pnlSexta.Visible = true;

            //                if (listaHorarios.ContainsKey("Sexta"))
            //                {
            //                    foreach (var horario in listaHorarios["Sexta"])
            //                    {
            //                        cbHorariosSexta.Items.Add(new ListItem(
            //                            $"{horario.horarioInicio} - {horario.horarioFim}",
            //                            horario.idHora.ToString()
            //                        ));
            //                    }
            //                }
            //                break;
            //        }
            //    }
            //}

        }

        protected void cbPasseLivre_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbPasseLivre.Checked)
            //{
            //    foreach (ListItem item in cbDias.Items)
            //    {
            //        item.Selected = true;
            //    }
            //}
            //else
            //{
            //    foreach (ListItem item in cbDias.Items)
            //    {
            //        item.Selected = false;
            //    }
            //}

            //cbDias_SelectedIndexChanged(null, null);

            //if (cbPasseLivre.Checked)
            //{
            //    foreach (ListItem item in cbHorariosSegunda.Items) item.Selected = true;
            //    foreach (ListItem item in cbHorariosTerca.Items) item.Selected = true;
            //    foreach (ListItem item in cbHorariosQuarta.Items) item.Selected = true;
            //    foreach (ListItem item in cbHorariosQuinta.Items) item.Selected = true;
            //    foreach (ListItem item in cbHorariosSexta.Items) item.Selected = true;
            //}
        }


        protected void btnEnviarInformacoes_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(ValorPagoPlano.Text))
            //{
            //    Response.Write("<script>alert('Selecione um valor para o plano');</script>");
            //    return;
            //}

            //int totalDiasSelecionados = cbDias.Items.Cast<ListItem>().Count(item => item.Selected);
            //int idPlano = int.Parse(ddPlanos.SelectedValue);

            //PlanoDAL planoDAL = new PlanoDAL();
            //PlanoModels planoSelecionado = planoDAL.BuscarPlanoDetalhes(idPlano, totalDiasSelecionados);

            //if (planoSelecionado == null)
            //{
            //    Response.Write("<script>alert('Plano não encontrado. Verifique os dias selecionados e o plano.');</script>");
            //    return;
            //}

            //int idDetalhe = planoSelecionado.IdDetalhe;

            //bool cadastroSucesso = false;

            //foreach (ListItem diaItem in cbDias.Items)
            //{
            //    if (diaItem.Selected)
            //    {
            //        int idDia = int.Parse(diaItem.Value);
            //        string nomeDia = diaItem.Text;

            //        CheckBoxList horariosDia = ObterCheckBoxListPorDia(nomeDia);

            //        if (horariosDia != null)
            //        {
            //            foreach (ListItem horarioItem in horariosDia.Items)
            //            {
            //                if (horarioItem.Selected)
            //                {
            //                    int idHorario = int.Parse(horarioItem.Value);

            //                    int idPlanoAlunoValor = planoDAL.CadastrarPlanoAlunoValor(decimal.Parse(ValorPagoPlano.Text));

            //                    int cadastroFuncionando = planoDAL.CadastrarPlanoAluno(idAluno, idDia, idHorario, idDetalhe, idPlanoAlunoValor);

            //                    if (cadastroFuncionando > 0)
            //                    {
            //                        cadastroSucesso = true;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            //if (cadastroSucesso)
            //{
            //    string script = @"
            //        Swal.fire({
            //            icon: 'success',
            //            title: 'Sucesso!',
            //            text: 'Plano cadastrado com sucesso!',
            //            confirmButtonColor: '#3085d6',
            //            confirmButtonText: 'OK'
            //        }).then((result) => {
            //            if (result.isConfirmed) {
            //                window.location.href = 'ListaAlunos.aspx';
            //            }
            //        });";

            //    ScriptManager.RegisterStartupScript(this, GetType(), "sweetalert", script, true);
            //}
            //else
            //{
            //    Response.Write("<script>alert('Erro ao cadastrar plano.');</script>");
            //}
        }


        protected void btnValorPlano_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(ddPlanos.SelectedValue))
            //{
            //    Response.Write("<script>alert('Selecione uma turma');</script>");
            //    return;
            //}

            //int totalDiasSelecionados = cbDias.Items.Cast<ListItem>().Count(item => item.Selected);

            //if (totalDiasSelecionados == null || totalDiasSelecionados == 0)
            //{
            //    Response.Write("<script>alert('Selecione pelo menos um dia');</script>");
            //    return;
            //}


            //int totalHorariosSelecionados = 0;

            //totalHorariosSelecionados += ContarSelecionados(cbHorariosSegunda);
            //totalHorariosSelecionados += ContarSelecionados(cbHorariosTerca);
            //totalHorariosSelecionados += ContarSelecionados(cbHorariosQuarta);
            //totalHorariosSelecionados += ContarSelecionados(cbHorariosQuinta);
            //totalHorariosSelecionados += ContarSelecionados(cbHorariosSexta);



            //if (totalDiasSelecionados != totalHorariosSelecionados)
            //{
            //    Response.Write("<script>alert('Selecione pelo menos um horário para cada dia');</script>");
            //}
            //else
            //{
            //    PlanoDAL planoDAL = new PlanoDAL();
            //    decimal valorPlano = planoDAL.BuscarMensalidade(int.Parse(ddPlanos.SelectedValue), totalDiasSelecionados);

            //    if (totalHorariosSelecionados > totalDiasSelecionados)
            //    {
            //        valorPlano = 200.00m;

            //        ValorPagoPlano.Text = $"{valorPlano}";

            //        EnviarInformacoes.Visible = true;
            //        //Response.Write("<script>alert('Esse plano não permite mais de um horário por dia');</script>");
            //    }

            //    ValorPagoPlano.Text = $"{valorPlano}";

            //    EnviarInformacoes.Visible = true;
            //}
        }

        //private int ContarSelecionados(CheckBoxList cbl)
        //{
        //    return cbl.Items.Cast<ListItem>().Count(item => item.Selected);
        //}

        //private CheckBoxList ObterCheckBoxListPorDia(string nomeDia)
        //{
        //    switch (nomeDia)
        //    {
        //        case "Segunda":
        //            return cbHorariosSegunda;
        //        case "Terça":
        //            return cbHorariosTerca;
        //        case "Quarta":
        //            return cbHorariosQuarta;
        //        case "Quinta":
        //            return cbHorariosQuinta;
        //        case "Sexta":
        //            return cbHorariosSexta;
        //        default:
        //            return null;
        //    }
        //}



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

    }
}