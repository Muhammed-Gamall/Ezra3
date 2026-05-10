namespace Graduation.Contracts.Blog
{
    public record BlogResponse
    (
        int Id,
        string Title,
        string Content,
        string? Image,
        string? Excerpt,
        string? Category,
        string? Author
    );
}
