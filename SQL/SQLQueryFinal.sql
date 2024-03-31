
CREATE TABLE Cars(
 [Car_Id] INT PRIMARY KEY IDENTITY(1,1),
 [Brand] NVARCHAR(40) NOT NULL,
 [Model] NVARCHAR(40) NOT NULL,
 [Year] NVARCHAR(16)  NOT NULL,
 [Fuel_Type] INT FOREIGN KEY REFERENCES FuelTypes(Fuel_Type_id),
 [Body_Type_Id] INT FOREIGN KEY REFERENCES BodyTypes(Body_Type_Id),
 [Color_Id] INT FOREIGN KEY REFERENCES Colors(Color_Id)
)


CREATE TABLE Users(
 [User_Id] INT PRIMARY KEY IDENTITY(1,1),
 [Name] NVARCHAR(40) NOT NULL,
 [Surname] NVARCHAR(40) NOT NULL,
 [Email] NVARCHAR(90) NOT NULL CHECK (Email LIKE('_%@__%.__%'))
)

CREATE TABLE ManufacturingCountries(
 [Country_Id] INT PRIMARY KEY IDENTITY(1,1),
 [Country_Name] NVARCHAR(50) NOT NULL
)


CREATE TABLE FuelTypes(
 [Fuel_Type_id] INT PRIMARY KEY IDENTITY(1,1),
 [Fuel_Type] NVARCHAR(40) NOT NULL
)


CREATE TABLE BodyTypes(
 [Body_Type_Id] INT PRIMARY KEY IDENTITY(1,1),
 [Body_Type]NVARCHAR(40) NOT NULL
)

CREATE TABLE Colors(
 [Color_Id] INT PRIMARY KEY IDENTITY(1,1),
 [Color_Name] NVARCHAR(40) NOT NULL
)

CREATE TABLE Sellers(
 [Seller_Id] INT PRIMARY KEY IDENTITY(1,1),
 [User_Id] INT FOREIGN KEY REFERENCES Users(User_Id) ,
 [Company_Name] NVARCHAR(40), --I don't write 'NOT NULL' because seller maybe have no company
 [Contact_Number] NVARCHAR(20) NOT NULL ,
 [Country_Id] INT FOREIGN KEY REFERENCES ManufacturingCountries(Country_Id), -- I don't write 'NOT NULL' because seller maybe have no ManufacturingCountries
 [Rating] INT NOT NULL
)


INSERT INTO Users(Name,Surname,Email) VALUES (N'Vagif',N'Aliev',N'Vagif.Aliev.17.04@gmail.com')

INSERT INTO ManufacturingCountries(Country_Name) VALUES (N'Italy')

INSERT INTO FuelTypes(Fuel_Type) VALUES (N'Petrol')

INSERT INTO BodyTypes(Body_Type) VALUES (N'SportCar')

INSERT INTO Colors(Color_Name) VALUES (N'Red')

INSERT INTO Sellers(User_Id,Company_Name,Contact_Number,Country_Id,Rating) VALUES (1,N'VG',N'994705555998',1,12)

INSERT INTO Cars(Brand,Model,Year,Fuel_Type,Body_Type_Id,Color_Id) VALUES (N'Lamborghini',N'Avenatdor',N'17.04.2007',1,1,1)
