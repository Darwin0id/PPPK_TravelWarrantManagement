using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using UpravljanjeVozilima.Models;

namespace UpravljanjeVozilima.Dal.VehiclesManager
{
    public class VehicleSqlHandler : IVehicleRepo
    {
        //CONSTS
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        
        //ATTRIBUTES
        private const string IDVehicle = "@IDVehicle";
        private const string TypeID = "@TypeID";
        private const string BrandID = "@BrandID";
        private const string YearOfManufacture = "@YearOfManufacture";
        private const string StartingKM = "@StartingKM";
        
        public IEnumerable<VehicleType> GetAllVehicleTypes()
        {
            using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = nameof(GetAllVehicleTypes);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                yield return new VehicleType
                                {
                                    TypeID = (int)dr["IDType"],
                                    Title = dr["Title"].ToString()
                                };
                            }
                        }
                    }
            }
        }

        public IEnumerable<VehicleBrand> GetAllVehicleBrands()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetAllVehicleBrands);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new VehicleBrand
                            {
                                BrandID = (int)dr["IDBrand"],
                                Title = dr["Title"].ToString()
                            };
                        }
                    }
                }
            }
        }

        public IList<Vehicle> GetAllVehicles()
        {
            IList<Vehicle> vehicles = new List<Vehicle>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(GetAllVehicles);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            vehicles.Add(Vehicle.ParseFromDb(dr));
                        }
                    }
                }
              
               
            }
            return vehicles;
        }

        public int AddVehicle(Vehicle vehicle)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(AddVehicle);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(TypeID, vehicle.Type.TypeID);
                    cmd.Parameters.AddWithValue(BrandID, vehicle.Brand.BrandID);
                    cmd.Parameters.AddWithValue(YearOfManufacture, vehicle.YearOfManufacture);
                    cmd.Parameters.AddWithValue(StartingKM, vehicle.StartingKM);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int DeleteVehicle(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(DeleteVehicle);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IDVehicle, id);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public Vehicle GetVehicle(int id)
        {
            return GetAllVehicles().ToList().SingleOrDefault(k => k.IDVehicle == id);
        }

        public int EditVehicle(Vehicle vehicle)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(EditVehicle);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IDVehicle, vehicle.IDVehicle);
                    cmd.Parameters.AddWithValue(TypeID, vehicle.Type.TypeID);
                    cmd.Parameters.AddWithValue(BrandID, vehicle.Brand.BrandID);
                    cmd.Parameters.AddWithValue(YearOfManufacture, vehicle.YearOfManufacture);
                    cmd.Parameters.AddWithValue(StartingKM, vehicle.StartingKM);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}