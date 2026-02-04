using AutoMapper;
using MediatR;
using TaskManagement.Application.Features.ManagedTasks.Requests.Commands;
using TaskManagement.Application.Contracts.Persistence;
using TaskManagement.Application.Exceptions;

namespace TaskManagement.Application.Features.ManagedTasks.Handlers.Commands
{
    public class DeleteManagedTaskCommandHandler:IRequestHandler<DeleteManagedTaskCommand>
    {
        private readonly IManagedTaskRepository _managedTaskRepository;
        private readonly IMapper _mapper;

        public DeleteManagedTaskCommandHandler(IManagedTaskRepository managedTaskRepository, IMapper mapper)
        {
            _managedTaskRepository = managedTaskRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteManagedTaskCommand request, CancellationToken cancellationToken)
        { 
            var task = await _managedTaskRepository.GetAsync(request.id);
            if (task == null) 
                throw new NotFoundException(nameof(ManagedTasks), request.id);
            await _managedTaskRepository.DeleteAsync(task);
            return Unit.Value;
        }
    }
}
