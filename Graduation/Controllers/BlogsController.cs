using Graduation.Consts;
using Graduation.Contracts.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController(IBlogService blogService) : ControllerBase
    {
        private readonly IBlogService _blogService = blogService;

        [HttpGet]
        //[Authorize(Roles = DefaultRoles.Member)]
        public async Task<IActionResult> GetAllBlogs(CancellationToken cancellationToken)
        {
            var blogs = await _blogService.GetAllBlogsAsync(cancellationToken);
            return Ok(blogs);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(int id, CancellationToken cancellationToken)
        {
            var blog = await _blogService.GetBlogAsync(id, cancellationToken);
            return blog is null? NotFound(): Ok(blog);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetBlogsByCategory(string category, CancellationToken cancellationToken)
        {
            var blogs = await _blogService.GetBlogsByCategoryAsync(category, cancellationToken);
            return Ok(blogs);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBlog([FromForm] BlogRequest blogRequest, CancellationToken cancellationToken)
        {
            var blog = await _blogService.CreateBlogAsync(blogRequest, cancellationToken);
            return CreatedAtAction(nameof(GetBlog), new { id = blog.Id }, blog);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] BlogRequest blogRequest,int id , CancellationToken cancellationToken) { 

         var blog = await _blogService.UpdateBlogAsync(id, blogRequest , cancellationToken);
           return !blog ? NotFound() : NoContent();
        }

        [HttpPut("Toggle/{id}")]
        [Authorize]
        public async Task<IActionResult> Toggle(int id , CancellationToken cancellationToken) { 

         var blog = await _blogService.Toggle(id, cancellationToken);
           return !blog ? NotFound() : NoContent();
        }


    }
}
