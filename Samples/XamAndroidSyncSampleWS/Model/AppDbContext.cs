using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XamAndroidSyncSampleWS.Model
{
    public class AppDbContext : DbContext
    {
        const string connString = "server=localhost;port=3306;database=sync_test;uid=root;password=VolWork1";

        public AppDbContext() { }

        DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
