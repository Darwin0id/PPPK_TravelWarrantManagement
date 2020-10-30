package hr.darwin.model;

import java.util.Objects;

public class Vehicle {

    public Vehicle(int yearOfManufacture, int startingKM, String type, String brand) {
        this.yearOfManufacture = yearOfManufacture;
        this.startingKM = startingKM;
        this.type = type;
        this.brand = brand;
    }

    public Vehicle() {

    }

    public int getManufactureYear() { return yearOfManufacture; }
    public int getStartKM() { return startingKM; }
    public String getType() {
        return type;
    }
    public String getBrand() {
        return brand;
    }


    @Override
    public String toString() {
        return "Vehicle{" +
                "yearOfManufacture=" + yearOfManufacture +
                ", startingKM=" + startingKM +
                ", type='" + type + '\'' +
                ", brand='" + brand + '\'' +
                '}';
    }


    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Vehicle)) return false;
        Vehicle vehicle = (Vehicle) o;
        return yearOfManufacture == vehicle.yearOfManufacture &&
                startingKM == vehicle.startingKM &&
                type.equals(vehicle.type) &&
                brand.equals(vehicle.brand);
    }

    @Override
    public int hashCode() {
        return Objects.hash(yearOfManufacture, startingKM, type, brand);
    }

    private int yearOfManufacture;
    private int startingKM;
    private String type;
    private String brand;
}
