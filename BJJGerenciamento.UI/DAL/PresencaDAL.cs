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
        private readonly string connectionString;

        public PresencaDAL()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BJJGerenciamentoConnectionString"].ConnectionString;
        }

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