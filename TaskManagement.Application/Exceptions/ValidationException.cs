using FluentValidation.Results;

namespace TaskManagement.Application.Exceptions
{
    public class ValidationException:ApplicationException
    {
        public List<string> Errors { get; set; } = new();

        public ValidationException(ValidationResult validationResult)
        {
            validationResult.Errors.ForEach(a => Errors.Add(a.ErrorMessage));
        }
    }
}
