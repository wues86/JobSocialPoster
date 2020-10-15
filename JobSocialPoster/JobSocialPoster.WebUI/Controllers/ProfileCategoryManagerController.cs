using JobSocialPoster.Core.Models;
using JobSocialPoster.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobSocialPoster.WebUI.Controllers
{
    public class ProfileCategoryManagerController : Controller
    {
        ProfileCategoryRepository context;

        public ProfileCategoryManagerController()
        {
            context = new ProfileCategoryRepository();
        }

        // GET: ProfileManager
        public ActionResult Index()
        {
            List<ProfileCategory> profileCategories = context.Collection().ToList();
            return View(profileCategories);

        }

        public ActionResult Create()
        {
            ProfileCategory profileCategory = new ProfileCategory();
            return View(profileCategory);
        }

        [HttpPost]
        public ActionResult Create(ProfileCategory profileCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(profileCategory);
            }
            else
            {
                context.Insert(profileCategory);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProfileCategory profileCategory = context.Find(Id);

            if (profileCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(profileCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProfileCategory profileCategory, string Id)
        {
            ProfileCategory profileCategoryToEdit = context.Find(Id);

            if (profileCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(profileCategory);
                }

                profileCategoryToEdit.Name = profileCategory.Name;

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            ProfileCategory profileCategoryToDelete = context.Find(Id);

            if (profileCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(profileCategoryToDelete);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProfileCategory profileCategoryToDelete = context.Find(Id);

            if (profileCategoryToDelete == null)
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