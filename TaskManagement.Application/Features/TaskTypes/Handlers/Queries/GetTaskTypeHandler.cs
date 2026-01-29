using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs.TaskType;
using TaskManagement.Application.Features.TaskTypes.Requests.Queries;
using TaskManagement.Application.Contracts.Persistence;

namespace TaskManagement.Application.Features.TaskTypes.Handlers.Queries
{
    public class GetTaskTypeHandler : IRequestHandler<GetTaskTypeRequest, List<TaskTypeDto>>
    {
        private readonly ITaskTypeRepository _taskTypeRepository;
        private readonly IMapper _mapper;

        public GetTaskTypeHandler(ITaskTypeRepository taskTypeRepository,IMapper mapper)
        {
            _taskTypeRepository = taskTypeRepository;
            _mapper = mapper;
        }
        public async Task<List<TaskTypeDto>> Handle(GetTaskTypeRequest request, CancellationToken cancellationToken)
        {
            var taskTypes =await _taskTypeRepository.GetAllAsync();
            return _mapper.Map<List<TaskTypeDto>>(taskTypes);

        }
    }
}
