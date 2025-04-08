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
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                //pnlHorarios.Visible = false; 

                PlanoDAL planoDAL = new PlanoDAL();
                List<PlanoModels> planos = planoDAL.BuscarPlano();

                if (planos != null && planos.Count > 0)
                {
                    ddPlanos.DataSource = planos;
                    ddPlanos.DataTextField = "Nome";
                    ddPlanos.DataValueField = "idPlano";

                    ddPlanos.DataBind();

                }
            }
        }

        public List<string> horariosSelecionados;

        #region TextChangedPlano


        int idPlano;
        List<int> idDiasPlano = new List<int>();
        public Dictionary<string, List<string>> listaHorarios = new Dictionary<string, List<string>>();

        protected void ddPlanos_SelectedIndexChanged(object sender, EventArgs e)
        {
            AlunosDAL alunosDAL = new AlunosDAL();

            if (!string.IsNullOrEmpty(ddPlanos.SelectedValue))
            {
                idPlano = int.Parse(ddPlanos.SelectedValue);
                List<KeyValuePair<int, string>> diasPlano = alunosDAL.BuscarDiasPlano(idPlano);

                cbDias.Items.Clear();

                foreach (var dia in diasPlano)
                {
                    cbDias.Items.Add(new ListItem(dia.Value, dia.Key.ToString()));
                }
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
                    string nomeDia = item.Text;
                    int idDia = int.Parse(item.Value);

                    AlunosDAL alunosDAL = new AlunosDAL();
                    listaHorarios = alunosDAL.BuscarHorariosPlano( new KeyValuePair<int, String> (idDia, nomeDia), Convert.ToInt32(ddPlanos.SelectedValue)));

                    switch (nomeDia.ToLower()) 
                    {
                        case "Segunda":
                            pnlSegunda.Visible = true;
                            break;
                        case "Terça":
                            pnlTerca.Visible = true;
                            break;
                        case "Quarta":
                            pnlQuarta.Visible = true;
                            break;
                        case "Quinta":
                            pnlQuinta.Visible = true;
                            break;
                        case "Sexta":
                            pnlSexta.Visible = true;
                            break;
                    
                    }



                }
            }

        }

        #endregion

        protected void btnEnviarInformacoes_Click(object sender, EventArgs e)
        {

        //    horariosSelecionados = cbDias.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Text).ToList();

        //    diasSelecionados = cbHorarios.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Text).ToList();

        //    List<PlanoModels> ListaPlanos = alunosRepository.BuscarPlanoDetalhes(ddPlanos.SelectedValue);

        //    foreach (var plano in ListaPlanos)
        //    {
        //        if (diasSelecionados.Count == plano.Mensalidade)
        //        {
        //            ValorPagoPlano.Text = plano.Mensalidade.ToString();
        //            return;
        //        }
        //    }




        //    PlanoAlunoModels plano = new PlanoAlunoModels
        //    {
        //        idAlunos = idAluno,
        //        idDetalhe = int.Parse(ddPlanos.SelectedValue),
        //    };

        //    List<KeyValuePair<int, string>> diasHorariosSelecionados = new List<KeyValuePair<int, string>>();

        //    foreach (ListItem diaItem in cbDias.Items)
        //    {
        //        if (diaItem.Selected)
        //        {
        //            int idDia = int.Parse(diaItem.Value);

        //            foreach (Control ctrl in pnlHorarios.Controls)
        //            {
        //                if (ctrl is Panel panelDia)
        //                {
        //                    foreach (Control subCtrl in panelDia.Controls)
        //                    {
        //                        if (subCtrl is CheckBoxList cbHorariosDia)
        //                        {
        //                            foreach (ListItem horarioItem in cbHorariosDia.Items)
        //                            {
        //                                if (horarioItem.Selected)
        //                                {
        //                                    diasHorariosSelecionados.Add(new KeyValuePair<int, string>(idDia, horarioItem.Text));
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    if (diasHorariosSelecionados.Count > 0)
        //    {
        //        AlunosDAL alunosDAL = new AlunosDAL();
        //        alunosDAL.CadastrarPlanoAluno(plano, diasHorariosSelecionados);
        //        lblMensagem.Text = "Plano cadastrado com sucesso!";
        //    }
        //    else
        //    {
        //        lblMensagem.Text = "Selecione pelo menos um dia e horário!";
        //    }


        }
    }
}