using System.Collections.Generic;
using System.Linq;
using DotWeb.Models;

namespace DotWeb.Repositories
{
    public class FileTypeRepository
    {
        public static List<FileType> RetrieveFileTypes()
        {
            using (AppDb context = new AppDb())
            {
                return context.FileTypes.ToList();
            }
        }

        public static int RetrieveFileTypeIdByName(string fileTypeName)
        {
            using (AppDb context = new AppDb())
            {
                FileType fileType = context.FileTypes.FirstOrDefault(p => p.Name.Equals(fileTypeName));
                if (fileType != null)
                    return fileType.Id;
            }
            return 0;
        }
    }
}
