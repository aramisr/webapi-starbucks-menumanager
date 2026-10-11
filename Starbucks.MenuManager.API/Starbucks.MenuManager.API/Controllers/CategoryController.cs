using Microsoft.AspNetCore.Mvc;
using Core.mediatOR.Contracts;
using static Starbucks.MenuManager.API.Application.Categories.Querys.CategoryListGet;

namespace Starbucks.MenuManager.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet] 
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var query = new Query();
            var resultados = await _mediator.Send(query, cancellationToken);
            return Ok(resultados);
        }
    }
}
