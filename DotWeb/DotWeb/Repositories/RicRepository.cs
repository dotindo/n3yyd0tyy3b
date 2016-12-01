using System;
using System.Data;
using System.Data.SqlClient;
using DotWeb.Models;

namespace DotWeb.Repositories
{
    public class RicRepository
    {
        public static int GetRicId(int packingmth, int model, int variant)
        {
            AppDb context = new AppDb();

            SqlConnection sqlConnection1 = new SqlConnection(context.Database.Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            //cmd.CommandText = "select r.Id from RecordImplemControl r where r.PackingMonthId = " + packingmth + " AND r.ModelId =" + model + " AND r.VarianId =" + variant;
            cmd.CommandText = "select r.Id from RecordImplemControl r where r.ModelId =" + model + " AND r.VarianId =" + variant;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();
            int id = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id = int.Parse(reader["Id"].ToString());
                }
            }
            reader.Close();
            sqlConnection1.Close();

            return id;
        }
    }
}
