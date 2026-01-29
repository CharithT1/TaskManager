using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common;

namespace TaskManagement.Domain
{
    public class TaskType: BaseDomainEntity
    {
        public string Name { get; set; }

    }
}
