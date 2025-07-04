﻿using System;
using System.Collections.Generic;
using System.Configuration;
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
        private readonly string connectionString;

        public PlanoDAL()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BJJGerenciamentoConnectionString"].ConnectionString;
        }

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
        public int CadastrarPlanoAluno(int idAlunos, int idDia, int idHorario, int idDetalhe, int idPlanoAlunoValor, bool passeLivre, int diaVencimento, DateTime dataProximaCobranca)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO TBPlanoAluno 
                         (IdAluno, IdDia, IdHorario, IdDetalhe, IdPlanoAlunoValor, PasseLivre, DiaVencimento, DataProximaCobranca)
                         VALUES 
                         (@IdAluno, @IdDia, @IdHorario, @IdDetalhe, @IdPlanoAlunoValor, @PasseLivre, @DiaVencimento, @DataProximaCobranca)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdAluno", idAlunos);
                    cmd.Parameters.AddWithValue("@IdDia", idDia);
                    cmd.Parameters.AddWithValue("@IdHorario", idHorario);
                    cmd.Parameters.AddWithValue("@IdDetalhe", idDetalhe);
                    cmd.Parameters.AddWithValue("@IdPlanoAlunoValor", idPlanoAlunoValor);
                    cmd.Parameters.AddWithValue("@PasseLivre", passeLivre);
                    cmd.Parameters.AddWithValue("@DiaVencimento", diaVencimento);
                    cmd.Parameters.AddWithValue("@DataProximaCobranca", dataProximaCobranca);

                    con.Open();
                    return Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
        }




        public void ExcluirPlanoAluno(int idAluno)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM TBPlanoAluno WHERE IdAluno = @IdAluno";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdAluno", idAluno);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ExcluirPlanoAlunoValor(int idAluno)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Buscar os Ids de PlanoAlunoValor usados por este aluno
                string buscarIds = @"SELECT DISTINCT IdPlanoAlunoValor 
                             FROM TBPlanoAluno 
                             WHERE IdAluno = @IdAluno";

                List<int> idsValor = new List<int>();

                using (SqlCommand cmdBuscar = new SqlCommand(buscarIds, con))
                {
                    cmdBuscar.Parameters.AddWithValue("@IdAluno", idAluno);

                    using (SqlDataReader reader = cmdBuscar.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idsValor.Add(Convert.ToInt32(reader["IdPlanoAlunoValor"]));
                        }
                    }
                }

                // Agora deletar da tabela TBPlanoAlunoValor
                foreach (int idValor in idsValor)
                {
                    string deletar = "DELETE FROM TBPlanoAlunoValor WHERE Id = @Id";

                    using (SqlCommand cmdDeletar = new SqlCommand(deletar, con))
                    {
                        cmdDeletar.Parameters.AddWithValue("@Id", idValor);
                        cmdDeletar.ExecuteNonQuery();
                    }
                }
            }
        }

        public string BuscarNomePlano(int idPlanoDetalhes)
        {
            string nomePlano = string.Empty;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Nome FROM TBPlanos p " +
                    "INNER JOIN TBPlanoDetalhes d ON p.IdPlano = d.IdPlano " +
                    "WHERE d.IdDetalhe = @idPlanoDetalhes";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idPlanoDetalhes", idPlanoDetalhes);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    nomePlano = reader["Nome"].ToString();
                }
            }
            return nomePlano;
        }

        public string BuscarDiaSemana(int idDia)
        {
            string dia = string.Empty;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Dia FROM TBDiasSemana " +
                    "WHERE IdDia = @idDia;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idDia", idDia);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dia = reader["Dia"].ToString();
                }
                return dia;   
            }
        }


        public List<PlanoAlunoModels> BuscarPlanoAluno(int idMatricula)
        {
            List<PlanoAlunoModels> planoAlunos = new List<PlanoAlunoModels>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT 
                    pa.IdPlanoAluno, 
                    pa.IdAluno,
                    a.Nome, 
                    a.Sobrenome,
                    pa.IdDia, 
                    pa.IdHorario, 
                    pa.IdDetalhe, 
                    pa.IdPlanoAlunoValor,
                    pa.PasseLivre,
                    pa.DiaVencimento,  
                    d.QtsDias, 
                    d.Mensalidade, 
                    h.HorarioInicio, 
                    h.HorarioFim
                FROM TBPlanoAluno pa
                INNER JOIN TBPlanoDetalhes d ON pa.IdDetalhe = d.IdDetalhe
                INNER JOIN TBHora h ON pa.IdHorario = h.IdHora
                INNER JOIN TBAlunos a ON pa.IdAluno = a.IdAluno
                WHERE a.IdMatricula = @IdMatricula;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdMatricula", idMatricula);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PlanoAlunoModels planoAluno = new PlanoAlunoModels()
                    {
                        idPlanoAluno = reader.GetInt32(0),
                        idAlunos = reader.GetInt32(1),
                        Nome = reader.GetString(2),
                        Sobrenome = reader.GetString(3),
                        idDia = reader.GetInt32(4),
                        idHorario = reader.GetInt32(5),
                        idDetalhe = reader.GetInt32(6),
                        idPlanoAlunoValor = reader.GetInt32(7),
                        passeLivre = reader.GetBoolean(8),
                        DiaVencimento = reader.GetInt32(9), 
                        qtdDias = reader.GetInt32(10),
                        mensalidade = reader.GetDecimal(11),
                        horarioInicio = reader.GetTimeSpan(12).ToString(@"hh\:mm"),
                        horarioFim = reader.GetTimeSpan(13).ToString(@"hh\:mm")
                    };
                    planoAlunos.Add(planoAluno);
                }

            }
            return planoAlunos;
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
        public int CriarNovoPlano(string nome)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TBPlanos (Nome) OUTPUT INSERTED.IdPlano VALUES (@Nome)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nome", nome);
                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void VincularPlanoADia(int idPlano, int idDia)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TBPlanoDias (IdPlano, IdDia) VALUES (@IdPlano, @IdDia)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdPlano", idPlano);
                cmd.Parameters.AddWithValue("@IdDia", idDia);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void VincularPlanoHorario(int idPlano, int idDia, int idHora)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TBPlanoHorario (IdPlano, IdDia, IdHora) VALUES (@IdPlano, @IdDia, @IdHora)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdPlano", idPlano);
                cmd.Parameters.AddWithValue("@IdDia", idDia);
                cmd.Parameters.AddWithValue("@IdHora", idHora);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SalvarValorPlano(int idPlano, int quantidadeDias, decimal mensalidade)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TBPlanoDetalhes (IdPlano, QtsDias, Mensalidade) VALUES (@IdPlano, @QtsDias, @Mensalidade)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdPlano", idPlano);
                cmd.Parameters.AddWithValue("@QtsDias", quantidadeDias);
                cmd.Parameters.AddWithValue("@Mensalidade", mensalidade);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<KeyValuePair<int, string>> BuscarDiasSemana()
        {
            var lista = new List<KeyValuePair<int, string>>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT IdDia, Dia FROM TBDiasSemana order by IdDia asc";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new KeyValuePair<int, string>(
                        Convert.ToInt32(reader["IdDia"]),
                        reader["Dia"].ToString()));
                }
            }
            return lista;
        }

        public List<KeyValuePair<int, string>> BuscarHorarios()
        {
            var lista = new List<KeyValuePair<int, string>>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT IdHora, HorarioInicio, HorarioFim FROM TBHora";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["IdHora"]);
                    TimeSpan inicio = (TimeSpan)reader["HorarioInicio"];
                    TimeSpan fim = (TimeSpan)reader["HorarioFim"];
                    string texto = $"{inicio:hh\\:mm} - {fim:hh\\:mm}";

                    lista.Add(new KeyValuePair<int, string>(id, texto));
                }
            }
            return lista;
        }


        public List<PlanoModels> ListarTurmas()
        {
            List<PlanoModels> turmas = new List<PlanoModels>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT 
            P.IdPlano, 
            P.Nome,  
            P.Ativo
        FROM TBPlanos P";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    turmas.Add(new PlanoModels
                    {
                        IdPlano = Convert.ToInt32(reader["IdPlano"]),
                        Nome = reader["Nome"].ToString(),
                        Ativo = Convert.ToBoolean(reader["Ativo"])
                    });
                }
            }

            return turmas;
        }


        public void AtualizarTurma(PlanoModels turma)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string queryPlanos = @"UPDATE TBPlanos 
                               SET Nome = @Nome, Ativo = @Ativo 
                               WHERE IdPlano = @IdPlano";

                SqlCommand cmdPlanos = new SqlCommand(queryPlanos, con);
                cmdPlanos.Parameters.AddWithValue("@Nome", turma.Nome);
                cmdPlanos.Parameters.AddWithValue("@Ativo", turma.Ativo);
                cmdPlanos.Parameters.AddWithValue("@IdPlano", turma.IdPlano);
                cmdPlanos.ExecuteNonQuery();

                string queryDetalhes = @"UPDATE TBPlanoDetalhes 
                                 SET QtsDias = @QtdDias, Mensalidade = @Mensalidade 
                                 WHERE IdPlano = @IdPlano";

                SqlCommand cmdDetalhes = new SqlCommand(queryDetalhes, con);
                cmdDetalhes.Parameters.AddWithValue("@QtdDias", turma.QtdDias);
                cmdDetalhes.Parameters.AddWithValue("@Mensalidade", turma.Mensalidade);
                cmdDetalhes.Parameters.AddWithValue("@IdPlano", turma.IdPlano);
                cmdDetalhes.ExecuteNonQuery();
            }
        }
        public PlanoModels ObterTurmaPorId(int idPlano)
        {
            PlanoModels plano = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
            SELECT 
                p.IdPlano, 
                p.Nome, 
                ISNULL(d.QtsDias, 0) AS QtdDias, 
                ISNULL(d.Mensalidade, 0) AS Mensalidade, 
                p.Ativo
            FROM TBPlanos p
            LEFT JOIN TBPlanoDetalhes d ON p.IdPlano = d.IdPlano
            WHERE p.IdPlano = @IdPlano";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdPlano", idPlano);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            plano = new PlanoModels
                            {
                                IdPlano = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                QtdDias = reader.GetInt32(2),
                                Mensalidade = reader.GetDecimal(3),
                                Ativo = reader.GetBoolean(4)
                            };
                        }
                    }
                }
            }

            return plano;
        }


        public List<int> ListarDiasDoPlano(int idPlano)
        {
            List<int> dias = new List<int>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT IdDia FROM TBPlanoDias WHERE IdPlano = @IdPlano";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdPlano", idPlano);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dias.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            return dias;
        }

        public void AtualizarDiasDoPlano(int idPlano, List<int> diasSelecionados)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                     
                        string deleteSql = "DELETE FROM TBPlanoDias WHERE IdPlano = @IdPlano";
                        using (SqlCommand cmdDelete = new SqlCommand(deleteSql, conn, tran))
                        {
                            cmdDelete.Parameters.AddWithValue("@IdPlano", idPlano);
                            cmdDelete.ExecuteNonQuery();
                        }

                     
                        string insertSql = "INSERT INTO TBPlanoDias (IdPlano, IdDia) VALUES (@IdPlano, @IdDia)";
                        using (SqlCommand cmdInsert = new SqlCommand(insertSql, conn, tran))
                        {
                            cmdInsert.Parameters.AddWithValue("@IdPlano", idPlano);
                            var paramIdDia = cmdInsert.Parameters.Add("@IdDia", SqlDbType.Int);

                            foreach (int idDia in diasSelecionados)
                            {
                                paramIdDia.Value = idDia;
                                cmdInsert.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<int> ListarHorariosDoPlano(int idPlano)
        {
            List<int> horarios = new List<int>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT IdHora FROM TBPlanoHorario WHERE IdPlano = @IdPlano";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdPlano", idPlano);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            horarios.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            return horarios;
        }

        public List<KeyValuePair<int, string>> BuscarHorariosPorDia(int idDia)
        {
            var horarios = new List<KeyValuePair<int, string>>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT h.IdHora, CONVERT(varchar, h.HorarioInicio, 108) + ' - ' + CONVERT(varchar, h.HorarioFim, 108) AS Horario
                         FROM TBHora h
                         INNER JOIN TBPlanoHorario ph ON ph.IdHora = h.IdHora
                         WHERE ph.IdDia = @IdDia
                         ORDER BY h.HorarioInicio";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdDia", idDia);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            horarios.Add(new KeyValuePair<int, string>(dr.GetInt32(0), dr.GetString(1)));
                        }
                    }
                }
            }
            return horarios;
        }

        public void AtualizarHorariosDoPlanoComDias(int idPlano, List<(int IdDia, int IdHora)> horariosSelecionados)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Passo 1: Obter os horários atuais
                        List<(int IdDia, int IdHora)> horariosAtuais = new List<(int, int)>();
                        string selectSql = "SELECT IdDia, IdHora FROM TBPlanoHorario WHERE IdPlano = @IdPlano";
                        using (SqlCommand cmdSelect = new SqlCommand(selectSql, conn, tran))
                        {
                            cmdSelect.Parameters.AddWithValue("@IdPlano", idPlano);
                            using (SqlDataReader reader = cmdSelect.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int idDia = Convert.ToInt32(reader["IdDia"]);
                                    int idHora = Convert.ToInt32(reader["IdHora"]);
                                    horariosAtuais.Add((idDia, idHora));
                                }
                            }
                        }

                        // Passo 2: Identificar os que devem ser inseridos
                        var novos = horariosSelecionados.Except(horariosAtuais).ToList();

                        // Passo 3: Identificar os que devem ser removidos
                        var removidos = horariosAtuais.Except(horariosSelecionados).ToList();

                        // Inserir novos
                        string insertSql = "INSERT INTO TBPlanoHorario (IdPlano, IdDia, IdHora) VALUES (@IdPlano, @IdDia, @IdHora)";
                        using (SqlCommand cmdInsert = new SqlCommand(insertSql, conn, tran))
                        {
                            cmdInsert.Parameters.AddWithValue("@IdPlano", idPlano);
                            var pDia = cmdInsert.Parameters.Add("@IdDia", SqlDbType.Int);
                            var pHora = cmdInsert.Parameters.Add("@IdHora", SqlDbType.Int);

                            foreach (var h in novos)
                            {
                                pDia.Value = h.IdDia;
                                pHora.Value = h.IdHora;
                                cmdInsert.ExecuteNonQuery();
                            }
                        }

                        // Remover antigos
                        string deleteSql = "DELETE FROM TBPlanoHorario WHERE IdPlano = @IdPlano AND IdDia = @IdDia AND IdHora = @IdHora";
                        using (SqlCommand cmdDelete = new SqlCommand(deleteSql, conn, tran))
                        {
                            cmdDelete.Parameters.AddWithValue("@IdPlano", idPlano);
                            var pDia = cmdDelete.Parameters.Add("@IdDia", SqlDbType.Int);
                            var pHora = cmdDelete.Parameters.Add("@IdHora", SqlDbType.Int);

                            foreach (var h in removidos)
                            {
                                pDia.Value = h.IdDia;
                                pHora.Value = h.IdHora;
                                cmdDelete.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public Dictionary<int, List<PlanoHorarioModels>> BuscarHorariosPorPlano(int idPlano)
        {
            Dictionary<int, List<PlanoHorarioModels>> horariosPorDia = new Dictionary<int, List<PlanoHorarioModels>>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT ds.IdDia, ds.Dia, h.HorarioInicio, h.HorarioFim, h.IdHora
                         FROM TBHora h
                         INNER JOIN TBPlanoHorario ph ON h.IdHora = ph.IdHora
                         INNER JOIN TBDiasSemana ds ON ph.IdDia = ds.IdDia
                         WHERE ph.IdPlano = @IdPlano
                         ORDER BY ds.IdDia, h.HorarioInicio ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPlano", idPlano);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idDia = reader.GetInt32(0);

                            PlanoHorarioModels planoHorario = new PlanoHorarioModels
                            {
                                dia = reader.GetString(1),
                                horarioInicio = reader.GetTimeSpan(2).ToString(@"hh\:mm"),
                                horarioFim = reader.GetTimeSpan(3).ToString(@"hh\:mm"),
                                idHora = reader.GetInt32(4)
                            };

                            if (!horariosPorDia.ContainsKey(idDia))
                            {
                                horariosPorDia[idDia] = new List<PlanoHorarioModels>();
                            }

                            horariosPorDia[idDia].Add(planoHorario);
                        }
                    }
                }
            }

            return horariosPorDia;
        }
        public List<PlanoAlunoModels> BuscarTodosPlanosAlunos()
        {
            List<PlanoAlunoModels> planoAlunos = new List<PlanoAlunoModels>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT 
                   pa.IdPlanoAluno, 
                   pa.IdAluno, 
                   a.Nome,
                   a.Sobrenome,    
                   pa.IdDia, 
                   pa.IdHorario, 
                   pa.IdDetalhe, 
                   pa.IdPlanoAlunoValor,
                   pa.PasseLivre,
                   pa.DiaVencimento,
                   pa.DataProximaCobranca, -- AQUI
                   d.QtsDias, 
                   d.Mensalidade, 
                   h.HorarioInicio, 
                   h.HorarioFim
               FROM TBPlanoAluno pa
               INNER JOIN TBPlanoDetalhes d ON pa.IdDetalhe = d.IdDetalhe
               INNER JOIN TBHora h ON pa.IdHorario = h.IdHora
               INNER JOIN TBAlunos a ON pa.IdAluno = a.IdAluno";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PlanoAlunoModels planoAluno = new PlanoAlunoModels()
                    {
                        idPlanoAluno = reader.GetInt32(0),
                        idAlunos = reader.GetInt32(1),
                        Nome = reader.GetString(2),
                        Sobrenome = reader.GetString(3),
                        idDia = reader.GetInt32(4),
                        idHorario = reader.GetInt32(5),
                        idDetalhe = reader.GetInt32(6),
                        idPlanoAlunoValor = reader.GetInt32(7),
                        passeLivre = reader.GetBoolean(8),
                        DiaVencimento = reader.GetInt32(9),

                
                        DataProximaCobranca = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10),

                        qtdDias = reader.GetInt32(11),
                        mensalidade = reader.GetDecimal(12),
                        horarioInicio = reader.GetTimeSpan(13).ToString(@"hh\:mm"),
                        horarioFim = reader.GetTimeSpan(14).ToString(@"hh\:mm")
                    };

                    planoAlunos.Add(planoAluno);
                }
            }

            return planoAlunos;
        }

        public List<PlanoAlunoModels> BuscarTodosPlanosComAlunos()
        {
            List<PlanoAlunoModels> listaCompleta = new List<PlanoAlunoModels>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT 
            p.IdPlanoAluno,
            p.IdAluno,
            p.IdDetalhe,
            p.IdPlanoAlunoValor,
            a.Nome,
            a.Sobrenome,
            p.DiaVencimento,
            p.DataProximaCobranca,
            d.Mensalidade
        FROM TBPlanoAluno p
        INNER JOIN TBAlunos a ON p.IdAluno = a.IdAluno
        INNER JOIN TBPlanoDetalhes d ON p.IdDetalhe = d.IdDetalhe";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PlanoAlunoModels plano = new PlanoAlunoModels
                    {
                        idPlanoAluno = reader.GetInt32(0),
                        idAlunos = reader.GetInt32(1),
                        idDetalhe = reader.GetInt32(2),
                        idPlanoAlunoValor = reader.GetInt32(3),
                        Nome = reader["Nome"].ToString(),
                        Sobrenome = reader["Sobrenome"].ToString(),
                        DiaVencimento = reader.GetInt32(6),
                        DataProximaCobranca = reader.IsDBNull(reader.GetOrdinal("DataProximaCobranca"))
                            ? (DateTime?)null
                            : reader.GetDateTime(reader.GetOrdinal("DataProximaCobranca")),
                        mensalidade = Convert.ToDecimal(reader["Mensalidade"])
                    };

                    listaCompleta.Add(plano);
                }
            }

            return listaCompleta;
        }

        public int AtualizarDataPagamento(int idPlanoAluno, DateTime novaData)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                System.Diagnostics.Debug.WriteLine($"[DEBUG DAL] Tentando atualizar plano {idPlanoAluno} para {novaData:yyyy-MM-dd}");

                string sql = @"UPDATE TBPlanoAluno 
                       SET DataProximaCobranca = @novaData 
                       WHERE IdPlanoAluno = @id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@novaData", novaData);
                    cmd.Parameters.AddWithValue("@id", idPlanoAluno);

                    conn.Open();
                    System.Diagnostics.Debug.WriteLine($"[DEBUG DAL] Conectado ao banco: {conn.Database}");

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine($"[DEBUG DAL] Linhas afetadas: {linhasAfetadas}");
                    return linhasAfetadas;
                }
            }
        }


        public PlanoAlunoModels BuscarPlanoPorId(int idPlanoAluno)
        {
            PlanoAlunoModels plano = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
        SELECT p.*, a.Nome, a.Sobrenome, d.mensalidade
        FROM TBPlanoAluno p
        INNER JOIN TBAlunos a ON p.idAluno = a.IdAluno
        INNER JOIN TBPlanoDetalhes d ON p.idDetalhe = d.idDetalhe
        WHERE p.idPlanoAluno = @idPlanoAluno";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idPlanoAluno", idPlanoAluno);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    plano = new PlanoAlunoModels()
                    {
                        idPlanoAluno = (int)reader["idPlanoAluno"],
                        idAlunos = (int)reader["idAluno"],
                        idDetalhe = (int)reader["idDetalhe"],
                        DiaVencimento = (int)reader["DiaVencimento"],
                        mensalidade = Convert.ToDecimal(reader["mensalidade"]),
                        Nome = reader["Nome"].ToString(),
                        Sobrenome = reader["Sobrenome"].ToString(),

                        // ✅ Adiciona isso!
                        DataProximaCobranca = reader["DataProximaCobranca"] != DBNull.Value
                            ? Convert.ToDateTime(reader["DataProximaCobranca"])
                            : (DateTime?)null
                    };
                }
            }
            return plano;
        }








    }
}
