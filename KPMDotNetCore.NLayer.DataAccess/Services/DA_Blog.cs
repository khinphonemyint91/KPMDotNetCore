using KPMDotNetCore.NLayer.DataAccess.Db;
using KPMDotNetCore.NLayer.DataAccess.Models;

namespace KPMDotNetCore.NLayer.DataAccess.Services
{
    public class DA_Blog
    {
        private readonly AppDbContext _DbContext;
        public DA_Blog()
        {
            _DbContext = new AppDbContext();
        }

        public List<BlogModel> GetBlog()
        {
            var lst = _DbContext.Blogs.ToList();
            return lst;
        }

        public BlogModel GetBlogById(int id)
        {
            var item = _DbContext.Blogs.FirstOrDefault(x => x.BlogId == id);

            return item;
        }

        public int CreateBlog(BlogModel requestmodel)
        {
            _DbContext.Blogs.Add(requestmodel);
            var result = _DbContext.SaveChanges();
            return result;
        }
        public int UpdateBlog(int id, BlogModel requestmodel)
        {
            var item = _DbContext.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null) return 0;

            item.BlogTitle = requestmodel.BlogTitle;
            item.BlogAuthor = requestmodel.BlogAuthor;
            item.BlogContent = requestmodel.BlogContent;

            var result = _DbContext.SaveChanges();
            return result;
        }

        public int PatchBlog(int id, BlogModel requestModel)
        {
            var item = _DbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null) return 0;

            if (!string.IsNullOrEmpty(requestModel.BlogTitle))
            {
                item.BlogTitle = requestModel.BlogTitle;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
            {
                item.BlogAuthor = requestModel.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogContent))
            {
                item.BlogContent = requestModel.BlogContent;
            }

            var result = _DbContext.SaveChanges();
            return result;
        }
        public int DeleteBlog(int id)
        {
            var item = _DbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null) return 0;

            _DbContext.Blogs.Remove(item);
            var result = _DbContext.SaveChanges();
            return result;
        }
    }


}
