using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotWeb.Models;
using System.Data.SqlClient;
using System.Data;

namespace DotWeb.Repositories
{
    public class RecordImplemControlRepository
    {
        private class minmaxModel
        {
            public int Id { get; set; }
            public int Min { get; set; }
            public int Max { get; set; }
            public string Codes { get; set; }
            public string VehicleNumber { get; set; }
        }

        public static string RetrieveBauMusterByModelId(int id)
        {
            using (AppDb context = new AppDb())
            {
                Model model = context.Models.FirstOrDefault(m => m.Id == id);
                if (model != null)
                    return model.Baumuster;
            }
            return "";
        }

        public static string RetrieveModelNameByModelId(int id)
        {
            using (AppDb context = new AppDb())
            {
                Model model = context.Models.FirstOrDefault(m => m.Id == id);
                if (model != null)
                    return model.ModelName;
            }
            return "";
        }

        public static int RetrieveVehiclesIdByPM(string packingMonth)
        {
            using (AppDb context = new AppDb())
            {
                VehicleOrders id = context.VehicleOrders.FirstOrDefault(i => i.PackingMonth == packingMonth);
                if (id != null)
                    return id.Id;
            }
            return 0;
        }

        public static int RetrieveProdNumberMaxById(int id)
        {
            using (AppDb context = new AppDb())
            {
                SqlParameter Id = new SqlParameter("@Id", id);
                int dataMax = context.Database.SqlQuery<minmaxModel>("exec usp_getMinMaxCommnos @Id", Id).Select(x => x.Max)
                               .FirstOrDefault();
                if (dataMax != 0)
                    return dataMax;
                else
                    return 0;
            }
        }

        public static int RetrieveProdNumberMinById(int id)
        {
            using (AppDb context = new AppDb())
            {
                SqlParameter Id = new SqlParameter("@Id", id);
                int dataMin = context.Database.SqlQuery<minmaxModel>("exec usp_getMinMaxCommnos @Id", Id).Select(x => x.Min)
                               .FirstOrDefault();
                if (dataMin != 0)
                    return dataMin;
                else
                    return 0;
            }
        }

        public static string RetrieveChassisNumberMaxById(int id)
        {
            using (AppDb context = new AppDb())
            {
                SqlParameter Id = new SqlParameter("@Id", RetrieveProdNumberMaxById(id));
                var sqlQuery = @"select VehicleNumber from VehicleOrderDetails
                                 where substring(ProdNumber,3,5) = @Id ";

                string a = context.Database.SqlQuery<minmaxModel>(sqlQuery, Id).Select(x => x.VehicleNumber).FirstOrDefault();
                //int dataMax = context.Database.SqlQuery<minmaxModel>("exec usp_getMinMaxChassis @Id", Id).Select(x => x.Max)
                //               .FirstOrDefault();
                //if (dataMax != 0)
                //    return dataMax;
                //else
                //    return 0;
                return a;
            }
        }

        public static string RetrieveChassisNumberMinById(int id)
        {            
            using (AppDb context = new AppDb())
            {
                SqlParameter Id = new SqlParameter("@Id", RetrieveProdNumberMinById(id));
                var sqlQuery = @"select VehicleNumber from VehicleOrderDetails
                                 where substring(ProdNumber,3,5) = @Id ";

                return context.Database.SqlQuery<minmaxModel>(sqlQuery, Id).Select(x => x.VehicleNumber).FirstOrDefault();
                //int dataMin = context.Database.SqlQuery<minmaxModel>("exec usp_getMinMaxChassis @Id", Id).Select(x => x.Min)
                //               .FirstOrDefault();
                //if (dataMin != 0)
                //    return dataMin;
                //else
                //    return 0;

            }
        }

        public static int GetLastId()
        {
            using (AppDb context = new AppDb())
            {
                //string consString = context.Database.Connection.ConnectionString;
                var sqlQuery = @"select top 1 Id from [mercedesdb].[dbo].RecordImplemControl
                               Order By id desc";

                return context.Database.SqlQuery<minmaxModel>(sqlQuery).Select(x => x.Id).FirstOrDefault();
            }
        }
        public static string GetCodes(int id)
        {
            using (AppDb context = new AppDb())
            {
                //string consString = context.Database.Connection.ConnectionString;
                SqlParameter ID = new SqlParameter("@Id", id);

                var sqlQuery = @"SELECT distinct top 1 CONCAT(Codes,'; ',Codes1,Codes2,Codes3,Codes4,Codes5,Codes6,Codes7,Codes8,Codes9,Codes10,Codes11,Codes12,Codes13,Codes14,Codes15,Codes16,Codes17,Codes18,Codes19,Codes20,Codes21,Codes22,Codes23,Codes24,Codes25,Codes26,Codes27,Codes28,Codes29,Codes30,Codes31,Codes32,Codes33,Codes34,Codes35,Codes36,Codes37,Codes38,Codes39,Codes40,Codes41,Codes42,Codes43,Codes44,Codes45,Codes46,Codes47,Codes48,Codes49,Codes50,
                                Codes51,Codes52,Codes53,Codes54,Codes55,Codes56,Codes57,Codes58,Codes59,Codes60,Codes61,Codes62,Codes63,Codes64,Codes65,Codes66,Codes67,Codes68,Codes69,Codes70,Codes71,Codes72,Codes73,Codes74,Codes75,Codes76,Codes77,Codes78,Codes79,Codes80,Codes81,Codes82,Codes83,Codes84,Codes85,Codes86,Codes87,Codes88,Codes89,Codes90,Codes91,Codes92,Codes93,Codes94,Codes95,Codes96,Codes97,Codes98,Codes99,Codes100,
                                Codes101,Codes102,Codes103,Codes104,Codes105,Codes106,Codes107,Codes108,Codes109,Codes110,Codes111,Codes112,Codes113,Codes114,Codes115,Codes116,Codes117,Codes118,Codes119,Codes120,Codes121,Codes122,Codes123,Codes124,Codes125,Codes126,Codes127,Codes128,Codes129,Codes130,Codes131,Codes132,Codes133,Codes134,Codes135,Codes136,Codes137,Codes138,Codes139,Codes140,Codes141,Codes142,Codes143,Codes144,Codes145,Codes146,Codes147,Codes148,Codes149,Codes150,
                                Codes151,Codes152,Codes153,Codes154,Codes155,Codes156,Codes157,Codes158,Codes159,Codes160,Codes161,Codes162,Codes163,Codes164,Codes165,Codes166,Codes167,Codes168,Codes169,Codes170,Codes171,Codes172,Codes173,Codes174,Codes175,Codes176,Codes177,Codes178,Codes179,Codes180,Codes181,Codes182,Codes183,Codes184,Codes185,Codes186,Codes187,Codes188,Codes190,Codes191,Codes192,Codes193,Codes194,Codes195,Codes196,Codes197,Codes198,Codes199,Codes200) as Codes FROM [mercedesdb].[dbo].VehicleOrderDetails where VehicleOrderId = @id";

                return context.Database.SqlQuery<minmaxModel>(sqlQuery, ID).Select(x=> x.Codes).FirstOrDefault();                             
            }
        }

        public static bool SaveSTA(string issuer, DateTime issuedOn, int RicId, string RICNumber, string AlterId, string reason,
            int commnosCon, int commnossFrom, int CommnosTo, string pckMth, string chassisNum, string implePlan, string cumFigure,
            DateTime imple, DateTime actualImple, bool remark, string codes, DateTime oldUntil, DateTime newFrom, string approve1,
            string approve2, string approve3, string approve4, int modelId, int variantId)
        {
            int? createdbyid = UserRepository.RetrieveOrganizationIdByUserName(issuer);
            bool exe = false;
            try
            {
                using (AppDb context = new AppDb())
                {
                    #region linq
                    //RecordImplemControl data = new RecordImplemControl()
                    //{
                    //    Issuer = UserRepository.RetrieveUserIdByUserName(issuer),
                    //    IssuedDate = issuedOn,
                    //    RICStatusId = RicId,
                    //    RICNR = RICNumber,
                    //    AlterationId = AlterId,
                    //    ReasonOfAlteration = reason,
                    //    CommnosCountry = commnosCon,
                    //    CommnosFrom = commnossFrom,
                    //    CommnosTo = CommnosTo,
                    //    PackingMonth = pckMth,
                    //    ChassisNR = chassisNum,
                    //    ImplPlan = implePlan,
                    //    CumulativeFigure = cumFigure,
                    //    ImplementationDate = imple,
                    //    ActualImplementationDate = actualImple,
                    //    Remark = remark,
                    //    Codes = codes,
                    //    OldUntil = oldUntil,
                    //    NewFrom = newFrom,
                    //    CreatedById = UserRepository.RetrieveOrganizationByName(issuer),
                    //    ApprovedBy1 = UserRepository.RetrieveUserIdByUserName(approve1),
                    //    ApprovedBy1Date = issuedOn,
                    //    ApprovedBy2 = UserRepository.RetrieveUserIdByUserName(approve2),
                    //    ApprovedBy2Date = issuedOn,
                    //    ApprovedBy3 = UserRepository.RetrieveUserIdByUserName(approve3),
                    //    ApprovedBy3Date = issuedOn,
                    //    ApprovedBy4 = UserRepository.RetrieveUserIdByUserName(approve3),
                    //    ApprovedBy4Date = issuedOn,
                    //    ModelId = modelId,
                    //    VarianId = variantId
                    //};

                    //context.RecordImplemControls.Add(data);
                    //context.SaveChanges();

                    //RecordImplemControlDetail data2 = new RecordImplemControlDetail()
                    //{
                    //    RICId = data.Id,
                    //    OldNo = null,
                    //    OldPartNo = null,
                    //    OldPartDescription = null,
                    //    OldQty = null,
                    //    OldESD = null,
                    //    NewPartNo = null,
                    //    NewPartDescription = null,
                    //    NewQty = null,
                    //    NewESD = null,
                    //    PackID = null,
                    //    NewIndex = null,
                    //    DialogAddress = null,
                    //    CGISNumber = null,
                    //    Remark = null,
                    //    NewPartNumber = null,
                    //    NewDescription = null,
                    //    PartNumberGab = null,
                    //    OldCode = null,
                    //    NewCode = null
                    //};

                    //context.RecordImplemControlDetails.Add(data2);
                    //context.SaveChanges();
                    //exe = true;
                    #endregion
                    string consString = context.Database.Connection.ConnectionString;
                    using (SqlConnection con = new SqlConnection(consString))
                    {
                        using (SqlCommand cmd = new SqlCommand("usp_saveRIC", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Issuer", SqlDbType.Int).Value = UserRepository.RetrieveUserIdByUserName(issuer);
                            cmd.Parameters.Add("@IssuedDate", SqlDbType.DateTime).Value = issuedOn;
                            cmd.Parameters.Add("@RICStatusId", SqlDbType.Int).Value = RicId;
                            cmd.Parameters.Add("@RICNr", SqlDbType.NVarChar).Value = RICNumber;
                            cmd.Parameters.Add("@AlterationId", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd.Parameters.Add("@ReasonOfAlteration", SqlDbType.NVarChar).Value = reason;
                            cmd.Parameters.Add("@CommnosCountry", SqlDbType.Int).Value = commnosCon;
                            cmd.Parameters.Add("@CommnosFrom", SqlDbType.Int).Value = commnossFrom;
                            cmd.Parameters.Add("@CommnosTo", SqlDbType.Int).Value = CommnosTo;
                            cmd.Parameters.Add("@PackingMonth", SqlDbType.NVarChar).Value = pckMth;
                            cmd.Parameters.Add("@ChassisNR", SqlDbType.NVarChar).Value = chassisNum;
                            cmd.Parameters.Add("@ImplPlan", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd.Parameters.Add("@CumulativeFigure", SqlDbType.NVarChar).Value = cumFigure;
                            cmd.Parameters.Add("@ImplementationDate", SqlDbType.DateTime).Value = imple;
                            cmd.Parameters.Add("@ActualImplementationDate", SqlDbType.DateTime).Value = DBNull.Value;
                            cmd.Parameters.Add("@Remark", SqlDbType.Bit).Value = remark;
                            cmd.Parameters.Add("@Codes", SqlDbType.NVarChar).Value = codes;
                            cmd.Parameters.Add("@OldUntil", SqlDbType.DateTime).Value = oldUntil;
                            cmd.Parameters.Add("@NewFrom", SqlDbType.DateTime).Value = newFrom;
                            cmd.Parameters.Add("@CreatedById", SqlDbType.Int).Value = createdbyid;
                            cmd.Parameters.Add("@ApprovedBy1", SqlDbType.Int).Value = UserRepository.RetrieveUserIdByUserName(approve1);
                            cmd.Parameters.Add("@ApprovedBy1Date", SqlDbType.DateTime).Value = DBNull.Value;
                            cmd.Parameters.Add("@ApprovedBy2", SqlDbType.Int).Value = UserRepository.RetrieveUserIdByUserName(approve2);
                            cmd.Parameters.Add("@ApprovedBy2Date", SqlDbType.DateTime).Value = DBNull.Value;
                            cmd.Parameters.Add("@ApprovedBy3", SqlDbType.Int).Value = UserRepository.RetrieveUserIdByUserName(approve3);
                            cmd.Parameters.Add("@ApprovedBy3Date", SqlDbType.DateTime).Value = DBNull.Value;
                            cmd.Parameters.Add("@ApprovedBy4", SqlDbType.Int).Value = UserRepository.RetrieveUserIdByUserName(approve4);
                            cmd.Parameters.Add("@ApprovedBy4Date", SqlDbType.DateTime).Value = DBNull.Value;
                            cmd.Parameters.Add("@ModelId", SqlDbType.Int).Value = modelId;
                            cmd.Parameters.Add("@VariantId", SqlDbType.Int).Value = variantId;
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
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return exe;
        }
    }
}
