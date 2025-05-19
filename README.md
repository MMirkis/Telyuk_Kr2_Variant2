Scaffold-DbContext "Server=1misa;Database=Variant2;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Login NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    RegistrationDate DATE NOT NULL,
    FullName NVARCHAR(200) NOT NULL,
    PhoneNumber NVARCHAR(20)
);

CREATE TABLE Inventory (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Article NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(200) NOT NULL,
    Type NVARCHAR(100),
    Description NVARCHAR(MAX),
    ReleaseDate DATE,
    Status INT NOT NULL, -- 0: Available, 1: Issued, 2: Maintenance
    UserId INT FOREIGN KEY REFERENCES Users(Id)
);
