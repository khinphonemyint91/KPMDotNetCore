using Microsoft.EntityFrameworkCore;
using KPMDotNetCore.NLayer.DataAccess.Models;
using KPMDotNetCore.NLayer.DataAccess.Services;
namespace KPMDotNetCore.NLayer.BusinessLogic.Services
{
    public class BL_Blog
    {
        private readonly DA_Blog _da_blog;
        public BL_Blog()
        {
            _da_blog = new DA_Blog();
        }

        public List<BlogModel> GetBlog()
        {
            var lst = _da_blog.GetBlog();
            return lst;
        }

        public BlogModel GetBlogById(int id)
        {
            var item = _da_blog.GetBlogById(id);
            return item;
        }

        public int CreateBlog(BlogModel requestmodel)
        {
            var result = _da_blog.CreateBlog(requestmodel);
            return result;
        }
        public int UpdateBlog(int id, BlogModel requestmodel)
        {
            var result = _da_blog.UpdateBlog(id, requestmodel);
            return result;
        }

        public int PatchBlog(int id, BlogModel requestmodel)
        {
            var result = _da_blog.PatchBlog(id, requestmodel);
            return result;

        }
        public int DeleteBlog(int id)
        {
            var result = _da_blog.DeleteBlog(id);
            return result;
        }
    }
}
