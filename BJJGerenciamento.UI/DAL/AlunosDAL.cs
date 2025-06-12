
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Web;
using BJJGerenciamento.UI.Models;

namespace BJJGerenciamento.UI.DAL
{
    public class AlunosDAL
    {

        //private string connectionString = "Data Source=rsm-dev-works-server.database.windows.net;Initial Catalog=BJJ_DB;User ID=rsm-dev;Password=adm1234@;";
        private readonly string connectionString;

        public AlunosDAL()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BJJGerenciamentoConnectionString"].ConnectionString;
        }

        public int CadastrarResponsavel(ResponsavelModels responsavel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO TBResponsaveis " +
                                      "(Nome, Sobrenome, CPF, Telefone, Email, Bairro, CEP, Cidade, Rua, Estado, DataDeNascimento, Complemento, NumeroCasa) " +
                                      "VALUES (@nome, @sobrenome, @cpf, @telefone, @email, @bairro, @cep, @cidade, @rua, @estado, @dataNascimento, @complemento, @numeroCasa);" +
                                      "SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", responsavel.Nome);
                    command.Parameters.AddWithValue("@sobrenome", responsavel.Sobrenome);
                    command.Parameters.AddWithValue("@telefone", responsavel.Telefone);
                    command.Parameters.AddWithValue("@email", responsavel.Email);
                    command.Parameters.AddWithValue("@cpf", responsavel.Cpf);
                    command.Parameters.AddWithValue("@dataNascimento", responsavel.DataNascimento);
                    command.Parameters.AddWithValue("@cep", responsavel.Cep);
                    command.Parameters.AddWithValue("@rua", responsavel.Rua);
                    command.Parameters.AddWithValue("@bairro", responsavel.Bairro);
                    command.Parameters.AddWithValue("@cidade", responsavel.Cidade);
                    command.Parameters.AddWithValue("@estado", responsavel.Estado);
                    command.Parameters.AddWithValue("@numeroCasa", responsavel.NumeroCasa);
                    command.Parameters.AddWithValue("@complemento", responsavel.Complemento);

                    var idResponsavel = command.ExecuteScalar();
                    return Convert.ToInt32(idResponsavel);
                }
            }

        }
        public int CadastrarAluno(AlunoModels aluno)
        {
            int cadastroRealizado;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand inserirCommand = new SqlCommand("insert into TBAlunos(IdResponsavel, Nome, Sobrenome, Telefone, Email, Cpf, DataNascimento, CEP, Rua, Bairro, Cidade, Estado, NumeroCasa, CarteiraFPJJ, Complemento, IdMatricula) values(@idResponsavel, @nome, @sobrenome, @telefone, @email, @cpf, @dataNascimento, @cep, @rua, @bairro, @cidade, @estado, @numeroCasa, @carteiraFPJJ, @complemento, @idMatricula); " +
                "SELECT SCOPE_IDENTITY();", connection);

            inserirCommand.Parameters.AddWithValue("@idResponsavel", (object)aluno.IdResponsavel ?? DBNull.Value);
            inserirCommand.Parameters.AddWithValue("@nome", aluno.Nome);
            inserirCommand.Parameters.AddWithValue("@sobrenome", aluno.Sobrenome);
            inserirCommand.Parameters.AddWithValue("@telefone", aluno.Telefone);
            inserirCommand.Parameters.AddWithValue("@email", aluno.Email);
            inserirCommand.Parameters.AddWithValue("@cpf", aluno.Cpf);
            inserirCommand.Parameters.AddWithValue("@dataNascimento", aluno.DataNascimento);
            inserirCommand.Parameters.AddWithValue("@cep", aluno.Cep);
            inserirCommand.Parameters.AddWithValue("@rua", aluno.Rua);
            inserirCommand.Parameters.AddWithValue("@bairro", aluno.Bairro);
            inserirCommand.Parameters.AddWithValue("@cidade", aluno.Cidade);
            inserirCommand.Parameters.AddWithValue("@estado", aluno.Estado);
            inserirCommand.Parameters.AddWithValue("@numeroCasa", aluno.NumeroCasa);
            inserirCommand.Parameters.AddWithValue("@carteiraFPJJ", aluno.CarteiraFPJJ);
            inserirCommand.Parameters.AddWithValue("@complemento", aluno.Complemento);
            inserirCommand.Parameters.AddWithValue("@idMatricula", aluno.IdMatricula);


            cadastroRealizado = Convert.ToInt32(inserirCommand.ExecuteScalar());

            connection.Close();

            return Convert.ToInt32(cadastroRealizado);
        }
        public int CadastrarMatricula(DateTime dataAtual, bool estadoMatricula)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO TBMatriculas " +
                                      "(StatusdaMatricula, Data) " +
                                      "VALUES (@estadoMatricula, @dataMatricula);" +
                                      "SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@dataMatricula", dataAtual);
                    command.Parameters.AddWithValue("@estadoMatricula", estadoMatricula);

                    var cadastroRealizado = Convert.ToInt32(command.ExecuteScalar());

                    return cadastroRealizado;
                }
            }
        }
        public AlunoModels BuscarCpfAluno(string cpf)
        {
            AlunoModels aluno = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM TBAlunos WHERE CPF = @cpf;", connection))
                {
                    command.Parameters.AddWithValue("@cpf", cpf);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            aluno = new AlunoModels
                            {
                                Cpf = reader.GetString(6)
                            };
                        }
                    }
                }
            }

            return aluno;
        }
        public ResponsavelModels BuscarCpfResponsavel(string cpf)
        {
            ResponsavelModels responsavel = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM TBResponsaveis WHERE CPF = @cpf;", connection))
                {
                    command.Parameters.AddWithValue("@cpf", cpf);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            responsavel = new ResponsavelModels
                            {
                                IdResponsavel = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Sobrenome = reader.GetString(2),
                                Cpf = reader.GetString(3),
                                Telefone = reader.GetString(5),
                                Email = reader.GetString(6),
                                Bairro = reader.GetString(7),
                                Cep = reader.GetString(8),
                                Cidade = reader.GetString(9),
                                Rua = reader.GetString(10),
                                Estado = reader.GetString(11),
                                DataNascimento = reader.GetDateTime(12).ToString("dd/MM/yyyy"),
                                Complemento = reader.IsDBNull(13) ? "" : reader.GetString(13),
                                NumeroCasa = reader.GetInt32(14).ToString()
                            };
                        }
                    }
                }
            }

            return responsavel;
        }
        public List<AlunoModels> VisualizarDados()
        {
            List<AlunoModels> alunoList = new List<AlunoModels>();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand readerCommand = new SqlCommand($"SELECT a.*," +
    $" m.StatusdaMatricula, m.Data FROM TBAlunos a LEFT JOIN " +
    $"TBMatriculas m ON a.IdMatricula = m.IdMatricula;", connection);

            SqlDataReader reader = readerCommand.ExecuteReader();

            while (reader.Read())
            {
                AlunoModels aluno = new AlunoModels()
                {
                    IdAlunos = reader.GetInt32(0),

                    IdResponsavel = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                    Nome = reader.GetString(2),
                    Sobrenome = reader.GetString(3),
                    Telefone = reader.GetString(4),
                    Email = reader.GetString(5),
                    DataNascimento = reader.GetDateTime(6).ToString("dd/MM/yyyy"),
                    Cpf = reader.GetString(7),

                    Estado = reader.GetString(9),
                    Bairro = reader.GetString(10),
                    Cidade = reader.GetString(11),
                    Rua = reader.GetString(12),
                    NumeroCasa = reader.GetInt32(13).ToString(),
                    Complemento = reader.GetString(14),
                    Cep = reader.GetString(15),
                    CarteiraFPJJ = reader.GetString(16),
                    IdMatricula = reader.GetInt32(17),

                    StatusMatricula = reader.GetBoolean(reader.GetOrdinal("StatusdaMatricula")),
                    DataMatricula = reader.GetDateTime(reader.GetOrdinal("Data")).ToString("dd/MM/yyyy")
                };

                alunoList.Add(aluno);
            }

            connection.Close();

            return alunoList;
        }

        public List<AlunoModels> VisualizarDadosChamada()
        {
            List<AlunoModels> alunoList = new List<AlunoModels>();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand readerCommand = new SqlCommand($@"
            SELECT a.*, m.StatusdaMatricula, m.Data 
            FROM TBAlunos a 
            INNER JOIN TBMatriculas m ON a.IdMatricula = m.IdMatricula 
            WHERE m.StatusdaMatricula = 1;", connection);

            SqlDataReader reader = readerCommand.ExecuteReader();

            while (reader.Read())
            {
                AlunoModels aluno = new AlunoModels()
                {
                    IdAlunos = reader.GetInt32(0),

                    IdResponsavel = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                    Nome = reader.GetString(2),
                    Sobrenome = reader.GetString(3),
                    Telefone = reader.GetString(4),
                    Email = reader.GetString(5),
                    DataNascimento = reader.GetDateTime(6).ToString("dd/MM/yyyy"),
                    Cpf = reader.GetString(7),

                    Estado = reader.GetString(9),
                    Bairro = reader.GetString(10),
                    Cidade = reader.GetString(11),
                    Rua = reader.GetString(12),
                    NumeroCasa = reader.GetInt32(13).ToString(),
                    Complemento = reader.GetString(14),
                    Cep = reader.GetString(15),
                    CarteiraFPJJ = reader.GetString(16),
                    IdMatricula = reader.GetInt32(17),

                    StatusMatricula = reader.GetBoolean(reader.GetOrdinal("StatusdaMatricula")),
                    DataMatricula = reader.GetDateTime(reader.GetOrdinal("Data")).ToString("dd/MM/yyyy")
                };

                alunoList.Add(aluno);
            }

            connection.Close();

            return alunoList;
        }

        public List<AlunoModels> PesquisarAlunos(string termo = null, int? idPlano = null, bool apenasAtivos = false)
        {
            List<AlunoModels> alunoList = new List<AlunoModels>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            SELECT DISTINCT 
                a.*,
                m.StatusdaMatricula, 
                m.Data AS DataMatricula
            FROM TBAlunos a 
            LEFT JOIN TBMatriculas m ON a.IdMatricula = m.IdMatricula
            LEFT JOIN TBPlanoAluno pa ON a.IdAluno = pa.IdAluno
            LEFT JOIN TBPlanoDetalhes pd ON pa.IdDetalhe = pd.IdDetalhe
            WHERE (ISNULL(@termo, '') = '' OR a.Nome LIKE @termo OR a.Sobrenome LIKE @termo)
              AND (@idPlano IS NULL OR pd.IdPlano = @idPlano)
              AND (@apenasAtivos = 0 OR m.StatusdaMatricula = 1);
            ";

                using (SqlCommand readerCommand = new SqlCommand(query, connection))
                {
                    if (!string.IsNullOrWhiteSpace(termo))
                        readerCommand.Parameters.AddWithValue("@termo", "%" + termo + "%");
                    else
                        readerCommand.Parameters.AddWithValue("@termo", DBNull.Value);

                    if (idPlano.HasValue)
                        readerCommand.Parameters.AddWithValue("@idPlano", idPlano.Value);
                    else
                        readerCommand.Parameters.AddWithValue("@idPlano", DBNull.Value);

                    readerCommand.Parameters.AddWithValue("@apenasAtivos", apenasAtivos ? 1 : 0);

                    SqlDataReader reader = readerCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        AlunoModels aluno = new AlunoModels()
                        {
                            IdAlunos = reader.GetInt32(0),

                            IdResponsavel = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                            Nome = reader.GetString(2),
                            Sobrenome = reader.GetString(3),
                            Telefone = reader.GetString(4),
                            Email = reader.GetString(5),
                            DataNascimento = reader.GetDateTime(6).ToString("dd/MM/yyyy"),
                            Cpf = reader.GetString(7),

                            Estado = reader.GetString(9),
                            Bairro = reader.GetString(10),
                            Cidade = reader.GetString(11),
                            Rua = reader.GetString(12),
                            NumeroCasa = reader.GetInt32(13).ToString(),
                            Complemento = reader.GetString(14),
                            Cep = reader.GetString(15),
                            CarteiraFPJJ = reader.GetString(16),
                            IdMatricula = reader.GetInt32(17),
                            StatusMatricula = reader.GetBoolean(18),
                            DataMatricula = reader.GetDateTime(19).ToString("dd/MM/yyyy")
                        };

                        alunoList.Add(aluno);
                    }
                }
            }

            return alunoList;
        }


        public List<AlunoModels> VisualizarAlunosPresencas()
        {
            List<AlunoModels> alunoList = new List<AlunoModels>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(@"
            SELECT 
                a.IdMatricula,
                a.Nome,
                a.Sobrenome,
                a.CPF,
                a.Telefone,
                m.StatusdaMatricula,
                COUNT(DISTINCT CASE
                    WHEN p.DataPresenca > ISNULL(g.UltimaGraduacao, '1900-01-01') THEN p.IdPresenca
                    ELSE NULL
                END) AS TotalPresencas

            FROM TBAlunos a
            JOIN TBMatriculas m ON a.IdMatricula = m.IdMatricula
            LEFT JOIN TBPresencas p ON m.IdMatricula = p.IdMatricula
            OUTER APPLY (
                SELECT MAX(DataGraduacao) AS UltimaGraduacao
                FROM TBGraduacoes g
                WHERE g.IdMatricula = a.IdMatricula
            ) g
            WHERE m.StatusdaMatricula = 1
            GROUP BY a.IdMatricula, a.Nome, a.Sobrenome, a.CPF, a.Telefone, m.StatusdaMatricula
            ORDER BY a.IdMatricula ASC", connection);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AlunoModels aluno = new AlunoModels()
                    {
                        IdMatricula = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Sobrenome = reader.GetString(2),
                        Cpf = reader.GetString(3),
                        Telefone = reader.GetString(4),
                        StatusMatricula = reader.GetBoolean(5),
                        TotalPresencas = reader.GetInt32(6)
                    };

                    alunoList.Add(aluno);
                }
            }

            return alunoList;
        }

        public List<AlunoModels> PesquisarAlunosPresencas(string termo = null, int? idPlano = null, int? idHora = null)

        {
            List<AlunoModels> alunoList = new List<AlunoModels>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        a.IdMatricula,
                        a.Nome,
                        a.Sobrenome,
                        a.CPF,
                        a.Telefone,
                        m.StatusdaMatricula,
                        COUNT(DISTINCT CASE
                            WHEN p.DataPresenca > ISNULL(g.UltimaGraduacao, '1900-01-01') THEN p.IdPresenca
                            ELSE NULL
                        END) AS TotalPresencas
                    FROM TBAlunos a
                    JOIN TBMatriculas m ON a.IdMatricula = m.IdMatricula
                    LEFT JOIN TBPresencas p ON m.IdMatricula = p.IdMatricula
                    LEFT JOIN TBPlanoAluno pa ON a.IdAluno = pa.IdAluno
                    LEFT JOIN TBPlanoDetalhes pd ON pa.IdDetalhe = pd.IdDetalhe
                    LEFT JOIN TBPlanoHorario ph 
                        ON pd.IdPlano = ph.IdPlano 
                        AND pa.IdHorario = ph.IdHora 
                        AND pa.IdDia = ph.IdDia
                    OUTER APPLY (
                        SELECT MAX(DataGraduacao) AS UltimaGraduacao
                        FROM TBGraduacoes g
                        WHERE g.IdMatricula = a.IdMatricula
                    ) g
                    WHERE 
                        m.StatusdaMatricula = '1' AND
                        (ISNULL(@termo, '') = '' OR a.Nome LIKE @termo OR a.Sobrenome LIKE @termo)
                        AND (@idPlano IS NULL OR pd.IdPlano = @idPlano)
                        AND (@idHora IS NULL OR ph.IdHora = @idHora) 
                    GROUP BY 
                        a.IdMatricula, a.Nome, a.Sobrenome, a.CPF, a.Telefone, m.StatusdaMatricula
                    ORDER BY a.IdMatricula ASC
                    ";


                using (SqlCommand readerCommand = new SqlCommand(query, connection))
                {
                    if (!string.IsNullOrWhiteSpace(termo))
                        readerCommand.Parameters.AddWithValue("@termo", "%" + termo + "%");
                    else
                        readerCommand.Parameters.AddWithValue("@termo", DBNull.Value);

                    if (idPlano.HasValue)
                        readerCommand.Parameters.AddWithValue("@idPlano", idPlano.Value);
                    else
                        readerCommand.Parameters.AddWithValue("@idPlano", DBNull.Value);
                    if (idHora.HasValue)
                        readerCommand.Parameters.AddWithValue("@idHora", idHora.Value);
                    else
                        readerCommand.Parameters.AddWithValue("@idHora", DBNull.Value);


                    
                    
                    
                    
                    
                    
                    SqlDataReader reader = readerCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        AlunoModels aluno = new AlunoModels()
                        {
                            IdMatricula = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Sobrenome = reader.GetString(2),
                            Cpf = reader.GetString(3),
                            Telefone = reader.GetString(4),
                            StatusMatricula = reader.GetBoolean(5),
                            TotalPresencas = reader.GetInt32(6)
                        };

                        alunoList.Add(aluno);
                    }
                }
            }

            return alunoList;
        }

        public bool AtualizarAluno(AlunoModels aluno)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"BEGIN TRANSACTION;
                                UPDATE TBAlunos 
                                SET 
                                    Nome = @nome, 
                                    Sobrenome = @sobrenome, 
                                    Cpf = @cpf, 
                                    Telefone = @telefone, 
                                    Email = @email, 
                                    DataNascimento = @dataNascimento, 
                                    CEP = @cep, 
                                    Rua = @rua, 
                                    Bairro = @bairro, 
                                    Cidade = @cidade, 
                                    Estado = @estado, 
                                    NumeroCasa = @numeroCasa, 
                                    CarteiraFPJJ = @carteiraFPJJ, 
                                    Complemento = @complemento 
                                WHERE IdAluno = @idAlunos;

                                UPDATE TBMatriculas 
                                SET StatusdaMatricula = @statusMatricula
                                WHERE IdMatricula = (SELECT IdMatricula FROM TBAlunos WHERE IdAluno = @idAlunos);

                                COMMIT TRANSACTION;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", aluno.Nome);
                    command.Parameters.AddWithValue("@sobrenome", aluno.Sobrenome);
                    command.Parameters.AddWithValue("@telefone", aluno.Telefone);
                    command.Parameters.AddWithValue("@email", aluno.Email);
                    command.Parameters.AddWithValue("@cpf", aluno.Cpf);
                    command.Parameters.AddWithValue("@dataNascimento", DateTime.ParseExact(aluno.DataNascimento, "dd/MM/yyyy", null));
                    command.Parameters.AddWithValue("@cep", aluno.Cep);
                    command.Parameters.AddWithValue("@rua", aluno.Rua);
                    command.Parameters.AddWithValue("@bairro", aluno.Bairro);
                    command.Parameters.AddWithValue("@cidade", aluno.Cidade);
                    command.Parameters.AddWithValue("@estado", aluno.Estado);
                    command.Parameters.AddWithValue("@numeroCasa", aluno.NumeroCasa);
                    command.Parameters.AddWithValue("@carteiraFPJJ", aluno.CarteiraFPJJ);
                    command.Parameters.AddWithValue("@complemento", aluno.Complemento);
                    command.Parameters.AddWithValue("@idAlunos", aluno.IdAlunos);
                    command.Parameters.AddWithValue("@statusMatricula", aluno.StatusMatricula);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool AtualizarResponsavel(ResponsavelModels responsavel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"UPDATE TBResponsaveis 
                                SET 
                                    Nome = @nome, 
                                    Sobrenome = @sobrenome, 
                                    Cpf = @cpf, 
                                    Telefone = @telefone, 
                                    Email = @email, 
                                    DataDeNascimento = @dataNascimento, 
                                    CEP = @cep, 
                                    Rua = @rua, 
                                    Bairro = @bairro, 
                                    Cidade = @cidade, 
                                    Estado = @estado, 
                                    NumeroCasa = @numeroCasa, 
                                    Complemento = @complemento 
                                WHERE IdResponsavel = @idResponsavel;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", responsavel.Nome);
                    command.Parameters.AddWithValue("@sobrenome", responsavel.Sobrenome);
                    command.Parameters.AddWithValue("@telefone", responsavel.Telefone);
                    command.Parameters.AddWithValue("@email", responsavel.Email);
                    command.Parameters.AddWithValue("@cpf", responsavel.Cpf);
                    command.Parameters.AddWithValue("@dataNascimento", responsavel.DataNascimento);
                    command.Parameters.AddWithValue("@cep", responsavel.Cep);
                    command.Parameters.AddWithValue("@rua", responsavel.Rua);
                    command.Parameters.AddWithValue("@bairro", responsavel.Bairro);
                    command.Parameters.AddWithValue("@cidade", responsavel.Cidade);
                    command.Parameters.AddWithValue("@estado", responsavel.Estado);
                    command.Parameters.AddWithValue("@numeroCasa", responsavel.NumeroCasa);
                    command.Parameters.AddWithValue("@complemento", responsavel.Complemento);
                    command.Parameters.AddWithValue("@idResponsavel", responsavel.IdResponsavel);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public ResponsavelModels BuscarResponsavel(int idMatricula)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT r.IdResponsavel, r.Nome, r.Sobrenome, r.CPF, r.Email, r.Telefone,
                                       r.DataDeNascimento, r.Cep, r.Rua, r.Bairro, r.Cidade, r.Estado, 
                                       r.NumeroCasa, r.Complemento
                                FROM TBResponsaveis r
                                JOIN TBAlunos a ON r.IdResponsavel = a.IdResponsavel
                                WHERE a.IdMatricula = @idMatricula;
                                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idMatricula", idMatricula);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ResponsavelModels responsavel = new ResponsavelModels
                            {
                                IdResponsavel = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Sobrenome = reader.GetString(2),
                                Cpf = reader.GetString(3),
                                Email = reader.GetString(4),
                                Telefone = reader.GetString(5),
                                DataNascimento = reader.GetDateTime(6).ToString("dd/MM/yyyy"),
                                Cep = reader.GetString(7),
                                Rua = reader.GetString(8),
                                Bairro = reader.GetString(9),
                                Cidade = reader.GetString(10),
                                Estado = reader.GetString(11),
                                NumeroCasa = reader.GetInt32(12).ToString(),
                                Complemento = reader.IsDBNull(13) ? "" : reader.GetString(13)
                            };

                            return responsavel;
                        }
                        return null;
                    }
                }

            }
        }

        public ResponsavelModels visualizarPlanoAluno(int idMatricula)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT r.IdResponsavel, r.Nome, r.Sobrenome, r.CPF, r.Email, r.Telefone,
                                       r.DataDeNascimento, r.Cep, r.Rua, r.Bairro, r.Cidade, r.Estado, 
                                       r.NumeroCasa, r.Complemento
                                FROM TBResponsaveis r
                                JOIN TBAlunos a ON r.IdResponsavel = a.IdResponsavel
                                WHERE a.IdMatricula = @idMatricula;
                                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idMatricula", idMatricula);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ResponsavelModels responsavel = new ResponsavelModels
                            {
                                IdResponsavel = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Sobrenome = reader.GetString(2),
                                Cpf = reader.GetString(3),
                                Email = reader.GetString(4),
                                Telefone = reader.GetString(5),
                                DataNascimento = reader.GetDateTime(6).ToString("dd/MM/yyyy"),
                                Cep = reader.GetString(7),
                                Rua = reader.GetString(8),
                                Bairro = reader.GetString(9),
                                Cidade = reader.GetString(10),
                                Estado = reader.GetString(11),
                                NumeroCasa = reader.GetInt32(12).ToString(),
                                Complemento = reader.IsDBNull(13) ? "" : reader.GetString(13)
                            };

                            return responsavel;
                        }
                        return null;
                    }
                }

            }
        }


        public static int ObterQuantidadeAtivos() 
        {
            using (SqlConnection conn = new SqlConnection( new AlunosDAL().connectionString))
            {
                string query = "SELECT COUNT(*) FROM TBMatriculas WHERE StatusdaMatricula = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
        public static int ObterQuantidadeInativos()
        {
            using (SqlConnection conn = new SqlConnection(new AlunosDAL().connectionString))
            {
                string query = "SELECT COUNT(*) FROM TBMatriculas WHERE StatusdaMatricula = 0";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
        public static List <AlunoModels> ObterAniversariantesDoMes()
        {
            List<AlunoModels> aniversariantes = new List<AlunoModels>();
            using (SqlConnection connection = new SqlConnection(new AlunosDAL().connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT a.IdAluno, a.Nome, a.Sobrenome, a.DataNascimento
                    FROM TBAlunos a
                    WHERE MONTH(a.DataNascimento) = MONTH(GETDATE())";
                    
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AlunoModels aluno = new AlunoModels
                            {
                                IdAlunos = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Sobrenome = reader.GetString(2),
                                DataNascimento = reader.GetDateTime(3).ToString("dd/MM/yyyy")
                            };
                            aniversariantes.Add(aluno);
                        }
                    }
                }
            }
            return aniversariantes;
        }
        public List<HoraModel> GetHorariosPorPlano(int idPlano)
        {
            List<HoraModel> horarios = new List<HoraModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            SELECT DISTINCT h.IdHora, h.HorarioInicio, h.HorarioFim
            FROM TBHora h
            INNER JOIN TBPlanoHorario ph ON h.IdHora = ph.IdHora
            WHERE ph.IdPlano = @idPlano
            ORDER BY h.HorarioInicio;";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@idPlano", idPlano); // Corrigido aqui!

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            horarios.Add(new HoraModel
                            {
                                IdHora = reader.GetInt32(0),
                                HorarioInicio = reader.GetTimeSpan(1),
                                HorarioFim = reader.GetTimeSpan(2)
                                // Removido Ativa pois não está na SELECT
                            });
                        }
                    }
                }
            }

            return horarios;
        }



    }
}