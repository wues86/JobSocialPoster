using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CsvHelper;
using JobSocialPoster.Core.Contracts;
using JobSocialPoster.Core.Models;
using JobSocialPoster.Core.ViewModels;
using JobSocialPoster.DataAccess.InMemory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JobSocialPoster.WebUI.Controllers
{
    [Authorize(Roles="Admin")]
    public class PostManagerController : Controller
    {
        IRepository<Post> context;
        IRepository<PostCategory> postCategories;
        IRepository<Profile> pcontext;

        public PostManagerController(IRepository<Post> postContext, IRepository<PostCategory> postCategoryContext, IRepository<Profile> profileContext)
        {
            context = postContext;
            postCategories = postCategoryContext;
            pcontext = profileContext;
        }

        // GET: PostManager

        public ActionResult Index(string message)
        {
            List<Post> posts = context.Collection().ToList();

            ViewBag.message = Request.QueryString["message"];

            return View(posts);

        }

        //public ActionResult DeleteInactivePosts(string message)
        public ActionResult DeleteInactivePosts()
        {
            List<Post> postsToDelete = context.Collection().Where(p => p.Status == true).ToList();

            //ViewBag.message = Request.QueryString["message"];

            if (postsToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(postsToDelete);
            }

        }

        [HttpPost]
        public ActionResult DeleteInactivePosts(Post post)
        {
            List<Post> postsToDelete = context.Collection().Where(p => p.Status == true).ToList();


            if (postsToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(postsToDelete);
                }

                foreach (var p in postsToDelete)
                {
                    context.Delete(p.Id);
                }

                context.Commit();

                var message = "Niekatywne posty zostały usunięte";

                return RedirectToAction("Index", new { message });
            }

        }


        public ActionResult ExportAll(string message)
        {
            List<Post> posts = context.Collection().Where(p=>p.Status == false).ToList();

            ViewBag.message = Request.QueryString["message"];
            if (posts == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(posts);
            }

        }


        [HttpPost]
        public ActionResult ExportAll(Post post)
        {
            List<Post> postsToExport = context.Collection().Where(p => p.Status == false).ToList();


            if (postsToExport == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(postsToExport);
                }

                var message = "";

                WebClient client = new WebClient();

                client.Encoding = Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                client.Headers.Add("x-api-key", "123");
                client.Headers.Add("xclientid", "123");
                client.Headers.Add("xclientsecret", "123");
                var email = "123";
                var password = "123";

                string response = client.UploadString("https://rest.socialpilot.co/v2/auth/login", "{\"username\":\"" + email + "\",\"password\":\"" + password + "\"}");
                var details = JObject.Parse(response);
                var id1 = details["message"].ToString();
                var id2 = details["response"]["apiToken"].ToString();

                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + id2;

                message = "Step 1: Token: " + id1 + " || ";

                var i = 1;
                foreach (var p in postsToExport)
                {
                    p.Status = true;

                    
                    response = client.UploadString("https://rest.socialpilot.co/v2/uploadmedia", "{\"mediaType\": \"IMAGE\",\"media\": [{\"url\": \"" + p.PostImg + "\",\"thumbnail\": \"" + p.PostImg + "\"}]}");
                    details = JObject.Parse(response);
                    var id3 = details["message"].ToString();
                    var id4 = details["response"]["mediaIds"][0]["mediaId"].ToString();

                    response = client.UploadString("https://rest.socialpilot.co/v2/posts/create", "{\"loginIds\":[634552],\"shareType\":0,\"posts\":[{\"postDesc\":\"" + p.PostContent + "\",\"postTitle\":\"" + p.Id + "\",\"mediaId\":[\"" + id4 + "\"],\"type\":\"facebook\"}]}");
                    details = JObject.Parse(response);

                    var id5 = details["message"].ToString();

                    message = message + "Post " + i + " | Image step: " + id3 + " | Post submit step: " + id5 + " ";
                    
                    i++;



                }

                context.Commit();

                //var message = "Posty zostały wyeksportowane";

                return RedirectToAction("Index", new { message });
            }

        }

        public ActionResult Export(string Id)
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
                viewModel.Profile = pcontext.Collection();


                if (post.Status == true)
                {
                    //return RedirectToAction("ImportDenied");
                    ViewBag.ImportCheck = "<h4 class=\"text-danger\">Post został już wyeksportowany, czy mimo to chcesz kontynuować?</h4>";
                    return View(viewModel);
                }
                else { 
                    return View(viewModel);
                }

            }
        }

        [HttpPost]
        public ActionResult Export(Post post, string Id)
        {
            Post postToExport = context.Find(Id);

            if (postToExport == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(post);
                }

                postToExport.Status = true;


                WebClient client = new WebClient();

                client.Encoding = Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                client.Headers.Add("x-api-key", "123");
                client.Headers.Add("xclientid", "123");
                client.Headers.Add("xclientsecret", "123");
                var email = "123";
                var password = "123";

                string response = client.UploadString("https://rest.socialpilot.co/v2/auth/login", "{\"username\":\"" + email + "\",\"password\":\"" + password + "\"}");
                var details = JObject.Parse(response);
                var id1 = details["message"].ToString();
                var id2 = details["response"]["apiToken"].ToString();

                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + id2;
                response = client.UploadString("https://rest.socialpilot.co/v2/uploadmedia", "{\"mediaType\": \"IMAGE\",\"media\": [{\"url\": \"" + postToExport.PostImg + "\",\"thumbnail\": \"" + postToExport.PostImg + "\"}]}");
                details = JObject.Parse(response);
                var id3 = details["message"].ToString();
                var id4 = details["response"]["mediaIds"][0]["mediaId"].ToString();


                response = client.UploadString("https://rest.socialpilot.co/v2/posts/create", "{\"loginIds\":[634552],\"shareType\":0,\"posts\":[{\"postDesc\":\"" + postToExport.PostContent + "\",\"postTitle\":\"" + postToExport.Id + "\",\"mediaId\":[\"" + id4 + "\"],\"type\":\"facebook\"}]}");
                details = JObject.Parse(response);

                var id5 = details["message"].ToString();

                var message = "Token step: " + id1 + " | ";
                message = message + " Image step: " + id3 + " | Post submit step: " + id5 + " ";
                

                context.Commit();

                return RedirectToAction("Index", new { message });
            }

        }


        public ActionResult Create()
        {
            PostManagerViewModel viewModel = new PostManagerViewModel();

            viewModel.Post = new Post();
            viewModel.PostCategories = postCategories.Collection();
            viewModel.Profile = pcontext.Collection();
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
                viewModel.Profile = pcontext.Collection();
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
                postToEdit.Profile = post.Profile;

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