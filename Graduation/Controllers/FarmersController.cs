namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmersController(IFarmerService service) : ControllerBase
    {
        private readonly IFarmerService _service = service;


        [HttpGet("GetFarmerForFarmer")]
        public async Task<IActionResult> GetFarmerByFarmer(CancellationToken cancellation)
        {
            var farmer = await _service.GetFarmerByFarmer(cancellation);
            return Ok(farmer);
        }

        [HttpGet("{id}/ForUser")]
        public async Task<IActionResult> GetFarmerByUser(int id,CancellationToken cancellation)
        {
            var farmer = await _service.GetFarmerByUser(id, cancellation);
            return Ok(farmer);
        }

        [HttpGet("{id}/Rating")]
        public async Task<IActionResult> GetFarmerRating(int id, CancellationToken cancellation)
        {
            var farmer = await _service.GetFarmerRatings(id, cancellation);
            return Ok(farmer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFarmerProfile(FarmerRequest request , CancellationToken cancellation)
        {
            var farmer = await _service.CreateFarmerProfile(request, cancellation);
            return Ok(farmer);
        }

        [HttpPost("{id}/Rating")]
        public async Task<IActionResult> CreateRating(FarmerRatingRequest request , int id, CancellationToken cancellation)
        {
            var farmer = await _service.CreateRatingRequest(request,  id, cancellation);
            return Ok(farmer);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFarmer(FarmerRequest request , CancellationToken cancellation)
        {
            var farmer = await _service.UpdateFarmer(request, cancellation);
            return Ok(farmer);
        }

        [HttpPut]
        public async Task<IActionResult> Toggle(FarmerRequest request , CancellationToken cancellation)
        {
            await _service.Toggle( cancellation);
            return Ok();
        }
    }
}
