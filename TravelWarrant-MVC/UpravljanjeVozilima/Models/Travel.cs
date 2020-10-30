using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using UpravljanjeVozilima.App_Data;

namespace UpravljanjeVozilima.Models
{
    public class Travel
    {
        public Travel(int idTravelWarrant, Person driver, Vehicle vehicle, string tripDuration, string roadDistance, string fuelPrice, TravelStatus status, string startCordinate, string startAddress, string endCordinate, string endAddress)
        {
            IDTravelWarrant = idTravelWarrant;
            Driver = driver;
            Vehicle = vehicle;
            TripDuration = tripDuration;
            RoadDistance = roadDistance;
            FuelPrice = fuelPrice;
            Status = status;
            StartLocation = new Location(startCordinate, startAddress);
            EndLocation = new Location(endCordinate, endAddress);
        }

        public Travel(int idTravelWarrant, Person driver, Vehicle vehicle, string tripDuration, string roadDistance, string fuelPrice, TravelStatus status)
        {
            IDTravelWarrant = idTravelWarrant;
            Driver = driver;
            Vehicle = vehicle;
            TripDuration = tripDuration;
            RoadDistance = roadDistance;
            FuelPrice = fuelPrice;
            Status = status;
        }

        //CONSTRUCTOR
        public Travel()
        {
         
        }
        

        //ATTRIBUTES
        public int IDTravelWarrant { get; set; }
        
        [Display(Name = "Vozač")]
        [Required(ErrorMessage = "* Polje vozač je obavezno")]
        public Person Driver { get; set; }
        
        [Display(Name = "Vozilo")]
        [Required(ErrorMessage = "* Polje vozilo je obavezno")]
        public Vehicle Vehicle { get; set; }
        
        [Display(Name = "Trajanje")]
        public string TripDuration { get; set; }
        
        [Display(Name = "Udaljenost")]
        public string RoadDistance { get; set; }
        
        [Display(Name = "Troškovi goriva")]
        public string FuelPrice { get; set; }
        
        [Display(Name = "Status")]
        public TravelStatus Status { get; set; }
        
        [Display(Name = "Početak puta")]
        [Required(ErrorMessage = "* Polje početak puta je obavezno")]
        public Location StartLocation { get; set; }
        
        [Display(Name = "Kraj puta")]
        [Required(ErrorMessage = "* Polje kraj puta je obavezno")]
        public Location EndLocation { get; set; }

        public static Travel ParseFromDb(SqlDataReader row) =>  new Travel(
            (int)row[nameof(IDTravelWarrant)],
            new Person
            {
                IDPerson = (int)row["PersonID"],
                FName = row["FName"].ToString(),
                LName = row["LName"].ToString()
            }, 
            new Vehicle
            {
                IDVehicle = (int)row["VehicleID"],
                Type = VehicleTypeData.listOfVehicleType.First(x => x.TypeID == (int)row["TypeID"]), 
                Brand = VehicleBrandData.listOfVehicleBrands.First(x => x.BrandID == (int)row["BrandID"]),
            }, 
            row["TripDuration"].ToString(), 
            row["RoadDistance"].ToString(), 
            row["FuelPrice"].ToString(),
            TravelStatusData.listOfTraveLStatuses.First(x => x.StatusID == (int)row["TravelWarrantStatusID"]),
            row["StartCordinate"].ToString(), 
            row["StartAddress"].ToString(),
            row["EndCordinate"].ToString(),
            row["EndAddress"].ToString()
        );
    }
}