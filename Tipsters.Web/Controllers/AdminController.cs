using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Tipsters.Data;
using Tipsters.Data.Interfaces;
using Tipsters.Models.BindingModels;
using Tipsters.Models.Models;
using Tipsters.Models.ViewModels.AdminViewModels;
using Tipsters.Models.ViewModels.PronosticsViewModel;
using Tipsters.Services;
using Tipsters.Services.AdminServices;

namespace Tipsters.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {
        private TipstersData data;
        private Services.AdminServices.AdminHomeService adminHomeService;
        private Services.AdminServices.AdminUserServices adminUserService;
        private Services.AdminServices.AdminTipsService adminTipsService;
        private Services.AdminServices.AdminCommentService adminCommentService;
        public AdminController():this(new TipstersData())
        {
            this.data = new TipstersData();
        }

        public AdminController(ITipstersData data)
        {
            this.adminHomeService = new Services.AdminServices.AdminHomeService(data);
            this.adminUserService = new Services.AdminServices.AdminUserServices(data);
            this.adminTipsService = new Services.AdminServices.AdminTipsService(data);
            this.adminCommentService = new Services.AdminServices.AdminCommentService(data);
        }
        
        public ActionResult Home()
        {
            NavbarInfo();
            HomeViewModel model = new HomeViewModel()
            {
                NumbersOfUsers = this.adminHomeService.GetNumbersOfUsers(),
                NumbersOfTips = this.adminHomeService.GetNumbersOfTips(),
                NumbersOfComments = this.adminHomeService.GetNumbersOfComments()
            };
            return View(model);
        }

        public ActionResult Users()
        {
            NavbarInfo();
            return View(this.adminUserService.GetAllUsers());
        }

        public ActionResult Tips()
        {
            NavbarInfo();
            return View(this.adminTipsService.GetAllTips());
        }

        public ActionResult Comments()
        {
            NavbarInfo();
            return View(this.adminCommentService.GetAllComments());
        }

        public ActionResult Others()
        {
            NavbarInfo();
            return View();
        }

        public ActionResult RemoveUser(string id)
        {
            NavbarInfo();
            this.adminUserService.RemoveUser(id);
            return RedirectToAction("Users", "Admin");
        }
        [HttpGet]
        [Route("EditUser/{id}")]
        public ActionResult EditUser(string id)
        {
            NavbarInfo();
            return View(this.adminUserService.UserById(id));
        }
        [HttpPost]
        [Route("EditUser/{id}")]
        public ActionResult EditUserSave(string id, EditUserBindingModel eubm)
        {
            NavbarInfo();
            this.adminUserService.EditUserAndSave(id,eubm);
            return RedirectToAction("Users", "Admin");
        }

        [HttpGet]
        [Route("ViewUser/{id}")]
        public ActionResult ViewUser(string id)
        {
            NavbarInfo();
            return View(this.adminUserService.UserById(id));
        }
        [HttpGet]
        [Route("AddAdminUser")]
        public ActionResult SelectAdminUser()
        {
            NavbarInfo();
            var users = this.adminUserService.GetAllUsers();
            return View(users);
        }
        [HttpPost]
        [Route("AddAdminUser")]
        public ActionResult AddAdminUser([Bind(Include = "Email")] string email)
        {
            NavbarInfo();
            var context = new TipstersContext();
            var user = data.Users.Find(x => x.Email == email).First();
            UserManagerExtensions.AddToRole(
                new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)), user.Id,
                "Admin");
            user.IsAdmin = true;
            data.SaveChanges();
            return RedirectToAction("SelectAdminUser", "Admin");
        }

        [HttpPost]
        public ActionResult RemoveAdmin([Bind(Include = "Email")] string email)
        {
            NavbarInfo(); 
            var context = new TipstersContext();
            var user = data.Users.Find(x => x.Email == email).First();
            UserManagerExtensions.RemoveFromRole(
                new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)), user.Id,
                "Admin");
            user.IsAdmin = false;
            data.SaveChanges();

            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            FormsAuthentication.RedirectToLoginPage();

            return RedirectToAction("SelectAdminUser", "Admin");
        }
        [HttpGet]
        [Route("RemoveTip/{id}")]
        public ActionResult RemoveTip(string id)
        {
            NavbarInfo();
            this.adminTipsService.RemoveTips(id);
            return RedirectToAction("Tips", "Admin");
        }
        [HttpGet]
        [Route("ViewTip/{id}")]
        public ActionResult ViewTip(string id)
        {
            NavbarInfo();
            return View(this.adminTipsService.TipByIdViewModel(id));
        }
        [HttpGet]
        [Route("EditTip/{id}")]
        public ActionResult EditTip(string id)
        {
            NavbarInfo();
            return View(this.adminTipsService.TipByIdViewModel(id));
        }
        [HttpPost]
        [Route("EditTipSave/{id}")]
        public ActionResult EditTipSave(string id,EditTipBindingModel etbm)
        {
            NavbarInfo();
            this.adminTipsService.EditTip(id,etbm);
            return RedirectToAction("Tips", "Admin");
        }
        [HttpGet]
        [Route("RemoveComment/{id}")]
        public ActionResult RemoveComment(string id)
        {
            NavbarInfo();
            this.adminCommentService.RemoveComment(id);
            return RedirectToAction("Comments", "Admin");
        }
        [HttpGet]
        [Route("ViewComment/{id}")]
        public ActionResult ViewComment(string id)
        {
            NavbarInfo();
            return View(this.adminCommentService.CommentsById(id));
        }
        [HttpGet]
        [Route("EditComment/{id}")]
        public ActionResult EditComment(string id)
        {
            NavbarInfo();
            return View(this.adminCommentService.CommentsById(id));
        }
        [HttpPost]
        [Route("EditComment/{id}")]
        public ActionResult EditComment(string id, PostCommentBindingModel pcbm)
        {
            this.adminCommentService.EditComment(id,pcbm);
            return RedirectToAction("Comments", "Admin");
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