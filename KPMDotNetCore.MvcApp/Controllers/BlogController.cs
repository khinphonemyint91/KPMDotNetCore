﻿using KPMDotNetCore.MvcApp.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPMDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;

        public BlogController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var lst = await _db.Blogs.ToListAsync();
            return View(lst);
        }
    }
}
