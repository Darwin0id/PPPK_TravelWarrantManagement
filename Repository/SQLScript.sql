USE master;
GO
DROP DATABASE IF EXISTS FlotaVozilaDB;
GO
CREATE DATABASE FlotaVozilaDB;
GO
USE FlotaVozilaDB;
GO

/*** TABLE ***/
CREATE TABLE Person (
    IDPerson INT PRIMARY KEY IDENTITY,
    FName NVARCHAR(50) NOT NULL,
    Lname NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(50) NOT NULL,
    DriverLicenseID NVARCHAR(50) NOT NULL,
    Active BIT DEFAULT 1 NOT NULL
)

GO

CREATE PROC GetAllPersons
AS
SELECT * FROM Person WHERE Active = 1

GO

/*** TRIGERS ***/
CREATE TRIGGER PersonDelete ON Person INSTEAD OF DELETE
AS
UPDATE Person SET Active=0 WHERE IDPerson=(SELECT IDPerson FROM Deleted)

GO

CREATE PROC AddPerson
    @fName NVARCHAR(50),
    @lName NVARCHAR(50),
    @phone NVARCHAR(50),
    @driverLicenseId NVARCHAR(50),
    @personID int out
AS
BEGIN
    INSERT INTO Person VALUES (@fName, @lName, @phone, @driverLicenseId, 1)
    SET @personID = SCOPE_IDENTITY()
END

GO

/*** DUMMY DATA ***/
-- INSERT INTO Person VALUES ('Matija', 'Kolaric', '(099)554495', '12345657', 1)

GO
/**********************************************/

/*** TABLE ***/
CREATE TABLE VehicleBrand (
    IDBrand INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(50) NOT NULL
)

GO

/*** DUMMY DATA ***/
--INSERT INTO VehicleBrand VALUES ('BMW'), ('Audi'), ('Mercedes'), ('Jaguar')

GO

/*** SP FOR TYPE ***/
CREATE PROC GetAllVehicleBrands
AS
SELECT * FROM VehicleBrand

GO

CREATE PROC CreateCarBrand
    @brandTitle nvarchar(100),
    @brandID INT OUT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM VehicleBrand WHERE Title=@brandTitle)
        BEGIN
            INSERT INTO VehicleBrand VALUES (@brandTitle);
            SET @brandID = SCOPE_IDENTITY()
        END
    ELSE
        BEGIN
            SET @brandID = (SELECT IDBrand FROM VehicleBrand WHERE Title=@brandTitle)
        END
END


-- SELECT * FROM VehicleBrand
-- SELECT * FROM Vehicle
-- DECLARE @output INT EXEC CreateCarBrand 'Test2', @brandID = @output OUT SELECT @output

/**********************************************/

GO

/**********************************************/

/*** TABLE ***/
CREATE TABLE VehicleType (
    IDType INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(50) NOT NULL
)

GO

/*** DUMMY DATA ***/
--INSERT INTO VehicleType VALUES ('Sedan'), ('Coupe'), ('Sports car'), ('Hatchback')

GO

/*** SP FOR TYPE ***/
CREATE PROC GetAllVehicleTypes
AS
SELECT * FROM VehicleType

GO

CREATE PROC CreateCarType
    @typeTitle nvarchar(100),
    @typeID INT OUT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM VehicleType WHERE Title=@typeTitle)
        BEGIN
            INSERT INTO VehicleType VALUES (@typeTitle);
            SET @typeID = SCOPE_IDENTITY()
        END
    ELSE
        BEGIN
            SET @typeID = (SELECT IDType FROM VehicleType WHERE Title=@typeTitle)
        END
END

-- SELECT * FROM VehicleType
-- EXEC CreateCarType 'Sedan'
-- DECLARE @output INT EXEC CreateCarType 'Sports car2', @typeID = @output OUT SELECT @output

/**********************************************/

GO

/**********************************************/

/*** TABLE ***/
CREATE TABLE Vehicle (
    IDVehicle INT PRIMARY KEY IDENTITY,
    TypeID INT FOREIGN KEY REFERENCES VehicleType(IDType),
    BrandID INT FOREIGN KEY REFERENCES VehicleBrand(IDBrand),
    YearOfManufacture INT NOT NULL,
    StartingKM INT NOT NULL,
    Active BIT DEFAULT 1
)

GO

/*** TRIGERS ***/
CREATE TRIGGER VehicleDelete ON Vehicle INSTEAD OF DELETE
AS
UPDATE Vehicle SET Active=0 WHERE IDVehicle=(SELECT IDVehicle FROM Deleted)

GO

/*** DUMMY DATA ***/
-- INSERT INTO Vehicle VALUES (1, 1, 2002, 10500, 1)

GO

/*** SP FOR VEHICLES ***/
CREATE PROC GetAllVehicles
AS
SELECT V.*, VB.Title AS Brand, VT.Title AS Type FROM Vehicle AS V
INNER JOIN VehicleBrand AS VB ON V.BrandID=VB.IDBrand
INNER JOIN VehicleType AS VT ON V.TypeID=VT.IDType
WHERE V.Active=1

GO

CREATE PROC AddVehicle
    @typeid int,
    @brandid int,
    @yearofmanufacture int,
    @startingkm int
AS
BEGIN
    INSERT INTO Vehicle VALUES (@typeid, @brandid, @yearofmanufacture, @startingkm, 1)
END

GO

CREATE PROC EditVehicle
    @idvehicle int,
    @typeid int,
    @brandid int,
    @yearofmanufacture int,
    @startingkm int
AS
BEGIN
    UPDATE Vehicle SET TypeID=@typeid, BrandID=@brandid, YearOfManufacture=@yearofmanufacture, StartingKM=@startingkm WHERE IDVehicle=@idvehicle
END

GO

CREATE PROC DeleteVehicle
	@idVehicle int
AS
DELETE FROM Vehicle WHERE IDVehicle = @idVehicle

/**********************************************/

GO

/**********************************************/

/*** TABLE ***/
CREATE TABLE TravelWarrantStatus (
    IDStatus INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(100)
)

GO

/*** DUMMY DATA ***/
INSERT INTO TravelWarrantStatus VALUES ('<span class="badge badge-warning">Budući</span>'), ('<span class="badge badge-success">Aktivan</span>'), ('<span class="badge badge-danger">Zatvoren</span>')

GO

/*** SP FOR TYPE ***/
CREATE PROC GetAllTravelStatuses
AS
SELECT * FROM TravelWarrantStatus
/**********************************************/

GO

/**********************************************/

/*** TABLE ***/
CREATE TABLE TravelWarrant (
    IDTravelWarrant INT PRIMARY KEY IDENTITY,
    PersonID INT FOREIGN KEY REFERENCES Person(IDPerson),
    VehicleID INT FOREIGN KEY REFERENCES Vehicle(IDVehicle),
    TripDuration NVARCHAR(10),
    RoadDistance NVARCHAR(10),
    FuelPrice NVARCHAR(10),
    StartCordinate NVARCHAR(40),
    StartAddress NVARCHAR(40),
    EndCordinate NVARCHAR(40),
    EndAddress NVARCHAR(40),
    TravelWarrantStatusID INT FOREIGN KEY REFERENCES TravelWarrantStatus(IDStatus),
    Active BIT DEFAULT 1
)

GO

/*** TRIGERS ***/
CREATE TRIGGER TravelWarrantDelete ON TravelWarrant INSTEAD OF DELETE
AS
UPDATE TravelWarrant SET Active=0 WHERE IDTravelWarrant=(SELECT IDTravelWarrant FROM Deleted)

GO

/*** SP FOR TYPE ***/
CREATE PROC AddTravelWarrant
    @personid int,
    @vehicleid int,
    @tripduration nvarchar(30),
    @roaddistance nvarchar(30),
    @fuelprice nvarchar(30),
    @startcordinate nvarchar(30),
    @startaddress nvarchar(30),
    @endcordinate nvarchar(30),
    @endaddress nvarchar(30),
    @travelwarrantstatusid int
AS
BEGIN
    INSERT INTO TravelWarrant VALUES (@personid, @vehicleid, @tripduration, @roaddistance, @fuelprice, @startcordinate, @startaddress, @endcordinate, @endaddress, @travelwarrantstatusid, 1)
END

GO

CREATE PROC GetAllTravels
AS
SELECT TW.*, P.FName, P.Lname, V.BrandID, V.TypeID FROM TravelWarrant AS TW INNER JOIN Person AS P
ON TW.PersonID=P.IDPerson INNER JOIN Vehicle AS V
ON TW.VehicleID=V.IDVehicle
WHERE TW.Active=1

GO

CREATE PROC DeleteTravelWarrant
	@idtravelwarrant int
AS
DELETE FROM TravelWarrant WHERE IDTravelWarrant = @idtravelwarrant
GO

/**********************************************/

