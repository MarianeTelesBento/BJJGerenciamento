using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;
using BJJGerenciamento.UI.Models;

namespace BJJGerenciamento.UI.DAL
{
    public class PlanoDAL
    {
        public string connectionString = "Data Source=FAC00DT68ZW11-1;Initial Catalog=BJJ_DB;User ID=Sa;Password=123456;";

        //public string connectionString = "Data Source=DESKTOP-FTCVI92\\SQLEXPRESS;Initial Catalog=BJJ_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public void CadastrarPlanoAluno(PlanoAlunoModels plano, List<KeyValuePair<int, string>> diasHorarios)
        {

        }


        public int CadastrarPlano(PlanoAlunoModels planoAluno)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO TBPlanoAluno " +
                                      "(IdAlunos, IdDia, IdHorario, IdDetalhe) " +
                                      "VALUES (@IdAlunos, @IdDia, @IdHorario, @IdDetalhe);" +
                                      "SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAlunos", planoAluno.idAlunos);
                    command.Parameters.AddWithValue("@IdDia", planoAluno.idDia);
                    command.Parameters.AddWithValue("@IdHorario", planoAluno.idHorario);
                    command.Parameters.AddWithValue("@IdDetalhe", planoAluno.idDetalhe);

                    int idPlanoAluno = Convert.ToInt32(command.ExecuteScalar());

                    return idPlanoAluno;
                }
            }
        }

        public List<PlanoModels> BuscarPlano()
        {
            List<PlanoModels> planoList = new List<PlanoModels>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT " +
                    "IdPlano, " +
                    "Nome " +
                    "FROM TBPlanos ", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            PlanoModels plano = new PlanoModels()
                            {
                                IdPlano = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                            };
                            planoList.Add(plano);
                        }
                    }
                }
            }
            return planoList;
        }
        public List<PlanoModels> BuscarPlanoDetalhes(string idPlano)
        {
            List<PlanoModels> planoList = new List<PlanoModels>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT " +
                    "IdDetalhe, " +
                    "QtsDias, " +
                    "Mensalidade " +
                    "FROM TBPlanoDetalhes " +
                    "WHERE IdPlano = @IdPlano", connection))
                {
                    command.Parameters.AddWithValue("@IdPlano", idPlano);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PlanoModels plano = new PlanoModels()
                            {
                                IdDetalhe = reader.GetInt32(0),
                                QtdDias = reader.GetInt32(1),
                                Mensalidade = reader.GetDecimal(2),
                            };
                            planoList.Add(plano);
                        }
                    }
                }
            }
            return planoList;
        }
        public List<KeyValuePair<int, string>> BuscarDiasPlano(int idPlano)
        {
            List<KeyValuePair<int, string>> diasPlanoList = new List<KeyValuePair<int, string>>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT d.IdDia, d.Dia
                    FROM TBDiasSemana d
                    INNER JOIN TBPlanoDias pd ON d.IdDia = pd.IdDia
                    WHERE pd.IdPlano = @IdPlano
                    ORDER BY d.IdDia ASC
                    ", connection))
                {
                    command.Parameters.AddWithValue("@IdPlano", idPlano);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idDia = reader.GetInt32(0);
                            string nomeDia = reader.GetString(1);
                            diasPlanoList.Add(new KeyValuePair<int, string>(idDia, nomeDia));
                        }
                    }
                }
            }
            return diasPlanoList;
        }
        public Dictionary<string, List<string>> BuscarHorariosPlano(KeyValuePair<int, string> diaSelecionado, int idPlano)
        {
            Dictionary<string, List<string>> horariosPorDia = new Dictionary<string, List<string>>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $@"SELECT ds.Dia, h.HorarioInicio 
                          FROM TBHora h
                          INNER JOIN TBPlanoHorario ph ON h.IdHora = ph.IdHora
                          INNER JOIN TBDiasSemana ds ON ph.IdDia = ds.IdDia
                          WHERE ds.IdDia = @IdDia AND ph.IdPlano = @IdPlano
                          ORDER BY h.HorarioInicio ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue($"@IdDia", diaSelecionado.Key);
                    command.Parameters.AddWithValue($"@IdPlano", idPlano);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string dia = reader.GetString(0);
                            string horario = reader.GetTimeSpan(1).ToString(@"hh\:mm");

                            if (!horariosPorDia.ContainsKey(dia))
                            {
                                horariosPorDia[dia] = new List<string>();
                            }
                            horariosPorDia[dia].Add(horario);
                        }
                    }
                }
            }
            return horariosPorDia;
        }
    }
}