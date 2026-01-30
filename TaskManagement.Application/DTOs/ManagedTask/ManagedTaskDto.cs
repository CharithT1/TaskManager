using TaskManagement.Application.DTOs.Common;
using TaskManagement.Application.DTOs.TaskType;

namespace TaskManagement.Application.DTOs.ManagedTask
{
    public class ManagedTaskDto : BaseDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int TaskTypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TaskTypeDto TaskType { get; set; }
    }
}
