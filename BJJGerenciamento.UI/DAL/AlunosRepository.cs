using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BJJGerenciamento.UI.DAL
{
    public class AlunosRepository
    {
        public string connectionString = "Data Source=FAC0539673W10-1;Initial Catalog=BJJ_DB;User ID=Sa;Password=123456;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public int CadastrarDados(string d1, string d2, string d3, string d4, string d5, string d6, string d7, string d8, string d9, string d10, string d11, string d12)
        {
            int cadastroRealizado = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand inserirCommand = new SqlCommand($"insert into Alunos values(@d1, @d2, @d3, @d4, @d5, @d6, @d7, @d8, @d9, @d10, @d11, @d12); SELECT SCOPE_IDENTITY();", connection);

            inserirCommand.Parameters.AddWithValue("@d1", d1);
            inserirCommand.Parameters.AddWithValue("@d2", d2);
            inserirCommand.Parameters.AddWithValue("@d3", d3);
            inserirCommand.Parameters.AddWithValue("@d4", d4);

            cadastroRealizado = Convert.ToInt32(inserirCommand.ExecuteScalar());

            connection.Close();

            return cadastroRealizado;
        }
    }
}