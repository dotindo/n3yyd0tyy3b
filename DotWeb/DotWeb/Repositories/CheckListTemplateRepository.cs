using System;
using System.Collections.Generic;
using System.Linq;
using DotWeb.Models;

namespace DotWeb.Repositories
{
    public class CheckListTemplateRepository
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
            CheckListTemplateInfo chkListInstanceInfo = RetrieveCheckListTemplateInfoBy(checkListTpl, packingMonth,
                oModel.ModelName);
            string runningNumber = "000001";
            if (chkListInstanceInfo == null)
            {
                docNumber = docNumber.Replace("{AUTONUMBER}", runningNumber);
            }
            else
            {
                runningNumber = (Convert.ToInt32(chkListInstanceInfo.TemplateDocNumber) + 1).ToString().PadLeft(6, '0');
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
                    infoHeader.CreatedDate = DateTime.Now;

                    //ambil detail step dari template step
                    List<CheckListTemplateStep> listDetailStep =
                        RetrieveCheckListTemplateStepByTemplateInfoId(checkListTpl);
                    foreach (CheckListTemplateStep step in listDetailStep)
                    {
                        CheckListInstanceStep stepInstance = new CheckListInstanceStep();
                        stepInstance.CheckListInstanceInfoId = checkListTpl;
                        stepInstance.SeqNo = step.SeqNo;
                        stepInstance.StepName = step.StepName;
                        stepInstance.UserGroupId = step.UserGroupId;
                        stepInstance.UrlLink = step.UrlLink;
                        stepInstance.StoreProcedureName = step.StoreProcedureName;
                        stepInstance.EmailNotification = step.EmailNotification;

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

        private static CheckListTemplateInfo RetrieveCheckListTemplateInfoBy(int checkListTpl, string templateName, string templateDocNumber)
        {
            using (AppDb context = new AppDb())
            {
                return
                    context.CheckListTemplateInfoes.FirstOrDefault(
                        p =>
                            p.Id == checkListTpl && p.TemplateName == templateName &&
                            p.TemplateDocNumber == templateDocNumber);
            }
        }

        private static CheckListTemplateInfo RetrieveCheckListTemplateById(int checkListTpl)
        {
            using (AppDb context = new AppDb())
            {
                return context.CheckListTemplateInfoes.FirstOrDefault(p => p.Id == checkListTpl);
            }
        }
    }
}
