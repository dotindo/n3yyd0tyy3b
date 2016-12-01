using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotWeb.Models;
using DotWeb.Utils;
using NLog;
using System.Data;
using System.Data.Sql;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DotWeb.Repositories
{
    public class IrregAltRepository
    {
        protected bool IsDedicatedConnection = true;
        public SqlConnection Connection;
        public SqlTransaction Transaction;
        public static string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;
        public void Initialize(SqlConnection conn, SqlTransaction trans)
        {
            IsDedicatedConnection = false;
            Connection = conn;
            Transaction = trans;
        }
        public void OpenConnection()
        {
            if (IsDedicatedConnection)
            {
                if (Connection == null)
                {
                    Connection = new SqlConnection(ConString);
                }
                Connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (Connection == null)
                return;

            if (IsDedicatedConnection)
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
                Connection.Dispose();
            }
        }
        public void BeginTransaction()
        {
            if (Transaction == null)
                Transaction = Connection.BeginTransaction();
        }
        public void CommitTransaction()
        {
            if (Transaction != null)
                Transaction.Commit();
        }
        public void RollbackTransaction()
        {
            Transaction.Rollback();
        }
        public enum SaveStatus
        {
            Initial = 0,
            Success = 1,
            Error = 2
        }
        public enum ApproveFlag
        {
            OnGoing = 0,
            InProgress = 1,
            Approve = 2,
        }
        protected static void AddInParameter(SqlCommand command, string name, object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.Value = value == null ? DBNull.Value : value;
            parameter.Direction = ParameterDirection.Input;

            command.Parameters.Add(parameter);
        }
        protected static void AddOutParameter(SqlCommand command, string name, SqlDbType type)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.SqlDbType = type;
            parameter.Direction = ParameterDirection.Output;

            command.Parameters.Add(parameter);
        }
        public string GetTitleHeaderForm(string formCondition)
        {
            string TitleHeader = string.Empty;
            string[] arrTitle = new string[] { "Irregular Alteration - Create", "Irregular Alteration - Edit", "Irregular Alteration - Approval" };
            foreach (string getTitle in arrTitle)
            {
                if (getTitle.Contains(formCondition))
                {
                    TitleHeader = getTitle.ToString();
                }
            }

            return TitleHeader;
        }
        public DataTable dtBindView(string struser, string strSP)
        {
            DataTable dtView = new DataTable();
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(strSP, con);
            AddInParameter(cmd, "@UserName", struser);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter daSQl = new SqlDataAdapter(cmd);
                daSQl.Fill(dtView);
                daSQl.Dispose();
            }
            catch (Exception e)
            {
                AppLogger.LogError(e);
            }
            finally
            {

                con.Close();
                con.Dispose();
            }

            return dtView;
        }
        public static List<IATypes> GetDDLIATypes(string strSP)
        {
            List<IATypes> listIAType = new List<IATypes>();
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(strSP, con);
            SqlDataReader reader = null;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    IATypes iaType = new IATypes();
                    if (reader["IDTypeIA"] != DBNull.Value)
                        iaType.IDTypeIA = (int)reader["IDTypeIA"];
                    if (reader["TypeIA"] != DBNull.Value)
                        iaType.TypeIA = reader["TypeIA"].ToString();
                    listIAType.Add(iaType);
                }

            }
            catch (Exception e)
            {
                AppLogger.LogError(e);
            }
            finally
            {
                reader.Close();
                reader.Dispose();
                con.Close();
                con.Dispose();
            }

            return listIAType;
        }
        public static List<IAModel> GetDDLIAModels(string strSP)
        {
            List<IAModel> listIAModelDDL = new List<IAModel>();
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(strSP, con);
            SqlDataReader reader = null;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    IAModel iaModelDDL = new IAModel();
                    if (reader["Id"] != DBNull.Value)
                        iaModelDDL.IDModel = (int)reader["Id"];
                    if (reader["ModelName"] != DBNull.Value)
                        iaModelDDL.ModelName = reader["ModelName"].ToString();
                    listIAModelDDL.Add(iaModelDDL);
                }

            }
            catch (Exception e)
            {
                AppLogger.LogError(e);
            }
            finally
            {
                reader.Close();
                reader.Dispose();
                con.Close();
                con.Dispose();
            }

            return listIAModelDDL;
        }
        public static List<IAStatusTask> GetDdlStatus()
        {
            List<IAStatusTask> listIAStatustask = new List<IAStatusTask>();
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand("usp_GetStatusApproval", con);
            SqlDataReader reader = null;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    IAStatusTask iaDDLStatustask = new IAStatusTask();
                    if (reader["Id"] != DBNull.Value)
                        iaDDLStatustask.IdStatusTask = (int)reader["Id"];
                    if (reader["StatusTask"] != DBNull.Value)
                        iaDDLStatustask.StatusTask = reader["StatusTask"].ToString();
                    listIAStatustask.Add(iaDDLStatustask);
                }

            }
            catch (Exception e)
            {
                AppLogger.LogError(e);
            }
            finally
            {
                reader.Close();
                reader.Dispose();
                con.Close();
                con.Dispose();
            }

            return listIAStatustask;
        }
        public static List<IATaskApproval> GetDdlUserApproval(string strUserDepart)
        {
            List<IATaskApproval> listIAUserApproval = new List<IATaskApproval>();
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand("usp_GetUserApproval", con);
            SqlDataReader reader = null;
            AddInParameter(cmd, "@UserApproval", strUserDepart);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    IATaskApproval iaDDLUserApproval = new IATaskApproval();
                    if (reader["UserID"] != DBNull.Value)
                        iaDDLUserApproval.IdTaskApproval = (int)reader["UserID"];
                    if (reader["UserName"] != DBNull.Value)
                        iaDDLUserApproval.NameApprovalTask = reader["UserName"].ToString();
                    listIAUserApproval.Add(iaDDLUserApproval);
                }

            }
            catch (Exception e)
            {
                AppLogger.LogError(e);
            }
            finally
            {
                reader.Close();
                reader.Dispose();
                con.Close();
                con.Dispose();
            }

            return listIAUserApproval;
        }
        public static List<IATaskDepartment> GetDdlDepartment()
        {
            List<IATaskDepartment> listIADepartment = new List<IATaskDepartment>();
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand("usp_GetDepartment", con);
            SqlDataReader reader = null;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    IATaskDepartment iaDDLTaskDepartment = new IATaskDepartment();
                    if (reader["Id"] != DBNull.Value)
                        iaDDLTaskDepartment.IdTaskDepartment = (int)reader["Id"];
                    if (reader["Name"] != DBNull.Value)
                        iaDDLTaskDepartment.NameTaskDepartment = reader["Name"].ToString();
                    listIADepartment.Add(iaDDLTaskDepartment);
                }

            }
            catch (Exception e)
            {
                AppLogger.LogError(e);
            }
            finally
            {
                reader.Close();
                reader.Dispose();
                con.Close();
                con.Dispose();
            }

            return listIADepartment;
        }

        public static List<IAUser> GetUserRole(string strConSQL, string strSP, string struser)
        {
            List<IAUser> roleUserLogin = new List<IAUser>();
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(strSP, con);
            SqlDataReader reader = null;
            AddInParameter(cmd, "@UserName", struser);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    IAUser iaUserLogin = new IAUser();
                    if (reader["UserName"] != DBNull.Value)
                        iaUserLogin.userName = reader["UserName"].ToString();
                    if (reader["GroupName"] != DBNull.Value)
                        iaUserLogin.RoleUser = reader["GroupName"].ToString();
                    if (reader["OrgzName"] != DBNull.Value)
                        iaUserLogin.OrgzName = reader["OrgzName"].ToString();
                    roleUserLogin.Add(iaUserLogin);
                }

            }
            catch (Exception e)
            {
                AppLogger.LogError(e);
            }
            finally
            {
                reader.Close();
                reader.Dispose();
                con.Close();
                con.Dispose();
            }

            return roleUserLogin;
        }

        public List<IAHeaders> GetDataIrregAlt(int id, string strSP)
        {
            List<IAHeaders> getData = new List<IAHeaders>();
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(strSP, con);
            SqlDataReader reader = null;
            AddInParameter(cmd, "@id", Convert.ToString(id));
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    IAHeaders irregAlt = new IAHeaders();
                    if (reader["IsDynamicCheck"] != DBNull.Value)
                        irregAlt.DynamicCheck = reader["IsDynamicCheck"] == null ? false : true;
                    if (reader["IAType"] != DBNull.Value)
                        irregAlt.IAType = reader["IAType"].ToString();
                    if (reader["InternalEpcNumber"] != DBNull.Value)
                        irregAlt.InternalEpcNumber = reader["InternalEpcNumber"].ToString();

                    if (reader["InfoNumber"] != DBNull.Value)
                        irregAlt.InfoNumber = reader["InfoNumber"].ToString();
                    if (reader["Description"] != DBNull.Value)
                        irregAlt.Description = reader["Description"].ToString();

                    if (reader["ValidPeriodFrom"] != DBNull.Value)
                        irregAlt.ValidPeriodFrom = Convert.ToDateTime(reader["ValidPeriodFrom"]);
                    if (reader["ValidPeriodTo"] != DBNull.Value)
                        irregAlt.ValidPeriodTo = Convert.ToDateTime(reader["ValidPeriodTo"]);
                    if (reader["ImplementationDate"] != DBNull.Value)
                        irregAlt.ImplementationDate = Convert.ToDateTime(reader["ImplementationDate"]);
                    getData.Add(irregAlt);
                }
            }
            catch (Exception e)
            {
                AppLogger.LogError(e);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return getData;
        }

        public DataTable GetDataTableItem(int idIrregAlt, string strSP)
        {
            DataTable retDataTable = new DataTable();
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(strSP, con);
            AddInParameter(cmd, "@Id", idIrregAlt);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter daSQl = new SqlDataAdapter(cmd);
                daSQl.Fill(retDataTable);
                daSQl.Dispose();
            }
            catch (Exception e)
            {
                AppLogger.LogError(e);
            }
            finally
            {

                con.Close();
                con.Dispose();
            }
            return retDataTable;
        }
        public int GetDataDDLType(string typeName)
        {
            int getDataIdType = 0;
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand("usp_GetDDltypeSelected", con);
            SqlDataReader reader = null;
            AddInParameter(cmd, "@SelectedType", typeName);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    if (reader["IDTypeIA"] != DBNull.Value)
                        getDataIdType = Convert.ToInt32(reader["IDTypeIA"]);

                }
            }
            catch (Exception e)
            {
                AppLogger.LogError(e);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return getDataIdType;
        }

        public int SaveData(string strConSQL, string strSP, IAHeaders header, DataTable dtModel, DataTable dtPart, DataTable dtDepartment, DataTable dtDepartmentDetail)
        {
            int result = (int)SaveStatus.Initial;
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(strSP, con);
            AddInParameter(cmd, "@IAid", header.IAId);
            AddInParameter(cmd, "@IAType", header.IAType);
            AddInParameter(cmd, "@InternalEpcNumber", header.InternalEpcNumber);
            AddInParameter(cmd, "@InfoNumber", header.InfoNumber);
            AddInParameter(cmd, "@InfoFrom", header.InfoFrom);
            AddInParameter(cmd, "@Description", header.Description);
            AddInParameter(cmd, "@ValidPeriodFrom", header.ValidPeriodFrom);
            AddInParameter(cmd, "@ValidPeriodTo", header.ValidPeriodTo);
            AddInParameter(cmd, "@ImplementationDate", header.ImplementationDate == DateTime.MinValue ? null : header.ImplementationDate);
            AddInParameter(cmd, "@DynamicCheck", header.DynamicCheck);
            AddInParameter(cmd, "@AuthorUserId", header.AuthorUserId);
            AddInParameter(cmd, "@StatusID", header.StatusID);
            AddInParameter(cmd, "@TvpIAModel", dtModel);
            AddInParameter(cmd, "@TvpIAPart", dtPart);
            AddInParameter(cmd, "@TvpIADepartment", dtDepartment);
            AddInParameter(cmd, "@TvpIADepartmentDetail", dtDepartmentDetail);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                result = (int)SaveStatus.Success;
            }
            catch (Exception e)
            {
                result = (int)SaveStatus.Error;
                AppLogger.LogError(e);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return result;
        }
        public static int SaveAttachmentTemp(string strSP, DataTable dtAttachData)
        {
            int result = (int)SaveStatus.Initial;
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(strSP, con);
            AddInParameter(cmd, "@TVPAttachment", dtAttachData);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                result = (int)SaveStatus.Success;
            }
            catch (Exception e)
            {
                result = (int)SaveStatus.Error;
                AppLogger.LogError(e);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return result;
        }
        public int ApproveData(string strUserName, int IdIrregAlt, string statusFlag)
        {
            int result = (int)SaveStatus.Initial;
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand("usp_UpdateFlgApproval", con);
            AddInParameter(cmd, "@iDIrregAlt", IdIrregAlt);
            AddInParameter(cmd, "@UsernName", strUserName);
            AddInParameter(cmd, "@FlagApproval", ApproveFlag.Approve);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                result = (int)SaveStatus.Success;
            }
            catch (Exception e)
            {
                result = (int)SaveStatus.Error;
                AppLogger.LogError(e);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return result;
        }


    }
}
