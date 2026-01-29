using AutoMapper;
using MediatR;
using TaskManagement.Application.Features.ManagedTasks.Requests.Commands;
using TaskManagement.Application.Contracts.Persistence;
using TaskManagement.Application.Responses;
using TaskManagement.Domain;
using TaskManagement.Application.DTOs.ManagedTask.Validators;

namespace TaskManagement.Application.Features.ManagedTasks.Handlers.Commands
{
    public class CreateManagedTaskCommandHandler : IRequestHandler<CreateManagedTaskCommand, BaseCommandResponse>
    {
        private readonly IManagedTaskRepository _managedTaskRepository;
        private readonly IMapper _mapper;

        public CreateManagedTaskCommandHandler(IManagedTaskRepository managedTaskRepository, IMapper mapper)
        {
            _managedTaskRepository = managedTaskRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateManagedTaskCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateManagedTaskDtoValidator(_managedTaskRepository);
            var validationResult = await validator.ValidateAsync(request.ManagedTaskDto);
            var response = new BaseCommandResponse();
            if (!validationResult.IsValid)
            {
                //throw new ValidationException(validationResult);
                response.IsError = true;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(a => a.ErrorMessage).ToList();
                return response;
            }
            var task = _mapper.Map<ManagedTask>(request.ManagedTaskDto);
            task = await _managedTaskRepository.AddAsync(task);

            response.IsError = false;
            response.Message = "Creation Successful";
            response.Id = task.Id;

            return response;
        }
    }
}
