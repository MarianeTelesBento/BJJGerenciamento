using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
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
        public int CadastrarPlanoAluno(int idAlunos, int idDia, int idHorario, int idDetalhe, int idPlanoAlunoValor, bool passeLivre, int diaVencimento, DateTime dataProximaCobranca, int? idAdesao)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO TBPlanoAluno 
                           (IdAluno, IdDia, IdHorario, IdDetalhe, IdPlanoAlunoValor, PasseLivre, DiaVencimento, DataProximaCobranca, IdAdesao)
                         VALUES 
                           (@IdAluno, @IdDia, @IdHorario, @IdDetalhe, @IdPlanoAlunoValor, @PasseLivre, @DiaVencimento, @DataProximaCobranca, @IdAdesao);
                         SELECT SCOPE_IDENTITY();"; 

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
                    cmd.Parameters.AddWithValue("@IdAdesao", idAdesao.HasValue ? (object)idAdesao.Value : DBNull.Value);

                    con.Open();
                 
                    return Convert.ToInt32(cmd.ExecuteScalar()); 
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
                string query = @"
            SELECT 
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
                -- ALTERAÇÃO PRINCIPAL AQUI:
                -- Pega o valor da tabela correta (TBPlanoAlunoValor).
                -- Se não existir, pega o valor antigo da TBPlanoDetalhes como um fallback.
                COALESCE(tpav.Valor, d.Mensalidade) AS mensalidade,
                h.HorarioInicio, 
                h.HorarioFim,
                ad.NomeAdesao
            FROM TBPlanoAluno pa
            INNER JOIN TBPlanoDetalhes d ON pa.IdDetalhe = d.IdDetalhe
            INNER JOIN TBHora h ON pa.IdHorario = h.IdHora
            INNER JOIN TBAlunos a ON pa.IdAluno = a.IdAluno
            LEFT JOIN TBAdesao ad ON pa.IdAdesao = ad.IdAdesao
            -- ADIÇÃO DO JOIN CORRETO:
            -- Faz a ligação com a tabela que tem o valor pago pelo aluno.
            LEFT JOIN TBPlanoAlunoValor tpav ON pa.IdPlanoAlunoValor = tpav.IdPlanoAlunoValor
            WHERE a.IdMatricula = @IdMatricula;
        ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdMatricula", idMatricula);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PlanoAlunoModels planoAluno = new PlanoAlunoModels()
                            {
                                idPlanoAluno = reader.GetInt32(reader.GetOrdinal("IdPlanoAluno")),
                                idAlunos = reader.GetInt32(reader.GetOrdinal("IdAluno")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Sobrenome = reader.GetString(reader.GetOrdinal("Sobrenome")),
                                idDia = reader.GetInt32(reader.GetOrdinal("IdDia")),
                                idHorario = reader.GetInt32(reader.GetOrdinal("IdHorario")),
                                idDetalhe = reader.GetInt32(reader.GetOrdinal("IdDetalhe")),
                                idPlanoAlunoValor = reader.IsDBNull(reader.GetOrdinal("IdPlanoAlunoValor")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdPlanoAlunoValor")),
                                passeLivre = reader.GetBoolean(reader.GetOrdinal("PasseLivre")),
                                DiaVencimento = reader.GetInt32(reader.GetOrdinal("DiaVencimento")),
                                qtdDias = reader.GetInt32(reader.GetOrdinal("QtsDias")),
                                mensalidade = reader.GetDecimal(reader.GetOrdinal("mensalidade")), // Agora pega a coluna correta
                                horarioInicio = reader.GetTimeSpan(reader.GetOrdinal("HorarioInicio")).ToString(@"hh\:mm"),
                                horarioFim = reader.GetTimeSpan(reader.GetOrdinal("HorarioFim")).ToString(@"hh\:mm"),
                                NomeAdesao = reader.IsDBNull(reader.GetOrdinal("NomeAdesao")) ? null : reader.GetString(reader.GetOrdinal("NomeAdesao"))
                            };
                            planoAlunos.Add(planoAluno);
                        }
                    }
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
                        while (reader.Read())
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
            List<PlanoAlunoModels> planos = new List<PlanoAlunoModels>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string query = @"
            SELECT 
                pa.IdPlanoAluno,
                pa.IdAluno,
                a.Nome,
                a.Sobrenome,
                pa.IdDetalhe,
                pa.IdAdesao, 
                pa.DataProximaCobranca,
                COALESCE(tpav.Valor, d.Mensalidade) AS mensalidade
            FROM 
                TBPlanoAluno pa
            INNER JOIN 
                TBAlunos a ON pa.IdAluno = a.IdAluno
            INNER JOIN 
                TBPlanoDetalhes d ON pa.IdDetalhe = d.IdDetalhe
            LEFT JOIN 
                TBPlanoAlunoValor tpav ON pa.IdPlanoAlunoValor = tpav.IdPlanoAlunoValor
            LEFT JOIN 
                TBAdesao ad ON pa.IdAdesao = ad.IdAdesao;
        ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            planos.Add(new PlanoAlunoModels
                            {
                                idPlanoAluno = reader.GetInt32(reader.GetOrdinal("IdPlanoAluno")),
                                idAlunos = reader.GetInt32(reader.GetOrdinal("IdAluno")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Sobrenome = reader.GetString(reader.GetOrdinal("Sobrenome")),
                                idDetalhe = reader.GetInt32(reader.GetOrdinal("IdDetalhe")),
                                IdAdesao = reader.IsDBNull(reader.GetOrdinal("IdAdesao")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdAdesao")), // <<< ADICIONADO
                                DataProximaCobranca = reader.IsDBNull(reader.GetOrdinal("DataProximaCobranca")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataProximaCobranca")),
                                mensalidade = reader.GetDecimal(reader.GetOrdinal("mensalidade"))
                            });
                        }
                    }
                }
            }
            return planos;
        }
        public int AtualizarDataPagamento(int idAluno, DateTime novaData, int idAdesao)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string query = @"
            UPDATE TBPlanoAluno 
            SET 
                DataProximaCobranca = @NovaData,
                IdAdesao = @IdAdesao -- CAMPO ADICIONADO
            WHERE 
                IdAluno = @IdAluno";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@NovaData", novaData);
                    cmd.Parameters.AddWithValue("@IdAdesao", idAdesao);
                    cmd.Parameters.AddWithValue("@IdAluno", idAluno);

                    con.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public int AtualizarApenasDataCobranca(int idAluno, DateTime novaData)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string query = @"
            UPDATE TBPlanoAluno 
            SET 
                DataProximaCobranca = @NovaData
            WHERE 
                IdAluno = @IdAluno";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@NovaData", novaData);
                    cmd.Parameters.AddWithValue("@IdAluno", idAluno);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public DateTime? BuscarDataMaisRecentePorAluno(int idAluno)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(DataProximaCobranca) FROM TBPlanoAluno WHERE IdAluno = @IdAluno";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdAluno", idAluno);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToDateTime(result);
                    }
                    return null;
                }
            }
        }



        public PlanoAlunoModels BuscarPlanoPorId(int idPlanoAluno)
        {
            PlanoAlunoModels plano = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
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
                -- ALTERAÇÃO PRINCIPAL AQUI:
                -- Prioriza o valor da TBPlanoAlunoValor. Se não houver, usa o valor antigo como fallback.
                COALESCE(tpav.Valor, d.Mensalidade) AS mensalidade,
                h.HorarioInicio, 
                h.HorarioFim,
                ad.NomeAdesao
            FROM TBPlanoAluno pa
            INNER JOIN TBPlanoDetalhes d ON pa.IdDetalhe = d.IdDetalhe
            INNER JOIN TBHora h ON pa.IdHorario = h.IdHora
            INNER JOIN TBAlunos a ON pa.IdAluno = a.IdAluno
            LEFT JOIN TBAdesao ad ON pa.IdAdesao = ad.IdAdesao
            -- ADIÇÃO DO JOIN CORRETO:
            LEFT JOIN TBPlanoAlunoValor tpav ON pa.IdPlanoAlunoValor = tpav.IdPlanoAlunoValor
            WHERE pa.IdPlanoAluno = @IdPlanoAluno;
        ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdPlanoAluno", idPlanoAluno);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            plano = new PlanoAlunoModels()
                            {
                                idPlanoAluno = reader.GetInt32(reader.GetOrdinal("IdPlanoAluno")),
                                idAlunos = reader.GetInt32(reader.GetOrdinal("IdAluno")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Sobrenome = reader.GetString(reader.GetOrdinal("Sobrenome")),
                                idDia = reader.GetInt32(reader.GetOrdinal("IdDia")),
                                idHorario = reader.GetInt32(reader.GetOrdinal("IdHorario")),
                                idDetalhe = reader.GetInt32(reader.GetOrdinal("IdDetalhe")),
                                idPlanoAlunoValor = reader.IsDBNull(reader.GetOrdinal("IdPlanoAlunoValor")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdPlanoAlunoValor")),
                                passeLivre = reader.GetBoolean(reader.GetOrdinal("PasseLivre")),
                                DiaVencimento = reader.GetInt32(reader.GetOrdinal("DiaVencimento")),
                                qtdDias = reader.GetInt32(reader.GetOrdinal("QtsDias")),
                                mensalidade = reader.GetDecimal(reader.GetOrdinal("mensalidade")),
                                horarioInicio = reader.GetTimeSpan(reader.GetOrdinal("HorarioInicio")).ToString(@"hh\:mm"),
                                horarioFim = reader.GetTimeSpan(reader.GetOrdinal("HorarioFim")).ToString(@"hh\:mm"),
                                NomeAdesao = reader.IsDBNull(reader.GetOrdinal("NomeAdesao")) ? "" : reader.GetString(reader.GetOrdinal("NomeAdesao"))
                            };
                        }
                    }
                }
            }
            return plano;
        }
        public void InserirAdesaoComFrequencias(AdesaoModels adesao)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Passo 1: Inserir na TBAdesao (correto, como no seu screenshot)
                    string sqlAdesao = @"
            INSERT INTO TBAdesao (NomeAdesao)
            VALUES (@NomeAdesao);
            SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmdAdesao = new SqlCommand(sqlAdesao, conn, transaction))
                    {
                        cmdAdesao.Parameters.AddWithValue("@NomeAdesao", adesao.NomeAdesao);
                        int idAdesao = Convert.ToInt32(cmdAdesao.ExecuteScalar());

                        // Passo 2: Inserir cada frequência com os dados de VIP na TBAdesaoFrequencias
                        foreach (var freq in adesao.Frequencias)
                        {
                            // A sua query original estava correta no conceito.
                            string sqlFreq = @"
                    INSERT INTO TBAdesaoFrequencias (IdAdesao, QtdDiasPermitidos, Mensalidade, IsVip, ValorVip)
                    VALUES (@IdAdesao, @QtdDiasPermitidos, @Mensalidade, @IsVip, @ValorVip)";

                            using (SqlCommand cmdFreq = new SqlCommand(sqlFreq, conn, transaction))
                            {
                                // Parâmetros que vêm de cada frequência
                                cmdFreq.Parameters.AddWithValue("@IdAdesao", idAdesao);
                                cmdFreq.Parameters.AddWithValue("@QtdDiasPermitidos", freq.QtdDiasPermitidos);
                                cmdFreq.Parameters.AddWithValue("@Mensalidade", freq.Mensalidade);

                                // **** A CORREÇÃO REAL É AQUI ****
                                // Os valores de VIP vêm do objeto 'adesao' principal, e não de cada 'freq'.
                                cmdFreq.Parameters.AddWithValue("@IsVip", adesao.IsVip);

                                if (adesao.ValorVip > 0)
                                {
                                    cmdFreq.Parameters.AddWithValue("@ValorVip", adesao.ValorVip);
                                }
                                else
                                {
                                    cmdFreq.Parameters.AddWithValue("@ValorVip", DBNull.Value);
                                }

                                cmdFreq.ExecuteNonQuery();
                            }
                        }

                        // Passo 3: Vincular os planos (turmas)
                        foreach (int idPlano in adesao.IdsPlanos)
                        {
                            string sqlVinculo = "INSERT INTO TBAdesaoPlanos (IdAdesao, IdPlano) VALUES (@IdAdesao, @IdPlano)";
                            using (SqlCommand cmdVinculo = new SqlCommand(sqlVinculo, conn, transaction))
                            {
                                cmdVinculo.Parameters.AddWithValue("@IdAdesao", idAdesao);
                                cmdVinculo.Parameters.AddWithValue("@IdPlano", idPlano);
                                cmdVinculo.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }



        // Coloque este método dentro da sua classe PlanoDAL.cs

        public List<AdesaoModels> ListarAdesoesComFrequencias()
        {
            var adesoes = new Dictionary<int, AdesaoModels>();
            string query = @"
        SELECT 
            a.IdAdesao, 
            a.NomeAdesao, 
            f.IdFrequencia, 
            f.QtdDiasPermitidos, 
            f.Mensalidade,
            f.IsVip,
            f.ValorVip
        FROM 
            TBAdesao a
        LEFT JOIN 
            TBAdesaoFrequencias f ON a.IdAdesao = f.IdAdesao
        ORDER BY 
            a.IdAdesao, f.QtdDiasPermitidos;";

            using (SqlConnection conn = new SqlConnection(connectionString)) // Certifique-se que connectionString está acessível
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idAdesao = reader.GetInt32(reader.GetOrdinal("IdAdesao"));
                            AdesaoModels adesao;

                            if (!adesoes.TryGetValue(idAdesao, out adesao))
                            {
                                adesao = new AdesaoModels
                                {
                                    IdAdesao = idAdesao,
                                    NomeAdesao = reader["NomeAdesao"].ToString(),
                                    // AQUI ESTÁ A MÁGICA: Populamos as propriedades do objeto principal
                                    IsVip = reader["IsVip"] != DBNull.Value && reader.GetBoolean(reader.GetOrdinal("IsVip")),
                                    ValorVip = reader["ValorVip"] != DBNull.Value ? reader.GetDecimal(reader.GetOrdinal("ValorVip")) : 0,
                                    Frequencias = new List<FrequenciaAdesaoModels>()
                                };
                                adesoes.Add(idAdesao, adesao);
                            }

                            if (reader["IdFrequencia"] != DBNull.Value)
                            {
                                adesao.Frequencias.Add(new FrequenciaAdesaoModels
                                {
                                    IdFrequencia = reader.GetInt32(reader.GetOrdinal("IdFrequencia")),
                                    QtdDiasPermitidos = reader.GetInt32(reader.GetOrdinal("QtdDiasPermitidos")),
                                    Mensalidade = reader.GetDecimal(reader.GetOrdinal("Mensalidade"))
                                });
                            }
                        }
                    }
                }
            }
            return adesoes.Values.ToList();
        }

        public DataTable ListarTurmasAdesao()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT IdPlano, Nome FROM TBPlanos";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // PlanoDAL.cs
        public void ExcluirAdesao(int idAdesao)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    string sqlPlanos = "DELETE FROM TBAdesaoPlanos WHERE IdAdesao = @IdAdesao";
                    using (SqlCommand cmd = new SqlCommand(sqlPlanos, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdAdesao", idAdesao);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlFrequencias = "DELETE FROM TBAdesaoFrequencias WHERE IdAdesao = @IdAdesao";
                    using (SqlCommand cmd = new SqlCommand(sqlFrequencias, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdAdesao", idAdesao);
                        cmd.ExecuteNonQuery();
                    }

                    // Finalmente, deleta da tabela principal
                    string sqlAdesao = "DELETE FROM TBAdesao WHERE IdAdesao = @IdAdesao";
                    using (SqlCommand cmd = new SqlCommand(sqlAdesao, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IdAdesao", idAdesao);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void InserirFrequenciasAdesao(List<FrequenciaAdesaoModels> frequencias, SqlConnection conn, SqlTransaction transaction)
        {
            foreach (var freq in frequencias)
            {
                string sql = @"
        INSERT INTO TBAdesaoFrequencias (IdAdesao, QtdDias, Mensalidade, IsVip, ValorVip)
        VALUES (@IdAdesao, @QtdDias, @Mensalidade, @IsVip, @ValorVip)";

                using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@IdAdesao", freq.IdAdesao);
                    cmd.Parameters.AddWithValue("@QtdDias", freq.QtdDiasPermitidos);
                    cmd.Parameters.AddWithValue("@Mensalidade", freq.Mensalidade);
                    cmd.Parameters.AddWithValue("@IsVip", freq.IsVip); // bool aceita null diretamente
                    cmd.Parameters.AddWithValue("@ValorVip", (object)freq.ValorVip ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        // Coloque este método dentro da sua classe PlanoDAL.cs
        public void AtualizarAdesao(AdesaoModels adesao)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Atualiza o nome na tabela principal
                    string sqlUpdateAdesao = "UPDATE TBAdesao SET NomeAdesao = @NomeAdesao WHERE IdAdesao = @IdAdesao";
                    using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdateAdesao, conn, transaction))
                    {
                        cmdUpdate.Parameters.AddWithValue("@NomeAdesao", adesao.NomeAdesao);
                        cmdUpdate.Parameters.AddWithValue("@IdAdesao", adesao.IdAdesao);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    // 2. Deleta as frequências antigas
                    string sqlDeleteFreq = "DELETE FROM TBAdesaoFrequencias WHERE IdAdesao = @IdAdesao";
                    using (SqlCommand cmdDelete = new SqlCommand(sqlDeleteFreq, conn, transaction))
                    {
                        cmdDelete.Parameters.AddWithValue("@IdAdesao", adesao.IdAdesao);
                        cmdDelete.ExecuteNonQuery();
                    }

                    // 3. Insere as novas frequências (com os dados de VIP atualizados)
                    foreach (var freq in adesao.Frequencias)
                    {
                        string sqlInsertFreq = @"
                    INSERT INTO TBAdesaoFrequencias (IdAdesao, QtdDiasPermitidos, Mensalidade, IsVip, ValorVip)
                    VALUES (@IdAdesao, @QtdDiasPermitidos, @Mensalidade, @IsVip, @ValorVip)";
                        using (SqlCommand cmdInsert = new SqlCommand(sqlInsertFreq, conn, transaction))
                        {
                            cmdInsert.Parameters.AddWithValue("@IdAdesao", adesao.IdAdesao);
                            cmdInsert.Parameters.AddWithValue("@QtdDiasPermitidos", freq.QtdDiasPermitidos);
                            cmdInsert.Parameters.AddWithValue("@Mensalidade", freq.Mensalidade);
                            cmdInsert.Parameters.AddWithValue("@IsVip", adesao.IsVip);
                            cmdInsert.Parameters.AddWithValue("@ValorVip", adesao.ValorVip > 0 ? (object)adesao.ValorVip : DBNull.Value);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }

                    // 4. Deleta os vínculos de planos antigos
                    string sqlDeletePlanos = "DELETE FROM TBAdesaoPlanos WHERE IdAdesao = @IdAdesao";
                    using (SqlCommand cmdDelete = new SqlCommand(sqlDeletePlanos, conn, transaction))
                    {
                        cmdDelete.Parameters.AddWithValue("@IdAdesao", adesao.IdAdesao);
                        cmdDelete.ExecuteNonQuery();
                    }

                    // 5. Insere os novos vínculos de planos
                    foreach (var idPlano in adesao.IdsPlanos)
                    {
                        string sqlInsertPlano = "INSERT INTO TBAdesaoPlanos (IdAdesao, IdPlano) VALUES (@IdAdesao, @IdPlano)";
                        using (SqlCommand cmdInsert = new SqlCommand(sqlInsertPlano, conn, transaction))
                        {
                            cmdInsert.Parameters.AddWithValue("@IdAdesao", adesao.IdAdesao);
                            cmdInsert.Parameters.AddWithValue("@IdPlano", idPlano);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public AdesaoModels BuscarAdesaoPorId(int idAdesao)
        {
            AdesaoModels adesao = null;

            // Query que busca a adesão principal e os dados de VIP da primeira frequência encontrada
            string query = @"
        SELECT 
            a.IdAdesao, 
            a.NomeAdesao, 
            f.IsVip,
            f.ValorVip
        FROM 
            TBAdesao a
        LEFT JOIN 
            TBAdesaoFrequencias f ON a.IdAdesao = f.IdAdesao
        WHERE 
            a.IdAdesao = @IdAdesao;
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Busca os dados principais e de VIP
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdAdesao", idAdesao);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Usamos o 'if' pois esperamos apenas uma linha principal
                        if (reader.Read())
                        {
                            adesao = new AdesaoModels
                            {
                                IdAdesao = Convert.ToInt32(reader["IdAdesao"]),
                                NomeAdesao = reader["NomeAdesao"].ToString(),
                                // AQUI ESTÁ A CORREÇÃO: Lemos os dados de VIP e populamos o objeto
                                IsVip = reader["IsVip"] != DBNull.Value && Convert.ToBoolean(reader["IsVip"]),
                                ValorVip = reader["ValorVip"] != DBNull.Value ? Convert.ToDecimal(reader["ValorVip"]) : 0,
                                Frequencias = new List<FrequenciaAdesaoModels>(),
                                IdsPlanos = new List<int>()
                            };
                        }
                    }
                }

                if (adesao == null) return null; 

                string freqQuery = "SELECT IdFrequencia, QtdDiasPermitidos, Mensalidade FROM TBAdesaoFrequencias WHERE IdAdesao = @IdAdesao";
                using (SqlCommand cmdFreq = new SqlCommand(freqQuery, conn))
                {
                    cmdFreq.Parameters.AddWithValue("@IdAdesao", idAdesao);
                    using (SqlDataReader readerFreq = cmdFreq.ExecuteReader())
                    {
                        while (readerFreq.Read())
                        {
                            adesao.Frequencias.Add(new FrequenciaAdesaoModels
                            {
                                IdFrequencia = Convert.ToInt32(readerFreq["IdFrequencia"]),
                                QtdDiasPermitidos = Convert.ToInt32(readerFreq["QtdDiasPermitidos"]),
                                Mensalidade = Convert.ToDecimal(readerFreq["Mensalidade"])
                            });
                        }
                    }
                }

                // 3. Busca a lista de planos/turmas associados
                string planosQuery = "SELECT IdPlano FROM TBAdesaoPlanos WHERE IdAdesao = @IdAdesao";
                using (SqlCommand cmdPlanos = new SqlCommand(planosQuery, conn))
                {
                    cmdPlanos.Parameters.AddWithValue("@IdAdesao", idAdesao);
                    using (SqlDataReader readerPlanos = cmdPlanos.ExecuteReader())
                    {
                        while (readerPlanos.Read())
                        {
                            adesao.IdsPlanos.Add(Convert.ToInt32(readerPlanos["IdPlano"]));
                        }
                    }
                }
            }

            return adesao;
        }


        public List<AdesaoModels> ListarTodasAdesoes()
            {
                List<AdesaoModels> lista = new List<AdesaoModels>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT IdAdesao, NomeAdesao FROM TBAdesao";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new AdesaoModels
                        {
                            IdAdesao = Convert.ToInt32(reader["IdAdesao"]),
                            NomeAdesao = reader["NomeAdesao"].ToString()
                        });
                    }
                }

                return lista;
        }
        public List<PlanoModels> BuscarPlanosPorAdesao(int idAdesao)
        {
            List<PlanoModels> lista = new List<PlanoModels>();
            using (SqlConnection con = new SqlConnection(connectionString))

            {
                string sql = @"SELECT p.IdPlano, p.Nome
                       FROM TBPlanos p
                       INNER JOIN TBAdesaoPlanos ap ON ap.IdPlano = p.IdPlano
                       WHERE ap.IdAdesao = @IdAdesao";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@IdAdesao", idAdesao);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    PlanoModels plano = new PlanoModels
                    {
                        IdPlano = Convert.ToInt32(dr["IdPlano"]),
                        Nome = dr["Nome"].ToString()
                    };
                    lista.Add(plano);
                }
            }
            return lista;
        }
        public decimal BuscarValorPorAdesaoEFrequencia(int idAdesao, int frequencia)
        {
            decimal valor = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string query = @"
            SELECT Mensalidade 
            FROM TBAdesaoFrequencias
            WHERE IdAdesao = @IdAdesao AND QtdDiasPermitidos = @QtdDiasPermitidos;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdAdesao", idAdesao);
                    cmd.Parameters.AddWithValue("@QtdDiasPermitidos", frequencia);

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        valor = Convert.ToDecimal(result);
                    }
                }
            }

            return valor;
        }
        public bool VerificarAdesaoEmUso(int idAdesao)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM TBPlanoAluno WHERE IdAdesao = @IdAdesao";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdAdesao", idAdesao);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public int BuscarIdDetalhe(int idPlano)
        {
            int idDetalhe = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT IdDetalhe FROM TBPlanoDetalhes WHERE IdPlano = @IdPlano;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdPlano", idPlano);
          
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        idDetalhe = Convert.ToInt32(result);
                    }
                }
            }
            return idDetalhe;
        }


















    }
}
