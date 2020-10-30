using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace UpravljanjeVozilima.Models
{
    public class VehicleType
    {
        
        //PROPERTIES
        public int TypeID { get; set; }
        [Display(Name = "Tip")]
        public string Title { get; set; }
        
        //TO STRING()
        public override string ToString() => $"{Title}";
        
        //PARSE PERSON FROM DATABASE
        public static VehicleType ParseFromDb(SqlDataReader row) => new VehicleType
        {
                TypeID = (int) row[nameof(TypeID)],
                Title = row[nameof(Title)].ToString(),
        };
    }
}