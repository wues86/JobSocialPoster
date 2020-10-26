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
    public class ProfileManagerController : Controller
    {
        IRepository<Profile> context;
        IRepository<ProfileCategory> profileCategories;
        IRepository<Post> pcontext;

        public ProfileManagerController(IRepository<Profile> profileContext, IRepository<ProfileCategory> profileCategoryContext, IRepository<Post> postContext)
        {
            context = profileContext;
            profileCategories = profileCategoryContext;
            pcontext = postContext;
        }

        // GET: ProfileManager
        public ActionResult Index(string message)
        {
            List<Profile> profiles = context.Collection().ToList();
            
            ViewBag.message = Request.QueryString["message"];

            return View(profiles);

        }

        public ActionResult Create()
        {
            ProfileManagerViewModel viewModel = new ProfileManagerViewModel();

            viewModel.Profile = new Profile();
            viewModel.ProfileCategories = profileCategories.Collection();
            return View(viewModel);
        }

        public ActionResult Import(string Id)
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

                if (profile.ImportCsv == false)
                {
                        //return RedirectToAction("ImportDenied");
                        ViewBag.ImportCheck = "<h4 class=\"text-danger\">Automatyczny import jest wyłączony, czy mimo to chcesz kontynuować?</h4>";
                        return View(viewModel);
                }

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Import(Profile profile, string Id, Post post)
        {
            Profile profileToImport = context.Find(Id);

            if (profileToImport == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(profile);
                }

                profileToImport.ImportCsv = false;
                profileToImport.SocialpilotId = profileToImport.File;
                Console.WriteLine(profileToImport.File);
                
                IList<Post> posts;

                var streamreader = new StreamReader(Server.MapPath("//Content//ImportFiles//") + profileToImport.File);
                var reader = new CsvReader(streamreader, CultureInfo.InvariantCulture);

                reader.Configuration.HeaderValidated = null;
                posts = reader.GetRecords<Post>().ToList();

                var message = "Zaimportowaliśmy te posty: ";
                foreach (var p in posts)
                {
                    pcontext.Insert(p);
                }

                pcontext.Commit();
                context.Commit();
                return RedirectToAction("Index", new { message });
            }

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
                    file.SaveAs(Server.MapPath("//Content//ImportFiles//") + profile.File);
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