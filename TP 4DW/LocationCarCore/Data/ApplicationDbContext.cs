using LocationManageCore.Domains;
using LocationManagerCore.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace LocationManageCore.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
//}

//using LocationManageCore.Domains;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using TP1.Models;
//using TP1.Models.Branch;

//namespace TP1.Data
//{
//    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options) { }

//        public DbSet<Car> Cars { get; set; }
//        public DbSet<Driver> Drivers { get; set; }
//        public DbSet<Location> Locations { get; set; }
//        public DbSet<Address> Addresses { get; set; }
//        public DbSet<Note> Notes { get; set; }
//    }
//}