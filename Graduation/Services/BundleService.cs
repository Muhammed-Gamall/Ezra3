using Graduation.Contracts.Bundle;

namespace Graduation.Services
{
    public interface IBundleService
    {
        public Task<IEnumerable<BundleResponse>> GetAllBundlesAsync (CancellationToken cancellationToken);   
        public Task<BundleResponse> CreateBundleAsync (BundleRequest bundleRequest, CancellationToken cancellationToken);   
        public Task<bool> UpdateBundleAsync (int id ,BundleRequest bundleRequest, CancellationToken cancellationToken);   
        public Task<bool> Toggle (int id, CancellationToken cancellationToken);   

    }
    public class BundleService(ApplicationDbContext context, ConstFunc constFunc) : IBundleService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ConstFunc _constFunc = constFunc;

        public async Task<IEnumerable<BundleResponse>> GetAllBundlesAsync(CancellationToken cancellationToken)
        {
            var bundles = await _context.Bundles.Include(x=>x.Items).ThenInclude(i => i.Plant).
                Where(b => b.IsActive).AsNoTracking().ToListAsync(cancellationToken);

            return bundles.Adapt<IEnumerable<BundleResponse>>();
        }

        public async Task<BundleResponse> CreateBundleAsync(BundleRequest bundleRequest, CancellationToken cancellationToken)
        {
            var bundle = bundleRequest.Adapt<Bundle>();
                        
            if(bundleRequest.Photo != null)
            {
                bundle.Image = _constFunc.UpluodImage(bundleRequest.Photo);
                bundle.HashCode = _constFunc.ComputeFileHash(bundleRequest.Photo);
            }

            await _context.Bundles.AddAsync(bundle, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
                
                var response = await _context.Bundles.Where(o => o.Id == bundle.Id)
                .Include(o => o.Items)
                .ThenInclude(i => i.Plant).AsNoTracking()
                .FirstOrDefaultAsync();

            return response!.Adapt<BundleResponse>();
        }

        public async Task<bool> UpdateBundleAsync(int id, BundleRequest bundleRequest, CancellationToken cancellationToken)
        {
            var existingBundle = await _context.Bundles.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            if (existingBundle == null)
                return false;

             bundleRequest.Adapt(existingBundle);

            if (bundleRequest.Photo != null) {
                var hash = _constFunc.ComputeFileHash(bundleRequest.Photo);
                if(hash != existingBundle.HashCode)
                {
                    existingBundle.HashCode= hash;
                    existingBundle.Image = _constFunc.UpluodImage(bundleRequest.Photo);
                }
            }
            _context.Bundles.Update(existingBundle);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        public async Task<bool> Toggle(int id, CancellationToken cancellationToken)
        {
           var bundle = await _context.Bundles.FirstOrDefaultAsync(b=>b.Id == id);
            if (bundle == null)
                return false;

            bundle!.IsActive = !bundle.IsActive;

            _context.Bundles.Update(bundle);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}
