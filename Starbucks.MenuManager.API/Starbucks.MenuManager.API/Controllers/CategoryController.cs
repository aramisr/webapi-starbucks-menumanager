using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Starbucks.MenuManager.API.Domain.Entities;
using Starbucks.MenuManager.API.Persistence.Contexts;

namespace Starbucks.MenuManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly StarbucksDbContext _context;
        public CategoryController(StarbucksDbContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public async Task<List<Category>> Get()
        {
           return await _context.Categories.ToListAsync();
        }
    }
}
