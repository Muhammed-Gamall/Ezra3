using Graduation.Contracts.Farmer;

namespace Graduation.Services
{
    public interface IFarmerService
    {
        Task<FarmerResponse> GetFarmerByFarmer(CancellationToken cancellation);
        Task<FarmerResponse> GetFarmerByUser(string userId, CancellationToken cancellation);
        Task<IEnumerable<FarmerRatingResponse>> GetFarmerRatings(string userId, CancellationToken cancellation);
        Task<FarmerResponse> CreateFarmerProfile(string professionalDescription, CancellationToken cancellation);
        Task<FarmerRatingResponse?> CreateRatingRequest(FarmerRatingRequest request, string userId, CancellationToken cancellation);
        Task<bool> UpdateFarmer(FarmerRequest request, CancellationToken cancellation);
        Task<bool> Toggle(CancellationToken cancellation);

    }

    public class FarmerService(ApplicationDbContext context, ConstFunc constFunc) : IFarmerService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ConstFunc _constFunc = constFunc;


        public async Task<FarmerResponse> GetFarmerByFarmer(CancellationToken cancellation)
        {
            var userId = _constFunc.GetUserId();

            var farmer = await _context.FarmerProfiles
                .Where(x => x.User.IsActive && x.UserId == userId)
                .Include(r => r.Ratings).AsNoTracking()
                //.ProjectToType<FarmerResponse>()
                .Select(f => new FarmerResponse
                (
                   f.UserId,
                   f.User.FullName,
                   f.ProfessionalDescription,
                   f.Ratings.Select(r => new FarmerRatingResponse(r.Id ,r.Rating, r.Review))
                ))
                .FirstOrDefaultAsync(cancellation);

            
            return farmer!;
        }
        public async Task<FarmerResponse> GetFarmerByUser(string userId, CancellationToken cancellation)
        {
            var farmer = await _context.FarmerProfiles
                 .Where(x => x.User.IsActive && x.UserId == userId)
                 .Include(r => r.Ratings).AsNoTracking()
                  //.ProjectToType<FarmerResponse>()
                  .Select(f => new FarmerResponse
                (
                   f.UserId,
                   f.User.FullName,
                   f.ProfessionalDescription,
                   f.Ratings.Select(r => new FarmerRatingResponse(r.Id, r.Rating, r.Review))
                ))
                 .FirstOrDefaultAsync(cancellation);

            return farmer!;
        }
        public async Task<IEnumerable<FarmerRatingResponse>> GetFarmerRatings(string UserId, CancellationToken cancellation)
        {
            var ratings = await _context.FarmerProfiles
                .Where(x => x.UserId == UserId)
                .SelectMany(f => f.Ratings).AsNoTracking()
                .ProjectToType<FarmerRatingResponse>()
                .ToListAsync(cancellation);

            return ratings.Adapt<IEnumerable<FarmerRatingResponse>>();
        }

        public async Task<FarmerRatingResponse?> CreateRatingRequest(FarmerRatingRequest request, string UserId, CancellationToken cancellation)
        {

            var rating = await _context.FarmerProfiles.FirstOrDefaultAsync(f => f.UserId == UserId);
            if (rating == null)
                return null;

            var newRating = new FarmerRating
            {
                Rating = request.Rating,
                Review = request.Review,
                FarmerId = UserId
            };

            await _context.FarmerRatings.AddAsync(newRating, cancellation);
            await _context.SaveChangesAsync(cancellation);

            return newRating.Adapt<FarmerRatingResponse>();
        }

        public async Task<FarmerResponse> CreateFarmerProfile(string professionalDescription, CancellationToken cancellation)
        {
            var userId = _constFunc.GetUserId();

            var farmer = new FarmerProfile
            {
                UserId = userId,
                ProfessionalDescription = professionalDescription
            };

            await _context.FarmerProfiles.AddAsync(farmer, cancellation);
            await _context.SaveChangesAsync();
            return farmer.Adapt<FarmerResponse>();
        }


        public async Task<bool> UpdateFarmer(FarmerRequest request, CancellationToken cancellation)
        {
            var userId = _constFunc.GetUserId();

            var Oldfarmer = await _context.FarmerProfiles.FirstOrDefaultAsync(x => x.UserId == userId);


            Oldfarmer!.ProfessionalDescription = request.ProfessionalDescription;

            _context.FarmerProfiles.Update(Oldfarmer);
            await _context.SaveChangesAsync(cancellation);

            return true;
        }
          
        public async Task<bool> Toggle(CancellationToken cancellation)
        {
            var userId = _constFunc.GetUserId();

            var farmer = await _context.FarmerProfiles.Include(x=>x.User)
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (farmer == null)
                return false;

            farmer!.User.IsActive = !farmer.User.IsActive;  
            await _context.SaveChangesAsync(cancellation);
            return true;
        }

    
    }
}
