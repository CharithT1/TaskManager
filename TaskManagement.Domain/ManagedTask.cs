using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common;

namespace TaskManagement.Domain
{
    public class ManagedTask:BaseDomainEntity
    {    

        public string Name { get; set; }

        public string Description { get; set; }

        public int TaskTypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TaskType TaskType { get; set; }
    }
}
