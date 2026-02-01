using System.ComponentModel.DataAnnotations.Schema;
using TaskManagement.Domain.Common;

namespace TaskManagement.Domain
{
    public class ManagedTask:BaseDomainEntity
    {
        [Column(TypeName ="nvarchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string? Description { get; set; }

        public int TaskTypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TaskType TaskType { get; set; }
    }
}
