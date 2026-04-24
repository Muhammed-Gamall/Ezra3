namespace Graduation.Contracts.Authentication
{
    public record AuthResponse
    (
        string Id,
        string Email,
        string FName,
        string LName,
        string Token,
        int TokenExpiration,
        string RefreshToken,
        DateTime RefreshTokenExpiration
        );
}
