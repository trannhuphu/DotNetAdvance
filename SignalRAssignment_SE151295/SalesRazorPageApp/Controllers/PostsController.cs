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
using DataAccess.Repository;

namespace SalesRazorPageApp.Controllers
{
    public class PostsController : Controller
    {
        public IPostRepository repository = new PostRepository();
        public IUserRepository userRepository = new UserRepository();
        public IHubContext<SignalrServer> _signalRHub;

        public PostsController(IHubContext<SignalrServer> signalRHub)
        {
            _signalRHub = signalRHub;
        }

        // GET: Posts
        public  IActionResult Index()
        {
            ViewBag.IsMemberLogin = userRepository.CheckIsMemberLogin().ToString();
            return View();
        }

        // GET: Posts
        [HttpGet]
        public List<Posts> GetPostList()
        {

            var applicationDBContext = repository.GetPostList();
            return applicationDBContext;
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*            var posts = await _context.Posts
                            .Include(p => p.AppUsers)
                            .Include(p => p.PostCategories)
                            .FirstOrDefaultAsync(m => m.PostID == id);*/
            var posts = repository.GetPostById((int)id);

            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(repository.GetAppUserList(), "UserID", "FullName");
            ViewData["CategoryID"] = new SelectList(repository.GetCategoryList(), "CategoryID", "CategoryName");
            return View();
        }

        // POST: Posts/Create
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostID,AuthorID,CreatedDate,UpdatedDate,Title,Content,PublishStatus,CategoryID")] Posts posts)
        {
            if (ModelState.IsValid)
            {
                repository.CreatePost(posts);
                await _signalRHub.Clients.All.SendAsync("LoadPosts");
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(repository.GetAppUserList(), "UserID", "FullName", posts.AuthorID);
            ViewData["CategoryID"] = new SelectList(repository.GetCategoryList(), "CategoryID", "CategoryName", posts.CategoryID);
            return View(posts);
        }

        // GET: Posts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = repository.GetPostById((int)id);
            if (posts == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(repository.GetAppUserList(), "UserID", "UserID", posts.AuthorID);
            ViewData["CategoryID"] = new SelectList(repository.GetCategoryList(), "CategoryID", "CategoryName", posts.CategoryID);
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
                    repository.UpdatePost(posts);
                    await _signalRHub.Clients.All.SendAsync("LoadPosts");
                }
                catch
                {

                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(repository.GetAppUserList(), "UserID", "UserID", posts.AuthorID);
            ViewData["CategoryID"] = new SelectList(repository.GetCategoryList(), "CategoryID", "CategoryID", posts.CategoryID);
            return View(posts);
        }

        // GET: Posts/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = repository.GetPostById((int)id);
               
           /* if (posts == null)
            {
                return NotFound();
            }*/

            return View(posts);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int PostID)
        {

            var posts = repository.GetPostById(PostID);
            repository.DeletePost(posts);

            await _signalRHub.Clients.All.SendAsync("LoadPosts");
            return RedirectToAction(nameof(Index));
        }
    }
}
