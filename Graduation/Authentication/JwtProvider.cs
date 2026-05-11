using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace Graduation.Authentication
{
    public interface IJwtProvider
    {
        public (string token , int expiration) GenerateToken(ApplicationUser user, IEnumerable<string> roles);
        public string? ValidateToken(string token);

    }

    public class JwtProvider(IOptions<OptionPattern> options) : IJwtProvider
    {
        private readonly OptionPattern _options = options.Value;
        public (string token, int expiration) GenerateToken(ApplicationUser user, IEnumerable<string> roles)
        {
            Claim[] claims = new Claim[]
        {
                new (JwtRegisteredClaimNames.Sub, user.Id),
                new (JwtRegisteredClaimNames.Email, user.Email!),
                new (JwtRegisteredClaimNames.GivenName, user.FullName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(nameof(roles), JsonSerializer.Serialize(roles), JsonClaimValueTypes.JsonArray),
        };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.key));

            var signingCredentiols = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _options.issuer,
                audience: _options.audience,
                claims: claims,
                signingCredentials: signingCredentiols,
                expires: DateTime.UtcNow.AddMinutes(_options.durationInMinutes)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return (token, _options.durationInMinutes);
        }

        public string? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.key));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = symmetricSecurityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var userId = ((JwtSecurityToken)validatedToken).Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
                return userId!;
            }
            catch
            {
                return null;
            }
        }
    }
}
