using JobSocialPoster.Core.Contracts;
using JobSocialPoster.Core.Models;
using JobSocialPoster.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobSocialPoster.WebUI.Controllers
{
    public class PostCategoryManagerController : Controller
    {
        IRepository<PostCategory> context;

        public PostCategoryManagerController(IRepository<PostCategory> context)
        {
            this.context = context;
        }

        // GET: PostManager
        public ActionResult Index()
        {
            List<PostCategory> postCategories = context.Collection().ToList();
            return View(postCategories);

        }

        public ActionResult Create()
        {
            PostCategory postCategory = new PostCategory();
            return View(postCategory);
        }

        [HttpPost]
        public ActionResult Create(PostCategory postCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(postCategory);
            }
            else
            {
                context.Insert(postCategory);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            PostCategory postCategory = context.Find(Id);

            if (postCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(postCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(PostCategory postCategory, string Id)
        {
            PostCategory postCategoryToEdit = context.Find(Id);

            if (postCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(postCategory);
                }

                postCategoryToEdit.Name = postCategory.Name;

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            PostCategory postCategoryToDelete = context.Find(Id);

            if (postCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(postCategoryToDelete);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            PostCategory postCategoryToDelete = context.Find(Id);

            if (postCategoryToDelete == null)
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