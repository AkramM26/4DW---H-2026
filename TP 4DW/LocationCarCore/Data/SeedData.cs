using LocationManageCore.Data;
using LocationManageCore.Domains;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static async Task InitializeAsync(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (await context.Branches.AnyAsync())
        {
            return;
        }

        // Adresses
        var addresses = new List<Address>
        {
            new Address { Id = Guid.NewGuid(), StreetNumber = "123", StreetName = "Principale", CityName = "MTL", Province = "QC", Country = "Canada", PostalCode = "H1H 1H1" },
            new Address { Id = Guid.NewGuid(), StreetNumber = "456", StreetName = "Royale", CityName = "TOR", Province = "ON", Country = "Canada", PostalCode = "M5V 2L1" },
            new Address { Id = Guid.NewGuid(), StreetNumber = "789", StreetName = "Laurier", CityName = "STH", Province = "QC", Country = "Canada", PostalCode = "J2S 1A1" },
            new Address { Id = Guid.NewGuid(), StreetNumber = "101", StreetName = "Gare", CityName = "GAT", Province = "QC", Country = "Canada", PostalCode = "J8X 2B3" },
            new Address { Id = Guid.NewGuid(), StreetNumber = "202", StreetName = "Lac", CityName = "SHE", Province = "QC", Country = "Canada", PostalCode = "J1K 3M4" }
        };
        context.AddRange(addresses);

        // Succursales
        var branch1 = Branch.Create(true, "Montréal");
        var branch2 = Branch.Create(true, "Toronto");
        var branch3 = Branch.Create(true, "STHLocations");
        var branch4 = Branch.Create(true, "Gatineau");
        var branch5 = Branch.Create(true, "Edmonston");

        context.Branches.AddRange(branch1, branch2, branch3, branch4, branch5);

        // Voitures
        var cars = new List<Car>
        {
            new Car { Id = Guid.NewGuid(), Nickname = "MTL001", Status = "Actif", Availability = true, State = true, SerialNumber = "2T1BURHE0LC123456", Registration = "MTL001", CarBrand = "Toyota", CarModel = "Corolla2020", Year = 2020, Color = "Blanc", Mileage = 50000, EstimatedValue = 18000m, BranchId = branch1.BranchId },
            new Car { Id = Guid.NewGuid(), Nickname = "MTL002", Status = "Actif", Availability = false, State = true, SerialNumber = "19XFC2F8XME012345", Registration = "MTL002", CarBrand = "Honda", CarModel = "Civic2021", Year = 2021, Color = "Noir", Mileage = 42000, EstimatedValue = 20000m, BranchId = branch1.BranchId },
            new Car { Id = Guid.NewGuid(), Nickname = "QC001", Status = "Actif", Availability = true, State = true, SerialNumber = "1FMCU9GD4KUA56789", Registration = "QC001", CarBrand = "Ford", CarModel = "Escape2019", Year = 2019, Color = "Bleu", Mileage = 60000, EstimatedValue = 17000m, BranchId = branch2.BranchId },
            new Car { Id = Guid.NewGuid(), Nickname = "QC002", Status = "Actif", Availability = true, State = true, SerialNumber = "KMHD84LF1NU234567", Registration = "QC002", CarBrand = "Hyundai", CarModel = "Elantra2022", Year = 2022, Color = "Gris", Mileage = 30000, EstimatedValue = 21000m, BranchId = branch2.BranchId },
            new Car { Id = Guid.NewGuid(), Nickname = "TOR001", Status = "Actif", Availability = true, State = false, SerialNumber = "1N4BL4DV8LC098765", Registration = "TOR001", CarBrand = "Nissan", CarModel = "Altima2020", Year = 2020, Color = "Rouge", Mileage = 45000, EstimatedValue = 19000m, BranchId = branch3.BranchId },
            new Car { Id = Guid.NewGuid(), Nickname = "TOR002", Status = "Actif", Availability = false, State = true, SerialNumber = "KNDPMCAC9M7890123", Registration = "TOR002", CarBrand = "Kia", CarModel = "Sportage2021", Year = 2021, Color = "Blanc", Mileage = 35000, EstimatedValue = 23000m, BranchId = branch3.BranchId },
            new Car { Id = Guid.NewGuid(), Nickname = "OTT001", Status = "Actif", Availability = true, State = true, SerialNumber = "JM3KFACM0L0123456", Registration = "OTT001", CarBrand = "Mazda", CarModel = "CX52020", Year = 2020, Color = "Noir", Mileage = 42000, EstimatedValue = 22000m, BranchId = branch4.BranchId },
            new Car { Id = Guid.NewGuid(), Nickname = "OTT002", Status = "Actif", Availability = true, State = true, SerialNumber = "3VWC57BU9KM567890", Registration = "OTT002", CarBrand = "Volkswagen", CarModel = "Jetta2019", Year = 2019, Color = "Argent", Mileage = 52000, EstimatedValue = 16000m, BranchId = branch4.BranchId },
            new Car { Id = Guid.NewGuid(), Nickname = "LAV001", Status = "Actif", Availability = true, State = true, SerialNumber = "1G1ZD5ST4MF123456", Registration = "LAV001", CarBrand = "Chevrolet", CarModel = "Malibu2021", Year = 2021, Color = "Bleu", Mileage = 33000, EstimatedValue = 21000m, BranchId = branch5.BranchId },
            new Car { Id = Guid.NewGuid(), Nickname = "LAV002", Status = "Actif", Availability = true, State = true, SerialNumber = "4S3GTAB65N3123456", Registration = "LAV002", CarBrand = "Subaru", CarModel = "Impreza2022", Year = 2022, Color = "Blanc", Mileage = 25000, EstimatedValue = 24000m, BranchId = branch5.BranchId }
        };

        context.Cars.AddRange(cars);

        // Conducteurs
        var drivers = new List<Driver>
        {
            new Driver { Id = Guid.NewGuid(), LastName = "Tremblay", FirstName = "Jean-Marc", EmailAdress = "jeanmarc.tremblay@email.com", PhoneNumber = "(450) 555-1234", DriverLicenceNumber = "A1234-121299-12", AddressId = addresses[0].Id },
            new Driver { Id = Guid.NewGuid(), LastName = "Gagnon", FirstName = "Sophie", EmailAdress = "sophie.gagnon@outlook.com", PhoneNumber = "514-777-8888", DriverLicenceNumber = "B5678-150589-08", AddressId = addresses[1].Id },
            new Driver { Id = Guid.NewGuid(), LastName = "Beaulieu", FirstName = "Mathieu", EmailAdress = "mathieu.beaulieu@gmail.com", PhoneNumber = "(819) 333-9999", DriverLicenceNumber = "C9012-030401-15", AddressId = addresses[2].Id },
            new Driver { Id = Guid.NewGuid(), LastName = "Lavoie", FirstName = "Émilie", EmailAdress = "emilie.lavoie@protonmail.com", PhoneNumber = "438-222-3344", DriverLicenceNumber = "D3456-220795-19", AddressId = addresses[3].Id },
            new Driver { Id = Guid.NewGuid(), LastName = "Roy", FirstName = "Alexandre", EmailAdress = "alex.roy@live.ca", PhoneNumber = "(450) 666-7777", DriverLicenceNumber = "E7890-010203-22", AddressId = addresses[4].Id }
        };
        context.Drivers.AddRange(drivers);

        await context.SaveChangesAsync();

        // Locations
        var locations = new List<Location>
        {
            new Location { Id = Guid.NewGuid(), Status = true, Opening = new DateTime(2025, 10, 15, 9, 30, 0), PlanedClosing = new DateTime(2025, 10, 18, 17, 0, 0), OfficialClosing = null, CarId = cars[0].Id, DriverId = drivers[0].Id },
            new Location { Id = Guid.NewGuid(), Status = true, Opening = new DateTime(2025, 11, 2, 14, 0, 0), PlanedClosing = new DateTime(2025, 11, 5, 18, 30, 0), OfficialClosing = null, CarId = cars[1].Id, DriverId = drivers[1].Id },
            new Location { Id = Guid.NewGuid(), Status = false, Opening = new DateTime(2025, 9, 10, 8, 0, 0), PlanedClosing = new DateTime(2025, 9, 12, 20, 0, 0), OfficialClosing = new DateTime(2025, 9, 12, 19, 45, 0), CarId = cars[2].Id, DriverId = drivers[2].Id },
            new Location { Id = Guid.NewGuid(), Status = true, Opening = new DateTime(2025, 12, 20, 10, 15, 0), PlanedClosing = new DateTime(2025, 12, 23, 16, 0, 0), OfficialClosing = null, CarId = cars[3].Id, DriverId = drivers[3].Id },
            new Location { Id = Guid.NewGuid(), Status = true, Opening = new DateTime(2026, 1, 5, 13, 45, 0), PlanedClosing = new DateTime(2026, 1, 8, 21, 0, 0), OfficialClosing = null, CarId = cars[4].Id, DriverId = drivers[4].Id }
        };

        context.Locations.AddRange(locations);

        await context.SaveChangesAsync();

        // Roles
        var roles = new List<IdentityRole<Guid>>
        {
            new IdentityRole<Guid> { Name = TP1.Constantes.Roles.ADMIN, NormalizedName = TP1.Constantes.Roles.ADMIN.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole<Guid> { Name = TP1.Constantes.Roles.MANAGER, NormalizedName = TP1.Constantes.Roles.MANAGER.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole<Guid> { Name = TP1.Constantes.Roles.CLERK, NormalizedName = TP1.Constantes.Roles.CLERK.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole<Guid> { Name = TP1.Constantes.Roles.USER, NormalizedName = TP1.Constantes.Roles.USER.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() }
        };

        context.Roles.AddRange(roles);

        await context.SaveChangesAsync();
    }
}