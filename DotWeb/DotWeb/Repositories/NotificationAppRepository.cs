using System;
using System.Collections.Generic;
using System.Linq;
using DotWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace DotWeb.Repositories
{
    public class NotificationAppRepository
    {
        public static bool SaveForNotification(string userName, string message, string link, string organization)
        {

            bool exe = false;
            DateTime date = DateTime.Now;
            int userId = UserRepository.RetrieveUserIdByUserName(userName);
            int organizationId = UserRepository.RetrieveOrganizationByName(organization);

            try
            {
                if (string.IsNullOrEmpty(message))
                    return exe;

                using (AppDb context = new AppDb())
                {
                    #region stored procedure
                    string consString = context.Database.Connection.ConnectionString;
                    using (SqlConnection con = new SqlConnection(consString))
                    {
                        using (SqlCommand cmd = new SqlCommand("usp_NotificationApp_SaveItem", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                            cmd.Parameters.Add("@Message", SqlDbType.NVarChar).Value = message;
                            cmd.Parameters.Add("@Link", SqlDbType.NVarChar).Value = link;
                            cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = date;
                            cmd.Parameters.Add("@OrganizationId", SqlDbType.Int).Value = organizationId;
                            SqlParameter returnParameter = cmd.Parameters.Add("@Status_Save", SqlDbType.Int);
                            returnParameter.Direction = ParameterDirection.ReturnValue;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            int res = (int)returnParameter.Value;
                            con.Close();
                            if (res != 1)
                            {
                                exe = false;
                            }
                            else
                                exe = true;
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return exe;
        }
        
        public static List<NotificationApp> getNotification(string userName)
        {
            //DataTable Data = new DataTable();
            int userId = UserRepository.RetrieveUserIdByUserName(userName);

            List<NotificationApp> data = new List<NotificationApp>();
            using (AppDb context = new AppDb())
            {
                SqlParameter userID = new SqlParameter("@UserID", userId); 
                data = context.Database.SqlQuery<NotificationApp>("exec [dbo].[usp_NotificationAppGetItem] @UserId", userID)
                    .ToList<NotificationApp>();
            }
            return data;
        }
    }
}
