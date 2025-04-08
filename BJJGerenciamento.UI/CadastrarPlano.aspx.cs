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
        public int idAlunos = 0; //Pegar da págida de cadastro Aluno

		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {

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

            PlanoDAL planoDal = new PlanoDAL();

            if (!string.IsNullOrEmpty(ddPlanos.SelectedValue))
            {
                int idPlano = int.Parse(ddPlanos.SelectedValue);
                List<KeyValuePair<int, string>> diasPlano = planoDal.BuscarDiasPlano(idPlano);

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
                    string nomeDia = item.Text.Trim();
                    int idDia = int.Parse(item.Value);

                    PlanoDAL planoDal = new PlanoDAL();
                    Dictionary<string, List<string>> listaHorarios = planoDal.BuscarHorariosPlano( new KeyValuePair<int, String> (idDia, nomeDia), Convert.ToInt32(ddPlanos.SelectedValue));

                    switch (nomeDia)
                    {
                        case "Segunda":
                            pnlSegunda.Visible = true;
                            if (listaHorarios.ContainsKey("Segunda"))
                                foreach (var horario in listaHorarios["Segunda"])
                                    cbHorariosSegunda.Items.Add(horario);
                            break;
                        case "Terça":
                            pnlTerca.Visible = true;
                            if (listaHorarios.ContainsKey("Terça"))
                                foreach (var horario in listaHorarios["Terça"])
                                    cbHorariosTerca.Items.Add(horario);
                            break;
                        case "Quarta":
                            pnlQuarta.Visible = true;
                            if (listaHorarios.ContainsKey("Quarta"))
                                foreach (var horario in listaHorarios["Quarta"])
                                    cbHorariosQuarta.Items.Add(horario);
                            break;
                        case "Quinta":
                            pnlQuinta.Visible = true;
                            if (listaHorarios.ContainsKey("Quinta"))
                                foreach (var horario in listaHorarios["Quinta"])
                                    cbHorariosQuinta.Items.Add(horario);
                            break;
                        case "Sexta":
                            pnlSexta.Visible = true;
                            if (listaHorarios.ContainsKey("Sexta"))
                                foreach (var horario in listaHorarios["Sexta"])
                                    cbHorariosSexta.Items.Add(horario);
                            break;
                    
                    }
                }
            }

        }

        protected void btnEnviarInformacoes_Click(object sender, EventArgs e)
        {
            
        


        }
    }
}