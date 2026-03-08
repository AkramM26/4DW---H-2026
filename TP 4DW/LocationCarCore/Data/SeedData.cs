using LocationManageCore.Data;
using LocationManageCore.Domains;
using LocationManagerCore.Domains;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

public static class SeedData
{
    public static async Task InitializeAsync(ApplicationDbContext context)
    {
        if (await context.Branches.AnyAsync())
        {
            return;
        }

        var branch1 = Branch.Create(true, "Montréal");
        var branch2 = Branch.Create(true, "Toronto");
        var branch3 = Branch.Create(true, "STHLocations");
        var branch4 = Branch.Create(true, "Gatineau");
        var branch5 = Branch.Create(true, "Edmonston");

        context.Branches.AddRange(branch1, branch2, branch3, branch4, branch5);

        var cars = new List<Car>
        {
            new Car {
                Id = Guid.NewGuid(),
                CarBrand="Toyota",
                CarModel="Corolla",
                Year=2020,
                Nickname="MTL-CAR-1",
                SerialNumber="SNMTL001",
                Mileage=50000,
                Color="White",
                Registration="MTL-001",
                EstimatedValue=18000,
                BranchId = branch1.Id
            },

            new Car {
                Id = Guid.NewGuid(),
                CarBrand="Honda",
                CarModel="Civic",
                Year=2021,
                Nickname="MTL-CAR-2",
                SerialNumber="SNMTL002",
                Mileage=42000,
                Color="Black",
                Registration="MTL-002",
                EstimatedValue=20000,
                BranchId = branch1.Id
            },

            new Car {
                Id = Guid.NewGuid(),
                CarBrand="Ford",
                CarModel="Escape",
                Year=2019,
                Nickname="QC-CAR-1",
                SerialNumber="SNQC001",
                Mileage=60000,
                Color="Blue",
                Registration="QC-001",
                EstimatedValue=17000,
                BranchId = branch2.Id
            },

            new Car {
                Id = Guid.NewGuid(),
                CarBrand="Hyundai",
                CarModel="Elantra",
                Year=2022,
                Nickname="QC-CAR-2",
                SerialNumber="SNQC002",
                Mileage=30000,
                Color="Gray",
                Registration="QC-002",
                EstimatedValue=21000,
                BranchId = branch2.Id
            },

            new Car {
                Id = Guid.NewGuid(),
                CarBrand="Nissan",
                CarModel="Altima",
                Year=2020,
                Nickname="TOR-CAR-1",
                SerialNumber="SNTOR001",
                Mileage=45000,
                Color="Red",
                Registration="TOR-001",
                EstimatedValue=19000,
                BranchId = branch3.Id
            },

            new Car {
                Id = Guid.NewGuid(),
                CarBrand="Kia",
                CarModel="Sportage",
                Year=2021,
                Nickname="TOR-CAR-2",
                SerialNumber="SNTOR002",
                Mileage=35000,
                Color="White",
                Registration="TOR-002",
                EstimatedValue=23000,
                BranchId = branch3.Id
            },

            new Car {
                Id = Guid.NewGuid(),
                CarBrand="Mazda",
                CarModel="CX5",
                Year=2020,
                Nickname="OTT-CAR-1",
                SerialNumber="SNOTT001",
                Mileage=42000,
                Color="Black",
                Registration="OTT-001",
                EstimatedValue=22000,
                BranchId = branch4.Id
            },

            new Car {
                Id = Guid.NewGuid(),
                CarBrand="Volkswagen",
                CarModel="Jetta",
                Year=2019,
                Nickname="OTT-CAR-2",
                SerialNumber="SNOTT002",
                Mileage=52000,
                Color="Silver",
                Registration="OTT-002",
                EstimatedValue=16000,
                BranchId = branch4.Id
            },

            new Car {
                Id = Guid.NewGuid(),
                CarBrand="Chevrolet",
                CarModel="Malibu",
                Year=2021,
                Nickname="LAV-CAR-1",
                SerialNumber="SNLAV001",
                Mileage=33000,
                Color="Blue",
                Registration="LAV-001",
                EstimatedValue=21000,
                BranchId = branch5.Id
            },

            new Car {
                Id = Guid.NewGuid(),
                CarBrand="Subaru",
                CarModel="Impreza",
                Year=2022,
                Nickname="LAV-CAR-2",
                SerialNumber="SNLAV002",
                Mileage=25000,
                Color="White",
                Registration="LAV-002",
                EstimatedValue=24000,
                BranchId = branch5.Id
            }
        };

        context.Cars.AddRange(cars);

        await context.SaveChangesAsync();
    }
}