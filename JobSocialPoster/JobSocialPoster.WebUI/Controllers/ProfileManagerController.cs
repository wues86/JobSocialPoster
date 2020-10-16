using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSocialPoster.Core.Contracts;
using JobSocialPoster.Core.Models;
using JobSocialPoster.Core.ViewModels;
using JobSocialPoster.DataAccess.InMemory;


namespace JobSocialPoster.WebUI.Controllers
{
    public class ProfileManagerController : Controller
    {
        IRepository<Profile> context;
        IRepository<ProfileCategory> profileCategories;

        public ProfileManagerController(IRepository<Profile> profileContext, IRepository<ProfileCategory> profileCategoryContext)
        {
            context = profileContext;
            profileCategories = profileCategoryContext;
        }

        // GET: ProfileManager
        public ActionResult Index()
        {
            List<Profile> profiles = context.Collection().ToList();
            return View(profiles);

        }

        public ActionResult Create()
        {
            ProfileManagerViewModel viewModel = new ProfileManagerViewModel();

            viewModel.Profile = new Profile();
            viewModel.ProfileCategories = profileCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Profile profile, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(profile);
            }
            else
            {

                if (file != null)
                {
                    profile.File = profile.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Context//ImportFiles//") + profile.File);
                }
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
                ProfileManagerViewModel viewModel = new ProfileManagerViewModel();
                viewModel.Profile = profile;
                viewModel.ProfileCategories = profileCategories.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Profile profile, string Id, HttpPostedFileBase file)
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
                
                if (file != null)
                {
                    profileToEdit.File = file.FileName;
                    file.SaveAs(Server.MapPath("//Content//ImportFiles//") + profileToEdit.File);
                }

                profileToEdit.Category = profile.Category;
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