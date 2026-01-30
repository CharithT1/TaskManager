using System.ComponentModel.DataAnnotations.Schema;
using TaskManagement.Domain.Common;

namespace TaskManagement.Domain
{
    public class TaskType: BaseDomainEntity
    {
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

    }
}
