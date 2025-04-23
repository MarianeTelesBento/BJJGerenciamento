using BJJGerenciamento.UI.DAL;
using BJJGerenciamento.UI.Models;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BJJGerenciamento.UI
{
    public partial class ListaProfessores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarProfessores();
            }
        }

        private void CarregarProfessores()
        {
            ProfessorDAL dal = new ProfessorDAL();
            GridView1.DataSource = dal.ListarProfessores();
            GridView1.DataBind();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnDetalhes_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            int idProfessor = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);

            ProfessorDAL dal = new ProfessorDAL();
            var professor = dal.ListarProfessores().FirstOrDefault(p => p.IdProfessor == idProfessor);

            if (professor != null)
            {
                modalIdProfessor.Text = professor.IdProfessor.ToString();
                modalNome.Text = professor.Nome;
                modalSobrenome.Text = professor.Sobrenome;
                modalDataNasc.Text = professor.DataNasc.ToString("yyyy-MM-dd");
                modalCpf.Text = professor.Cpf;
                modalTelefone.Text = professor.Telefone;
                modalEmail.Text = professor.Email;
                modalCep.Text = professor.CEP;
                modalRua.Text = professor.Rua;
                modalBairro.Text = professor.Bairro;
                modalCarteiraFPJJ.Text = professor.CarteiraFPJJ;
                modalCarteiraCBJJ.Text = professor.CarteiraCBJJ;
                modalNumero.Text = professor.Numero;
                modalComplemento.Text = professor.Complemento;
                modalCidade.Text = professor.Cidade;
                modalEstado.Text = professor.Estado;
                modalAtivo.Checked = professor.Ativo;

                ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", "abrirModal();", true);
            }
        }

        protected void SalvarProfessor_Click(object sender, EventArgs e)
        {
            try
            {
                int idProfessor = Convert.ToInt32(modalIdProfessor.Text);

                // 1. Buscar o professor atual do banco
                ProfessorDAL dal = new ProfessorDAL();
                var professorAtual = dal.ListarProfessores().FirstOrDefault(p => p.IdProfessor == idProfessor);

                if (professorAtual == null)
                    return; // Ou pode exibir mensagem de erro

                // 2. Obter o status mais recente do professor
                bool statusBanco = dal.ObterStatusAtualProfessor(idProfessor);

                // 3. Criar novo objeto com os dados atualizados
                ProfessorModels professorNovo = new ProfessorModels
                {
                    IdProfessor = idProfessor,
                    Nome = modalNome.Text,
                    Sobrenome = modalSobrenome.Text,
                    DataNasc = DateTime.Parse(modalDataNasc.Text),
                    Cpf = modalCpf.Text,
                    Telefone = modalTelefone.Text,
                    Email = modalEmail.Text,
                    CEP = modalCep.Text,
                    Rua = modalRua.Text,
                    Bairro = modalBairro.Text,
                    CarteiraFPJJ = modalCarteiraFPJJ.Text,
                    CarteiraCBJJ = modalCarteiraCBJJ.Text,
                    Numero = modalNumero.Text,
                    Complemento = modalComplemento.Text,
                    Cidade = modalCidade.Text,
                    Estado = modalEstado.Text,
                    Ativo = modalAtivo.Checked
                };

                // 4. Verificar se houve alteração nos dados do professor
                bool houveAlteracaoNosDados =
                professorAtual.Nome != professorNovo.Nome ||
                professorAtual.Sobrenome != professorNovo.Sobrenome ||
                professorAtual.DataNasc != professorNovo.DataNasc ||
                professorAtual.Cpf != professorNovo.Cpf ||
                professorAtual.Telefone != professorNovo.Telefone ||
                professorAtual.Email != professorNovo.Email ||
                professorAtual.CEP != professorNovo.CEP ||
                professorAtual.Rua != professorNovo.Rua ||
                professorAtual.Bairro != professorNovo.Bairro ||
                professorAtual.CarteiraFPJJ != professorNovo.CarteiraFPJJ ||
                professorAtual.CarteiraCBJJ != professorNovo.CarteiraCBJJ ||
                professorAtual.Numero != professorNovo.Numero ||
                professorAtual.Complemento != professorNovo.Complemento ||
                professorAtual.Cidade != professorNovo.Cidade ||
                professorAtual.Estado != professorNovo.Estado;

                bool statusAlterado = statusBanco != professorNovo.Ativo;

                if (houveAlteracaoNosDados || statusAlterado)
                {
                    dal.AtualizarProfessor(professorNovo);

                    CarregarProfessores();
                }


                ScriptManager.RegisterStartupScript(this, GetType(), "FecharModal", "fecharModal();", true);
            }
            catch (Exception ex)
            {
                // lblErro.Text = "Erro ao atualizar professor: " + ex.Message;
            }
        }



    }
}
