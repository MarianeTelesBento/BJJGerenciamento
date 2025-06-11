using BJJGerenciamento.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BJJGerenciamento.UI.DAL
{
    public class LoginDAL
    {

        private readonly string connectionString;

        public LoginDAL()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BJJGerenciamentoConnectionString"].ConnectionString;
        }

        public bool ValidarLogin(string usuario, string senha)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT COUNT(*) FROM TBLogin WHERE Usuario = @usuario AND Senha = @senha";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@senha", senha);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();

                return count > 0;


            }

        }
        public void CadastrarUsuario(string usuario, string email, string senha)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO TBLogin (Usuario, Email, Senha) VALUES (@usuario, @email, @senha)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@senha", senha);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool UsuarioExiste(string usuario, string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT COUNT(*) FROM TBLogin WHERE Usuario = @usuario OR Email = @Email";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        public LoginModels ObterUsuario(string usuario)
        {
            LoginModels user = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT IdLogin, Usuario, Email, Senha FROM TBLogin WHERE Usuario = @usuario";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@usuario", usuario);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new LoginModels
                    {
                        IdLogin = Convert.ToInt32(reader["IdLogin"]),
                        Usuario = reader["Usuario"].ToString(),
                        Email = reader["Email"].ToString(),
                        Senha = reader["Senha"].ToString()
                    };
                }
            }

            return user;
        }

        public bool AtualizarUsuario(int idLogin, string novoEmail, string novaSenha)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "UPDATE TBLogin SET Email = @Email, Senha = @Senha WHERE IdLogin = @IdLogin";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Email", novoEmail);
                cmd.Parameters.AddWithValue("@Senha", novaSenha);
                cmd.Parameters.AddWithValue("@IdLogin", idLogin);

                con.Open();
                int linhasAfetadas = cmd.ExecuteNonQuery();
                return linhasAfetadas > 0;
            }
        }

    }
}