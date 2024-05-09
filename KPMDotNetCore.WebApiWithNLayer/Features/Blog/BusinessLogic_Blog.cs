using Microsoft.EntityFrameworkCore;

namespace KPMDotNetCore.WebApiWithNLayer.Features.Blog
{
    public class BusinessLogic_Blog
    {
        private readonly DataAccess_Blog _dataAccess_Blog;
        public BusinessLogic_Blog()
        {
            _dataAccess_Blog = new DataAccess_Blog();
        }

        public List<BlogModel> GetBlog()
        {
            var lst = _dataAccess_Blog.GetBlog();
            return (lst);
        }

        public BlogModel GetBlogById(int id)
        {
            var item = _dataAccess_Blog.GetBlogById(id);
            return item;
        }

        public int CreateBlog(BlogModel requestmodel)
        {
            var result= _dataAccess_Blog.CreateBlog(requestmodel);
            return result;
        }
        public int UpdateBlog(int id, BlogModel requestmodel)
        {
            var result = _dataAccess_Blog.UpdateBlog(id,requestmodel);
            return result;
        }

        public int PatchBlog(int id,BlogModel requestmodel)
        {
            var result = _dataAccess_Blog.PatchBlog(id, requestmodel);
            return result;

        }
        public int DeleteBlog(int id)
        {
            var result= _dataAccess_Blog.DeleteBlog(id);
            return result;
        }
    }
}
