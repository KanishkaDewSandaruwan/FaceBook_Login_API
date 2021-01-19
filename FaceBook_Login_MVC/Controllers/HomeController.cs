using ASPSnippets.FaceBookAPI;
using FaceBook_Login_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FaceBook_Login_MVC.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            FaceBookConnect.API_Key = "3107132136000264";
            FaceBookConnect.API_Secret = "cd391ae91510b36515491937e79c27a5";

            FaceBookUser faceBookUser = new FaceBookUser();
            if (Request.QueryString["error"] == "access_denied")
            {
                ViewBag.Message = "User has denied access.";
            }
            else
            {
                string code = Request.QueryString["code"];
                if (!string.IsNullOrEmpty(code))
                {
                    string data = FaceBookConnect.Fetch(code, "me?fields=id,name,email");
                    faceBookUser = new JavaScriptSerializer().Deserialize<FaceBookUser>(data);
                }
            }

            return View(faceBookUser);
        }

        [HttpPost]
        public EmptyResult Login()
        {
            FaceBookConnect.Authorize("email", string.Format("{0}://{1}/{2}", Request.Url.Scheme, Request.Url.Authority, ""));
            return new EmptyResult();
        }
    }
}