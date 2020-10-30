package hr.darwin.dal;

import hr.darwin.model.Person;
import hr.darwin.model.Vehicle;

import java.sql.SQLException;
import java.util.List;

public interface IRepo {
    int addBrand(String brand) throws SQLException;
    int addType(String type) throws SQLException;
    void addVehicle(List<Vehicle> vehicles) throws SQLException;
    void addPerson(List<Person> persons) throws SQLException;
    List<Vehicle> getVehicles() throws SQLException;
    List<Person> getPersons() throws SQLException;
}
