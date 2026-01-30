using AutoMapper;
using MediatR;
using TaskManagement.Application.Features.ManagedTasks.Requests.Commands;
using TaskManagement.Application.Contracts.Persistence;
using TaskManagement.Application.DTOs.ManagedTask.Validators;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Responses;

namespace TaskManagement.Application.Features.ManagedTasks.Handlers.Commands
{
    public class UpdateManagedTaskCommandHandler: IRequestHandler<UpdateManagedTaskCommand, BaseCommandResponse>
    {
        private readonly IManagedTaskRepository _managedTaskRepository;
        private readonly IMapper _mapper;

        public UpdateManagedTaskCommandHandler(IManagedTaskRepository managedTaskRepository, IMapper mapper)
        {
            _managedTaskRepository = managedTaskRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateManagedTaskCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateManagedTaskDtoValidator(_managedTaskRepository);
            var validationResult = await validator.ValidateAsync(request.managedTaskDto);
            var response = new BaseCommandResponse();
            if (!validationResult.IsValid)
            {
                //throw new ValidationException(validationResult);
                response.IsError = true;
                response.Message = "Update Failed";
                response.Errors = validationResult.Errors.Select(a => a.ErrorMessage).ToList();

                return response;
            }

            var task = await _managedTaskRepository.GetAsync(request.managedTaskDto.Id);
            _mapper.Map(request.managedTaskDto, task);
            await _managedTaskRepository.UpdateAsync(task);

            response.IsError = false;
            response.Message = "Update Successful";
            response.Id = task.Id;

            return response;
        }
    }
}
