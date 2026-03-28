
namespace Graduation.Services
{
    public interface IUserService
    {
        Task<UserResponse?> GetUserAsync(CancellationToken cancellation = default);
        Task<UserResponse> CreateUserAsync(UserRequest request, CancellationToken cancellation = default);
        Task<UserResponse> UpdateUserAsync(UserRequest request, CancellationToken cancellation = default);
        Task ToggleAsync( CancellationToken cancellation = default);
    }

    public class UserService(ApplicationDbContext context , IHttpContextAccessor accessor) : IUserService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _accessor = accessor;


        public async Task<UserResponse?> GetUserAsync(CancellationToken cancellation = default)
        {
            var userId =GetUserId();

            var userProfile = await _context.UserProfiles.SingleOrDefaultAsync(up => up.UserId == userId);

            return userProfile.Adapt<UserResponse>();   
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest request, CancellationToken cancellation = default)
        {
            var userId = GetUserId();

            var userProfile = new UserProfile
            {
                Name = request.Name,
                UserId = userId
            };
            await _context.UserProfiles.AddAsync(userProfile);
            await _context.SaveChangesAsync();
            return userProfile.Adapt<UserResponse>();
        }

        public async Task<UserResponse> UpdateUserAsync(UserRequest request, CancellationToken cancellation = default)
        {
            var userId = GetUserId();

            var userProfile = await _context.UserProfiles.SingleOrDefaultAsync(up => up.UserId == userId);

            userProfile!.Name = request.Name;

            _context.UserProfiles.Update(userProfile);
            await _context.SaveChangesAsync();
            return userProfile.Adapt<UserResponse>();
        }

        public async Task ToggleAsync(CancellationToken cancellation = default)
        {
            var userId = GetUserId();
            var userProfile = await _context.UserProfiles.SingleOrDefaultAsync(up => up.UserId == userId);

            userProfile!.IsActive = !userProfile.IsActive;
            await _context.SaveChangesAsync();
            return ;
        }

        public string GetUserId()
        {
            return _accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

    }
}
