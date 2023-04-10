using LockerAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace LockerAPI.DataContext
{
    public class LockerDataContext : DbContext
    {
        public LockerDataContext(DbContextOptions<LockerDataContext> options) : base(options)
        {
        }

        public DbSet<Company> Companys { get; set; }
        public DbSet<User> Users { get; set; }

        //}
    }
}
