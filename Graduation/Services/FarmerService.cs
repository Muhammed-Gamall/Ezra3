using Graduation.Contracts.Farmer;

namespace Graduation.Services
{
    public interface IFarmerService
    {
        Task<FarmerResponse> GetFarmerByFarmer(CancellationToken cancellation);
        Task<FarmerResponse> GetFarmerByUser(int id, CancellationToken cancellation);
        Task<IEnumerable<FarmerRatingResponse>> GetFarmerRatings(int id, CancellationToken cancellation);
        Task<FarmerResponse> CreateFarmerProfile(FarmerRequest request , CancellationToken cancellation);
        Task<FarmerRatingResponse?> CreateRatingRequest(FarmerRatingRequest request, int Id , CancellationToken cancellation);
        Task<FarmerResponse> UpdateFarmer(FarmerRequest request , CancellationToken cancellation);
        Task Toggle( CancellationToken cancellation);

    }

    public class FarmerService(ApplicationDbContext context , IHttpContextAccessor accessor) : IFarmerService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _accessor = accessor;


        public async Task<FarmerResponse> GetFarmerByFarmer(CancellationToken cancellation)
        {
            var userId = GetUserId();

            var farmer = await _context.FarmerProfiles
                .Where(x=>x.IsActive && x.UserId==userId)
                .Include(r=>r.Ratings).AsNoTracking()
                .ProjectToType<FarmerResponse>().SingleOrDefaultAsync(cancellation);
     
            return farmer!;
        }
        public async Task<FarmerResponse> GetFarmerByUser(int id ,CancellationToken cancellation)
        {
           var farmer = await _context.FarmerProfiles
                .Where(x=>x.IsActive && x.Id == id)
                .Include(r=>r.Ratings).AsNoTracking()
                .ProjectToType<FarmerResponse>().SingleOrDefaultAsync(cancellation);
            //if (farmer == null)
            //   throw new Exception("Farmer profile not found.");
            
            //var response = new FarmerResponse
            //(
            //     farmer.Id,
            //     farmer.Name,
            //     farmer.ProfessionalDescription,
            //     farmer.Ratings.Select(r => new FarmerRatingResponse
            //    (
            //        r.Id,
            //        r.Rating,
            //        r.Review
            //   )).ToList()
            //);
            return farmer!;
        }
        public async Task<IEnumerable<FarmerRatingResponse>> GetFarmerRatings(int id, CancellationToken cancellation)
        {
            //var userId = GetUserId();

            var ratings = await _context.FarmerProfiles
                .Where(x => x.Id == id)
                .SelectMany(f => f.Ratings).AsNoTracking()
                .ProjectToType<FarmerRatingResponse>()
                .ToListAsync(cancellation);

            return ratings.Adapt<IEnumerable<FarmerRatingResponse>>();
        }

        public async Task<FarmerRatingResponse?> CreateRatingRequest(FarmerRatingRequest request, int Id, CancellationToken cancellation)
        {
            var userId = GetUserId();

            var rating = await _context.FarmerProfiles.SingleOrDefaultAsync(f=>f.Id == Id);
            if (rating == null)
                return null;

            var newRating = new FarmerRating
            {
                UserId = userId,
                Rating = request.Rating,
                Review = request.Review,
                FarmerId = Id
            };

            await _context.FarmerRatings.AddAsync(newRating, cancellation);
            await _context.SaveChangesAsync(cancellation);

            return newRating.Adapt<FarmerRatingResponse>();
        }

        public async Task<FarmerResponse> CreateFarmerProfile(FarmerRequest request, CancellationToken cancellation)
        {
            var userId = GetUserId();
            
            var farmer = new FarmerProfile
            {
                UserId = userId,
                Name = request.Name,
                ProfessionalDescription = request.ProfessionalDescription
            };
          
            await _context.FarmerProfiles.AddAsync(farmer, cancellation);
            await _context.SaveChangesAsync();
            return farmer.Adapt<FarmerResponse>();
        }

      
        public async Task<FarmerResponse> UpdateFarmer(FarmerRequest request, CancellationToken cancellation)
        {
            var userId = GetUserId();
            
            var Oldfarmer =await _context.FarmerProfiles.SingleOrDefaultAsync(x => x.UserId == userId);


            Oldfarmer!.Name = request.Name;
               Oldfarmer.ProfessionalDescription = request.ProfessionalDescription;
            
            _context.FarmerProfiles.Update(Oldfarmer);
            await _context.SaveChangesAsync(cancellation);

            return Oldfarmer.Adapt<FarmerResponse>();
        }

        public async Task Toggle( CancellationToken cancellation)
        {
            var userId = GetUserId();
           
            var farmer =await _context.FarmerProfiles.SingleOrDefaultAsync(x => x.UserId == userId);

            farmer!.IsActive = !farmer.IsActive;
            await _context.SaveChangesAsync(cancellation);
            return;
        }

        public string GetUserId()
        {
            return _accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

      
    }
}
