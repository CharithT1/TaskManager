using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs.ManagedTask;

namespace TaskManagement.Application.Features.ManagedTasks.Requests.Queries
{
    public class GetManagedTaskDetailRequest : IRequest<ManagedTaskDto>
    {
        public int Id { get; set; }
    }
}
