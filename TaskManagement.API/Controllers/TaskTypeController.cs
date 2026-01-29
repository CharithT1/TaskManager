using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs.TaskType;
using TaskManagement.Application.Features.TaskTypes.Requests.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<TaskTypeController>
        [HttpGet]
        public async Task<ActionResult<List<TaskTypeDto>>> Get()
        {
            var taskTypes = await _mediator.Send(new GetTaskTypeRequest());
            return Ok(taskTypes);
        }

    }
}
