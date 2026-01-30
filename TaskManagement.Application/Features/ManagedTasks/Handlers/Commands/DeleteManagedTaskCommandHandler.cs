using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Features.ManagedTasks.Requests.Commands;
using TaskManagement.Application.Contracts.Persistence;

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
            await _managedTaskRepository.DeleteAsync(task);
            return Unit.Value;
        }
    }
}
