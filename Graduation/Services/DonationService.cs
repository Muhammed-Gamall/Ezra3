using Graduation.Contracts.Donation;

namespace Graduation.Services
{
    public interface IDonationService
    {
        public Task<IEnumerable<DonationResponse>> GetDonationsAsync(CancellationToken cancellationToken);
        public Task<DonationResponse> CreateDonationsAsync(DonationRequest request, CancellationToken cancellationToken);
        public Task<IEnumerable<DonationResponse>> GetDonationsForAdmin(CancellationToken cancellationToken);
    }
    public class DonationService(ApplicationDbContext context , ConstFunc constFunc) : IDonationService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ConstFunc constFunc = constFunc;

        public async Task<DonationResponse> CreateDonationsAsync(DonationRequest request, CancellationToken cancellationToken)
        {
            var donation = request.Adapt<Donation>();

            await _context.Donations.AddAsync(donation);
            await _context.SaveChangesAsync();
            return donation.Adapt<DonationResponse>();
        }

        public async Task<IEnumerable<DonationResponse>> GetDonationsAsync(CancellationToken cancellationToken)
        {
            var userId = constFunc.GetUserId();
            var donations = await _context.Donations.AsNoTracking().Where(x=> x.CreatedById == userId)
                .Select(p=> new DonationResponse
                (
                   p.CreatedBy.FullName,
                   p.TreeType.ToString(),
                   p.Amount,
                    p.Street,
                   p.District,
                   p.City,
                   p.Governorate
                  )).ToListAsync();
            return donations.Adapt<IEnumerable<DonationResponse>>();
        }
        public async Task<IEnumerable<DonationResponse>> GetDonationsForAdmin(CancellationToken cancellationToken)
        {
            var donations = await _context.Donations.AsNoTracking()
                .Select(p=> new DonationResponse
                (
                   p.CreatedBy.FullName,
                   p.TreeType.ToString(),
                   p.Amount,
                    p.Street,
                   p.District,
                   p.City,
                   p.Governorate
                  )).ToListAsync();
            return donations.Adapt<IEnumerable<DonationResponse>>();
        }
    }
}
