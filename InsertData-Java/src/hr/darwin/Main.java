package hr.darwin;

import hr.darwin.dal.IRepo;
import hr.darwin.dal.RepoFactory;
import hr.darwin.model.Person;
import hr.darwin.model.Vehicle;
import hr.darwin.parser.Parser;

import java.io.FileNotFoundException;
import java.sql.SQLException;
import java.util.List;

public class Main {
    private static final String CSV_PERSON = "../../Repository/Person.csv";
    private static final String CSV_VEHICLE = "../../Repository/Vehicle.csv";
    private static List<Person> personList;
    private static List<Vehicle> vehicleList;
    private static IRepo repo;

    public static void main(String[] args) {
        try {
            init();
            loadToCSV();
            saveToDB(personList, vehicleList);
            compareData();
            System.out.println("Complete");
        } catch (Exception e) {
            System.out.println("Error " + e.getMessage());
        }
    }

    private static void compareData() throws SQLException {
        List<Person> loadedPersonList = repo.getPersons();
        List<Vehicle> loadedVehicleList = repo.getVehicles();

        for (int i=0; i<personList.size(); i++) {
            if (!loadedPersonList.contains(personList.get(i))) {
                System.out.println("Person error");
                return;
            }

            if(i == personList.size()-1) {
                System.out.println("Person successfully loaded");
            }
        }

        for (int i=0; i<vehicleList.size(); i++) {
            if (!loadedVehicleList.contains(vehicleList.get(i))) {
                System.out.println("Person error");
                return;
            }

            if(i == vehicleList.size()-1) {
                System.out.println("Vehicle successfully loaded");
            }
        }
    }

    private static void loadToCSV() throws FileNotFoundException {
        personList = Parser.parseCSV(CSV_PERSON, new Person());
        vehicleList = Parser.parseCSV(CSV_VEHICLE, new Vehicle());
    }

    private static void saveToDB(List<Person> personList, List<Vehicle> vehicleList) throws SQLException {
        repo.addPerson(personList);
        repo.addVehicle(vehicleList);
    }

    private static void init() throws Exception {
        repo = RepoFactory.getRepository();
    }
}