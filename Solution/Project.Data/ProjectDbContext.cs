using System.Data.Entity.ModelConfiguration.Conventions;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public class ProjectDbContext//:DbContext
    {
        //public DbSet<Vehicle> Vehicles { get; set; }
        //public DbSet<VehicleType> VehicleTypes { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // Table names match entity names by default (don't pluralize)
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    // Globally disable the convention for cascading deletes
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //}
    }
}
