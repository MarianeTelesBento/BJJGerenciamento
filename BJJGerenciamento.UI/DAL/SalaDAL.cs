using BJJGerenciamento.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BJJGerenciamento.UI.DAL
{
    public class SalaDAL
    {
        private string connectionString = "Data Source=FAC00DT68ZW11-1;Initial Catalog=BJJ_DB;User ID=Sa;Password=123456;";

        // Retorna o próximo número de sala com base no maior número existente
        public int ObterProximoNumeroSala()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ISNULL(MAX(NumeroSala), 0) FROM TBSalas";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                int ultimoNumero = (int)cmd.ExecuteScalar();
                return ultimoNumero + 1;
            }
        }

        // Adiciona uma nova sala e já define como ativa
        public void AdicionarSala(int numeroSala)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TBSalas (NumeroSala, Ativa) VALUES (@NumeroSala, 1)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NumeroSala", numeroSala);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Lista todas as salas, ativas e inativas
        public List<SalaModel> ObterSalas()
        {
            List<SalaModel> salas = new List<SalaModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT IdSala, NumeroSala, Ativa FROM TBSalas";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SalaModel sala = new SalaModel
                    {
                        IdSala = Convert.ToInt32(reader["IdSala"]),
                        NumeroSala = Convert.ToInt32(reader["NumeroSala"]),
                        Ativa = Convert.ToBoolean(reader["Ativa"])
                    };
                    salas.Add(sala);
                }
            }

            return salas;
        }

        // Define o status da sala (true = ativa, false = inativa)
        public void DefinirStatusSala(int idSala, bool status)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE TBSalas SET Ativa = @Ativa WHERE IdSala = @IdSala";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Ativa", status);
                cmd.Parameters.AddWithValue("@IdSala", idSala);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ExcluirSala(int idSala)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM TBSalas WHERE IdSala = @IdSala";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdSala", idSala);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
