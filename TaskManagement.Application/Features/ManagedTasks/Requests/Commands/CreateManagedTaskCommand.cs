using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs.ManagedTask;
using TaskManagement.Application.Responses;

namespace TaskManagement.Application.Features.ManagedTasks.Requests.Commands
{
    public class CreateManagedTaskCommand:IRequest<BaseCommandResponse> 
    {
        public CreateManagedTaskDto ManagedTaskDto { get; set; }
    }
}
