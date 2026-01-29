using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs.TaskType;

namespace TaskManagement.Application.Features.TaskTypes.Requests.Queries
{
    public class GetTaskTypeRequest : IRequest<List<TaskTypeDto>>
    {
    }
}
