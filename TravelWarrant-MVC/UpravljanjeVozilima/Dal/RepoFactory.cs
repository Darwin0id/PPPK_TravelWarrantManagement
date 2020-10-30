using UpravljanjeVozilima.Dal.PersonManager;
using UpravljanjeVozilima.Dal.TravelManager;
using UpravljanjeVozilima.Dal.VehiclesManager;

namespace UpravljanjeVozilima.Dal
{
    public static class RepoFactory
    {
        public static IPersonRepo GetPersonRepo() => new PersonSqlHandler();
        public static IVehicleRepo GetVehicleRepo() => new VehicleSqlHandler();
        public static ITravelRepo GetTravelRepo() => new TraveleSqlHandler();
    }
}