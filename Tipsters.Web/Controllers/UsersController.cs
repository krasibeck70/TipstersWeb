using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Tipsters.Data;
using Tipsters.Data.Interfaces;
using Tipsters.Models.Models;
using Tipsters.Services;
using FacebookEasyAccess.Interfaces;

namespace Tipsters.Web.Controllers
{
    [RoutePrefix("Users")]
    public class UsersController : Controller
    {
        private TipstersData data;
        private UserService userService;
        public UsersController():this(new TipstersData())
        {
            this.data = new TipstersData();
        }

        public UsersController(ITipstersData data)
        {
            this.userService = new UserService(data);
        }

        [Route("Tipsters")]
        public ActionResult Tipsters()
        {
            var users = data.Users.GetAll().ToList();
            return View(users);
        }

        [HttpGet]
        [Route("Profile/{id?}")]
        public new ActionResult Profile(string id)
        {
            var user = new ApplicationUser();
            if (id != null)
            {
                user = this.data.Users.Find(x => x.Id == id).First();
                return View(user);
            }
            else
            {
                var userId = User.Identity.GetUserId();
                user = this.data.Users.Find(x => x.Id == userId).FirstOrDefault();
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(user);
            }
        }

        [Route("Profile")]
        [HttpPost]
        public new ActionResult Profile(HttpPostedFileBase file)
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                this.userService.ChangeProfilePicture(userId,file);

                return RedirectToAction("Profile", "Users", new { id = userId });
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        [Route("FollowUser/{id}")]
        public ActionResult FollowUser(string id)
        {
            if (Request.IsAuthenticated)
            {
                var userLoginId = User.Identity.GetUserId();
                var userfollowId = id;
                this.userService.FollowUser(userLoginId,userfollowId);
                return null;
            }
            return RedirectToAction("Login", "Account");
        }
        [Route("AllEmails")]
        public JsonResult AllEmails()
        {
            var users = this.data.Users.GetAll();
            var emails = users.Select(x => x.Email);
            return Json(emails, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult FindUserById(string id)
        {
            var user = this.data.Users.Find(x => x.Id == id).First();
            var currentUser = new
            {
                Email = user.Email,
                Image = user.Image
            };
            var json = JsonConvert.SerializeObject(currentUser);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}