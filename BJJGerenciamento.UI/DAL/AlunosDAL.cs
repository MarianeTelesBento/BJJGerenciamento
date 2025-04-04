﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;
using BJJGerenciamento.UI.Models;

namespace BJJGerenciamento.UI.DAL
{
    public class AlunosDAL
    {
        public string connectionString = "Data Source=FAC00DT68ZW11-1;Initial Catalog=BJJ_DB;User ID=Sa;Password=123456;";

        //public string connectionString = "Data Source=DESKTOP-FTCVI92\\SQLEXPRESS;Initial Catalog=BJJ_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public void CadastrarPlanoAluno(PlanoAlunoModels plano, List<KeyValuePair<int, string>> diasHorarios)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO TBPlanoAluno " +
                                      "(IdAlunos, IdDia, IdHorario, IdDetalhe) " +
                                      "VALUES (@idAlunos, @idDia, @idHorario, @idDetalhe);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@idAlunos", SqlDbType.Int);
                    command.Parameters.Add("@idDia", SqlDbType.Int);
                    command.Parameters.Add("@idHorario", SqlDbType.VarChar, 50);
                    command.Parameters.Add("@idDetalhe", SqlDbType.Int);

                    foreach (var diaHorario in diasHorarios)
                    {
                        command.Parameters["@idAlunos"].Value = plano.idAlunos;
                        command.Parameters["@idDia"].Value = diaHorario.Key;  
                        command.Parameters["@idHorario"].Value = diaHorario.Value;
                        command.Parameters["@idDetalhe"].Value = plano.idDetalhe;

                        command.ExecuteNonQuery();
                    }
                }
            }

        }


        public int CadastrarResponsavel(ResponsavelModels responsavel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO TBResponsaveis " +
                                      "(Nome, Sobrenome, CPF, RG, Telefone, Email, Bairro, CEP, Cidade, Rua, Estado, DataDeNascimento, Complemento, NumeroCasa) " +
                                      "VALUES (@nome, @sobrenome, @cpf, @rg, @telefone, @email, @bairro, @cep, @cidade, @rua, @estado, @dataNascimento, @complemento, @numeroCasa);" +
                                      "SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", responsavel.Nome);
                    command.Parameters.AddWithValue("@sobrenome", responsavel.Sobrenome);
                    command.Parameters.AddWithValue("@telefone", responsavel.Telefone);
                    command.Parameters.AddWithValue("@email", responsavel.Email);
                    command.Parameters.AddWithValue("@rg", responsavel.Rg);
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

                SqlCommand inserirCommand = new SqlCommand("insert into TBAlunos(IdPlano, IdResponsavel, Nome, Sobrenome, Telefone, Email, Rg, Cpf, DataNascimento, CEP, Rua, Bairro, Cidade, Estado, NumeroCasa, CarteiraFPJJ, Complemento, IdMatricula) values(@idPlano, @idResponsavel, @nome, @sobrenome, @telefone, @email, @rg, @cpf, @dataNascimento, @cep, @rua, @bairro, @cidade, @estado, @numeroCasa, @carteiraFPJJ, @complemento, @idMatricula); " +
                    "SELECT SCOPE_IDENTITY();", connection);

                inserirCommand.Parameters.AddWithValue("@idPlano", aluno.IdPlano);
                inserirCommand.Parameters.AddWithValue("@idResponsavel", aluno.IdResponsavel);
                inserirCommand.Parameters.AddWithValue("@nome", aluno.Nome);
                inserirCommand.Parameters.AddWithValue("@sobrenome", aluno.Sobrenome);
                inserirCommand.Parameters.AddWithValue("@telefone", aluno.Telefone);
                inserirCommand.Parameters.AddWithValue("@email", aluno.Email);
                inserirCommand.Parameters.AddWithValue("@rg", aluno.Rg);
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
                                Rg = reader.GetString(4),
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
                    IdPlano = reader.GetInt32(1),
                    IdResponsavel = reader.GetInt32(2),
                    Nome = reader.GetString(3),
                    Sobrenome = reader.GetString(4),
                    Telefone = reader.GetString(5),
                    Email = reader.GetString(6),
                    DataNascimento = reader.GetDateTime(7).ToString("dd/MM/yyyy"),
                    Cpf = reader.GetString(8),

                    Rg = reader.GetString(9),
                    Estado = reader.GetString(10),
                    Bairro = reader.GetString(11),
                    Cidade = reader.GetString(12),
                    Rua = reader.GetString(13),
                    NumeroCasa = reader.GetInt32(14).ToString(),
                    Complemento = reader.GetString(15),
                    Cep = reader.GetString(16),
                    CarteiraFPJJ = reader.GetString(17),
                    IdMatricula = reader.GetInt32(18),
                    StatusMatricula = reader.GetBoolean(19),
                    DataMatricula = reader.GetDateTime(20).ToString("dd/MM/yyyy")
                };

                alunoList.Add(aluno);
            }

            connection.Close();

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
                                    Rg = @rg, 
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
                    command.Parameters.AddWithValue("@rg", aluno.Rg);
                    command.Parameters.AddWithValue("@cpf", aluno.Cpf);
                    command.Parameters.AddWithValue("@dataNascimento", aluno.DataNascimento);
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
    }
}