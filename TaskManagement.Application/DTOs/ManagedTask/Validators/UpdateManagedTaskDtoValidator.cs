using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Contracts.Persistence;

namespace TaskManagement.Application.DTOs.ManagedTask.Validators
{
    public class UpdateManagedTaskDtoValidator: AbstractValidator<CreateManagedTaskDto>
    {
        private readonly IManagedTaskRepository _managedTaskRepository;

        public UpdateManagedTaskDtoValidator(IManagedTaskRepository managedTaskRepository)
        {
            _managedTaskRepository = managedTaskRepository;
            Include(new CreateManagedTaskDtoValidator(_managedTaskRepository));

            RuleFor(a => a.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
