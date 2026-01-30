using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Validators.Auth;
using TaskManagement.Application.DTOs.ManagedTask;
using TaskManagement.Application.Features.ManagedTasks.Requests.Commands;
using TaskManagement.Application.Features.ManagedTasks.Requests.Queries;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BasicAuthorize]
    public class ManageTaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManageTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<ManageTaskController>
        [HttpGet]
        public async Task<ActionResult<List<ManagedTaskDto>>> Get()
        {
            var tasks = await _mediator.Send(new GetManagedTaskRequest());
            return Ok(tasks);
        }

        // GET api/<ManageTaskController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManagedTaskDto>> Get(int id)
        {
            var tasks = await _mediator.Send(new GetManagedTaskDetailRequest(id));
            return Ok(tasks);
        }

        // POST api/<ManageTaskController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateManagedTaskDto task)
        {
            var command = new CreateManagedTaskCommand(task);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<ManageTaskController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateManagedTaskDto task)
        {
            var command = new UpdateManagedTaskCommand(task);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
         
        // DELETE api/<ManageTaskController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteManagedTaskCommand(id);
            var response = await _mediator.Send(command);
            return NoContent();
        }
    }
}
