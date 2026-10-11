using Core.mediatOR.Contracts;
using Microsoft.AspNetCore.Mvc;
using Starbucks.MenuManager.API.Application.Coffes.Commands;
using Starbucks.MenuManager.API.Application.Coffes.DTOs;
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
    }
}
