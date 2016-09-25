using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P2000Maintenance.Models
{
    using P2000;
    using System.Data.Entity;
    public class Context : BaseContext
    {
        public Context()
        {
            Database.SetInitializer<Context>(new DropCreateDatabaseAlways<Context>()); //new DropCreateDatabaseIfModelChanges<Context>());
        }

        public DbSet<P2000DBHardware> P2000Devices { get; set; }
        public DbSet<Partition> Partitions { get; set; }
        public DbSet<Panel> Panels { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<Input> Inputs { get; set; }
        public DbSet<Output> Outputs { get; set; }
        public DbSet<Workstation> Workstations { get; set; }

        
    }
}