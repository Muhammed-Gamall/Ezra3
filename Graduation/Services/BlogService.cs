using Graduation.Contracts.Blog;

namespace Graduation.Services
{

    public interface IBlogService
    {
        Task<IEnumerable<BlogResponse>> GetAllBlogsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<BlogResponse>> GetBlogsByCategoryAsync(string category, CancellationToken cancellationToken);
        Task<BlogResponse?> GetBlogAsync(int Id, CancellationToken cancellationToken);
        Task<BlogResponse> CreateBlogAsync(BlogRequest blogRequest , CancellationToken cancellationToken);
        Task<bool> UpdateBlogAsync(int Id, BlogRequest blogRequest , CancellationToken cancellationToken);
        Task<bool> Toggle(int Id, CancellationToken cancellationToken);

    }

    public class BlogService(ApplicationDbContext context , ConstFunc constFunc) : IBlogService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ConstFunc _constFunc = constFunc;

        public async Task<IEnumerable<BlogResponse>> GetAllBlogsAsync(CancellationToken cancellationToken)
        {
            var blogs = await _context.Blogs.AsNoTracking().Where(b => b.IsActive).ToListAsync(cancellationToken);
            return blogs.Adapt<IEnumerable<BlogResponse>>();
        }

        public async Task<BlogResponse?> GetBlogAsync(int Id, CancellationToken cancellationToken)
        {
            var blog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.Id == Id, cancellationToken);
            return blog?.Adapt<BlogResponse>();
        }

        public async Task<IEnumerable<BlogResponse>> GetBlogsByCategoryAsync(string category, CancellationToken cancellationToken)
        {
            var blogs = await _context.Blogs.AsNoTracking().Where(b => b.Category == category && b.IsActive).ToListAsync(cancellationToken);
            return blogs.Adapt<IEnumerable<BlogResponse>>();
        }

     

        public async Task<BlogResponse> CreateBlogAsync(BlogRequest blogRequest, CancellationToken cancellationToken)
        {
          var blog = blogRequest.Adapt<Blog>();

            if (blogRequest.Photo != null)
            {
                blog.Image = _constFunc.UpluodImage(blogRequest.Photo!);
                blog.HashCode = _constFunc.ComputeFileHash(blogRequest.Photo!);
            }

            await _context.Blogs.AddAsync(blog, cancellationToken);
          await _context.SaveChangesAsync(cancellationToken);
          return blog.Adapt<BlogResponse>();
        }

        public async Task<bool> UpdateBlogAsync(int Id, BlogRequest blogRequest, CancellationToken cancellationToken)
        {
            var existingBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == Id, cancellationToken);
            if (existingBlog is null)
                return false;
            
             blogRequest.Adapt(existingBlog);

            if (blogRequest.Photo != null)
            {
                var hashCode = _constFunc.ComputeFileHash(blogRequest.Photo!);
                if (hashCode != existingBlog.HashCode)
                {
                    existingBlog.Image = _constFunc.UpluodImage(blogRequest.Photo!);
                    existingBlog.HashCode = hashCode;
                }
            }
            _context.Blogs.Update(existingBlog);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> Toggle(int Id, CancellationToken cancellationToken)
        {
            var existingBlog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == Id, cancellationToken);
            if (existingBlog is null)
                return false;

            existingBlog.IsActive = !existingBlog.IsActive;
            _context.Blogs.Update(existingBlog);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

   
    }
}
