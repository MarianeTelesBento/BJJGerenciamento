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
	public partial class CadastrarPlano : System.Web.UI.Page
	{
        int idAluno;


        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["UsuarioLogado"] == null)
            //{
            //    Response.Redirect("Login.aspx");
            //}





            idAluno = Convert.ToInt32(Request.QueryString["idAluno"]);

            if (!IsPostBack)
            {
              
                PlanoDAL planoDal = new PlanoDAL();
                List<PlanoModels> planos = planoDal.BuscarPlano();

                if (planos != null && planos.Count > 0)
                {
                    ddPlanos.DataSource = planos;
                    ddPlanos.DataTextField = "Nome";
                    ddPlanos.DataValueField = "idPlano";
                    ddPlanos.DataBind();

                    ddPlanos.Items.Insert(0, new ListItem("-- Selecione uma turma --", ""));
                }

                if (Request.QueryString["excluirAnterior"] == "true")
                {
                    planoDal.ExcluirPlanoAluno(idAluno);
                    planoDal.ExcluirPlanoAlunoValor(idAluno);
                }

               


            }
        }

        protected void ddPlanos_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlSegunda.Visible = false;
            pnlTerca.Visible = false;
            pnlQuarta.Visible = false;
            pnlQuinta.Visible = false;
            pnlSexta.Visible = false;

            cbHorariosSegunda.Items.Clear();
            cbHorariosTerca.Items.Clear();
            cbHorariosQuarta.Items.Clear();
            cbHorariosQuinta.Items.Clear();
            cbHorariosSexta.Items.Clear();
            cbPasseLivre.Checked = false;

            PlanoDAL planoDal = new PlanoDAL();

            if (ddPlanos.SelectedValue != "-1")
            {
                int idPlano;
                if (int.TryParse(ddPlanos.SelectedValue, out idPlano))
                {
                    List<KeyValuePair<int, string>> diasPlano = planoDal.BuscarDiasPlano(idPlano);

                    cbDias.Items.Clear();

                    foreach (var dia in diasPlano)
                    {
                        cbDias.Items.Add(new ListItem(dia.Value, dia.Key.ToString()));
                    }
                }
                else
                {
                    cbDias.Items.Clear();
                }
            }
            else
            {
                cbDias.Items.Clear();
            }
        }

        protected void cbDias_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlSegunda.Visible = false;
            pnlTerca.Visible = false;
            pnlQuarta.Visible = false;
            pnlQuinta.Visible = false;
            pnlSexta.Visible = false;

            cbHorariosSegunda.Items.Clear();
            cbHorariosTerca.Items.Clear();
            cbHorariosQuarta.Items.Clear();
            cbHorariosQuinta.Items.Clear();
            cbHorariosSexta.Items.Clear();

            foreach(ListItem item in cbDias.Items)
            {
                if (item.Selected)
                {
                    string nomeDia = item.Text.Trim();
                    int idDia = int.Parse(item.Value);

                    PlanoDAL planoDal = new PlanoDAL();
                    Dictionary<string, List<PlanoHorarioModels>> listaHorarios = planoDal.BuscarHorariosPlano( new KeyValuePair<int, String> (idDia, nomeDia), Convert.ToInt32(ddPlanos.SelectedValue));

                    switch (nomeDia)
                    {
                        case "Segunda":
                            pnlSegunda.Visible = true;
                            if (listaHorarios.ContainsKey("Segunda"))
                            {
                                foreach (var horario in listaHorarios["Segunda"])
                                {
                                    cbHorariosSegunda.Items.Add(new ListItem($"{horario.horarioInicio} - {horario.horarioFim}", horario.idHora.ToString()
                                        ));
                                }
                            }
                            break;
                        case "Terça":
                            pnlTerca.Visible = true;
                            if (listaHorarios.ContainsKey("Terça"))
                            {
                                foreach (var horario in listaHorarios["Terça"])
                                {
                                    cbHorariosTerca.Items.Add(new ListItem($"{horario.horarioInicio} - {horario.horarioFim}", horario.idHora.ToString()
                                        ));
                                }
                            }
                            break;
                        case "Quarta":
                            pnlQuarta.Visible = true;
                            if (listaHorarios.ContainsKey("Quarta"))
                            {
                                foreach (var horario in listaHorarios["Quarta"])
                                {
                                    cbHorariosQuarta.Items.Add(new ListItem($"{horario.horarioInicio} - {horario.horarioFim}", horario.idHora.ToString()
                                        ));
                                }
                            }
                            break;
                        case "Quinta":
                            pnlQuinta.Visible = true;

                            if (listaHorarios.ContainsKey("Quinta"))
                            {
                                foreach (var horario in listaHorarios["Quinta"])
                                {
                                    cbHorariosQuinta.Items.Add(new ListItem(
                                        $"{horario.horarioInicio} - {horario.horarioFim}",
                                        horario.idHora.ToString()                          
                                    ));
                                }
                            }
                            break;
                        case "Sexta":
                            pnlSexta.Visible = true;

                            if (listaHorarios.ContainsKey("Sexta"))
                            {
                                foreach (var horario in listaHorarios["Sexta"])
                                {
                                    cbHorariosSexta.Items.Add(new ListItem(
                                        $"{horario.horarioInicio} - {horario.horarioFim}",
                                        horario.idHora.ToString()                          
                                    ));
                                }
                            }
                            break;
                    }
                }
            }

        }

        protected void cbPasseLivre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPasseLivre.Checked)
            {
                foreach (ListItem item in cbDias.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                foreach (ListItem item in cbDias.Items)
                {
                    item.Selected = false;
                }
            }

            cbDias_SelectedIndexChanged(null, null);

            if (cbPasseLivre.Checked)
            {
                foreach (ListItem item in cbHorariosSegunda.Items) item.Selected = true;
                foreach (ListItem item in cbHorariosTerca.Items) item.Selected = true;
                foreach (ListItem item in cbHorariosQuarta.Items) item.Selected = true;
                foreach (ListItem item in cbHorariosQuinta.Items) item.Selected = true;
                foreach (ListItem item in cbHorariosSexta.Items) item.Selected = true;
            }
        }


        protected void btnEnviarInformacoes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ValorPagoPlano.Text))
            {
                Response.Write("<script>alert('Selecione um valor para o plano');</script>");
                return;
            }

            DateTime dataEscolhida;
            if (!DateTime.TryParse(txtDataVencimento.Text, out dataEscolhida))
            {
                Response.Write("<script>alert('Data de vencimento inválida.');</script>");
                return;
            }

            DateTime hoje = DateTime.Today;
            DateTime primeiroDiaMesSeguinte = new DateTime(hoje.Year, hoje.Month, 1).AddMonths(1);

            if (dataEscolhida < primeiroDiaMesSeguinte)
            {
                Response.Write("<script>alert('A data de vencimento deve ser no mês seguinte ou posterior.');</script>");
                return;
            }

            // Agora sim: pode usar a data nos seus objetos
            int diaVencimento = dataEscolhida.Day;
            DateTime dataProximaCobranca = CalcularDataProximaCobranca(diaVencimento);

            // Exemplo: passando para o DAL ou model externo
            var planoAlunoModel = new PlanoAlunoModels
            {
                DiaVencimento = diaVencimento,
                DataProximaCobranca = dataProximaCobranca,
                // outros campos...
            };



            int totalDiasSelecionados = cbDias.Items.Cast<ListItem>().Count(item => item.Selected);
            int idPlano = int.Parse(ddPlanos.SelectedValue);

            PlanoDAL planoDAL = new PlanoDAL();
            PlanoModels planoSelecionado = planoDAL.BuscarPlanoDetalhes(idPlano, totalDiasSelecionados);

            if (planoSelecionado == null)
            {
                Response.Write("<script>alert('Plano não encontrado. Verifique os dias selecionados e o plano.');</script>");
                return;
            }

            int idDetalhe = planoSelecionado.IdDetalhe;
            bool cadastroSucesso = false;

            foreach (ListItem diaItem in cbDias.Items)
            {
                if (diaItem.Selected)
                {
                    int idDia = int.Parse(diaItem.Value);
                    string nomeDia = diaItem.Text;

                    CheckBoxList horariosDia = ObterCheckBoxListPorDia(nomeDia);

                    if (horariosDia != null)
                    {
                        foreach (ListItem horarioItem in horariosDia.Items)
                        {
                            if (horarioItem.Selected)
                            {
                                int idHorario = int.Parse(horarioItem.Value);
                                int idPlanoAlunoValor = planoDAL.CadastrarPlanoAlunoValor(decimal.Parse(ValorPagoPlano.Text));
                                bool passeLivre = cbPasseLivre.Checked;

                                // ✅ Novo: passando dia fixo (int) e data calculada (DateTime)
                                int cadastroFuncionando = planoDAL.CadastrarPlanoAluno(
                                    idAluno, idDia, idHorario, idDetalhe,
                                    idPlanoAlunoValor, passeLivre, diaVencimento, dataProximaCobranca
                                );

                                if (cadastroFuncionando > 0)
                                {
                                    cadastroSucesso = true;
                                }
                            }
                        }
                    }
                }
            }

            if (cadastroSucesso)
            {
                string script = @"
        Swal.fire({
            icon: 'success',
            title: 'Sucesso!',
            text: 'Plano cadastrado com sucesso!',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'OK'
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = 'ListaAlunos.aspx';
            }
        });";

                ScriptManager.RegisterStartupScript(this, GetType(), "sweetalert", script, true);
            }
            else
            {
                Response.Write("<script>alert('Erro ao cadastrar plano.');</script>");
            }
        }




        protected void btnValorPlano_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(ddPlanos.SelectedValue))
            {
                Response.Write("<script>alert('Selecione uma turma');</script>");
                return;
            }

            int totalDiasSelecionados = cbDias.Items.Cast<ListItem>().Count(item => item.Selected);

            if (totalDiasSelecionados == null || totalDiasSelecionados == 0 )
            {
                Response.Write("<script>alert('Selecione pelo menos um dia');</script>");
                return;
            }


            int totalHorariosSelecionados = 0;

            totalHorariosSelecionados += ContarSelecionados(cbHorariosSegunda);
            totalHorariosSelecionados += ContarSelecionados(cbHorariosTerca);
            totalHorariosSelecionados += ContarSelecionados(cbHorariosQuarta);
            totalHorariosSelecionados += ContarSelecionados(cbHorariosQuinta);
            totalHorariosSelecionados += ContarSelecionados(cbHorariosSexta);



            if (totalDiasSelecionados > totalHorariosSelecionados )
            {
                Response.Write("<script>alert('Selecione pelo menos um horário para cada dia');</script>");
            }
            else
            {
                PlanoDAL planoDAL = new PlanoDAL();
                decimal valorPlano = planoDAL.BuscarMensalidade(int.Parse(ddPlanos.SelectedValue), totalDiasSelecionados);

                if (totalHorariosSelecionados > totalDiasSelecionados && !cbPasseLivre.Checked)
                {            
                    Response.Write("<script>alert('Ative a opção passe livre para selecionar mais de um horário por dia');</script>");
                }

                ValorPagoPlano.Text = $"{valorPlano}";

                EnviarInformacoes.Visible = true;
            }
        }

        private int ContarSelecionados(CheckBoxList cbl)
        {
            return cbl.Items.Cast<ListItem>().Count(item => item.Selected);
        }

        private CheckBoxList ObterCheckBoxListPorDia(string nomeDia)
        {
            switch (nomeDia)
            {
                case "Segunda":
                    return cbHorariosSegunda;
                case "Terça":
                    return cbHorariosTerca;
                case "Quarta":
                    return cbHorariosQuarta;
                case "Quinta":
                    return cbHorariosQuinta;
                case "Sexta":
                    return cbHorariosSexta;
                default:
                    return null;
            }
        }

        private DateTime CalcularDataProximaCobranca(int diaVencimento)
        {
            DateTime hoje = DateTime.Today;
            int ano = hoje.Year;
            int mes = hoje.Month;

            if (diaVencimento <= hoje.Day)
            {
                mes++;
                if (mes > 12)
                {
                    mes = 1;
                    ano++;
                }
            }

            int ultimoDiaMes = DateTime.DaysInMonth(ano, mes);
            int diaFinal = Math.Min(diaVencimento, ultimoDiaMes);

            return new DateTime(ano, mes, diaFinal);
        }


    }
}