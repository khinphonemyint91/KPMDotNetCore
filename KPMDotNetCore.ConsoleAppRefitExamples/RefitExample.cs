using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace KPMDotNetCore.ConsoleAppRefitExamples;

public class RefitExample
{
    private readonly IBlogApi _service = RestService.For<IBlogApi>("https://localhost:7011");
    public async Task RunAsync()
    {
        //await GetBlogsAsync();
        //await GetBlogByIdAsync(1);
        //await GetBlogByIdAsync(10);
        //await CreateAsync("test refil", "kpm", "content");
        //await UpdateAsync(10, "tilte refil", "author refil", "content refil");
        //await UpdateAsync(21,"tilte update2", "kpm2", "content2");
        //await GetBlogByIdAsync(10);
        //await DeleteAsync(21);
        //await DeleteAsync(10);
        await PatchAsync(9, "tilte patch2", "kpm patch2", "content patch2");
        await GetBlogByIdAsync(9);

    }
    public async Task GetBlogsAsync()
    {            
        var lst = await _service.GetBlogs();

        foreach (var item in lst)
        {
            Console.WriteLine($"Blog Id   =>{item.BlogId}");
            Console.WriteLine($"Blog Title   =>{item.BlogTitle}");
            Console.WriteLine($"Blog Author  =>{item.BlogAuthor}");
            Console.WriteLine($"Blog Content =>{item.BlogContent}");
            Console.WriteLine("------------------------------");
        }

    }
    public async Task GetBlogByIdAsync(int id)
    {
        try
        {
            var item = await _service.GetBlogById(id);

            Console.WriteLine($"Blog Id   =>{item.BlogId}");
            Console.WriteLine($"Blog Title   =>{item.BlogTitle}");
            Console.WriteLine($"Blog Author  =>{item.BlogAuthor}");
            Console.WriteLine($"Blog Content =>{item.BlogContent}");
            Console.WriteLine("------------------------------");
        }
        catch (ApiException ex) 
        {
            Console.WriteLine (ex.StatusCode);
            Console.WriteLine(ex.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task CreateAsync(string title, string author, string content)
    {
        BlogModel blog = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        var message = await _service.CreateBlog(blog);
        Console.WriteLine(message);

    }
    private async Task UpdateAsync(int id,string title, string author, string content)
    {
        BlogModel blog = new BlogModel()
        {   
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        try
        {
            var message = await _service.UpdateBlog(id,blog);
            Console.WriteLine(message);
        }
        catch (ApiException ex)
        {
            Console.WriteLine(ex.StatusCode);
            Console.WriteLine(ex.Content);
        }
    }
    public async Task DeleteAsync(int id)
    {
        try
        {
            var message = await _service.DeleteBlog(id);
            Console.WriteLine(message);
        }
        catch (ApiException ex) 
        {
            Console.WriteLine(ex.StatusCode);
            Console.WriteLine(ex.Content);
        }
    }

    private async Task PatchAsync(int id, string title, string author, string content)
    {
        BlogModel blog = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        try
        {
            var message = await _service.PatchBlog(id, blog);
            Console.WriteLine(message);
        }
        catch (ApiException ex)
        {
            Console.WriteLine(ex.StatusCode);
            Console.WriteLine(ex.Content);
        }

    }
}
