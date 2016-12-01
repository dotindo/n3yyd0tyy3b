using System.Collections.Generic;
using System.Linq;
using DotWeb.Models;

namespace DotWeb.Repositories
{
    public class StationRepository
    {
        public static List<Stations> RetrieveStationByAssemblySectionId(int assemblySectionId)
        {
            using (AppDb context = new AppDb())
            {
                return context.Stationses.Where(p => p.AssemblySectionId == assemblySectionId).ToList();
            }
        }

        public static string RetrieveStationByStationId(int stationId)
        {
            using (AppDb context = new AppDb())
            {
                Stations stations = context.Stationses.FirstOrDefault(p => p.Id == stationId);
                if (stations != null)
                    return stations.StationName;
            }
            return "";
        }
    }
}
