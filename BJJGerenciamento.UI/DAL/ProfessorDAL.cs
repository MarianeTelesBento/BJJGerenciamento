using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using BJJGerenciamento.UI.Models;

namespace BJJGerenciamento.UI.DAL
{
    public class ProfessorDAL
    {
        public string connectionString = "Data Source=FAC00DT68ZW11-1;Initial Catalog=BJJ_DB;User ID=Sa;Password=123456;";

        public ProfessorModels Professor { get; set; }

        public ProfessorDAL(ProfessorModels professor)
        {
            Professor = professor;
        }

        public ProfessorDAL()
        {

        }

        public int CadastrarProfessor()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TBProfessores (Nome, Sobrenome, DataNasc, Cpf, Telefone, Email, CEP, Rua, Bairro, CarteiraFPJJ, CarteiraCBJJ, Numero, Complemento, Cidade, Estado) " +
               "VALUES (@Nome, @Sobrenome, @DataNasc, @Cpf, @Telefone, @Email, @CEP, @Rua, @Bairro, @CarteiraFPJJ, @CarteiraCBJJ, @Numero, @Complemento, @Cidade, @Estado)";


                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = Professor.Nome;
                command.Parameters.Add("@Sobrenome", SqlDbType.VarChar, 50).Value = Professor.Sobrenome;
                command.Parameters.Add("@DataNasc", SqlDbType.Date).Value = Professor.DataNasc;
                command.Parameters.Add("@Cpf", SqlDbType.VarChar, 14).Value = Professor.Cpf;
                command.Parameters.Add("@Telefone", SqlDbType.VarChar, 15).Value = Professor.Telefone;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = Professor.Email;
                command.Parameters.Add("@CEP", SqlDbType.VarChar, 9).Value = Professor.CEP ?? (object)DBNull.Value;
                command.Parameters.Add("@Rua", SqlDbType.VarChar, 100).Value = Professor.Rua;
                command.Parameters.Add("@Bairro", SqlDbType.VarChar, 50).Value = Professor.Bairro;
                command.Parameters.Add("@CarteiraFPJJ", SqlDbType.VarChar, 20).Value = Professor.CarteiraFPJJ ?? (object)DBNull.Value; 
                command.Parameters.Add("@CarteiraCBJJ", SqlDbType.VarChar, 20).Value = Professor.CarteiraCBJJ ?? (object)DBNull.Value; 
                command.Parameters.Add("@Numero", SqlDbType.VarChar, 10).Value = Professor.Numero;
                command.Parameters.Add("@Complemento", SqlDbType.VarChar, 100).Value = Professor.Complemento ?? (object)DBNull.Value;
                command.Parameters.Add("@Cidade", SqlDbType.VarChar, 100).Value = Professor.Cidade; 
                command.Parameters.Add("@Estado", SqlDbType.VarChar, 100).Value = Professor.Estado;


                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public List<ProfessorModels> ListarProfessores()
        {
            List<ProfessorModels> listaProfessores = new List<ProfessorModels>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM TBProfessores";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProfessorModels professor = new ProfessorModels
                    {
                        IdProfessor = Convert.ToInt32(reader["IdProfessor"]),
                        Nome = reader["Nome"].ToString(),
                        Sobrenome = reader["Sobrenome"].ToString(),
                        DataNasc = Convert.ToDateTime(reader["DataNasc"]),
                        Cpf = reader["Cpf"].ToString(),
                        Telefone = reader["Telefone"].ToString(),
                        Email = reader["Email"].ToString(),
                        CEP = reader["CEP"].ToString(),
                        Rua = reader["Rua"].ToString(),
                        Bairro = reader["Bairro"].ToString(),
                        CarteiraFPJJ = reader["CarteiraFPJJ"]?.ToString(),
                        CarteiraCBJJ = reader["CarteiraCBJJ"]?.ToString(),
                        Numero = reader["Numero"].ToString(),  
                        Complemento = reader["Complemento"]?.ToString(),  
                        Cidade = reader["Cidade"].ToString(),
                        Estado = reader["Estado"].ToString(),
                        Ativo = Convert.ToBoolean(reader["Ativo"])
                    };

                    listaProfessores.Add(professor);
                }
            }
            return listaProfessores;
        }

        public void AtualizarProfessor(ProfessorModels professor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE TBProfessores SET " +
                               "Nome = @Nome, Sobrenome = @Sobrenome, DataNasc = @DataNasc, " +
                               "Cpf = @Cpf, Telefone = @Telefone, Email = @Email, CEP = @CEP, " +
                               "Rua = @Rua, Bairro = @Bairro, CarteiraFPJJ = @CarteiraFPJJ, CarteiraCBJJ = @CarteiraCBJJ, " +
                               "Numero = @Numero, Complemento = @Complemento, Cidade = @Cidade, Estado = @Estado, " +
                               "Ativo = @Ativo " +
                               "WHERE IdProfessor = @IdProfessor";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = professor.Nome;
                command.Parameters.Add("@Sobrenome", SqlDbType.VarChar, 50).Value = professor.Sobrenome;
                command.Parameters.Add("@DataNasc", SqlDbType.Date).Value = professor.DataNasc;
                command.Parameters.Add("@Cpf", SqlDbType.VarChar, 14).Value = professor.Cpf;
                command.Parameters.Add("@Telefone", SqlDbType.VarChar, 15).Value = professor.Telefone;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = professor.Email;
                command.Parameters.Add("@CEP", SqlDbType.VarChar, 9).Value = professor.CEP ?? (object)DBNull.Value;
                command.Parameters.Add("@Rua", SqlDbType.VarChar, 100).Value = professor.Rua;
                command.Parameters.Add("@Bairro", SqlDbType.VarChar, 50).Value = professor.Bairro;
                command.Parameters.Add("@CarteiraFPJJ", SqlDbType.VarChar, 20).Value = professor.CarteiraFPJJ ?? (object)DBNull.Value;
                command.Parameters.Add("@CarteiraCBJJ", SqlDbType.VarChar, 20).Value = professor.CarteiraCBJJ ?? (object)DBNull.Value;
                command.Parameters.Add("@Numero", SqlDbType.VarChar, 10).Value = professor.Numero;
                command.Parameters.Add("@Complemento", SqlDbType.VarChar, 100).Value = professor.Complemento ?? (object)DBNull.Value;
                command.Parameters.Add("@Cidade", SqlDbType.VarChar, 100).Value = professor.Cidade;
                command.Parameters.Add("@Estado", SqlDbType.VarChar, 100).Value = professor.Estado;
                command.Parameters.Add("@IdProfessor", SqlDbType.Int).Value = professor.IdProfessor;
                command.Parameters.Add("@Ativo", SqlDbType.Bit).Value = professor.Ativo;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool ObterStatusAtualProfessor(int idProfessor)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Ativo FROM TBProfessores WHERE IdProfessor = @IdProfessor";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdProfessor", idProfessor);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null && Convert.ToBoolean(result);
            }
        }

        public static bool CpfJaCadastrado(string cpf)
        {
            using (SqlConnection connection = new SqlConnection( new ProfessorDAL().connectionString)) // sua string de conexão
            using (SqlConnection connection = new SqlConnection(new ProfessorDAL().connectionString)) // sua string de conexão
            {
                string query = "SELECT COUNT(*) FROM TBProfessores WHERE Cpf = @Cpf"; // Consulta para contar registros com o CPF informado
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Cpf", cpf); // Adiciona o parâmetro para evitar SQL Injection

                connection.Open();

                int count = Convert.ToInt32(command.ExecuteScalar()); // Executa o SELECT e obtém o valor de contagem

                return count > 0; // Se count > 0, significa que o CPF já está cadastrado
            }
        }


    }
}
