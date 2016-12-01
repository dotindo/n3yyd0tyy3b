using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DotWeb.Models;
using DotWeb.Utils;

namespace DotWeb.Repositories
{
    public class CgisFilterRepository
    {
        public static CGISFilter RetrieveCgisFilterById(int id)
        {
            using (AppDb context = new AppDb())
            {
                return context.CGISFilters.FirstOrDefault(p => p.Id == id);
            }
        }

        public static bool ProcessSyncData(string packingMonth, int modelId)
        {
            using (AppDb context = new AppDb())
            {
                DateTime dtPackingMonth = Convert.ToDateTime(packingMonth);

                Model oModel = context.Models.FirstOrDefault(p => p.Id == modelId);

                string asyncConnString = context.Database.Connection.ConnectionString;

                using (SqlConnection conn = new SqlConnection(asyncConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "EXEC usp_GetCGISStagingData @vpm, @model";
                    cmd.CommandTimeout = 7000;
                    cmd.Parameters.AddWithValue("@vpm", dtPackingMonth.ToString("yyyyMM"));
                    cmd.Parameters.AddWithValue("@model", oModel.ModelName);
                    cmd.Connection = conn;

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException se)
                    {
                        AppLogger.LogError(se);
                        return false;
                    }

                    cmd = new SqlCommand();
                    cmd.CommandText = "EXEC usp_ProcessCGISFromStaging @vpm, @model";
                    cmd.CommandTimeout = 7000;
                    cmd.Parameters.AddWithValue("@vpm", dtPackingMonth.ToString("yyyyMM"));
                    cmd.Parameters.AddWithValue("@model", oModel.ModelName);
                    cmd.Connection = conn;

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException se)
                    {
                        AppLogger.LogError(se);
                        return false;
                    }

                    CGISSynchronized csync = new CGISSynchronized();
                    csync.PackingMonth = dtPackingMonth.ToString("yyyyMM");
                    csync.ProcessDate = DateTime.Now;
                    csync.TypeId = oModel.TypeId.GetValueOrDefault();
                    csync.ModelId = modelId;
                    csync.ProcessBy = "Admin";  //TODO:Change to current apps logger
                    context.CGISSynchronizeds.Add(csync);
                    context.SaveChanges();
                }
            }
            return true;
        }
    }
}
