using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotWeb.Models;
using System.Configuration;

namespace DotWeb.Repositories
{

    public class VINNumberRepository
    {
        private static int getOrganizationId()
        {
            using (DotWebDb context = new DotWebDb())
            {
                Organization name = context.Organizations.FirstOrDefault(o => o.Name == "ENG");
                if (name != null)
                    return name.Id;
            }
            return 0;
        }
        
        public static List<ProductionLine> getLine()
        {
            using (AppDb context = new AppDb())
            {
                return context.Productline.ToList();
            }
        }
        public static string CheckSerialNumber(string barcode)
        {
            using (AppDb context = new AppDb())
            {
                ProductionSequenceDetail serialNumber = context.ProductionSequenceDetails.FirstOrDefault(p => p.SerialNumber == barcode);
                if (serialNumber != null)
                    return serialNumber.SerialNumber;                
            }
            return "";
        }
        public static string CheckUser(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "";

            using (DotWebDb context = new DotWebDb())
            {
                int organizationId = getOrganizationId();
                User user = context.Users.Where(p => p.OrganizationId == organizationId && p.AuthKey == password ).FirstOrDefault();
                if (user != null)
                    return user.UserName;
            }
            return "";
        }

    }
}
