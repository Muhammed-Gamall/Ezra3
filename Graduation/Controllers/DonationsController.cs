using Graduation.Contracts.Donation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DonationsController(IDonationService donationService) : ControllerBase
    {
        private readonly IDonationService _donationService = donationService;

        [HttpGet("ForAdmin")]
        [AllowAnonymous]
        public async Task<IActionResult> GetForAdmin(CancellationToken cancellationToken)
        { 
         var donation =await _donationService.GetDonationsForAdmin(cancellationToken);
            return Ok(donation);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        { 
         var donation =await _donationService.GetDonationsAsync(cancellationToken);
            return Ok(donation);
        }

        [HttpPost]
        public async Task<IActionResult> create(DonationRequest donationRequest , CancellationToken cancellationToken)
        {
            var donation = await _donationService.CreateDonationsAsync(donationRequest, cancellationToken);

            return Ok(donation);
        }
    }
}
