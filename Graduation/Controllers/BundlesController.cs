using Graduation.Contracts.Bundle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BundlesController(IBundleService bundleService) : ControllerBase
    {
        private readonly IBundleService _bundleService = bundleService;

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken) { 
         
            var bundle = await _bundleService.GetAllBundlesAsync(cancellationToken);
            return Ok(bundle);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] BundleRequest request , CancellationToken cancellationToken) 
        {
            var bundle = await _bundleService.CreateBundleAsync(request, cancellationToken);

            return Ok(bundle);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult>Update ([FromForm] BundleRequest request, int id, CancellationToken cancellationToken)
        {
            var bundle = await _bundleService.UpdateBundleAsync(id, request,cancellationToken);
            return bundle is true ? Ok() : BadRequest();
        }

        [HttpPut("Toggle/{id}")]
        [Authorize]
        public async Task<IActionResult> Toggle( int id, CancellationToken cancellationToken)
        {
            var bundle = await _bundleService.Toggle(id,cancellationToken);
            return bundle is true ? Ok() : BadRequest();
        }

    }
}
