using System.Collections.Generic;
using UpravljanjeVozilima.Models;

namespace UpravljanjeVozilima.Dal.VehiclesManager
{
    public interface IVehicleRepo
    {
        IEnumerable<VehicleType> GetAllVehicleTypes();
        IEnumerable<VehicleBrand> GetAllVehicleBrands();
        IList<Vehicle> GetAllVehicles();
        int AddVehicle(Vehicle vehicle);
        int DeleteVehicle(int id);
        Vehicle GetVehicle(int id);
        int EditVehicle(Vehicle vehicle);
    }
}