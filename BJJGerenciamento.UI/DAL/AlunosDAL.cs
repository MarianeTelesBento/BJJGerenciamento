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
        public string connectionString = "Data Source=FAC0539673W10-1;Initial Catalog=BJJ_DB;User ID=Sa;Password=123456;";
        //public string connectionString = "Data Source=DESKTOP-FTCVI92\\SQLEXPRESS;Initial Catalog=BJJ_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public int CadastrarDados(string nome, string sobrenome, string telefone, string email, string rg, string cpf, string dataNascimento, string cep, string rua, string bairro, string cidade, string estado, string numero)
        {
            int cadastroRealizado = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand inserirCommand = new SqlCommand($"insert into TBAlunos(IDTurma, Nome, Sobrenome, EstadoMatricula, Telefone, Email, Rg, Cpf, DataNascimento, CEP, Rua, Bairro, Cidade, Estado, Numero) values(2, @nome, @sobrenome, 'True', @telefone, @email, @rg, @cpf, @dataNascimento, @cep, @rua, @bairro, @cidade, @estado, @numero);", connection);

            inserirCommand.Parameters.AddWithValue("@nome", nome);
            inserirCommand.Parameters.AddWithValue("@sobrenome", sobrenome);
            inserirCommand.Parameters.AddWithValue("@telefone", telefone);
            inserirCommand.Parameters.AddWithValue("@email", email);
            inserirCommand.Parameters.AddWithValue("@rg", rg);
            inserirCommand.Parameters.AddWithValue("@cpf", cpf);
            inserirCommand.Parameters.AddWithValue("@dataNascimento", dataNascimento);
            inserirCommand.Parameters.AddWithValue("@cep", cep);
            inserirCommand.Parameters.AddWithValue("@rua", rua);
            inserirCommand.Parameters.AddWithValue("@bairro", bairro);
            inserirCommand.Parameters.AddWithValue("@cidade", cidade);
            inserirCommand.Parameters.AddWithValue("@estado", estado);
            inserirCommand.Parameters.AddWithValue("@numero", numero);

            cadastroRealizado = inserirCommand.ExecuteNonQuery();

            connection.Close();

            return cadastroRealizado;
        }

        public List<AlunoModels> VisualizarDados()
        {
            List<AlunoModels> alunoList = new List<AlunoModels>(); 

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand readerCommand = new SqlCommand($"SELECT * FROM TBAlunos;", connection);

            SqlDataReader reader = readerCommand.ExecuteReader();

            while (reader.Read())
            {
                AlunoModels aluno = new AlunoModels() 
                { 
                    IdAlunos = reader.GetInt32(0), 
                    IdTurma = reader.GetInt32(1),
                    Nome = reader.GetString(3),
                    Sobrenome = reader.GetString(4),
                    EstadoMatricula = reader.GetBoolean(5),
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
                    Numero = reader.GetString(16)
                };
                alunoList.Add(aluno);
            }

            connection.Close();

            return alunoList;
        }

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
                                IdAlunos = reader.GetInt32(0),
                                IdTurma = reader.GetInt32(1),
                                Nome = reader.GetString(3),
                                Sobrenome = reader.GetString(4),
                                EstadoMatricula = reader.GetBoolean(5),
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
                                Numero = reader.GetString(16)
                            };
                        }
                    }
                }
            }

            return aluno;
        }

    }
}