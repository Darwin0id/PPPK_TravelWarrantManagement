using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace UpravljanjeVozilima.Models
{
    public class Person
    {
        //CONSTRUCTOR
        public Person(int idPerson, string fName, string lName, string phone, string driverLicenseId)
        {
            IDPerson = idPerson;
            FName = fName;
            LName = lName;
            Phone = phone;
            DriverLicenseId = driverLicenseId;
        }

        public Person()
        {
                
        }

        //ATTRIBUTES
        public int IDPerson { get; set; }
        
        [Display(Name = "Ime")]
        [Required(ErrorMessage = "* Polje Ime je obavezno")]
        [StringLength(10, ErrorMessage = "Maksimalna duljina imena je 10 znakova")]
        public string FName { get; set; }
        
        [Display(Name = "Prezime")]
        [Required(ErrorMessage = "* Polje Prezime je obavezno")]
        [StringLength(20, ErrorMessage = "Maksimalna duljina imena je 20 znakova")]
        public string LName { get; set; }
        
        [Display(Name = "Telefon")]       
        [Required(ErrorMessage = "* Polje Broj mobitela je obavezno")]
        public string Phone { get; set; }
        
        [Display(Name = "ID Vozačke")]
        [Required(ErrorMessage = "* Polje ID vozačke je obavezno")]
        [StringLength(8, ErrorMessage = "Maksimalna duljina ID je 8 znakova")]
        public string DriverLicenseId { get; set; }
        
        //OVERRIDE
        public override string ToString() => $"{FName} {LName}";
        
        [Display(Name = "Prezime i ime")]
        public string FullName
        {
            get { return $"{LName} {FName}"; }
        }
        
        //PARSE PERSON FROM DATABASE
        internal static Person ParseFromDb(SqlDataReader row) =>  new Person(
            (int) row[nameof(IDPerson)], 
            row[nameof(FName)].ToString(), 
            row[nameof(LName)].ToString(), 
            row[nameof(Phone)].ToString(), 
            row[nameof(DriverLicenseId)].ToString());
    }
}