package hr.darwin.sql;

import com.sun.corba.se.spi.activation.ServerAlreadyActiveHolder;
import hr.darwin.dal.IRepo;
import hr.darwin.model.Person;
import hr.darwin.model.Vehicle;

import javax.sql.DataSource;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class SqlRepo implements IRepo {

    public static final String FIRST_NAME = "FName";
    public static final String LAST_NAME = "LName";
    public static final String PHONE = "Phone";
    public static final String LICENSE_NUMBER = "DriverLicenseID";
    public static final String MF_YEAR = "YearOfManufacture";
    public static final String ST_KM = "StartingKM";
    public static final String BRAND = "Brand";
    public static final String TYPE = "Type";

    private static final String NEW_VEHICLE = "{ CALL AddVehicle(?,?,?,?)}";
    private static final String NEW_BRAND = "{ CALL CreateCarBrand(?,?)}";
    private static final String NEW_TYPE = "{ CALL CreateCarType(?,?)}";
    private static final String NEW_PERSON = "{ CALL AddPerson(?,?,?,?,?)}";
    private static final String GET_VEHICLES = "{ CALL GetAllVehicles()}";
    private static final String GET_PERSONS = "{ CALL GetAllPersons()}";

    @Override
    public int addBrand(String brand) throws SQLException {
        DataSource dataSource = DataSourceSingleton.getInstance();
        try (Connection con = dataSource.getConnection();
             CallableStatement stmt = con.prepareCall(NEW_BRAND)) {

            con.setAutoCommit(false);

            stmt.setString(1, brand);
            stmt.registerOutParameter(2, Types.INTEGER);
            stmt.executeUpdate();

            con.commit();
            con.setAutoCommit(true);

            return stmt.getInt(2);
        }
    }

    @Override
    public int addType(String type) throws SQLException {
        DataSource dataSource = DataSourceSingleton.getInstance();
        try (Connection con = dataSource.getConnection();
             CallableStatement stmt = con.prepareCall(NEW_TYPE)) {

            con.setAutoCommit(false);

            stmt.setString(1, type);
            stmt.registerOutParameter(2, Types.INTEGER);
            stmt.executeUpdate();

            con.commit();
            con.setAutoCommit(true);

            return stmt.getInt(2);
        }
    }

    @Override
    public void addVehicle(List<Vehicle> vehicles) throws SQLException {
        DataSource dataSource = DataSourceSingleton.getInstance();
        try (Connection con = dataSource.getConnection();
             CallableStatement stmt = con.prepareCall(NEW_VEHICLE)) {

            con.setAutoCommit(false);

            for (Vehicle vehicle : vehicles) {
                stmt.setInt(1, addType(vehicle.getType().trim()));
                stmt.setInt(2, addBrand(vehicle.getBrand().trim()));
                stmt.setInt(3, vehicle.getManufactureYear());
                stmt.setInt(4, vehicle.getStartKM());
                stmt.executeUpdate();
            }
            con.commit();
            con.setAutoCommit(true);
        }
    }

    @Override
    public void addPerson(List<Person> persons) throws SQLException {
        DataSource dataSource = DataSourceSingleton.getInstance();
        try (Connection con = dataSource.getConnection();
             CallableStatement stmt = con.prepareCall(NEW_PERSON)) {

            con.setAutoCommit(false);

            for (Person person : persons) {
                stmt.setString(1, person.getfName().trim());
                stmt.setString(2, person.getlName().trim());
                stmt.setString(3, person.getPhone().trim());
                stmt.setString(4, person.getDriverLicenseId().trim());
                stmt.registerOutParameter(5, Types.INTEGER);
                stmt.executeUpdate();
            }
            con.commit();
            con.setAutoCommit(true);
        }
    }

    @Override
    public List<Vehicle> getVehicles() throws SQLException {
        List<Vehicle> vehicles = new ArrayList<>();
        DataSource dataSource = DataSourceSingleton.getInstance();
        try(Connection con = dataSource.getConnection();
            CallableStatement stmt = con.prepareCall(GET_VEHICLES);
            ResultSet rs = stmt.executeQuery()) {

            while(rs.next()) {
                vehicles.add(new Vehicle(
                        rs.getInt(MF_YEAR),
                        rs.getInt(ST_KM),
                        rs.getString(TYPE),
                        rs.getString(BRAND)));
            }
        }

        return vehicles;
    }

    @Override
    public List<Person> getPersons() throws SQLException {
        List<Person> persons = new ArrayList<>();
        DataSource dataSource = DataSourceSingleton.getInstance();
        try(Connection con = dataSource.getConnection();
            CallableStatement stmt = con.prepareCall(GET_PERSONS);
            ResultSet rs = stmt.executeQuery()) {

            while(rs.next()) {
                persons.add(new Person(
                        rs.getString(FIRST_NAME),
                        rs.getString(LAST_NAME),
                        rs.getString(PHONE),
                        rs.getString(LICENSE_NUMBER)));
            }
        }

        return persons;
    }
}
