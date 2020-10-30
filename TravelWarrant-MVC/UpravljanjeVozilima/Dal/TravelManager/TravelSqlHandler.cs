using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using UpravljanjeVozilima.Models;

namespace UpravljanjeVozilima.Dal.TravelManager
{
    public class TraveleSqlHandler : ITravelRepo
    {
        //CONSTS
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        
        //ATTRIBUTES
        private const string IDTravelWarrant = "@IDTravelWarrant";
        private const string PersonID = "@PersonID";
        private const string VehicleID = "@VehicleID";
        private const string TripDuration = "@TripDuration";
        private const string RoadDistance = "@RoadDistance";
        private const string FuelPrice = "@FuelPrice";
        private const string StartCordinate = "@StartCordinate";
        private const string StartAddress = "@StartAddress";
        private const string EndCordinate = "@EndCordinate";
        private const string EndAddress = "@EndAddress";
        private const string TravelWarrantStatusID = "@TravelWarrantStatusID";


        public IEnumerable<TravelStatus> GetAllTravelStatuses()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetAllTravelStatuses);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new TravelStatus
                            {
                                StatusID = (int)dr["IDStatus"],
                                Title = dr["Title"].ToString()
                            };
                        }
                    }
                }
            }
        }

        public int AddTravelWarrant(Travel travel)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlTransaction context = con.BeginTransaction())
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = nameof(AddTravelWarrant);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue(PersonID, travel.Driver.IDPerson);
                            cmd.Parameters.AddWithValue(VehicleID, travel.Vehicle.IDVehicle);
                            cmd.Parameters.AddWithValue(TripDuration, travel.TripDuration);
                            cmd.Parameters.AddWithValue(RoadDistance, travel.RoadDistance);
                            cmd.Parameters.AddWithValue(FuelPrice, travel.FuelPrice);

                            cmd.Parameters.AddWithValue(StartCordinate, travel.StartLocation.Cordinate);
                            cmd.Parameters.AddWithValue(StartAddress, travel.StartLocation.Address);

                            cmd.Parameters.AddWithValue(EndCordinate, travel.EndLocation.Cordinate);
                            cmd.Parameters.AddWithValue(EndAddress, travel.EndLocation.Address);

                            cmd.Parameters.AddWithValue(TravelWarrantStatusID, travel.Status.StatusID);
                            context.Commit();
                            return cmd.ExecuteNonQuery();
                        }
                        catch (Exception msg)
                        {
                            context.Rollback();
                            return -1;
                        }   
                    }
                }
            }
        }

        public IList<Travel> GetAllTravels()
        {
            IList<Travel> travels = new List<Travel>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetAllTravels);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            travels.Add(Travel.ParseFromDb(dr));
                        }
                    }
                }
            }
            return travels;
        }

        public Travel GetTravelWarrant(int id)
        {
            return GetAllTravels().ToList().SingleOrDefault(k => k.IDTravelWarrant == id);
        }

        public int DeleteTravelWarrant(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(DeleteTravelWarrant);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IDTravelWarrant, id);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}