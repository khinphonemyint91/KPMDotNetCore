using Dapper;
using KPMDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using KPMDotNetCore.Shared;
namespace KPMDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_Blog";
            var lst = _adoDotNetService.Query<BlogModel>(query);
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from Tbl_Blog where BlogId=@BlogId";
           
            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query,
                new AdoDotNetParameter("@BlogId", id)
                );

            if (item is null)
            {
                return NotFound("No data found!");
            }
            
            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog) 
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
        VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            
            return Ok(message);
           
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data Found.");
            }
            blog.BlogId = id;

            string query = @"UPDATE [dbo].[Tbl_Blog]
           SET [BlogTitle] = @BlogTitle
              ,[BlogAuthor] = @BlogAuthor
              ,[BlogContent] = @BlogContent
         WHERE BlogId=@BlogId";


            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogId", id),
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id,BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data Found.");
            }
            // Initialize a list to store parameters
            List<AdoDotNetParameter> parameters = new List<AdoDotNetParameter>();

            // Initialize conditions string
            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
                parameters.Add(new AdoDotNetParameter("@BlogTitle", blog.BlogTitle));
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
                parameters.Add(new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor));
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
                parameters.Add(new AdoDotNetParameter("@BlogContent", blog.BlogContent));
            }

            // Check if any conditions were added
            if (conditions.Length == 0)
            {
                return NotFound("No data to update");
            }

            // Remove the trailing comma and space
            conditions = conditions.Substring(0, conditions.Length - 2);

            // Add BlogId parameter
            parameters.Add(new AdoDotNetParameter("@BlogId", id));

            // Build the SQL query string
            string query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId=@BlogId";

            // Execute the query with parameters
            int result = _adoDotNetService.Execute(query, parameters.ToArray());

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data Found.");
            }
            string query = @"delete from Tbl_Blog where BlogId=@BlogId";

            int result = _adoDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", id));
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }
        private BlogModel? FindById(int id)
        {
            string query = "select * from Tbl_Blog where BlogId=@BlogId";
            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));
            return item;

        }
    }
}
