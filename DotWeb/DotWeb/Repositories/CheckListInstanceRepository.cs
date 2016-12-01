using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DotWeb.Models;

namespace DotWeb.Repositories
{
    public enum RowStatus
    {
        Deleted = 0,
        RowStatus = 1
    }

    public enum CheckListStatus
    {
        InComplete = 1,
        Complete = 2
    }

    public class CheckListInstanceRepository
    {
        public static bool CreateNewCheckListInstance(int checkListTpl, string packingMonth, int vModelId = 0,
            int variantId = 0)
        {
            CheckListTemplateInfo chkTpl = RetrieveCheckListTemplateById(checkListTpl);
            Model oModel = ModelRepository.RetrieveModelById(vModelId);
            Variant oVariant = ModelRepository.RetrieveModelVariantById(variantId);

            //{VPM}-{MODEL}-{VARIANT}/{AUTONUMBER}
            string docNumber = chkTpl.TemplateDocNumber;
            if (chkTpl.TemplateDocNumber.Contains("{VPM}"))
            {
                if (packingMonth == String.Empty)
                {
                    //set to current default packing month
                    docNumber = docNumber.Replace("{VPM}", DateTime.Now.ToString("yyyyMM"));
                }
                else
                {
                    docNumber = docNumber.Replace("{VPM}", packingMonth);
                }
            }

            if (chkTpl.TemplateDocNumber.Contains("{MODEL}"))
            {
                if (oModel != null)
                {
                    docNumber = docNumber.Replace("{MODEL}", oModel.ModelName);
                }
            }

            if (chkTpl.TemplateDocNumber.Contains("{VARIANT}"))
            {
                if (oVariant != null)
                {
                    docNumber = docNumber.Replace("{VARIANT}", oVariant.Variant1.Replace(" ", ""));
                }
            }

            //Generate AutoNumber
            CheckListInstanceInfo chkListInstanceInfo = RetrieveCheckListInstanceInfoBy(checkListTpl, oModel.ModelName);
            string runningNumber = "000001";
            if (chkListInstanceInfo == null)
            {
                docNumber = docNumber.Replace("{AUTONUMBER}", runningNumber);
            }
            else
            {
                runningNumber = (Convert.ToInt32(chkListInstanceInfo.RunningNumber) + 1).ToString().PadLeft(6, '0');
                docNumber = docNumber.Replace("{AUTONUMBER}", runningNumber);
            }

            //save to database after finish create documentNumber
            try
            {
                using (AppDb context = new AppDb())
                {
                    //header
                    CheckListInstanceInfo infoHeader = new CheckListInstanceInfo();
                    infoHeader.CheckListTemplateInfoId = checkListTpl;
                    infoHeader.PackingMonth = packingMonth;
                    infoHeader.Model = oModel.ModelName;
                    infoHeader.Variant = oVariant.Variant1.Replace(" ", "");
                    infoHeader.RunningNumber = runningNumber;
                    infoHeader.InstanceName = chkTpl.TemplateName;      //default instance Name, editable from Web UI
                    infoHeader.InstanceDocument = docNumber;
                    infoHeader.LastActivity = "Initial Checklist";      //default value
                    infoHeader.Progress = 0;        //default valueinfoHeader.CreatedBy = "Admin";
                    infoHeader.RowStatus = 1;       //default for active
                    infoHeader.CreatedDate = DateTime.Now;
                    infoHeader.CheckListGroupId = chkTpl.CheckListGroupId;

                    //ambil detail step dari template step
                    List<CheckListTemplateStep> listDetailStep =
                        RetrieveCheckListTemplateStepByTemplateInfoId(checkListTpl);
                    DateTime initialStartStep = DateTime.Now;
                    foreach (CheckListTemplateStep step in listDetailStep)
                    {
                        CheckListInstanceStep stepInstance = new CheckListInstanceStep();
                        stepInstance.CheckListInstanceInfoId = checkListTpl;
                        stepInstance.SeqNo = step.SeqNo;
                        stepInstance.StepName = step.StepName;
                        stepInstance.UserGroupId = step.UserGroupId;
                        stepInstance.UserId = step.UserId;
                        stepInstance.UrlLink = step.UrlLink;
                        stepInstance.StoreProcedureName = step.StoreProcedureName;
                        stepInstance.EmailNotification = step.EmailNotification;
                        stepInstance.Status = (byte?)CheckListStatus.InComplete;
                        stepInstance.StepProcessStart = initialStartStep;
                        stepInstance.Predecessor = step.Predecessor;

                        infoHeader.CheckListInstanceSteps.Add(stepInstance);
                    }

                    context.CheckListInstanceInfoes.Add(infoHeader);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private static List<CheckListTemplateStep> RetrieveCheckListTemplateStepByTemplateInfoId(int checkListTpl)
        {
            using (AppDb context = new AppDb())
            {
                return context.CheckListTemplateSteps.Where(p => p.CheckListTemplateInfoId == checkListTpl).ToList();
            }
        }

        private static CheckListInstanceInfo RetrieveCheckListInstanceInfoBy(int checkListTpl, string modelName)
        {
            using (AppDb context = new AppDb())
            {
                return
                    context.CheckListInstanceInfoes.FirstOrDefault(
                        p =>
                            p.CheckListTemplateInfoId == checkListTpl && p.Model == modelName);
            }
        }

        private static CheckListTemplateInfo RetrieveCheckListTemplateById(int checkListTpl)
        {
            using (AppDb context = new AppDb())
            {
                return context.CheckListTemplateInfoes.FirstOrDefault(p => p.Id == checkListTpl);
            }
        }

        public static void UpdateInstanceInfo(int idEdited, string newValue)
        {
            using (AppDb context = new AppDb())
            {
                CheckListInstanceInfo chkInfo = context.CheckListInstanceInfoes.FirstOrDefault(p => p.Id == idEdited);
                if (chkInfo != null)
                {
                    chkInfo.InstanceName = newValue;
                }
                context.SaveChanges();
            }
        }

        public static List<CheckListInstanceInfo> RetrieveAllData()
        {
            using (AppDb context = new AppDb())
            {
                return context.CheckListInstanceInfoes.Where(p => p.RowStatus == 1).ToList();
            }
        }

        public static void UpdateInstanceInfoToDeleted(DataRow row)
        {
            using (AppDb context = new AppDb())
            {
                int id = Convert.ToInt32(row[0]);
                CheckListInstanceInfo deletedCheckList = context.CheckListInstanceInfoes.FirstOrDefault(p => p.Id == id);
                if (deletedCheckList != null)
                {
                    deletedCheckList.RowStatus = (byte)RowStatus.Deleted;
                    deletedCheckList.ModifiedDate = DateTime.Now;
                }

                context.SaveChanges();
            }
        }

        public static void SetStatusCheckListStep(int id, CheckListStatus clStatus)
        {
            using (AppDb context = new AppDb())
            {
                CheckListInstanceStep completeStep = context.CheckListInstanceSteps.FirstOrDefault(p => p.Id == id);
                if (completeStep != null)
                {
                    completeStep.Status = (byte?)clStatus;
                    if (completeStep.StepProcessStart == null)
                    {
                        completeStep.StepProcessStart = DateTime.Now;
                    }
                    completeStep.StepProcessEnd = DateTime.Now;

                    if (completeStep.EmailNotification != null)
                    {
                        //TODO:send email notification
                    }
                }

                context.SaveChanges();

                //update checklistinfo progress
                byte status = (byte)CheckListStatus.Complete;
                int countComplete =
                    context.CheckListInstanceSteps.Count(
                        p => p.CheckListInstanceInfoId == completeStep.CheckListInstanceInfoId && p.Status == status);

                status = (byte)CheckListStatus.InComplete;
                int countInComplete =
                    context.CheckListInstanceSteps.Count(
                        p =>
                            p.CheckListInstanceInfoId == completeStep.CheckListInstanceInfoId &&
                            (p.Status == status || p.Status == null));
                decimal percentComplete = (Convert.ToDecimal(countComplete) / Convert.ToDecimal(countInComplete + countComplete)) * 100;

                CheckListInstanceInfo clInfo =
                    context.CheckListInstanceInfoes.FirstOrDefault(
                        p => p.Id == completeStep.CheckListInstanceInfoId);
                if (clInfo != null)
                {
                    clInfo.LastActivity = completeStep.StepName;
                    clInfo.Progress = percentComplete;
                    clInfo.ModifiedDate = DateTime.Now;
                }

                context.SaveChanges();
            }
        }

        public static CheckListInstanceStep RetrieveCurrentInstanceStep(int id)
        {
            using (AppDb context = new AppDb())
            {
                return context.CheckListInstanceSteps.FirstOrDefault(p => p.Id == id);
            }
        }

        public static DataTable RetrieveSpParamBySpName(string spName)
        {
            string sql = "SELECT * FROM INFORMATION_SCHEMA.PARAMETERS p WHERE p.SPECIFIC_NAME = @spName";
            AppDb context = new AppDb();
            DataSet dsData = new DataSet();

            using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@spName", spName));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dsData);
            }
            return dsData.Tables[0];
        }

        public static void ExecuteProcessData(string execScript, bool isUpdateToComplete, int id, DateTime startProcess, DateTime endProcess)
        {
            AppDb context = new AppDb();
            using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlCommand cmd = new SqlCommand(execScript, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 7000;

                cmd.ExecuteNonQuery();

                endProcess = DateTime.Now;

                if (isUpdateToComplete)
                {
                    string sql =
                        "UPDATE CheckListInstanceStep SET Status = 2, StepProcessStart = @StepProcessStart, StepProcessEnd = @StepProcessEnd WHERE Id = @id";

                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@StepProcessStart", startProcess);
                    cmd.Parameters.AddWithValue("@StepProcessEnd", endProcess);

                    cmd.ExecuteNonQuery();

                    //set header percentage status
                    CheckListInstanceStep oStep = context.CheckListInstanceSteps.FirstOrDefault(p => p.Id == id);
                    if (oStep != null)
                    {
                        CheckListInstanceInfo oStepHeader = oStep.CheckListInstanceInfo;

                        byte status = (byte)CheckListStatus.Complete;
                        int countComplete =
                                context.CheckListInstanceSteps.Count(
                                    p => p.CheckListInstanceInfoId == oStepHeader.Id && p.Status == status);

                        status = (byte)CheckListStatus.InComplete;
                        int countInComplete =
                            context.CheckListInstanceSteps.Count(
                                p =>
                                    p.CheckListInstanceInfoId == oStepHeader.Id &&
                                    (p.Status == status || p.Status == null));
                        decimal percentComplete = (Convert.ToDecimal(countComplete) / Convert.ToDecimal(countInComplete + countComplete)) * 100;

                        CheckListInstanceInfo clInfo =
                            context.CheckListInstanceInfoes.FirstOrDefault(
                                p => p.Id == oStepHeader.Id);
                        if (clInfo != null)
                        {
                            clInfo.LastActivity = oStep.StepName;
                            clInfo.Progress = percentComplete;
                            clInfo.ModifiedDate = DateTime.Now;
                        }

                        context.SaveChanges();
                    }
                }
            }
        }

        public static bool CheckIsCompleteStep(int? checkListInstanceInfoId, int seqNo)
        {
            using (AppDb context = new AppDb())
            {
                CheckListInstanceStep cStep =
                    context.CheckListInstanceSteps.FirstOrDefault(
                        p => p.CheckListInstanceInfoId == checkListInstanceInfoId && p.SeqNo == seqNo);
                if (cStep.Status == Convert.ToByte(CheckListStatus.Complete))
                {
                    return true;
                }
            }
            return false;
        }

        public static string RetrieveCheckListGroupNameById(int checkListGroupId)
        {
            using (AppDb context = new AppDb())
            {
                CheckListGroup cGroup = context.CheckListGroups.FirstOrDefault(p => p.Id == checkListGroupId);
                if (cGroup != null) return cGroup.CheckListGroupName;
            }
            return "All Check List Document";
        }

        public static CheckListTemplateInfo RetrieveCheckListTemplateInfoById(int chkListInfoTplId)
        {
            using (AppDb context = new AppDb())
            {
                CheckListTemplateInfo cInfo = context.CheckListTemplateInfoes.FirstOrDefault(p => p.Id == chkListInfoTplId);
                return cInfo;
            }
            return null;
        }

        public static CheckListInstanceInfo RetrieveCurrentInstanceInfoById(int id)
        {
            using (AppDb context = new AppDb())
            {
                return context.CheckListInstanceInfoes.FirstOrDefault(p => p.Id == id);
            }
        }

        public static void UpdateCheckListInstanceEmailAddress(string emailAddress, int oStepId)
        {
            using (AppDb context = new AppDb())
            {
                CheckListInstanceStep eStep = context.CheckListInstanceSteps.FirstOrDefault(p => p.Id == oStepId);
                if (eStep != null)
                {
                    if (string.IsNullOrEmpty(eStep.EmailNotification))
                    {
                        eStep.EmailNotification = emailAddress;
                    }
                    else
                    {
                        char[] emailChar = eStep.EmailNotification.ToCharArray();
                        if (emailChar[emailChar.Length - 1].Equals(","))
                        {
                            eStep.EmailNotification = eStep.EmailNotification + emailAddress;
                        }
                        else
                        {
                            eStep.EmailNotification = eStep.EmailNotification + "," + emailAddress;

                        }
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
