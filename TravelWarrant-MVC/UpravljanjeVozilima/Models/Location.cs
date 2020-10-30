using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using UpravljanjeVozilima.App_Data;

namespace UpravljanjeVozilima.Models
{
    public class Location
    {
        public Location(string cordinate, string address)
        {
            Cordinate = cordinate;
            Address = address;
        }

        public Location()
        {
                
        }

        //ATTRIBUTES
        public string Address { get; set; }
        public string Cordinate { get; set; }
    }
}