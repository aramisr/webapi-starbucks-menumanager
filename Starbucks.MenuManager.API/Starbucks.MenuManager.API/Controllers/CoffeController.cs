using Core.mediatOR.Contracts;
using Microsoft.AspNetCore.Mvc;
using Starbucks.MenuManager.API.Application.Coffes.Commands;
using Starbucks.MenuManager.API.Application.Coffes.DTOs;
using System.Threading;

namespace Starbucks.MenuManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<Guid> CreateCoffe(CoffeCreateRequest request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new CoffeCreate.Command { CoffeCreateRequest = request }, cancellationToken);

            return results;
        }
    }
}
