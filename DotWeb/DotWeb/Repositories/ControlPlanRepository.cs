using System;
using System.Data.Entity;
using System.Linq;
using DotWeb.Models;
using DotWeb.Utils;

namespace DotWeb.Repositories
{
    public class ControlPlanRepository
    {
        public static bool CreateNewControlPlan(int packingMonth, int modelId, int variantId)
        {
            using (AppDb context = new AppDb())
            {
                context.Database.CommandTimeout = 360; //in second
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        ControlPlan cpHeader = new ControlPlan();
                        cpHeader.PackingMonth = packingMonth.ToString();
                        cpHeader.ModelId = modelId;
                        cpHeader.VariantId = variantId;

                        context.ControlPlans.Add(cpHeader);
                        context.SaveChanges();
                        ////save ke CPHeader
                        //CPHeader cpHeader = new CPHeader();
                        //cpHeader.PackingMonthId = packingMonth;
                        //cpHeader.ModelId = modelId;
                        //cpHeader.VariantId = variantId;

                        //context.CpHeaders.Add(cpHeader);

                        //try
                        //{
                        //    context.SaveChanges();
                        //}
                        //catch (DbEntityValidationException e)
                        //{
                        //    foreach (var eve in e.EntityValidationErrors)
                        //    {
                        //        AppLogger.LogError(@"Entity of type '" + eve.Entry.Entity.GetType().Name + "' in state '" +
                        //                           eve.Entry.State + "' has the following validation errors:");

                        //        foreach (var ve in eve.ValidationErrors)
                        //        {
                        //            AppLogger.LogError(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                        //                ve.PropertyName, ve.ErrorMessage));
                        //        }
                        //    }
                        //    throw;
                        //}

                        ////save to CPDetail
                        //context.Database.ExecuteSqlCommand("usp_CreateControlPlanDetail @vpm, @model, @variant, @createdBy",
                        //    new SqlParameter("@vpm", packingMonth), new SqlParameter("@model", modelId),
                        //    new SqlParameter("@variant", variantId), new SqlParameter("@createdBy", ""));

                        ////save ke Consumption material
                        //context.Database.ExecuteSqlCommand("usp_CreateConsumptionMaterial @vpm, @model",
                        //    new SqlParameter("@vpm", packingMonth), new SqlParameter("@model", modelId));

                        ////save ke ToolList
                        //context.Database.ExecuteSqlCommand("usp_CreateControlPlanToolList @vpm, @model",
                        //    new SqlParameter("@vpm", packingMonth), new SqlParameter("@model", modelId));

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        AppLogger.LogError(ex);
                        transaction.Rollback();
                    }
                }
            }
            return false;
        }

        public static int RetrievePackingMonthIdByCpHeaderId(int cpHeaderId)
        {
            using (AppDb context = new AppDb())
            {
                ControlPlan controlPlan = context.ControlPlans.FirstOrDefault(p => p.Id == cpHeaderId);
                if (controlPlan != null)
                    return Convert.ToInt32(controlPlan.PackingMonth);
            }
            return 0;
        }

        public static ControlPlan RetrieveControlPlanById(int cpHeaderId)
        {
            ControlPlan cp;
            using (AppDb context = new AppDb())
            {
                cp = context.ControlPlans.FirstOrDefault(p => p.Id == cpHeaderId);
            }
            return cp;
        }

        public static ControlPlanProcess RetrieveCpDetailProcessById(int id)
        {
            using (AppDb context = new AppDb())
            {
                return context.ControlPlanProcesses.FirstOrDefault(p => p.Id == id);
            }
        }

        public static void UpdateStampNo(string stampNo, int cpDetailId)
        {
            using (AppDb context = new AppDb())
            {
                ControlPlanProcess cpProcess = context.ControlPlanProcesses.FirstOrDefault(p => p.Id == cpDetailId);
                cpProcess.StampNo = stampNo;
                context.SaveChanges();
            }
        }

        public static bool SaveCgisImage(int cpDetail1Id, byte[] imgCgisWithAnnot)
        {
            try
            {
                using (AppDb context = new AppDb())
                {
                    ControlPlanImage cpi = new ControlPlanImage();
                    cpi.ControlPlanProcessId = cpDetail1Id;
                    cpi.Image = imgCgisWithAnnot;

                    context.ControlPlanImages.Add(cpi);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex);
                return false;
            }
            return true;
        }

        public static ControlPlanImage RetrieveCpImageBy(int cpDetailId)
        {
            using (AppDb context = new AppDb())
            {
                return context.ControlPlanImages.FirstOrDefault(p => p.ControlPlanProcessId == cpDetailId);
            }
        }
    }
}
