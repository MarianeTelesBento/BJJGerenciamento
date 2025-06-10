
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
        private readonly string connectionString;

        public GraduacaoDAL()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BJJGerenciamentoConnectionString"].ConnectionString;
        }

        public int CadastrarGraduacao(GraduacaoModels graduacao)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO TBGraduacoes " +
                                      "(IdMatricula, Observacao, DataGraduacao) " + 
                                      "VALUES (@idMatricula, @Observacao, @dataGraduacao);" +
                                      "SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idMatricula", graduacao.idMatricula);
                    command.Parameters.AddWithValue("@Observacao", graduacao.observacao);
                    command.Parameters.AddWithValue("@dataGraduacao", graduacao.dataGraduacao);

                    var idResponsavel = command.ExecuteScalar();
                    return Convert.ToInt32(idResponsavel);
                }
            }

        }

        public List<GraduacaoModels> BuscarGraduacao(int idMatricula)
        {
            List<GraduacaoModels> graduacoes = new List<GraduacaoModels>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT TOP 4 g.IdGraduacao, g.Observacao, g.DataGraduacao FROM TBGraduacoes g 
                                JOIN TBAlunos a on a.IdMatricula = g.IdMatricula WHERE a.IdMatricula = @IdMatricula ORDER BY g.DataGraduacao desc";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idMatricula", idMatricula);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GraduacaoModels graducao = new GraduacaoModels
                            {
                                idGraduacao = reader.GetInt32(0),
                                observacao = reader.GetString(1),
                                dataGraduacao = reader.GetDateTime(2)
                            };
                            graduacoes.Add(graducao);
                         
                        }
                    }
                }

            }
            return graduacoes;
        }
        public int ExcluirGraduacao(int idGraduacao)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"DELETE FROM TBGraduacoes WHERE IdGraduacao = @idGraduacao";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idGraduacao", idGraduacao);
                    int rowsAffeted = command.ExecuteNonQuery();
                    return rowsAffeted;
                }

            }
        }

    }
}