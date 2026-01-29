using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs.ManagedTask;
using TaskManagement.Application.Features.ManagedTasks.Requests.Queries;
using TaskManagement.Application.Contracts.Persistence;

namespace TaskManagement.Application.Features.ManagedTasks.Handlers.Queries
{
    public class GetManagedTaskRequestHandler : IRequestHandler<GetManagedTaskRequest, List<ManagedTaskDto>>
    {
        private readonly IManagedTaskRepository _managedTaskRepository;
        private readonly IMapper _mapper;

        public GetManagedTaskRequestHandler(IManagedTaskRepository managedTaskRepository, IMapper mapper)
        {
            _managedTaskRepository = managedTaskRepository;
            _mapper = mapper;
        }
        public async Task<List<ManagedTaskDto>> Handle(GetManagedTaskRequest request, CancellationToken cancellationToken)
        {
            var tasks = await _managedTaskRepository.GetManagedTasksAsync();
            return _mapper.Map<List<ManagedTaskDto>>(tasks);
        }
    }
}
