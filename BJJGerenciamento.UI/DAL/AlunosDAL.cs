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

        public int CadastrarDados(AlunoModels aluno)
        {
            int cadastroRealizado;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand inserirCommand = new SqlCommand("insert into TBAlunos(IdTurma, IdMatricula, Nome, Sobrenome, Telefone, Email, Rg, Cpf, DataNascimento, CEP, Rua, Bairro, Cidade, Estado, NumeroCasa, CarteiraFPJJ) values(@idTurma, @idMatricula, @nome, @sobrenome, @telefone, @email, @rg, @cpf, @dataNascimento, @cep, @rua, @bairro, @cidade, @estado, @numeroCasa, @carteiraFPJJ);", connection);

            inserirCommand.Parameters.AddWithValue("@idTurma", aluno.IdTurma);
            inserirCommand.Parameters.AddWithValue("@idMatricula", aluno.IdMatricula);
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

            cadastroRealizado = inserirCommand.ExecuteNonQuery();

            connection.Close();

            return cadastroRealizado;
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

        public AlunoModels BuscarCpfAluno(string cpf)
        {
            AlunoModels aluno = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM TBAlunos WHERE Cpf = @cpf;", connection))
                {
                    command.Parameters.AddWithValue("@cpf", cpf);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            aluno = new AlunoModels
                            {
                                IdAluno = reader.GetInt32(0),
                                IdTurma = reader.GetInt32(1),
                                IdMatricula = reader.GetInt32(2),
                                Nome = reader.GetString(4),
                                Sobrenome = reader.GetString(5),
                                Telefone = reader.GetString(6),
                                Email = reader.GetString(7),
                                Rg = reader.GetString(8),
                                Cpf = reader.GetString(9),
                                DataNascimento = reader.GetDateTime(10).ToString("dd/MM/yyyy"),
                                Cep = reader.GetString(11),
                                Rua = reader.GetString(12),
                                Bairro = reader.GetString(13),
                                Cidade = reader.GetString(14),
                                Estado = reader.GetString(15),
                                NumeroCasa = reader.GetString(16),
                                CarteiraFPJJ = reader.GetString(17)
                            };
                        }
                    }
                }
            }

            return aluno;
        }
    }
}