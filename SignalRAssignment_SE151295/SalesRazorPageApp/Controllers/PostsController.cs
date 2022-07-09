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
using Microsoft.AspNetCore.Http;

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

        public void checkPermission()
        {

                //ViewBag.IsMemberLogin = "False";
            

/*                ViewBag.IsMemberLogin = "True";
                if (userRepository.GetCurrentMemberLogin() != null)
                {
                    ViewBag.UserName = userRepository.GetCurrentMemberLogin().FullName;
                }*/
            
        }

        // GET: Posts
        public  IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }

            ViewBag.IsMemberLogin = "False";
            return View();
        }

        public IActionResult IndexUser(int? userId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }

            ViewBag.IsMemberLogin = "True";
            if (userId != null)
            {
                var tempUserName = userRepository.GetUsersById((int)userId);
                if (tempUserName != null)
                {
                    ViewBag.UserName = tempUserName.FullName;
                    ViewBag.UserID = tempUserName.UserID;
                }
            }
            else if (userRepository.GetCurrentMemberLogin() != null)
            {
                ViewBag.UserName = userRepository.GetCurrentMemberLogin().FullName;
                ViewBag.UserID = userRepository.GetCurrentMemberLogin().UserID;
            }
            return View();
        }

        // GET: Posts
        [HttpGet]
        public List<Posts> GetPostList()
        {
            var applicationDBContext = repository.GetPostList();
            return applicationDBContext;
        }

 
        [HttpPost]
        public IActionResult SearchPostView(string search)
        {
            var listSearch = repository.SearchPost(search);
            ViewBag.IsMemberLogin = "False";
            if (listSearch == null || listSearch.Count == 0)
            {
                ViewBag.ErrMsgSearch = "Search not found!";
            }
            return View(listSearch);
        }


        [HttpPost]
        public IActionResult SearchPostViewUser(string search, string userId)
        {
            var listSearch = repository.SearchPost(search);
            ViewBag.IsMemberLogin = "True";

            var tempUserName = userRepository.GetUsersById(int.Parse(userId));

            if(tempUserName != null)
            {
                ViewBag.UserName = tempUserName.FullName;
                ViewBag.UserID = tempUserName.UserID;
            } else if (userRepository.GetCurrentMemberLogin() != null)
            {
                ViewBag.UserName = userRepository.GetCurrentMemberLogin().FullName;
                ViewBag.UserID = userRepository.GetCurrentMemberLogin().UserID;
            }
            if (listSearch == null || listSearch.Count == 0)
            {
                ViewBag.ErrMsgSearch = "Search not found!";
            }
            return View(listSearch);
        }


        // GET: Posts/Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }
            ViewBag.IsMemberLogin = "False";

            ViewData["AuthorID"] = new SelectList(repository.GetAppUserList(), "UserID", "FullName");
            ViewData["CategoryID"] = new SelectList(repository.GetCategoryList(), "CategoryID", "CategoryName");
            return View();
        }

        // GET: Posts/Create
        public IActionResult CreateUser(int? userId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }

            ViewBag.IsMemberLogin = "True";
            
            var tempUserName = userRepository.GetUsersById((int)userId);
            if(tempUserName != null)
            {
                ViewBag.UserName = tempUserName.FullName;
                ViewBag.UserID = tempUserName.UserID;
            } else if (userRepository.GetCurrentMemberLogin() != null)
            {
                ViewBag.UserName = userRepository.GetCurrentMemberLogin().FullName;
                ViewBag.UserID = userRepository.GetCurrentMemberLogin().UserID;
            }

            List<AppUsers> listApp = new List<AppUsers>();
            listApp.Add(tempUserName);
            ViewData["AuthorID"] = new SelectList(listApp, "UserID", "FullName");
            ViewData["CategoryID"] = new SelectList(repository.GetCategoryList(), "CategoryID", "CategoryName");
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostID,AuthorID,CreatedDate,UpdatedDate,Title,Content,PublishStatus,CategoryID")] Posts posts)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }
            ViewBag.IsMemberLogin = "False";


            if (ModelState.IsValid)
            {
                repository.CreatePost(posts);
                TempData["SuccessMessage"] = "Create successfully";
                await _signalRHub.Clients.All.SendAsync("LoadPosts");
                return View(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(repository.GetAppUserList(), "UserID", "FullName", posts.AuthorID);
            ViewData["CategoryID"] = new SelectList(repository.GetCategoryList(), "CategoryID", "CategoryName", posts.CategoryID);
            return View(posts);
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser([Bind("PostID,AuthorID,CreatedDate,UpdatedDate,Title,Content,PublishStatus,CategoryID")] Posts posts)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }

            ViewBag.IsMemberLogin = "True";

            var tempUserName =  userRepository.GetUsersById(posts.AuthorID);
               
            if(tempUserName != null)
            {
                ViewBag.UserName = tempUserName.FullName;
                ViewBag.UserID = tempUserName.UserID;
            } else if (userRepository.GetCurrentMemberLogin() != null)
            {
                ViewBag.UserName = userRepository.GetCurrentMemberLogin().FullName;
                ViewBag.UserID = userRepository.GetCurrentMemberLogin().UserID;
            }

            if (ModelState.IsValid)
            {
                repository.CreatePost(posts);
                TempData["SuccessMessage"] = "Create successfully";
                await _signalRHub.Clients.All.SendAsync("LoadPosts");
                return View(nameof(IndexUser));
            }

            List<AppUsers> listApp = new List<AppUsers>();
            listApp.Add(tempUserName);
            ViewData["AuthorID"] = new SelectList(listApp, "UserID", "FullName");
            ViewData["CategoryID"] = new SelectList(repository.GetCategoryList(), "CategoryID", "CategoryName", posts.CategoryID);
            return View(posts);
        }

        // GET: Posts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }

            if (id == null)
            {
                return NotFound();
            }
            ViewBag.IsMemberLogin = "False";


            var posts = repository.GetPostById((int)id);
            if (posts == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(repository.GetAppUserList(), "UserID", "FullName", posts.AuthorID);
            ViewData["CategoryID"] = new SelectList(repository.GetCategoryList(), "CategoryID", "CategoryName", posts.CategoryID);
            return View(posts);
        }

        // GET: Posts/Edit/5
        public IActionResult EditUser(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }

            if (id == null)
            {
                return NotFound();
            }
            ViewBag.IsMemberLogin = "True";
            var tempUserName = repository.GetPostById((int)id).AppUsers;
            if (tempUserName != null)
            {
                ViewBag.UserName = tempUserName.FullName;
                ViewBag.UserID = tempUserName.UserID;
            }
            else if (userRepository.GetCurrentMemberLogin() != null)
            {
                ViewBag.UserName = userRepository.GetCurrentMemberLogin().FullName;
                ViewBag.UserID = userRepository.GetCurrentMemberLogin().UserID;
            }


            var posts = repository.GetPostById((int)id);
            if (posts == null)
            {
                return NotFound();
            }
            List<AppUsers> listApp = new List<AppUsers>();
            listApp.Add(repository.GetPostById((int)id).AppUsers);
            ViewData["AuthorID"] = new SelectList(listApp, "UserID", "FullName");
            //   ViewData["AuthorID"] = new SelectList(repository.GetAppUserList(), "UserID", "FullName", posts.AuthorID);
            ViewData["CategoryID"] = new SelectList(repository.GetCategoryList(), "CategoryID", "CategoryName", posts.CategoryID);
            return View(posts);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int PostID, [Bind("PostID,AuthorID,CreatedDate,UpdatedDate,Title,Content,PublishStatus,CategoryID")] Posts posts)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }
            if (PostID != posts.PostID)
            {
                return NotFound();
            }

            ViewBag.IsMemberLogin = "False";

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["SuccessMessage"] = "Edit successfully";
                    repository.UpdatePost(posts);
                    await _signalRHub.Clients.All.SendAsync("LoadPosts");
                }
                catch
                {

                }

                return View(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(repository.GetAppUserList(), "UserID", "FullName", posts.AuthorID);
            ViewData["CategoryID"] = new SelectList(repository.GetCategoryList(), "CategoryID", "CategoryID", posts.CategoryID);
            return View(posts);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int PostID, [Bind("PostID,AuthorID,CreatedDate,UpdatedDate,Title,Content,PublishStatus,CategoryID")] Posts posts)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }
            if (PostID != posts.PostID)
            {
                return NotFound();
            }

            ViewBag.IsMemberLogin = "True";
            var tempUserName = repository.GetPostById(PostID).AppUsers;
            if(tempUserName != null)
            {
                ViewBag.UserName = tempUserName.FullName;
                ViewBag.UserID = tempUserName.UserID;
            }    
            else if (userRepository.GetCurrentMemberLogin() != null)
            {
                ViewBag.UserName = userRepository.GetCurrentMemberLogin().FullName;
                ViewBag.UserID = userRepository.GetCurrentMemberLogin().UserID;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["SuccessMessage"] = "Edit successfully";
                    repository.UpdatePost(posts);
                    await _signalRHub.Clients.All.SendAsync("LoadPosts");
                }
                catch
                {

                }

                return View(nameof(IndexUser));
            }
            List<AppUsers> listApp = new List<AppUsers>();
            listApp.Add(repository.GetPostById((int)PostID).AppUsers);
            ViewData["AuthorID"] = new SelectList(listApp, "UserID", "FullName");
            //   ViewData["AuthorID"] = new SelectList(repository.GetAppUserList(), "UserID", "FullName", posts.AuthorID);

            ViewData["CategoryID"] = new SelectList(repository.GetCategoryList(), "CategoryID", "CategoryID", posts.CategoryID);
            return View(posts);
        }

        // GET: Posts/Delete/5
        public IActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }

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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToAction("ErrorSession", "Login");
            }
            checkPermission();


            var posts = repository.GetPostById(PostID);
            repository.DeletePost(posts);
            TempData["SuccessMessage"] = "Delete successfully";
            await _signalRHub.Clients.All.SendAsync("LoadPosts");
            return RedirectToAction(nameof(Index));
        }
    }
}
