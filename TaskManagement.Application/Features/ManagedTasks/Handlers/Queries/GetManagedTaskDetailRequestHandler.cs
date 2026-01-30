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
    public class GetManagedTaskDetailRequestHandler : IRequestHandler<GetManagedTaskDetailRequest, ManagedTaskDto>
    {
        private readonly IManagedTaskRepository _managedTaskRepository;
        private readonly IMapper _mapper;

        public GetManagedTaskDetailRequestHandler(IManagedTaskRepository managedTaskRepository, IMapper mapper)
        {
            _managedTaskRepository = managedTaskRepository;
            _mapper = mapper;
        }
        public async Task<ManagedTaskDto> Handle(GetManagedTaskDetailRequest request, CancellationToken cancellationToken)
        {
            var task = await _managedTaskRepository.GetManagedTaskAsync(request.id);
            return _mapper.Map<ManagedTaskDto>(task);
        }
    }
}
