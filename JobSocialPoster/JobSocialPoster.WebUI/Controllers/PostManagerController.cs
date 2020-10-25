using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsvHelper;
using JobSocialPoster.Core.Contracts;
using JobSocialPoster.Core.Models;
using JobSocialPoster.Core.ViewModels;
using JobSocialPoster.DataAccess.InMemory;


namespace JobSocialPoster.WebUI.Controllers
{
    public class PostManagerController : Controller
    {
        IRepository<Post> context;
        IRepository<PostCategory> postCategories;

        public PostManagerController(IRepository<Post> postContext, IRepository<PostCategory> postCategoryContext)
        {
            context = postContext;
            postCategories = postCategoryContext;
        }

        // GET: PostManager
        public ActionResult Index(string message)
        {
            List<Post> posts = context.Collection().ToList();

            ViewBag.message = Request.QueryString["message"];

            return View(posts);

        }

        public ActionResult Create()
        {
            PostManagerViewModel viewModel = new PostManagerViewModel();

            viewModel.Post = new Post();
            viewModel.PostCategories = postCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }
            else
            {

                context.Insert(post);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Post post = context.Find(Id);

            if (post == null)
            {
                return HttpNotFound();
            }
            else
            {
                PostManagerViewModel viewModel = new PostManagerViewModel();
                viewModel.Post = post;
                viewModel.PostCategories = postCategories.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Post post, string Id)
        {
            Post postToEdit = context.Find(Id);

            if (postToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(post);
                }

                postToEdit.Title = post.Title;
                postToEdit.PostContent = post.PostContent;
                postToEdit.PostImg = post.PostImg;
                postToEdit.Url = post.Url;
                postToEdit.UrlShort = post.UrlShort;
                postToEdit.Status = post.Status;
                postToEdit.Category = post.Category;

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            Post postToDelete = context.Find(Id);

            if (postToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(postToDelete);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Post postToDelete = context.Find(Id);

            if (postToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}