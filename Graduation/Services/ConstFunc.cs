namespace Graduation.Services
{
    public  class ConstFunc(Cloudinary cloudinary, IHttpContextAccessor accessor)
    {
        private readonly Cloudinary _cloudinary = cloudinary;
        private readonly IHttpContextAccessor _accessor = accessor;

        public  string UpluodImage(IFormFile file)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };
            var uploadResult = _cloudinary.Upload(uploadParams);
            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }
            return string.Empty;
        }

        public string ComputeFileHash(IFormFile file)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            using var stream = file.OpenReadStream();
            var hashBytes = sha256.ComputeHash(stream);
            return Convert.ToBase64String(hashBytes);
        }

        public string GetUserId()
        {
            return _accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }
    }
}
