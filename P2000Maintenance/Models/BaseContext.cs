using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace P2000Maintenance.Models
{
    public class BaseContext : DbContext
    {
        public  BaseContext()
        {

        }
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }


        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
                ? HttpContext.Current.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseModel)entity.Entity).DateCreated = DateTime.UtcNow;
                    ((BaseModel)entity.Entity).UserCreated = currentUsername;
                }

                ((BaseModel)entity.Entity).DateModified = DateTime.UtcNow;
                ((BaseModel)entity.Entity).UserModified = currentUsername;
            }
        }
    }
}