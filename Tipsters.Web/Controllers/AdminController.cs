using System.Web.Mvc;
using Tipsters.Data;
using Tipsters.Data.Interfaces;
using Tipsters.Models.ViewModels.AdminViewModels;
using Tipsters.Services;
using Tipsters.Services.AdminServices;

namespace Tipsters.Web.Controllers
{
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
            return View(this.adminUserService.GetAllUsers());
        }

        public ActionResult Tips()
        {
            return View(this.adminTipsService.GetAllTips());
        }

        public ActionResult Comments()
        {
            return View(this.adminCommentService.GetAllComments());
        }

        public ActionResult Others()
        {
            return View();
        }

        public ActionResult RemoveUser(string id)
        {
            this.adminUserService.RemoveUser(id);
            return RedirectToAction("Users", "Admin");
        }

    }
}