using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain;

namespace TaskManagement.Application.Contracts.Persistence
{
    public interface IManagedTaskRepository: IGenericRepository<ManagedTask>
    {
        Task<List<ManagedTask>> GetManagedTasksAsync();

        Task<ManagedTask> GetManagedTaskAsync(int id);
    }
}
