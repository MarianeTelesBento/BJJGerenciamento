
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using System.Web;
using BJJGerenciamento.UI.Models;

namespace BJJGerenciamento.UI.DAL
{
    public class GraduacaoDAL
    {
        public string connectionString = "Data Source=FAC00DT68ZW11-1;Initial Catalog=BJJ_DB;User ID=Sa;Password=123456;";

        public int CadastrarGraduacao(GraduacaoModels graduacao)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO TBGraduacoes " +
                                      "(IdMatricula, Obsrevacao, DataGraduacao) " + 
                                      "VALUES (@idMatricula, @idObservacao, @dataGraduacao);" +
                                      "SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idMatricula", graduacao.idMatricula);
                    command.Parameters.AddWithValue("@idObservacao", graduacao.observacao);
                    command.Parameters.AddWithValue("@dataGraduacao", graduacao.dataGraduacao);

                    var idResponsavel = command.ExecuteScalar();
                    return Convert.ToInt32(idResponsavel);
                }
            }

        }
       
    }
}