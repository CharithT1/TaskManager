using AutoMapper;
using MediatR;
using TaskManagement.Application.Features.ManagedTasks.Requests.Commands;
using TaskManagement.Application.Contracts.Persistence;
using TaskManagement.Application.Responses;
using TaskManagement.Domain;
using TaskManagement.Application.DTOs.ManagedTask.Validators;
using TaskManagement.Application.Common;
using FluentValidation.Results;

namespace TaskManagement.Application.Features.ManagedTasks.Handlers.Commands
{
    public class CreateManagedTaskCommandHandler : IRequestHandler<CreateManagedTaskCommand, BaseCommandResponse>
    {
        private readonly IManagedTaskRepository _managedTaskRepository;
        private readonly IMapper _mapper;
        private readonly IMapper<ValidationResult, BaseCommandResponse> _serviceErrorMapper;
        private readonly IMapper<int, BaseCommandResponse> _serviceSuccessMapper;

        public CreateManagedTaskCommandHandler(IManagedTaskRepository managedTaskRepository, IMapper mapper, IMapper<ValidationResult, BaseCommandResponse> serviceErrorMapper, IMapper<int, BaseCommandResponse> serviceSuccessMapper)
        {
            _managedTaskRepository = managedTaskRepository;
            _mapper = mapper;
            _serviceErrorMapper = serviceErrorMapper;
            _serviceSuccessMapper = serviceSuccessMapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateManagedTaskCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateManagedTaskDtoValidator(_managedTaskRepository);
            var validationResult = await validator.ValidateAsync(request.managedTaskDto);
            var response = new BaseCommandResponse();
            if (!validationResult.IsValid)
                return _serviceErrorMapper.Map(validationResult);

            var task = _mapper.Map<ManagedTask>(request.managedTaskDto);
            task = await _managedTaskRepository.AddAsync(task);
            return _serviceSuccessMapper.Map(task.Id);
        }
    }
}
