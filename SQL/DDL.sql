CREATE TABLE VehicleMake
(
Id UNIQUEIDENTIFIER PRIMARY KEY,
Name varchar(30),
Abrv varchar (30)
)

CREATE TABLE VehicleModel
(
Id UNIQUEIDENTIFIER PRIMARY KEY,
Name varchar(30),
Abrv varchar (30),
MakeId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES VehicleMake(Id)
);