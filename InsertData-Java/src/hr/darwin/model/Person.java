package hr.darwin.model;

import java.util.Objects;

public class Person {
    public Person(String fName, String lName, String phone, String driverLicenseId) {
        this.fName = fName;
        this.lName = lName;
        this.phone = phone;
        this.driverLicenseId = driverLicenseId;
    }

    public Person() {
    }

    @Override
    public String toString() {
        return "Person{" +
                "fName='" + fName + '\'' +
                ", lName='" + lName + '\'' +
                ", phone='" + phone + '\'' +
                ", driverLicenseId='" + driverLicenseId + '\'' +
                '}';
    }

    public String getfName() {
        return fName;
    }

    public String getlName() {
        return lName;
    }

    public String getPhone() {
        return phone;
    }

    public String getDriverLicenseId() {
        return driverLicenseId;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Person)) return false;
        Person person = (Person) o;
        return fName.equals(person.fName) &&
                lName.equals(person.lName) &&
                phone.equals(person.phone) &&
                driverLicenseId.equals(person.driverLicenseId);
    }

    @Override
    public int hashCode() {
        return Objects.hash(fName, lName, phone, driverLicenseId);
    }

    private String fName;
    private String lName;
    private String phone;
    private String driverLicenseId;

}
