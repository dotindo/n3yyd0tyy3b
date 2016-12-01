using DotWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotWeb.Utils;

namespace DotWeb.Repositories
{
    public class AssignToolRepository
    {
        public SqlConnection Connection;
        public SqlTransaction Transaction;
        public static string ConString = System.Configuration.ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;
        public class DDLToolAssignBinding
        {
            public int ModelId { get; set; }
            public int ToolSetupId { get; set; }
            public int IdControlPlan { get; set; }
            public int IdTool { get; set; }
            public string Name { get; set; }
            public int? InventoryNumber { get; set; }
            public string ToolNumber { get; set; }
            public string ProductionLine { get; set; }
            public string StationName { get; set; }
            public string PackingMth { get; set; }
            public int Id { get; set; }
            public string ModelName { get; set; }
            public string VariantName { get; set; }
            public int BindingIdControlPlan { get; set; }
            public int? BindingPackingMonthId { get; set; }
            public int? BindingModelId { get; set; }
            public int? BindingVariantId { get; set; }
            public string ProcessNumber { get; set; }
            public string Description { get; set; }
            public bool? BindingCheck { get; set; }
            
        }
        public class SaveDDLToolAssign
        {
            public int ControlPlanToolId { get; set; }
            public int ToolInventoryId { get; set; }
        }
        public static IList<DDLToolAssignBinding> RetrieveDDLToolNumber()
        {
            List<DDLToolAssignBinding> list = new List<DDLToolAssignBinding>();
            try
            {
                using (AppDb context = new AppDb())
                {
                    list = (from a in context.Tools select new DDLToolAssignBinding{
                     ToolNumber = a.Number,
                     ToolSetupId = a.Id
                    }).ToList();
                }
            }
               
            catch (Exception ex)
            {
                
                throw;
            }
            return list;
        }
        public static IList<DDLToolAssignBinding> RetrieveDDLInventoryNumber(string ToolSetupId)
        {
            List<DDLToolAssignBinding> list = new List<DDLToolAssignBinding>();
            try
            {
                 using (AppDb context = new AppDb())
                {
                   
                    list = (from a in context.Tools join b in context.ToolInventories on a.Id equals b.Id
                            where a.Number == ToolSetupId
                            select new DDLToolAssignBinding {
                     InventoryNumber = b.InventoryNumber
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return list;
        }
        public static IList<DDLToolAssignBinding> RetrieveProdLine()
        {
            List<DDLToolAssignBinding> list = new List<DDLToolAssignBinding>();
            try
            {
                using (AppDb context = new AppDb())
                {
                    list = (from a in context.Productline
                            select new DDLToolAssignBinding
                            {
                                ProductionLine = a.LineName,
                                IdTool = a.Id
                            }).ToList();
                }

            }
            catch (Exception)
            {
                
                throw;
            }
            return list;
        }
        public static IList<DDLToolAssignBinding> RetrieveStation()
        {
            List<DDLToolAssignBinding> list = new List<DDLToolAssignBinding>();
            try
            {
                using (AppDb context = new AppDb())
                {
                    list = (from a in context.Stationses
                            select new DDLToolAssignBinding
                            {
                                StationName = a.StationName,
                                IdTool = a.Id
                            }).ToList();
                }

            }
            catch (Exception)
            {
                
                throw;
            }
            return list;
        }
        
        protected static void AddInParameter(SqlCommand command, string name, object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;

            command.Parameters.Add(parameter);
        }
        public static IList<DDLToolAssignBinding> RetrieveModel()
        {
            List<DDLToolAssignBinding> list = new List<DDLToolAssignBinding>();
            try
            {
                using (AppDb context = new AppDb())
                {
                    list = (from a in context.Models
                            select new DDLToolAssignBinding
                            {
                                ModelId = a.Id,
                                ModelName = a.ModelName
                            }).ToList();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return list;
        }
        public static IList<DDLToolAssignBinding> RetrieveModelVariant(string IdModelVariant)
        {
            List<DDLToolAssignBinding> list = new List<DDLToolAssignBinding>();
            try
            {
                int x = int.Parse(IdModelVariant);
                using (AppDb context = new AppDb())
                {
                    list = (from a in context.Variants where a.ModelId == x
                            select new DDLToolAssignBinding
                            {
                                Id = a.Id,
                                VariantName = a.Variant1,
                            }).ToList();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return list;
        }
        //public static DataTable DataGridview(string PackingMonthId,string ModelId,string VariantId)
        public DataTable RetrieveGridData(string strSP,string ModelId, string VariantModelId, string PackingMonthId, string Station, string InventoryNumber, string ToolNumber, string ProductionLine)
        {
           // List<DDLToolAssignBinding> list = new List<DDLToolAssignBinding>();
            //
            DataTable dtView = new DataTable();
            SqlConnection con = new SqlConnection(ConString);
            SqlCommand cmd = new SqlCommand(strSP, con);
            AddInParameter(cmd, "@ModelId", ModelId);
            AddInParameter(cmd, "@VariantModelId", VariantModelId);
            AddInParameter(cmd, "@PackingMonthId", PackingMonthId);
            AddInParameter(cmd, "@Station", Station);
            AddInParameter(cmd, "@InventoryNumber", InventoryNumber);
            AddInParameter(cmd, "@ToolNumber", ToolNumber);
            AddInParameter(cmd, "@ProductionLine", ProductionLine);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter daSQl = new SqlDataAdapter(cmd);
                daSQl.Fill(dtView);
                //dtView.Columns.Add("IsAssign", typeof(string));
                daSQl.Dispose();
                //  using (AppDb context = new AppDb())
                //{
                //      int IntPackingMonthId = int.Parse(PackingMonthId);
                //      int IntModelId = int.Parse(ModelId);
                //      int IntToolNumber = int.Parse(ToolNumber);
                //      var GetControlPlanId = (from a in context.ControlPlans
                //                              where a.PackingMonthId == IntPackingMonthId && a.ModelId == IntModelId
                //                              select a.Id).SingleOrDefault();
                //      var GetControlPlanId1 = (from a in context.ControlPlanDetails1 where a.ControlPlanId == GetControlPlanId select a.id).FirstOrDefault();
                //      list = (from a in context.ControlPlanDetails3
                //                       where a.ControlPlanDetail1Id == GetControlPlanId1 && a.ToolId == IntToolNumber
                //                       select new DDLToolAssignBinding
                //                       {
                //                           IdControlPlan = a.Id,
                //                           ProcessNumber = a.AssemblyProcessNo,
                //                           Description = a.AssemblyProcessName,
                //                           BindingCheck = a.IsAssign
                //                       }).ToList();
                //}
               
                
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
           // return list;
            return dtView;
        }
        public static void SaveGrid(string Id,string ToolId, string ControlPlanId)
        {
            try
            {
                using (AppDb context = new AppDb())
                {
                    ToolAssignment Data = new ToolAssignment();
                    {
                        Data.ControlPlanToolId = int.Parse(Id);
                        Data.ToolInventoryId = int.Parse(ToolId);
                        context.ToolAssigns.Add(Data);
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
