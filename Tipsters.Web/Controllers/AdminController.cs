using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tipsters.Data;
using Tipsters.Data.Interfaces;
using Tipsters.Models.BindingModels;
using Tipsters.Models.ViewModels.AdminViewModels;
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

        private void NavbarInfo()
        {
            var userId = User.Identity.GetUserId();
            var user = data.Users.Find(x => x.Id == userId).FirstOrDefault();
            ViewBag.Image = user != null ? user.Image : null;
            ViewBag.FullName = user != null ? user.FullName : null;
        }

    }
}