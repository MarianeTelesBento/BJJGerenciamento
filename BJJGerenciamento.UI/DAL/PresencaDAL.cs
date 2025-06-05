using BJJGerenciamento.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace BJJGerenciamento.UI.DAL
{
	public class PresencaDAL
	{
        private string connectionString = "Data Source=rsm-dev-works-server.database.windows.net;Initial Catalog=BJJ_DB;User ID=rsm-dev;Password=adm1234@;";

        public int RegistrarPresenca(PresencaModels presencaModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TBPresencas (IdMatricula, IdProfessor, StatusPresenca, IdSala, DataPresenca) " +
               "VALUES (@IdMatricula, @IdProfessor, @StatusPresenca, @IdSala, @dataPresenca)";


                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@IdMatricula", SqlDbType.Int, 50).Value = presencaModel.IdMatricula;
                command.Parameters.Add("@IdProfessor", SqlDbType.Int, 50).Value = presencaModel.IdProfessor;
                command.Parameters.Add("@StatusPresenca", SqlDbType.Bit).Value = presencaModel.StatusPresenca;
                command.Parameters.Add("@IdSala", SqlDbType.Int, 14).Value = presencaModel.IdSala;
                command.Parameters.Add("@dataPresenca", SqlDbType.Date, 8).Value = presencaModel.Data;

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}