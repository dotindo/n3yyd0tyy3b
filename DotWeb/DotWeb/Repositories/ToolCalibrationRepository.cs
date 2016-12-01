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
    public class ToolCalibrationRepository
    {
        public SqlConnection Connection;
        public SqlTransaction Transaction;
        public static string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;
        public class ToolCalibrationBinding
        {
            public DateTime? LastCalibrationDate { get; set; }
            public DateTime LastVerificationDate { get; set; }
            public string ToolNumber { get; set; }
            public int InventoryNumber { get; set; }
            public decimal Max { get; set; }
            public decimal Min { get; set; }
            public decimal SetNM { get; set; }

        }
        public class ToolCalibrationSave
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
            public bool StatusSave { get; set; }
        }
        public static class ParameterSPnEntity
        {
            public const string ToolNumber = "Number";
            public const string ToolId = "Id";
            public const string ToolDescription = "Description";
            public const string ToolInventoryNumber = "InventoryNumber";
            public const string ToolCalDate = "CalDate";
            public const string ToolVerDate = "VerDate";
            public const string ToolMaxNM = "MaxNM";
            public const string ToolMinNM = "MinNM";
            public const string ToolSetNM = "SetNM";
        }
        protected static void AddInParameter(SqlCommand command, string name, object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            command.Parameters.Add(parameter);
        }
        public static IList<ToolCalibrationBinding> RetrieveDataToolCalibration(string strSP,string id, string invNumber)
        {
            List<ToolCalibrationBinding> list = new List<ToolCalibrationBinding>();
            //try
            //{
                SqlConnection con = new SqlConnection(ConString);
                SqlCommand cmd = new SqlCommand(strSP, con);
                AddInParameter(cmd, "@ToolId", id);
                AddInParameter(cmd, "@InventoryNumber", invNumber);
                int idValue = int.Parse(id);
                int invNumberValue = int.Parse(invNumber);
                string consString = ConfigurationManager.ConnectionStrings["DotWebDb"].ConnectionString;
                //using (AppDb context = new AppDb())
                //{

                    ////list = (from a in context.ToolLists
                    ////        join b in context.ToolSetups on a.ToolSetupId equals b.Id into leftA from b in leftA.DefaultIfEmpty()
                    ////        join e in context.ToolVerifications on a.ToolSetupId equals e.ToolSetupId into leftC
                    ////        from e in leftC.DefaultIfEmpty()
                    ////        join d in context.ToolCalibrations on b.Id equals d.ToolSetupID into leftB
                    ////        from d in leftB.DefaultIfEmpty()
                 //   list = (from a in context.ToolLists
                //            from b in context.ToolSetups.Where(mapping => mapping.Id == a.ToolSetupId).DefaultIfEmpty()
                //            from c in context.ToolVerifications.Where(mapping => mapping.ToolSetupId == a.ToolSetupId).DefaultIfEmpty()
                //            from d in context.ToolCalibrations.Where(mapping => mapping.ToolSetupId == a.ToolSetupId).DefaultIfEmpty()
                //            where a.ToolSetupId == idValue && a.InventoryNumber == invNumberValue
                //            select new ToolCalibrationBinding
                //            {
                //                LastCalibrationDate = d.CalDate,
                //                LastVerificationDate = c.VerDate,
                //                ToolNumber = b.Number,
                //                InventoryNumber = a.InventoryNumber,
                //                Max = b.MaxNM,
                //                Min = b.MinNM,
                //                SetNM = b.SetNM
                //            }).ToList();
                //}
         //  }
                try
                {
                IDataReader datareader = null;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                datareader = cmd.ExecuteReader();
                list = MapDataToList(datareader);
                }
                catch (Exception)
                {
                    
                    throw;
                }
           
            return list;
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
        private static List<ToolCalibrationBinding> MapDataToList(IDataReader reader)
        {
            List<ToolCalibrationBinding> MapDataToList = new List<ToolCalibrationBinding>();
            while (reader.Read())
            {
                ToolCalibrationBinding param = new ToolCalibrationBinding();
                param.ToolNumber = GetStringData(reader, ParameterSPnEntity.ToolNumber);
                param.LastVerificationDate = GetStringData(reader, ParameterSPnEntity.ToolVerDate) == null ? DateTime.Now : DateTime.Parse(GetStringData(reader, ParameterSPnEntity.ToolVerDate));
                param.LastCalibrationDate = GetStringData(reader, ParameterSPnEntity.ToolCalDate) == null ? DateTime.Now : DateTime.Parse(GetStringData(reader, ParameterSPnEntity.ToolCalDate));
                param.InventoryNumber = GetStringData(reader, ParameterSPnEntity.ToolInventoryNumber) == null ? 0 : int.Parse(GetStringData(reader, ParameterSPnEntity.ToolInventoryNumber));
                param.Max = GetStringData(reader, ParameterSPnEntity.ToolMaxNM) == null ? 0 : Decimal.Parse(GetStringData(reader, ParameterSPnEntity.ToolMaxNM));
                param.Min = GetStringData(reader, ParameterSPnEntity.ToolMinNM) == null ? 0 : Decimal.Parse(GetStringData(reader, ParameterSPnEntity.ToolMinNM));
                param.SetNM = GetStringData(reader, ParameterSPnEntity.ToolSetNM) == null ? 0 : Decimal.Parse(GetStringData(reader, ParameterSPnEntity.ToolSetNM));
                MapDataToList.Add(param);
            }
            return MapDataToList;
        }
        public static void SaveDataToolVerificationMark(string VerDate,string CallNumber,string ToolSetupId,string ToolSetupInv,string SetNM,string MinNM,string MaxNM,string Verification1,string Verification2,string Verification3,string Verification4,string Verification5,string NextCalDate,string Remarks)
        {
            try
            {
                using (AppDb context = new AppDb())
                {
                    ToolCalibration data = new ToolCalibration();
                    {
                        data.CalDate = DateTime.Parse(VerDate);
                        data.MaxNM = decimal.Parse(MaxNM);
                        data.CalNumber = "1";
                        data.ToolSetupId = int.Parse(ToolSetupId);
                        data.ToolSetupInv = int.Parse(ToolSetupInv);
                        data.SetNM = decimal.Parse(SetNM) == null ? 0 : decimal.Parse(SetNM);
                        data.MinNM = decimal.Parse(MinNM) == null ? 0 : decimal.Parse(MinNM);
                        data.MaxNM = decimal.Parse(MaxNM) == null ? 0 : decimal.Parse(MaxNM);
                        data.Verification1 = decimal.Parse(Verification1) == null ? 0 : decimal.Parse(Verification1);
                        data.Verification2 = decimal.Parse(Verification2) == null ? 0 : decimal.Parse(Verification2);
                        data.Verification3 = decimal.Parse(Verification3) == null ? 0 : decimal.Parse(Verification3);
                        data.Verification4 = decimal.Parse(Verification4) == null ? 0 : decimal.Parse(Verification4);
                        data.Verification5 = decimal.Parse(Verification5) == null ? 0 : decimal.Parse(Verification5);
                        data.ResultId = 1;
                        data.NextCalibrationDate = DateTime.Parse(NextCalDate);
                        data.Remarks = Remarks;
                        context.ToolCalibrations.Add(data);
                        context.SaveChanges();
                    }

                 
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }



        public static void SaveDataToolVerification(string VerDate, string CallNumber, string ToolSetupId, string ToolSetupInv, string SetNM, string MinNM, string MaxNM, string Verification1, string Verification2, string Verification3, string Verification4, string Verification5, string NextCalDate, string Remarks)
        {
            try
            {
                using (AppDb context = new AppDb())
                {
                    ToolCalibration data = new ToolCalibration();
                    {
                        data.CalDate = DateTime.Parse(VerDate);
                        data.MaxNM = decimal.Parse(MaxNM);
                        data.CalNumber = "1";
                        data.ToolSetupId = int.Parse(ToolSetupId);
                        data.ToolSetupInv = int.Parse(ToolSetupInv);
                        data.SetNM = decimal.Parse(SetNM) == null ? 0 : decimal.Parse(SetNM);
                        data.MinNM = decimal.Parse(MinNM) == null ? 0 : decimal.Parse(MinNM);
                        data.MaxNM = decimal.Parse(MaxNM) == null ? 0 : decimal.Parse(MaxNM);
                        data.Verification1 = decimal.Parse(Verification1) == null ? 0 : decimal.Parse(Verification1);
                        data.Verification2 = decimal.Parse(Verification2) == null ? 0 : decimal.Parse(Verification2);
                        data.Verification3 = decimal.Parse(Verification3) == null ? 0 : decimal.Parse(Verification3);
                        data.Verification4 = decimal.Parse(Verification4) == null ? 0 : decimal.Parse(Verification4);
                        data.Verification5 = decimal.Parse(Verification5) == null ? 0 : decimal.Parse(Verification5);
                        data.ResultId = 0;
                        data.NextCalibrationDate = DateTime.Parse(NextCalDate);
                        data.Remarks = Remarks;
                        context.ToolCalibrations.Add(data);
                        context.SaveChanges();
                    }

                  
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
