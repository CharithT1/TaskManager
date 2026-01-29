using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain;
using TaskManagement.Domain.Common;

namespace TaskManagement.Persistance
{
    public class TaskManagementDbContext:DbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
            :base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagementDbContext).Assembly);
        }

        public virtual async Task<int> SaveChangesAsync(String username = "SYSTEM")
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.ModifiedDate = DateTime.Now;
                entry.Entity.ModifiedBy = username;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = username;
                }
            }
            return await base.SaveChangesAsync();
        }

        public DbSet<ManagedTask> ManagedTasks { get; set; }

        public DbSet<TaskType> TaskTypes { get; set; }
    }
}
