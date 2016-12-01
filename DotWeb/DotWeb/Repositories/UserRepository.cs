using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DotWeb.Models;

namespace DotWeb.Repositories
{
    public class UserRepository
    {
        public static int RetrieveUserIdByUserName(string userName)
        {
            using (DotWebDb context = new DotWebDb())
            {
                var user = context.Users.FirstOrDefault(p => p.UserName == userName);
                if (user != null)
                    return user.UserId;
            }
            return 0;
        }

        public static int RetrieveOrganizationByName(string organizationName)
        {
            using (DotWebDb context = new DotWebDb())
            {
                var organization = context.Organizations.FirstOrDefault(o => o.Name == organizationName);
                if (organization != null)
                    return organization.Id;
            }
            return 0;
        }

        public static List<User> RetrieveUsersByGroupId(string groupId)
        {
            using (DotWebDb context = new DotWebDb())
            {
                int[] userIdArray;
                string sql = "SELECT DISTINCT UserId FROM UserGroupMembers WHERE GroupId = @GroupId";
                using (SqlConnection con = new SqlConnection(context.Database.Connection.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@GroupId", groupId);
                        cmd.CommandTimeout = 7000;

                        DataSet dsData = new DataSet();
                        new SqlDataAdapter(cmd).Fill(dsData);

                        userIdArray = new int[dsData.Tables[0].Rows.Count];
                        if (dsData.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                            {
                                userIdArray[i] = Convert.ToInt32(dsData.Tables[0].Rows[i][0]);
                            }
                        }
                    }
                }

                List<User> lUsers = (from a in context.Users
                                     where userIdArray.Contains(a.UserId)
                                     select a).ToList();

                return lUsers;
            }
        }

        public static List<User> getUserName()
        {
            using (DotWebDb context = new DotWebDb())
            {
                return context.Users.ToList();
            }
        }

        public static string RetrieveUserGroupNameById(int groupId)
        {
            using (DotWebDb context = new DotWebDb())
            {
                UserGroup userGroup = context.UserGroups.FirstOrDefault(p => p.GroupId == groupId);
                if (userGroup != null)
                    return userGroup.GroupName;
            }
            return "";
        }

        public static string RetrieveUserNameById(int userId)
        {
            using (DotWebDb context = new DotWebDb())
            {
                User user = context.Users.FirstOrDefault(p => p.UserId == userId);
                if (user != null)
                    return user.FullName;
            }
            return "";
        }

        public static int? RetrieveOrganizationIdByUserName(string userName)
        {
            using (DotWebDb context = new DotWebDb())
            {
                var user = context.Users.FirstOrDefault(p => p.UserName == userName);
                if (user != null)
                    return user.OrganizationId;
            }
            return 0;
        }
    }
}
