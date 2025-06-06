using BJJGerenciamento.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BJJGerenciamento.UI.DAL
{
	public class HorarioDAL
	{
        public string connectionString = "Data Source=rsm-dev-works-server.database.windows.net;Initial Catalog=BJJ_DB;User ID=rsm-dev;Password=adm1234@;";
        public void Inserir(HoraModels hora)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO TBHora (HorarioInicio, HorarioFim, Ativa) VALUES (@HorarioInicio, @HorarioFim, 1)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HorarioInicio", hora.HorarioInicio);
                cmd.Parameters.AddWithValue("@HorarioFim", hora.HorarioFim);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<HoraModels> ListarTodos()
        {
            List<HoraModels> lista = new List<HoraModels>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT IdHora, HorarioInicio, HorarioFim, Ativa FROM TBHora ORDER BY HorarioInicio";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new HoraModels
                    {
                        IdHora = Convert.ToInt32(dr["IdHora"]),
                        HorarioInicio = (TimeSpan)dr["HorarioInicio"],
                        HorarioFim = (TimeSpan)dr["HorarioFim"],
                        Ativa = Convert.ToBoolean(dr["Ativa"])
                    });
                }
            }
            return lista;
        }
        public void AlterarStatus(int idHora, bool ativa)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE TBHora SET Ativa = @Ativa WHERE IdHora = @IdHora";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ativa", ativa);
                cmd.Parameters.AddWithValue("@IdHora", idHora);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Atualizar(HoraModels hora)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE TBHora SET HorarioInicio = @HorarioInicio, HorarioFim = @HorarioFim WHERE IdHora = @IdHora";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HorarioInicio", hora.HorarioInicio);
                cmd.Parameters.AddWithValue("@HorarioFim", hora.HorarioFim);
                cmd.Parameters.AddWithValue("@IdHora", hora.IdHora);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}