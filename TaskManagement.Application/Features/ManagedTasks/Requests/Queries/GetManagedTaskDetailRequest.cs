using MediatR;
using TaskManagement.Application.DTOs.ManagedTask;

namespace TaskManagement.Application.Features.ManagedTasks.Requests.Queries
{
    public record GetManagedTaskDetailRequest(int id) : IRequest<ManagedTaskDto>;

}
