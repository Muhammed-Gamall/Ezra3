namespace Graduation.Contracts.Authentication
{
    public record SignUpRequest
    (
        string FName,
        string LName,
        string Email,
        string Password
    );
}
