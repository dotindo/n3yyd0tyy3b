using System.Linq;
using DotWeb.Models;

namespace DotWeb.Repositories
{
    public class AttachmentRepository
    {
        public static void SaveAttachment(int docId, string docType, string fileType, string fullFilePath)
        {
            using (AppDb context = new AppDb())
            {
                Attachment attch = new Attachment();
                attch.DocId = docId;
                attch.DocTypeId = GetDocTypeIdByDocTypeName(docType);
                attch.FileLocation = fullFilePath;
                attch.FileType = fileType;

                context.Attachments.Add(attch);
                context.SaveChanges();
            }
        }

        private static int? GetDocTypeIdByDocTypeName(string docType)
        {
            using (AppDb context = new AppDb())
            {
                DocType type = context.DocTypes.FirstOrDefault(p => p.Description.Equals(docType));
                if (type != null)
                    return type.Id;
            }
            return 0;
        }

        public static int CountData(int id, string docType)
        {
            int docTypeId = GetDocTypeIdByDocTypeName(docType).GetValueOrDefault();
            using (AppDb context = new AppDb())
            {
                return context.Attachments.Count(p => p.DocTypeId == docTypeId && p.DocId == id);
            }
        }

        public static Attachment RetrieveAttachmentFileByDocIdAndDocTypeName(int id, string docTypeName)
        {
            using (AppDb context = new AppDb())
            {
                int docTypeId = GetDocTypeIdByDocTypeName(docTypeName).GetValueOrDefault();
                return context.Attachments.FirstOrDefault(p => p.DocId == id && p.DocTypeId == docTypeId);
            }
        }
    }
}
