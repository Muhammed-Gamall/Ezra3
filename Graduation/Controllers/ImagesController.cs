using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController(ApplicationDbContext context , Cloudinary cloudinary) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly Cloudinary _cloudinary = cloudinary;

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file , CancellationToken cancellation)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream() )
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams ,  cancellation);
            // Save URL to database
            var imageUrl = uploadResult.SecureUrl.ToString();
            //await _context.ImageTests.AddAsync(new ImageTest { Url= imageUrl });
            return Ok(new { Url = imageUrl });

        }
    }
}
