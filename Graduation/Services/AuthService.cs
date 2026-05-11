using Azure.Core;
using Graduation.Consts;
using Graduation.Contracts.Authentication;
using System.Security.Cryptography;

namespace Graduation.Services
{
    public interface IAuthService
    {
        public Task<AuthResponse?> SignUpAsync(SignUpRequest signUpRequest , string role, CancellationToken cancellationToken);
        public Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
        public Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken);
    }

    public class AuthService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly int _RefreshExpirationDays = 14;

        public async Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return null;


            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var (token, expiration) = _jwtProvider.GenerateToken(user , roles);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = DateTime.UtcNow.AddDays(_RefreshExpirationDays)
            });

            await _userManager.UpdateAsync(user);
            return new AuthResponse(user.Id, user.Email!, user.FullName, token, expiration, newRefreshToken, DateTime.UtcNow.AddDays(_RefreshExpirationDays));

        }

        public async Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken)
        {
            var userId = _jwtProvider.ValidateToken(token);
            if (userId == null)
                return null;

            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return null;

            var ExistingRefreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken && rt.IsActive);
            if (ExistingRefreshToken == null)
                return null;

            ExistingRefreshToken.RevokedOn = DateTime.UtcNow;

            var roles = await _userManager.GetRolesAsync(user);
            var (newToken, expiration) = _jwtProvider.GenerateToken(user, roles);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = DateTime.UtcNow.AddDays(_RefreshExpirationDays)
            });

            await _userManager.UpdateAsync(user);
            return new AuthResponse(user.Id, user.Email!, user.FullName, newToken, expiration, newRefreshToken, DateTime.UtcNow.AddDays(_RefreshExpirationDays));
        }
        public async Task<AuthResponse?> SignUpAsync(SignUpRequest signUpRequest , string role, CancellationToken cancellationToken)
        {
            var IsEmailExist = await _userManager.FindByEmailAsync(signUpRequest.Email);
            if (IsEmailExist != null)
                return null;

            var user = new ApplicationUser
            {
                Email = signUpRequest.Email,
                UserName = signUpRequest.Email,
                FullName = signUpRequest.FullName
            };

            var result = await _userManager.CreateAsync(user, signUpRequest.Password);
            if (!result.Succeeded)
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            var (token, expireIn) = _jwtProvider.GenerateToken(user, roles);

            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(_RefreshExpirationDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiry
            });

            await _userManager.AddToRoleAsync(user , role);
            await _userManager.UpdateAsync(user);


            return new AuthResponse(user.Id, user.Email!, user.FullName, token, expireIn, refreshToken, refreshTokenExpiry);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

     
    }
}
