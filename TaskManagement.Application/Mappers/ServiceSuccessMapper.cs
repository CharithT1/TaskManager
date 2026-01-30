using TaskManagement.Application.Common;
using TaskManagement.Application.Responses;

namespace TaskManagement.Application.Mappers
{
    public class ServiceSuccessMapper : IMapper<int, BaseCommandResponse>
    {
        /// <summary>
        /// Maps the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public BaseCommandResponse Map(int input) => new BaseCommandResponse
        {
            IsError = false,
            Message = "Success",
            Id = input
        };
    }
}
