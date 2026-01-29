using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain;

namespace TaskManagement.Persistance.Configurations.Entities
{
    public class TaskTypeConfiguration : IEntityTypeConfiguration<TaskType>
    {
        public void Configure(EntityTypeBuilder<TaskType> builder)
        {
            builder.HasData(
                new TaskType { Id = 1, Name = "A" },
                new TaskType { Id = 2, Name = "B" }
                );
        }
    }
}
