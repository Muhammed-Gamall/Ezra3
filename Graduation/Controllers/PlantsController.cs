using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlantsController(IPlantService plantService) : ControllerBase
    {
        private readonly IPlantService plantService = plantService;

        [HttpGet]
        public async Task<IActionResult> GetAllPlants(PlantCategory category, CancellationToken cancellation)
        {
            var plants = await plantService.GetAllAsync(category, cancellation);
            return Ok(plants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellation)
        {
            var plant = await plantService.GetByIdAsync(id, cancellation);
            return Ok(plant);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePlant([FromForm] PlantRequest request, CancellationToken cancellation)
        {
            var plant = await plantService.CreatAsync(request, cancellation);
            return Ok(plant);
        }
        [HttpPut("{Id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] PlantRequest request, int Id, CancellationToken cancellation)
        {
            var plant = await plantService.UpdateAsync(request,Id , cancellation);
            return Ok(plant);
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<IActionResult> Toggle(int id, CancellationToken cancellation)
        {
            var plant = await plantService.ToggleAsync(id, cancellation);
            return Ok(plant);
        }
    }
}
