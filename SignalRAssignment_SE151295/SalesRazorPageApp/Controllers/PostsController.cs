using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using SignalRAssignment;
using Microsoft.AspNetCore.SignalR;

namespace SalesRazorPageApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDBContext _context;
        public IHubContext<SignalrServer> _signalRHub;

        public PostsController(ApplicationDBContext context, IHubContext<SignalrServer> signalRHub)
        {
            _context = context;
            _signalRHub = signalRHub;
        }

        // GET: Posts
        public  IActionResult Index()
        {
            //var applicationDBContext = _context.Posts.Include(p => p.AppUsers).Include(p => p.PostCategories).ToList();
            // _signalRHub.Clients.All.SendAsync("LoadPosts");
            return View();
        }

        // GET: Posts
        [HttpGet]
        public IActionResult GetPostList()
        {
            var applicationDBContext = _context.Posts
                .Include(o => o.PostCategories)
                .ToList();
            return Ok(applicationDBContext);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .Include(p => p.AppUsers)
                .Include(p => p.PostCategories)
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.AppUsers, "UserID", "FullName");
            ViewData["CategoryID"] = new SelectList(_context.PostCategories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Posts/Create
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostID,AuthorID,CreatedDate,UpdatedDate,Title,Content,PublishStatus,CategoryID")] Posts posts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(posts);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadPosts");
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.AppUsers, "UserID", "FullName", posts.AuthorID);
            ViewData["CategoryID"] = new SelectList(_context.PostCategories, "CategoryID", "CategoryName", posts.CategoryID);
            return View(posts);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts.FindAsync(id);
            if (posts == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.AppUsers, "UserID", "UserID", posts.AuthorID);
            ViewData["CategoryID"] = new SelectList(_context.PostCategories, "CategoryID", "CategoryName", posts.CategoryID);
            return View(posts);
        }

        // POST: Posts/Edit/5
       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int PostID, [Bind("PostID,AuthorID,CreatedDate,UpdatedDate,Title,Content,PublishStatus,CategoryID")] Posts posts)
        {
            if (PostID != posts.PostID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posts);
                    await _context.SaveChangesAsync();
                    await _signalRHub.Clients.All.SendAsync("LoadPosts");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostsExists(posts.PostID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.AppUsers, "UserID", "UserID", posts.AuthorID);
            ViewData["CategoryID"] = new SelectList(_context.PostCategories, "CategoryID", "CategoryID", posts.CategoryID);
            return View(posts);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .Include(p => p.AppUsers)
                .Include(p => p.PostCategories)
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int PostID)
        {
            var posts = await _context.Posts.FindAsync(PostID);
            _context.Posts.Remove(posts);
            await _context.SaveChangesAsync();
            await _signalRHub.Clients.All.SendAsync("LoadPosts");
            return RedirectToAction(nameof(Index));
        }

        private bool PostsExists(int id)
        {
            return _context.Posts.Any(e => e.PostID == id);
        }
    }
}
