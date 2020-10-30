using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UpravljanjeVozilima.App_Data;
using UpravljanjeVozilima.Dal;
using UpravljanjeVozilima.Dal.VehiclesManager;
using UpravljanjeVozilima.Models;

namespace UpravljanjeVozilima
{
    public static class VehicleTypeConfig
    {
        private static IVehicleRepo Repo;
        
        public static void RegisterVehiclesTypeList()
        {
            Repo = RepoFactory.GetVehicleRepo();
            VehicleTypeData.listOfVehicleType = Repo.GetAllVehicleTypes();
        }
    }
}