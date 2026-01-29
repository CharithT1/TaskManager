using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Contracts.Persistence;
using TaskManagement.Domain;
using Microsoft.EntityFrameworkCore;


namespace TaskManagement.Persistance.Repositories
{
    public class ManagedTaskRepository : GenericRepository<ManagedTask>, IManagedTaskRepository
    {
        private readonly TaskManagementDbContext _dbContext;

        public ManagedTaskRepository(TaskManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ManagedTask> GetManagedTaskAsync(int id)
        {
            var task = await _dbContext.ManagedTasks
               .Include(a => a.TaskType)
               .FirstOrDefaultAsync(a => a.Id == id);
            return task;
        }

        public async Task<List<ManagedTask>> GetManagedTasksAsync()
        {
            var tasks = await _dbContext.ManagedTasks
               .Include(a => a.TaskType)
               .ToListAsync();
            return tasks;
        }
    }
}
