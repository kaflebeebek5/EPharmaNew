CREATE TABLE tblMedicine(
Id INT PRIMARY KEY IDENTITY(1,1),
Name NVARCHAR(50) ,
UserId NVARCHAR(450) FOREIGN KEY(UserId) REFERENCES [Identity].Users(Id),
Description NVARCHAR(MAX),
ManufactureDate DATETIME,
ExpiryDate DATETIME,
QuantityAvailable INT,
ImagePath NVARCHAR(MAX)
)
CREATE TABLE tblDoctor
(
ID INT PRIMARY KEY IDENTITY(1,1),
Name NVARCHAR(100),
Specialist NVARCHAR(100),
Address NVARCHAR(200),
PhoneNumber NVARCHAR(15),
Email NVARCHAR(100),
GenderId Int foreign key(GenderId) references Genders(Id)
) 
CREATE TABLE tblBillEntry
(
  Id INT PRIMARY KEY IDENTITY(1,1),
  BillNumber NVARCHAR(100),
  UserId NVARCHAR(450) FOREIGN KEY(UserId) REFERENCES [Identity].Users(Id),
  DoctorId INT FOREIGN KEY(DoctorId) REFERENCES tblDoctor(Id),
  CreatedDate DATETIME,
  StatusId INT FOREIGN KEY(StatusId) REFERENCES tblStatus(Id)
)
CREATE TABLE tblBillSetup
(
Id INT PRIMARY KEY IDENTITY(1,1),
Suffix nvarchar(5),
Prefix nvarchar(5),
StartingNumber INT 
)
CREATE TABLE tblUserMedicine(
Id INT PRIMARY KEY IDENTITY(1,1),
BillId INT FOREIGN KEY(BillId) REFERENCES tblBillEntry(Id),
MedId INT FOREIGN KEY(MedId) REFERENCES tblMedicine(Id),
Timing NVARCHAR(200),
Quantity INT
)

INSERT INTO tblBillSetup(Suffix,Prefix,StartingNumber)
VALUES('AA','BB','1')


CREATE TABLE TblUserOrder(
Id INT PRIMARY KEY IDENTITY(1,1),
ProductId int FOREIGN KEY REFERENCES tblMedicine(Id),
PrescriptionImage NVARCHAR(MAX),
UserId Nvarchar(450) REFERENCES [Identity].Users(Id),
PhoneNumber Nvarchar(20),
BillingAddress Nvarchar(400)
)
CREATE Table tblUserOTP
(
Id int primary key identity(1,1),
PhoneNumber Nvarchar(20),
OTP NVARCHAR(20),
ISUSED bit,
CreatedDate DATETIME
)
