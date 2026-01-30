using MediatR;
using TaskManagement.Application.DTOs.TaskType;

namespace TaskManagement.Application.Features.TaskTypes.Requests.Queries
{
    public record class GetTaskTypeRequest() : IRequest<List<TaskTypeDto>>;
}
