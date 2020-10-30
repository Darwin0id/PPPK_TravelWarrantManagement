using UpravljanjeVozilima.App_Data;
using UpravljanjeVozilima.Dal;
using UpravljanjeVozilima.Dal.VehiclesManager;

namespace UpravljanjeVozilima
{
    public class VehicleBrandConfig
    {
        private static IVehicleRepo Repo;
        
        public static void RegisterVehiclesBrandList()
        {
            Repo = RepoFactory.GetVehicleRepo();
            VehicleBrandData.listOfVehicleBrands = Repo.GetAllVehicleBrands();
        }
    }
}