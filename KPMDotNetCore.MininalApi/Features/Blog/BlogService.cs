﻿using KPMDotNetCore.MininalApi.Db;
using KPMDotNetCore.MininalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KPMDotNetCore.MininalApi.Features.Blog
{
    public static class BlogService
    {
        public static IEndpointRouteBuilder MapBlogs(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/BlogList", async (AppDbContext db) =>
            {
                var lst = await db.Blogs.AsNoTracking().ToListAsync();
                return Results.Ok(lst);
            });

            app.MapPost("api/BlogCreate", async (AppDbContext db, BlogModel blog) =>
            {
                await db.Blogs.AddAsync(blog);
                var result = await db.SaveChangesAsync();

                string message = result > 0 ? "Saving Successful." : "Saving Failed.";
                return Results.Ok(message);
            });

            app.MapPut("api/BlogUpdate/{id}", async (AppDbContext db, int id, BlogModel blog) =>
            {
                var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.NotFound("No data found.");
                }
                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;

                var result = await db.SaveChangesAsync();
                string message = result > 0 ? "Updating Successful." : "Updating Failed.";
                return Results.Ok(message);
            });

            app.MapDelete("api/BlogDelete/{id}", async (AppDbContext db, int id) =>
            {
                var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.NotFound("No data found.");
                }
                db.Blogs.Remove(item);

                var result = await db.SaveChangesAsync();
                string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
                return Results.Ok(message);
            });

            return app;
        }
    }
}