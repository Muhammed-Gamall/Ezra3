namespace Graduation.Contracts.Authentication
{
    public record AuthResponse
    (
        string Id,
        string Email,
        string FullName,
        string Token,
        int TokenExpiration,
        string RefreshToken,
        DateTime RefreshTokenExpiration
        );
}
