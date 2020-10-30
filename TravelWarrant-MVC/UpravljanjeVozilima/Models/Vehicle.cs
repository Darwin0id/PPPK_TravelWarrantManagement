using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using UpravljanjeVozilima.App_Data;

namespace UpravljanjeVozilima.Models
{
    public class Vehicle
    {
        //CONSTRUCTOR
        public Vehicle(int idVehicle, VehicleType type, VehicleBrand brand, int yearOfManufacture, int startingKm)
        {
            IDVehicle = idVehicle;
            Type = type;
            Brand = brand;
            YearOfManufacture = yearOfManufacture;
            StartingKM = startingKm;
        }

        public Vehicle()
        {
            
        }

        //ATTRIBUTES
        public int IDVehicle { get; set; }
        
        [Display(Name = "Tip")]
        public VehicleType Type { get; set; }
        
        [Display(Name = "Marka")]
        public VehicleBrand Brand { get; set; }
        
        [Display(Name = "Godina proizvodnje")]
        [Required(ErrorMessage = "* Polje godina proizvodnje je obavezno")]
        public int YearOfManufacture { get; set; }
        
        [Display(Name = "Inicijalno stanje kilometara")]
        [Required(ErrorMessage = "* Polje inicijalno stanje kilometara je obavezno")]
        public int StartingKM { get; set; }
        
        [Display(Name = "Automobil")]
        public string FullTitle
        {
            get { return $"{Brand} {Type}"; }
        }

        public static Vehicle ParseFromDb(SqlDataReader row) =>  new Vehicle(
            (int)row[nameof(IDVehicle)],
            VehicleTypeData.listOfVehicleType.First(x => x.TypeID == (int)row[nameof(VehicleType.TypeID)]), 
            VehicleBrandData.listOfVehicleBrands.First(x => x.BrandID == (int)row[nameof(VehicleBrand.BrandID)]), 
            (int)row[nameof(YearOfManufacture)], 
            (int)row[nameof(StartingKM)]
            );
    }
}
