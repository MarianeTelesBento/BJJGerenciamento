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

        public int CadastrarPlanoAlunoValor(decimal valorPlano)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO TBPlanoAlunoValor (Valor)
                         VALUES (@valorPlano); SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@valorPlano", valorPlano);

                    con.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public int CadastrarPlanoAluno(int idAlunos, int idDia, int idHorario, int idDetalhe, int idPlanoAlunoValor)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO TBPlanoAluno (IdAluno, IdDia, IdHorario, IdDetalhe, IdPlanoAlunoValor)
                         VALUES (@IdAluno, @IdDia, @IdHorario, @IdDetalhe, @IdPlanoAlunoValor)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdAluno", idAlunos);
                    cmd.Parameters.AddWithValue("@IdDia", idDia);
                    cmd.Parameters.AddWithValue("@IdHorario", idHorario);
                    cmd.Parameters.AddWithValue("@IdDetalhe", idDetalhe);
                    cmd.Parameters.AddWithValue("@IdPlanoAlunoValor", idPlanoAlunoValor);

                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
        }

        public decimal BuscarMensalidade(int idPlano, int QtsDias)
        {
            decimal mensalidade = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT TOP 1 IdDetalhe, QtsDias, Mensalidade " +
                "FROM TBPlanoDetalhes " +
                "WHERE IdPlano = @IdPlano AND QtsDias >= @QtsDias " +
                "ORDER BY QtsDias ASC", connection))
                {
                    command.Parameters.AddWithValue("@IdPlano", idPlano);
                    command.Parameters.AddWithValue("@QtsDias", QtsDias);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        mensalidade = reader.GetDecimal(2);                    
                    }

                }
            }
            return mensalidade;
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
        public PlanoModels BuscarPlanoDetalhes(int idPlano, int qtsDias)
        {
            PlanoModels plano = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(
                    "SELECT TOP 1 IdDetalhe, QtsDias, Mensalidade " +
                    "FROM TBPlanoDetalhes " +
                    "WHERE IdPlano = @IdPlano AND QtsDias >= @QtsDias " +
                    "ORDER BY QtsDias ASC", connection))
                {
                    command.Parameters.AddWithValue("@IdPlano", idPlano);
                    command.Parameters.AddWithValue("@QtsDias", qtsDias);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            plano = new PlanoModels()
                            {
                                IdDetalhe = reader.GetInt32(0),
                                QtdDias = reader.GetInt32(1),
                                Mensalidade = reader.GetDecimal(2),
                            };
                        }
                    }
                }
            }
            return plano;
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
        public Dictionary<string, List<PlanoHorarioModels>> BuscarHorariosPlano(KeyValuePair<int, string> diaSelecionado, int idPlano)
        {
            Dictionary<string, List<PlanoHorarioModels>> horariosPorDia = new Dictionary<string, List<PlanoHorarioModels>>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $@"SELECT ds.Dia, h.HorarioInicio, h.HorarioFim, h.IdHora
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
                            PlanoHorarioModels planoHorarioModels = new PlanoHorarioModels();

                            string dia = reader.GetString(0);

                            planoHorarioModels.horarioInicio = reader.GetTimeSpan(1).ToString(@"hh\:mm");
                            planoHorarioModels.horarioFim = reader.GetTimeSpan(2).ToString(@"hh\:mm");
                            planoHorarioModels.idHora = reader.GetInt32(3);

                            if (!horariosPorDia.ContainsKey(dia))
                            {
                                horariosPorDia[dia] = new List<PlanoHorarioModels>();
                            }
                            horariosPorDia[dia].Add(planoHorarioModels);
                        }
                    }
                }
            }
            return horariosPorDia;
        }
    }
}