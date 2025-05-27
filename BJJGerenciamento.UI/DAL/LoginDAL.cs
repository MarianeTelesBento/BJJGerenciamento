using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BJJGerenciamento.UI.DAL
{
    public class LoginDAL
    {

        public string connectionString = "Data Source=FAC00DT68ZW11-1;Initial Catalog=BJJ_DB;User ID=Sa;Password=123456;";

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

    }
}