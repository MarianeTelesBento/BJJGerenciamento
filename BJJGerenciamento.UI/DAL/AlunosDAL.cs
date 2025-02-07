using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BJJGerenciamento.UI.Models;

namespace BJJGerenciamento.UI.DAL
{
    public class AlunosDAL
    {
        public string connectionString = "Data Source=FAC0539673W10-1;Initial Catalog=BJJ_DB;User ID=Sa;Password=123456;";

        public int CadastrarDados(string matricula, string nome, string sobrenome, string telefone, string email, string rg, string cpf, string dataNascimento, string cep, string endereco, string bairro, string numero)
        {
            int cadastroRealizado = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand inserirCommand = new SqlCommand($"insert into TBAlunos(IDTurma, Matricula, Nome, Sobrenome, EstadoMatricula, Telefone, Email, Rg, Cpf, DataNascimento, CEP, Endereco, Bairro, Numero) values(2, @matricula, @nome, @sobrenome, 'True', @telefone, @email, @rg, @cpf, @dataNascimento, @cep, @endereco, @bairro, @numero);", connection);

            inserirCommand.Parameters.AddWithValue("@matricula", matricula);
            inserirCommand.Parameters.AddWithValue("@nome", nome);
            inserirCommand.Parameters.AddWithValue("@sobrenome", sobrenome);
            inserirCommand.Parameters.AddWithValue("@telefone", telefone);
            inserirCommand.Parameters.AddWithValue("@email", email);
            inserirCommand.Parameters.AddWithValue("@rg", rg);
            inserirCommand.Parameters.AddWithValue("@cpf", cpf);
            inserirCommand.Parameters.AddWithValue("@dataNascimento", dataNascimento);
            inserirCommand.Parameters.AddWithValue("@cep", cep);
            inserirCommand.Parameters.AddWithValue("@endereco", endereco);
            inserirCommand.Parameters.AddWithValue("@bairro", bairro);
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
                    Matricula = reader.GetString(2),
                    Nome = reader.GetString(3),
                    Sobrenome = reader.GetString(4),
                    EstadoMatricula = reader.GetBoolean(5),
                    Telefone = reader.GetString(6),
                    Email = reader.GetString(7),
                    Rg = reader.GetString(8),
                    Cpf = reader.GetString(9),
                    DataNascimento = reader.GetDateTime(10),
                    Cep = reader.GetString(11),
                    Endereco = reader.GetString(12),
                    Bairro = reader.GetString(13),
                    Numero = reader.GetString(14)
                };
                alunoList.Add(aluno);
            }

            connection.Close();

            return alunoList;
        }
    }
}