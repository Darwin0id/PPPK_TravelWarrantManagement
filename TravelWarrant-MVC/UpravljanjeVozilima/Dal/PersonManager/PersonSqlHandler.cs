using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using UpravljanjeVozilima.Models;

namespace UpravljanjeVozilima.Dal.PersonManager
{
   public class PersonSqlHandler : IPersonRepo
    {
        //CONSTS
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        
        //ATTRIBUTES
        private const string IDPerson = "@IDPerson";
        private const string FName = "@FName";
        private const string Lname = "@Lname";
        private const string Phone = "@Phone";
        private const string DriverLicenseID = "@DriverLicenseID";
        
        //METHODS
        private static readonly string GET_ALL_PERSONS = $"SELECT * FROM Person WHERE Active=1";
        private static readonly string DELETE_PERSON = $"DELETE FROM Person WHERE IDPerson={IDPerson}";
        private static readonly string ADD_PERSON = $"INSERT INTO Person VALUES({FName}, {Lname}, {Phone}, {DriverLicenseID}, 1)";
        private static readonly string UPDATE_PERSON = $"UPDATE Person SET {nameof(Person.FName)}={FName}, {nameof(Person.LName)}={Lname}, {nameof(Person.Phone)}={Phone}, {nameof(Person.DriverLicenseId)}={DriverLicenseID} WHERE {nameof(Person.IDPerson)}={IDPerson}";

        public IList<Person> GetAllPersons()
        {
            IList<Models.Person> people = new List<Models.Person>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand command = new SqlCommand(GET_ALL_PERSONS, con);
                using(SqlDataReader row = command.ExecuteReader())
                {
                    while (row.Read())
                    {
                        people.Add(Models.Person.ParseFromDb(row));
                    }
                }
            }
            return people;
        }

        public int DeletePerson(int idPerson)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(DELETE_PERSON, con))
                {
                    command.Parameters.AddWithValue(IDPerson, idPerson);
                    command.ExecuteNonQuery();
                }
                con.Close();
            }
            return 0;
        }

        public int AddPerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(ADD_PERSON, con))
                {
                    command.Parameters.AddWithValue(FName, person.FName);  
                    command.Parameters.AddWithValue(Lname, person.LName);  
                    command.Parameters.AddWithValue(Phone, person.Phone);  
                    command.Parameters.AddWithValue(DriverLicenseID, person.DriverLicenseId);  
            
                    command.ExecuteNonQuery();
                }
                con.Close();
            }
            return 0;
        }

        public Person GetPerson(int id)
        {
            return GetAllPersons().ToList().SingleOrDefault(k => k.IDPerson == id);
        }

        public int EditPerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(UPDATE_PERSON, con))
                {
                    command.Parameters.AddWithValue(FName, person.FName);  
                    command.Parameters.AddWithValue(Lname, person.LName);  
                    command.Parameters.AddWithValue(Phone, person.Phone);  
                    command.Parameters.AddWithValue(DriverLicenseID, person.DriverLicenseId);  
                    command.Parameters.AddWithValue(IDPerson, person.IDPerson);  
            
                    command.ExecuteNonQuery();
                }
                con.Close();
            }
            return 0;
        }
    }
}