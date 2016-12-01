using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotWeb.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DotWeb.Repositories
{
    public class ToolVerificationRepository
    {
        public SqlConnection Connection;
        public SqlTransaction Transaction;
        public static string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;
        public class ToolVerificationBinding
        {
            public DateTime? LastCalibrationDate { get; set; }
            public DateTime LastVerificationDate { get; set; }
            public string ToolNumber { get; set; }
            public int InventoryNumber { get; set; }

        }
        public static class ParameterSPnEntity
        {
            public const string ToolNumber = "Number";
            public const string ToolId = "Id";
            public const string ToolDescription = "Description";
            public const string ToolInventoryNumber = "InventoryNumber";
            public const string ToolCalDate = "CalDate";
            public const string ToolVerDate = "VerDate";
        }
        public class ToolVerificationSave
        {
            public int IdVerificationResult { get; set; }
            public string NameVerificationResult { get; set; }
            public int IdToolVerification { get; set; }
            public DateTime VerDate { get; set; }
            public string CalNumber { get; set; }
            public int ToolSetupId { get; set; }
            public int ToolSetupInv { get; set; }
            public int SetNM { get; set; }
            public int MinNM { get; set; }
            public int MaxNM { get; set; }
            public int Verification1 { get; set; }
            public int Verification2 { get; set; }
            public int Verification3 { get; set; }
            public int ResultId { get; set; }   
            public DateTime NextVerificationDate { get; set; }  
            public bool StatusSave {get; set;}
        }
        public static List<ToolVerificationBinding> RetrieveDataToolVerification(string strSP,string id, string invNumber)
        {
            List<ToolVerificationBinding> list = new List<ToolVerificationBinding>();
            try
            {
                IDataReader datareader = null;
                int idValue = int.Parse(id);
                int invNumberValue = int.Parse(invNumber);
                //string consString = ConfigurationManager.ConnectionStrings["DotWebDb"].ConnectionString;
                //using (AppDb context = new AppDb())
                //{
                    
                //  ////list = (from a in context.ToolLists
                //  ////        join b in context.ToolSetups on a.ToolSetupId equals b.Id into leftA from b in leftA.DefaultIfEmpty()
                //  ////        join e in context.ToolVerifications on a.ToolSetupId equals e.ToolSetupId into leftC
                //  ////        from e in leftC.DefaultIfEmpty()
                //  ////        join d in context.ToolCalibrations on b.Id equals d.ToolSetupID into leftB
                //  ////        from d in leftB.DefaultIfEmpty()
                //    list=(from a in context.ToolLists from b in context.ToolSetups .Where(mapping => mapping.Id == a.ToolSetupId).DefaultIfEmpty()
                //          from c in context.ToolVerifications .Where(mapping => mapping.ToolSetupId == a.ToolSetupId).DefaultIfEmpty()
                //          from d in context.ToolCalibrations .Where(mapping => mapping.ToolSetupId == a.ToolSetupId).DefaultIfEmpty()
                //          where a.ToolSetupId == idValue && a.InventoryNumber == invNumberValue
                //          select new ToolVerificationBinding
                //          {
                //              LastCalibrationDate = d.CalDate,
                //              LastVerificationDate = c.VerDate,
                //              ToolNumber = b.Number,
                //              InventoryNumber = a.InventoryNumber

                //          }).ToList();
                //}
                SqlConnection con = new SqlConnection(ConString);
                SqlCommand cmd = new SqlCommand(strSP, con);
                AddInParameter(cmd, "@ToolId", id);
                AddInParameter(cmd, "@InventoryNumber", invNumber);
                try
                {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                datareader = cmd.ExecuteReader();
                list = MapDataToList(datareader);
                //SqlDataAdapter daSQl = new SqlDataAdapter(cmd);
                //daSQl
                //daSQl.Dispose();
                
                }
                catch (Exception)
                {
                    
                    throw;
                }
                finally
                {
                    CloseConnection(con);
                    CloseDataReader(datareader);
                }
            }
            catch (Exception ex)
            {
                    
               // lblMsgError.Text = ex.Message;
            }
           return list;
        }
        protected static void CloseDataReader(IDataReader dataReader)
        {
            if (dataReader == null)
                return;
            dataReader.Close();
            dataReader.Dispose();
        }
        public static void CloseConnection(SqlConnection connection)
        {
            if (connection == null)
                return;

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Dispose();
        }
        private static List<ToolVerificationBinding> MapDataToList(IDataReader reader)
        {
            List<ToolVerificationBinding> MapDataToList = new List<ToolVerificationBinding>();
            while (reader.Read())
            {
                ToolVerificationBinding param = new ToolVerificationBinding();
                param.ToolNumber = GetStringData(reader, ParameterSPnEntity.ToolNumber);
                param.LastVerificationDate = GetStringData(reader, ParameterSPnEntity.ToolVerDate)  == null ? DateTime.Now : DateTime.Parse(GetStringData(reader, ParameterSPnEntity.ToolVerDate));
                param.LastCalibrationDate = GetStringData(reader, ParameterSPnEntity.ToolCalDate) == null ? DateTime.Now : DateTime.Parse(GetStringData(reader, ParameterSPnEntity.ToolCalDate));
                param.InventoryNumber = GetStringData(reader, ParameterSPnEntity.ToolInventoryNumber) == null ? 0 : int.Parse(GetStringData(reader, ParameterSPnEntity.ToolInventoryNumber));
                MapDataToList.Add(param);
            }
            return MapDataToList;
        }
        protected static string GetStringData(IDataReader dataReader, string columnName)
        {
            string result = null;
            if (dataReader[columnName] != System.DBNull.Value)
            {
                result = dataReader[columnName].ToString();
            }

            return result;
        }
        protected static void AddInParameter(SqlCommand command, string name, object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            command.Parameters.Add(parameter);
        }
        public static void SaveDataToolVerificationMark(string strSP,string VerDate, string CalNumber, string ToolSetupId, string ToolSetupInv, string SetNM, string MinNM, string MaxNM
            , string Verification1, string Verification2, string Verification3, string ResultId, string NextVerificationDate, string InvNumber, string StatusSave)
        {
            //int result = 0;
            //SqlConnection con = new SqlConnection(ConString);
            //SqlCommand cmd = new SqlCommand(strSP, con);
            //con.Open();
            
            try
            {
              //  //AddInParameter(cmd, "@VerDate", DateTime.Parse(VerDate));
              //  AddInParameter(cmd, "@CalNumber", CalNumber);
              //  AddInParameter(cmd, "@ToolSetupId", ToolSetupId);
              //  AddInParameter(cmd, "@ToolSetupInv", ToolSetupInv);
              //  AddInParameter(cmd, "@SetNM", SetNM);
              //  AddInParameter(cmd, "@MinNM", MinNM);
              //  AddInParameter(cmd, "@MaxNM", MaxNM);
              //  AddInParameter(cmd, "@Verification1", Verification1);
              //  AddInParameter(cmd, "@Verification2", Verification2);
              //  AddInParameter(cmd, "@Verification3", Verification3);
              //  AddInParameter(cmd, "@ResultId", ResultId);
              ////  AddInParameter(cmd, "@NextVerificationDate", NextVerificationDate);
              //  result = cmd.ExecuteNonQuery();
              //  cmd.Parameters.Clear();
                using (AppDb context = new AppDb())
                {
                    ToolVerification data = new ToolVerification();
                    {
                        //data.Id = 0;
                        data.VerDate = DateTime.Parse(VerDate);
                        data.MaxNM = decimal.Parse(MaxNM);
                        //data.Tool =
                        data.CalNumber = "1";
                        data.ToolSetupId = int.Parse(ToolSetupId);
                        data.ToolSetupInv = int.Parse(ToolSetupInv);
                        data.SetNM = decimal.Parse(SetNM) == null ? 0 : decimal.Parse(SetNM);
                        data.MinNM = decimal.Parse(MinNM) == null ? 0 : decimal.Parse(MinNM);
                        data.MaxNM = decimal.Parse(MaxNM) == null ? 0 : decimal.Parse(MaxNM);
                        data.Verification1 = decimal.Parse(Verification1) == null ? 0 : decimal.Parse(Verification1);
                        data.Verification2 = decimal.Parse(Verification2) == null ? 0 : decimal.Parse(Verification2);
                        data.Verification3 = decimal.Parse(Verification3) == null ? 0 : decimal.Parse(Verification3);
                        data.ResultId = false;
                        data.NextVerificationDate = DateTime.Now;
                        context.ToolVerifications.Add(data);
                        context.SaveChanges();
                    }

                    int convertIntInvNumber = int.Parse(InvNumber);
                    int convertIntToolSetupId = int.Parse(ToolSetupId);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            //con.Close();

        }

        public static void SaveDataToolVerification(string VerDate, string CalNumber, string ToolSetupId, string ToolSetupInv, string SetNM, string MinNM, string MaxNM
                    , string Verification1, string Verification2, string Verification3, string ResultId, string NextVerificationDate, string InvNumber, string StatusSave)
        {
            try
            {
                using (AppDb context = new AppDb())
                {
                    ToolVerification data = new ToolVerification();
                    {
                        data.VerDate = DateTime.Parse(VerDate);
                        data.MaxNM = decimal.Parse(MaxNM);
                        data.CalNumber = "1";
                        data.ToolSetupId = 1;
                        data.ToolSetupInv = int.Parse(ToolSetupInv);
                        data.SetNM = decimal.Parse(SetNM);
                        data.MinNM = decimal.Parse(MinNM);
                        data.MaxNM = decimal.Parse(MaxNM);
                        data.Verification1 = decimal.Parse(Verification1);
                        data.Verification2 = decimal.Parse(Verification2);
                        data.Verification3 = decimal.Parse(Verification3);
                        data.ResultId = true;
                        data.NextVerificationDate = DateTime.Now;
                        context.ToolVerifications.Add(data);
                        context.SaveChanges();
                    }

                    int convertIntInvNumber = int.Parse(InvNumber);
                    int convertIntToolSetupId = int.Parse(ToolSetupId);
                    //var ToolListData = (from a in context.ToolLists where a.InventoryNumber == convertIntInvNumber && a.Id == convertIntToolSetupId select a).SingleOrDefault();

                    //ToolVerificationSave dataToolSetup = new ToolVerificationSave();
                    //{
                    //    ToolListData.Status = true;
                    //    context.SaveChanges();
                    //}
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }

}
