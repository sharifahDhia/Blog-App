using blogApp.Data;
using blogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blogApp.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly AppDbContext _context;

        public BlogPostController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var blogPosts = _context.Blog.ToList();

            return View(blogPosts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.CreatedAt = DateTime.Now;
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(blog);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.Blog.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blog blog)
        {
            if (id != blog.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.Blog.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _context.Blog.FindAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }
            _context.Blog.Remove(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public async Task<IActionResult> AddComment(int id, string Text)
        //{
        //    if (!string.IsNullOrEmpty(Text))
        //    {
        //        var blog = await _context.Blog.Include(b => b.Comments).FirstOrDefaultAsync(b => b.id == id);
        //        if (blog != null)
        //        {
        //            var comment = new Comment
        //            {
        //                Text = Text,
        //                CreatedAt = DateTime.Now,
        //                AuthorEmail = User?.Identity?.Name ?? ""
        //            };
        //            blog.Comments.Add(comment);
        //            await _context.SaveChangesAsync();
        //        }
        //    }

        //    return RedirectToAction("CreateComment", new { id = id });
        //}
    }
}
