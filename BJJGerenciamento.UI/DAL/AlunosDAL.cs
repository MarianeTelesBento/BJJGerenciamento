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

        public int CadastrarDados(string d1, string d2, string d3, string d4, string d5, string d6, string d7, string d8, string d9, string d10, string d11, string d12)
        {
            int cadastroRealizado = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand inserirCommand = new SqlCommand($"insert into TBAlunos(IDTurma, Matricula, Nome, Sobrenome, EstadoMatricula, Telefone, Email, Rg, Cpf, DataNascimento, CEP, Endereco, Bairro, Numero) values(2, @d1, @d2, @d3, 'True', @d4, @d5, @d6, @d7, @d8, @d9, @d10, @d11, @d12); SELECT SCOPE_IDENTITY();", connection);

            inserirCommand.Parameters.AddWithValue("@d1", d1);
            inserirCommand.Parameters.AddWithValue("@d2", d2);
            inserirCommand.Parameters.AddWithValue("@d3", d3);
            inserirCommand.Parameters.AddWithValue("@d4", d4);
            inserirCommand.Parameters.AddWithValue("@d5", d5);
            inserirCommand.Parameters.AddWithValue("@d6", d6);
            inserirCommand.Parameters.AddWithValue("@d7", d7);
            inserirCommand.Parameters.AddWithValue("@d8", d8);
            inserirCommand.Parameters.AddWithValue("@d9", d9);
            inserirCommand.Parameters.AddWithValue("@d10", d10);
            inserirCommand.Parameters.AddWithValue("@d11", d11);
            inserirCommand.Parameters.AddWithValue("@d12", d12);

            cadastroRealizado = Convert.ToInt32(inserirCommand.ExecuteScalar());

            connection.Close();

            return cadastroRealizado;
        }

        public List<AlunoModels> VisualizarDados()
        {
            List<AlunoModels> alunoList = new List<AlunoModels>(); 

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand inserirCommand = new SqlCommand($"SELECT * FROM TBAlunos;", connection);

            SqlDataReader reader = inserirCommand.ExecuteReader();

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
                    Rg = reader.GetString(7),
                    Cpf = reader.GetString(8),
                    DataNascimento = reader.GetDateTime(9),
                    Cep = reader.GetString(10),
                    Endereco = reader.GetString(11),
                    Bairro = reader.GetString(12),
                    Numero = reader.GetString(13),
                    Cidade = reader.GetString(14)
                };
                alunoList.Add(aluno);
            }

            connection.Close();

            return alunoList;
        }
    }
}