using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Tipsters.Data;
using Tipsters.Data.Interfaces;
using Tipsters.Models.Models;
using Tipsters.Services;
using FacebookEasyAccess.Interfaces;
using Tipsters.Models.ViewModels.UsersViewMode;

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
            NavbarInfo();
            return View(this.userService.UserViewModels());
        }

        [HttpGet]
        [Route("Profile/{id?}")]
        public new ActionResult Profile(string id)
        {
            NavbarInfo();
            var user = new ApplicationUser();
            if (id != null)
            {
                user = this.data.Users.Find(x => x.Id == id).First();
                UserViewModel userModel = Mapper.Map<UserViewModel>(user);
                return View(userModel);
            }
            else
            {
                var userId = User.Identity.GetUserId();
                user = this.data.Users.Find(x => x.Id == userId).FirstOrDefault();
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                UserViewModel userModel = Mapper.Map<UserViewModel>(user);
                return View(userModel);
            }
        }

        [Route("Profile")]
        [HttpPost]
        public new ActionResult Profile(HttpPostedFileBase file)
        {
            NavbarInfo();
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
            NavbarInfo();
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
            NavbarInfo();
            var users = this.data.Users.GetAll();
            var emails = users.Select(x => x.Email);
            return Json(emails, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult FindUserById(string id)
        {
            NavbarInfo();
            var user = this.data.Users.Find(x => x.Id == id).First();
            var currentUser = new
            {
                Email = user.Email,
                Image = user.Image
            };
            var json = JsonConvert.SerializeObject(currentUser);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        private void NavbarInfo()
        {
            var userId = User.Identity.GetUserId();
            var user = data.Users.Find(x => x.Id == userId).FirstOrDefault();
            ViewBag.Image = user != null ? user.Image : null;
            ViewBag.FullName = user != null ? user.FullName : null;
        }
    }
}