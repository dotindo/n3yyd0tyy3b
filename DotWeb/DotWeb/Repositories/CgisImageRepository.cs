using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using DotWeb.Models;
using Ebiz.AnnotObject;

namespace DotWeb.Repositories
{
    public class CgisImageRepository
    {
        public static List<CgisImage> RetrieveCgisImageByAssemblyProcessNoAndCgisSingleViewId(int assyProcessNo, int cgisSingleViewId)
        {
            List<CgisImage> iList = new List<CgisImage>();
            AppDb context = new AppDb();
            
            string connection = context.Database.Connection.ConnectionString;
            string sql =
                "SELECT DISTINCT c.AssemblyProcessNo FROM CGISImage c " + 
                "WHERE c.AssemblyProcessNo = @AssemblyProcessNo AND c.FkCGISSingleViewId = @FkCGISSingleViewId";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                if(conn.State == ConnectionState.Closed) conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AssemblyProcessNo", assyProcessNo);
                cmd.Parameters.AddWithValue("@FkCGISSingleViewId", cgisSingleViewId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<Bitmap> lBitmap = new AnnotObject(connection).GetImageFromAnnotationObject(reader.GetInt32(0));
                    if (lBitmap.Count > 0)
                    {
                        for (int i = 0; i < lBitmap.Count; i++)
                        {
                            CgisImage ci = new CgisImage();
                            ci.ImageName = "CgisImage" + i;
                            ci.CgisImageWithAnnot = lBitmap[i];
                            ci.CgisByteImageWithAnnot = ImageToByteArray(lBitmap[i]);

                            iList.Add(ci);
                        }
                    }
                }
                reader.Close();
            }
            return iList;
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }
    }
}
