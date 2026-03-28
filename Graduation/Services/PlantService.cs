using Graduation.Contracts.Plant;
using Graduation.Data;
using Mapster;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace Graduation.Services
{
    public interface IPlantService
    {
        Task<IEnumerable<PlantResponse?>> GetAllAsync(PlantCategory category, CancellationToken cancellation);
        Task<PlantResponse?> GetByIdAsync(int Id,CancellationToken cancellation);
        Task<PlantResponse> CreatAsync(PlantRequest request , CancellationToken cancellation);
        Task<bool> UpdateAsync(PlantRequest request, int plantId , CancellationToken cancellation);
        Task<bool> ToggleAsync( int plantId, CancellationToken cancellation);

    }

    public class PlantService(ApplicationDbContext context  , IHttpContextAccessor accessor , Cloudinary cloudinary) : IPlantService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly Cloudinary _cloudinary = cloudinary;

        public async Task<IEnumerable<PlantResponse?>> GetAllAsync(PlantCategory category, CancellationToken cancellation)
        {
            var plants= await _context.Plants
                .Where(p => p.Category == category && p.IsActive == true)
                .Include(x=>x.Images).AsNoTracking()
                  .Select(p => new PlantResponse
                 (
                     p.Id,
                     p.Name,
                     p.Describtion,
                     p.Category,
                     p.Climate,
                     p.SuitableLocation,
                     p.CareTips!,
                     p.Price,
                     p.PlantingServicePrice,
                     p.Images.Where(i => i.IsActive == true).Select(i => new PhotoResponse(i.Id, i.Photo!)).ToList()
               ))
                //.ProjectToType<PlantResponse>()
                .ToListAsync(cancellation);
            return plants;
        }

        public async Task<PlantResponse?> GetByIdAsync(int Id ,CancellationToken cancellation)
        {
            var plant =await _context.Plants
                .Where(x=>x.Id == Id && x.IsActive == true)
                .Include(x => x.Images).AsNoTracking()
                .Select(p=> new PlantResponse
                 (
                     p.Id,
                     p.Name,
                     p.Describtion,
                     p.Category,
                     p.Climate,
                     p.SuitableLocation,
                     p.CareTips!,
                     p.Price,
                     p.PlantingServicePrice,
                     p.Images.Where(i=>i.IsActive == true).Select(i => new PhotoResponse ( i.Id, i.Photo! )).ToList()
               ))
                //.ProjectToType<PlantResponse>()
                .FirstOrDefaultAsync(cancellation);
            return plant;
        }
      
        public string ComputeFileHash(IFormFile file)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            using var stream = file.OpenReadStream();
            var hashBytes = sha256.ComputeHash(stream);
            return Convert.ToBase64String(hashBytes);
        }
        public string UpluodImage(IFormFile file)
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

        public async Task<PlantResponse> CreatAsync(PlantRequest request, CancellationToken cancellation)
        {
            var plant = new Plant
            {
                Name = request.Name,
                Describtion = request.Describtion,
                Category = request.Category,
                Climate = request.Climate,
                SuitableLocation = request.SuitableLocation,
                CareTips = request.CareTips,
                Price = request.Price,
                PlantingServicePrice = request.PlantingServicePrice,
                Images = new List<PlantPhoto>()
            };

            // make sure to avoid duplicate images by comparing hash codes 
            // adding images after check for duplicates
            foreach (var image in request.Image)
            {
              var hash =ComputeFileHash(image);
                if(plant.Images.Any(i=>i.HashCode == hash))
                    continue;
               var url= UpluodImage(image);
                plant.Images.Add(new PlantPhoto
                {
                    Photo = url,
                    HashCode = hash
                });
            }

            await _context.Plants.AddAsync(plant, cancellation);
             await _context.SaveChangesAsync(cancellation);

            return plant.Adapt<PlantResponse>();
        }


        public async Task<bool> UpdateAsync(PlantRequest request, int plantId, CancellationToken cancellation)
        {
            var plant = await _context.Plants.Include(p => p.Images)
                .SingleOrDefaultAsync(p => p.Id == plantId, cancellation);

            if (plant == null)
                return false;

            plant.Name = request.Name;
            plant.Describtion = request.Describtion;
            plant.Category = request.Category;
            plant.Climate = request.Climate;
            plant.SuitableLocation = request.SuitableLocation;
            plant.CareTips = request.CareTips;
            plant.Price = request.Price;
            plant.PlantingServicePrice = request.PlantingServicePrice;


            ////current images
            //var currentImageUrls = plant.Images.Select(i => i.HashCode).ToList();

            //// new images urls
            // var newImageUrls = ComputeFileHash(request.Image)  ;

            //if (request.Image.Any())
            //{
            //    var imageUrls = await UploadImagesAsync(request.Image);

            //    foreach (var url in imageUrls)
            //        plant.Images.Add(new PlantPhoto { Photo = url });
            //}
            //var requestImageHashCodes = request.Image.Select(ComputeFileHash).ToList();
            var requestImageHashCodes =new List<string>();

            foreach (var image in request.Image)
            {
                var hash = ComputeFileHash(image);
                if (plant.Images.Any(i => i.HashCode == hash))
                    continue;
                var url = UpluodImage(image);
                plant.Images.Add(new PlantPhoto
                {
                    Photo = url,
                    HashCode = hash
                });
                requestImageHashCodes.Add(hash);

            }
            plant.Images.ToList().ForEach(i => 
            {
                i.IsActive = requestImageHashCodes.Contains(i.HashCode!);
            });

            await _context.SaveChangesAsync(cancellation);
            return true;
        }

        public async Task<bool> ToggleAsync( int plantId, CancellationToken cancellation)
        {
            var plant =await _context.Plants.SingleOrDefaultAsync(p => p.Id == plantId);
            if (plant == null)
                return false;
            
            plant.IsActive = !plant.IsActive;
            await _context.SaveChangesAsync(cancellation);
            return true;
        }

    }
}
