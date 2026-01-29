using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Contracts.Persistence;
using TaskManagement.Domain;

namespace TaskManagement.Persistance.Repositories
{
    public class TaskTypeRepository: GenericRepository<TaskType>, ITaskTypeRepository
    {
        private readonly TaskManagementDbContext _dbContext;

        public TaskTypeRepository(TaskManagementDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
