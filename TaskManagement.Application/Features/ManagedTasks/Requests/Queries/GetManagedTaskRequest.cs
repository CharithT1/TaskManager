using MediatR;
using TaskManagement.Application.DTOs.ManagedTask;

namespace TaskManagement.Application.Features.ManagedTasks.Requests.Queries
{
    public record GetManagedTaskRequest() : IRequest<List<ManagedTaskDto>>;
}
