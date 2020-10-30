using System.ComponentModel.DataAnnotations;

namespace UpravljanjeVozilima.Models
{
    public class TravelStatus
    {
        //PROPERTIES
        public int StatusID { get; set; }
        
        [Display(Name = "Status")]
        public string Title { get; set; }
    }
}