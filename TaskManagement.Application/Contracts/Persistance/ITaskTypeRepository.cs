using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain;

namespace TaskManagement.Application.Contracts.Persistence
{
    public interface ITaskTypeRepository : IGenericRepository<TaskType>
    {
    }
}
