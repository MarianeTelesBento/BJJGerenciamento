using System;
using System.Collections.Generic;
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

        public int CadastrarAluno(AlunoModels aluno)
        {
            int cadastroRealizado;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand inserirCommand = new SqlCommand("insert into TBAlunos(Nome, Sobrenome, Telefone, Email, Rg, Cpf, DataNascimento, CEP, Rua, Bairro, Cidade, Estado, NumeroCasa, CarteiraFPJJ, Complemento, IdResponsavel) values(@nome, @sobrenome, @telefone, @email, @rg, @cpf, @dataNascimento, @cep, @rua, @bairro, @cidade, @estado, @numeroCasa, @carteiraFPJJ, @complemento, @idResponsavel);", connection);

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
            inserirCommand.Parameters.AddWithValue("@idResponsavel", aluno.IdResponsavel);

            cadastroRealizado = inserirCommand.ExecuteNonQuery();

            connection.Close();

            return cadastroRealizado;
        }

        public int CadastrarResponsavel(ResponsavelModels responsavel)
        {
            int idResponsavel = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO TBResponsaveis " +
                               "(Nome, Sobrenome, CPF, RG, Telefone, Email, Bairro, CEP, Cidade, Rua, Estado, DataNascimento, Complemento, NumeroCasa) " +
                               "VALUES (@nome, @sobrenome, @cpf, @rg, @telefone, @email, @bairro, @cep, @cidade, @rua, @estado, @dataNascimento, @complemento, @numeroCasa); " +
                               "SELECT SCOPE_IDENTITY();";  // Retorna o último ID gerado

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

                    // Obtém o ID gerado
                    idResponsavel = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return idResponsavel;
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
                using (SqlCommand command = new SqlCommand("SELECT * FROM TBPlanos", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            PlanoModels plano = new PlanoModels()
                            {
                                IdPlano = reader.GetInt32(0),
                                Nome = reader.GetString(1)
                            };
                            planoList.Add(plano);
                        }
                    }
                }
            }
            return planoList;
        }

        public List<string> BuscarDiasPlano(int idPlano)
        {
            List<string> diasPlanoList = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT d.Dia FROM TBDiasSemana d 
                    INNER JOIN TBPlanoDias pd ON d.IdDia = pd.IdDia 
                    WHERE pd.IdPlano = @IdPlano 
                    ORDER BY d.IdDia ASC", connection))
                {
                    command.Parameters.AddWithValue("@IdPlano", idPlano);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            diasPlanoList.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return diasPlanoList;
        }


        //public List<AlunoModels> VisualizarDados()
        //{
        //    List<AlunoModels> alunoList = new List<AlunoModels>();

        //    SqlConnection connection = new SqlConnection(connectionString);
        //    connection.Open();

        //    SqlCommand readerCommand = new SqlCommand($"SELECT * FROM TBAlunos;", connection);

        //    SqlDataReader reader = readerCommand.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        AlunoModels aluno = new AlunoModels()
        //        {
        //            IdAlunos = reader.GetInt32(0),
        //            IdTurma = reader.GetInt32(1),
        //            Nome = reader.GetString(3),
        //            Sobrenome = reader.GetString(4),
        //            EstadoMatricula = reader.GetBoolean(5),
        //            Telefone = reader.GetString(6),
        //            Email = reader.GetString(7),
        //            Rg = reader.GetString(8),
        //            Cpf = reader.GetString(9),
        //            DataNascimento = reader.GetDateTime(10).ToString("dd/MM/yyyy"),
        //            Cep = reader.GetString(11),
        //            Rua = reader.GetString(12),
        //            Bairro = reader.GetString(13),
        //            Cidade = reader.GetString(14),
        //            Estado = reader.GetString(15),
        //            Numero = reader.GetString(16)
        //        };
        //        alunoList.Add(aluno);
        //    }

        //    connection.Close();

        //    return alunoList;
        //}
    }
}