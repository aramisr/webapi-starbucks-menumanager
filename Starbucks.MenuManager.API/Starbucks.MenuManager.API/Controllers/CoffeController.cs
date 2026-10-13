using Core.mediatOR.Contracts;
using Microsoft.AspNetCore.Mvc;
using Starbucks.MenuManager.API.Application.Coffes.Commands;
using Starbucks.MenuManager.API.Application.Coffes.DTOs;
using Starbucks.MenuManager.API.Application.Coffes.Queries;
using System.Threading;

namespace Starbucks.MenuManager.API.Controllers
{
    [Route("api/coffes")]
    [ApiController]
    public class CoffeController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateCoffe(CoffeCreateRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new CoffeCreate.Command { CoffeCreateRequest = request }, 
                cancellationToken
            );

            if (result.IsSuccess)
            {
                var coffeId = result.Value;
                return Created($"api/coffes/{coffeId}", coffeId);
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoffe(Guid id, CoffeUpdateRequest request, CancellationToken cancellationToken)
        {
            var command = new CoffeUpdate.Command { Id = id, CoffeUpdateRequest = request };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(Guid id, CancellationToken cancellationToken)
        {
            
            var query = new CoffeDetail.Query { Id = id };

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }
    }
}
