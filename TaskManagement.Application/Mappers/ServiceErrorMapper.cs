using FluentValidation.Results;
using TaskManagement.Application.Responses;
using TaskManagement.Application.Common;

namespace TaskManagement.Application.Mappers
{
    public class ServiceErrorMapper : IMapper<ValidationResult, BaseCommandResponse>
    {
        /// <summary>
        /// Maps the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public BaseCommandResponse Map(ValidationResult input) => new BaseCommandResponse
        {
            IsError = true,
            Message = "Error",
            Errors = input.Errors.Select(a => a.ErrorMessage).ToList()
        };
    }
}
