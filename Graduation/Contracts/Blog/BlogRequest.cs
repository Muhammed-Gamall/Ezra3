namespace Graduation.Contracts.Blog
{
    public record BlogRequest
    (
         string Title,
         string Content,
         IFormFile? Photo,
         string? Excerpt,
         string? Category,
         string? Author
    );
}
