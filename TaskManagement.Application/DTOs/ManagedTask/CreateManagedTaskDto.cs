using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs.Common;
using TaskManagement.Application.DTOs.TaskType;

namespace TaskManagement.Application.DTOs.ManagedTask
{
    public class CreateManagedTaskDto:BaseDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int TaskTypeId { get; set; }
         
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
