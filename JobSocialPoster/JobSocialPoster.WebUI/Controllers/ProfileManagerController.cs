using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSocialPoster.Core.Models;
using JobSocialPoster.DataAccess.InMemory;


namespace JobSocialPoster.WebUI.Controllers
{
    public class ProfileManagerController : Controller
    {
        ProfileRepository context;

        public ProfileManagerController()
        {
            context = new ProfileRepository();
        }

        // GET: ProfileManager
        public ActionResult Index()
        {
            List<Profile> profiles = context.Collection().ToList();
            return View(profiles);

        }

        public ActionResult Create()
        {
            Profile profile = new Profile();
            return View(profile);
        }

        [HttpPost]
        public ActionResult Create(Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return View(profile);
            }
            else
            {
                context.Insert(profile);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Profile profile = context.Find(Id);

            if (profile == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(profile);
            }
        }

        [HttpPost]
        public ActionResult Edit(Profile profile, string Id)
        {
            Profile profileToEdit = context.Find(Id);

            if (profileToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(profile);
                }

                profileToEdit.Category = profile.Category;
                profileToEdit.File = profile.File;
                profileToEdit.ImportCsv = profile.ImportCsv;
                profileToEdit.IsActive = profile.IsActive;
                profileToEdit.IsSent = profile.IsSent;
                profileToEdit.Name = profile.Name;
                profileToEdit.SocialpilotId = profile.SocialpilotId;
                profileToEdit.Url = profile.Url;
                profileToEdit.Weight = profile.Weight;

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            Profile profileToDelete = context.Find(Id);

            if (profileToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(profileToDelete);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Profile profileToDelete = context.Find(Id);

            if (profileToDelete == null)
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