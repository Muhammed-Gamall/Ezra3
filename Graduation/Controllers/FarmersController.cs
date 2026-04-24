namespace Graduation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FarmersController(IFarmerService service) : ControllerBase
    {
        private readonly IFarmerService _service = service;

        [Authorize]
        [HttpGet("ForFarmer")]
        public async Task<IActionResult> GetFarmerByFarmer(CancellationToken cancellation)
        {
            var farmer = await _service.GetFarmerByFarmer(cancellation);
            return Ok(farmer);
        }

        [HttpGet("{userId}/ForUser")]
        public async Task<IActionResult> GetFarmerByUser(string userId, CancellationToken cancellation)
        {
            var farmer = await _service.GetFarmerByUser( userId, cancellation);
            return Ok(farmer);
        }

        [HttpGet("{userId}/Rating")]
        public async Task<IActionResult> GetFarmerRating(string userId, CancellationToken cancellation)
        {
            var farmer = await _service.GetFarmerRatings( userId, cancellation);
            return Ok(farmer);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateFarmerProfile(string ProfessionalDescription, CancellationToken cancellation)
        {
            var farmer = await _service.CreateFarmerProfile(ProfessionalDescription, cancellation);
            return Ok(farmer);
        }

        [Authorize]
        [HttpPost("{userId}/Rating")]
        public async Task<IActionResult> CreateRating(FarmerRatingRequest request, string userId, CancellationToken cancellation)
        {
            var farmer = await _service.CreateRatingRequest(request,  userId, cancellation);
            return Ok(farmer);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateFarmer(FarmerRequest request, CancellationToken cancellation)
        {
            var farmer = await _service.UpdateFarmer(request, cancellation);
            return Ok(farmer);
        }

        [Authorize]
        [HttpPut("Toggle")]
        public async Task<IActionResult> Toggle(CancellationToken cancellation)
        {
          var  farmer = await _service.Toggle(cancellation);
            return farmer is true? Ok() : NotFound();
        }
    }
}
