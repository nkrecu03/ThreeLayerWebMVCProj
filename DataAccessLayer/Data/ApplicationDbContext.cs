using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    //Here is my dbContext class that I will use to connect to SQL Server
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        //Here is my Employees DbSet for Employees table in SQL
        public DbSet<Employee> Employees { get; set; }
    }
}
