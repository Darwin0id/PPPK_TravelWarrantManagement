using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UpravljanjeVozilima.App_Data;
using UpravljanjeVozilima.Dal;
using UpravljanjeVozilima.Dal.TravelManager;
using UpravljanjeVozilima.Dal.VehiclesManager;
using UpravljanjeVozilima.Models;

namespace UpravljanjeVozilima
{
    public static class TravelStatusConfig
    {
        private static ITravelRepo Repo;
        
        public static void RegisterTravelStatusList()
        {
            Repo = RepoFactory.GetTravelRepo();
            TravelStatusData.listOfTraveLStatuses = Repo.GetAllTravelStatuses();
        }
    }
}