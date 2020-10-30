using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace UpravljanjeVozilima.Models
{
    public class VehicleBrand
    {
        //PROPERTIES
        public int BrandID { get; set; }
        
        [Display(Name = "Brand")]
        public string Title { get; set; }
        
        //TO STRING()
        public override string ToString() => $"{Title}";
        
        //PARSE PERSON FROM DATABASE
        public static VehicleType ParseFromDb(SqlDataReader row) => new VehicleType
        {
            TypeID = (int) row[nameof(BrandID)],
            Title = row[nameof(Title)].ToString(),
        };
    }
}