using FluentValidation;
using TaskManagement.Application.Contracts.Persistence;

namespace TaskManagement.Application.DTOs.ManagedTask.Validators
{
    public class CreateManagedTaskDtoValidator:AbstractValidator<CreateManagedTaskDto>
    {
        private readonly IManagedTaskRepository _managedTaskRepository;

        public CreateManagedTaskDtoValidator(IManagedTaskRepository managedTaskRepository)
        {
            _managedTaskRepository = managedTaskRepository;
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");

            RuleFor(a => a.Description)
                .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters");

            RuleFor(a => a.StartDate)
                .LessThan(a => a.EndDate).WithMessage("{PropertyName} must not exceed {ComparisonValue:yyyy-MM-dd}");

            RuleFor(a => a.EndDate)
                .GreaterThan(a => a.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue:yyyy-MM-dd}");

            RuleFor(a => a.TaskTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) => { 
                    return !await _managedTaskRepository.ExistAsync(id);
                }).WithMessage("{PropertyName} does not exist");
           
        }
    }
}
