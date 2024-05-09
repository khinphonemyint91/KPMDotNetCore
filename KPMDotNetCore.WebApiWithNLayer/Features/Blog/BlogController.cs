using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPMDotNetCore.WebApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BusinessLogic_Blog _businessLogic_Blog;

        public BlogController()
        {
            _businessLogic_Blog=new BusinessLogic_Blog();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var lst = _businessLogic_Blog.GetBlog();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            var item = _businessLogic_Blog.GetBlogById(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            var result = _businessLogic_Blog.CreateBlog(blog);
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _businessLogic_Blog.GetBlogById(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }

            var result = _businessLogic_Blog.UpdateBlog(id,blog);
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _businessLogic_Blog.GetBlogById(id);
           
            if (item is null)
            {
                return NotFound("No data found.");
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }

            var result = _businessLogic_Blog.PatchBlog(id, blog);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _businessLogic_Blog.GetBlogById(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
           
            var result = _businessLogic_Blog.DeleteBlog(id);
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }
    }
}
