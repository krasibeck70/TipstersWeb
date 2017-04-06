using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Tipsters.Data;
using Tipsters.Data.Interfaces;
using Tipsters.Models.Models;
using Tipsters.Services;

namespace Tipsters.Web.Controllers
{
    [RoutePrefix("Users")]
    public class UsersController : Controller
    {
        private TipstersData data;
        private UserService service;
        public UsersController():this(new TipstersData())
        {
            this.data = new TipstersData();
        }

        public UsersController(ITipstersData data)
        {
            this.service = new UserService(data);
        }

        //[Route("Register")]
        //[HttpGet]
        //public ActionResult Register()
        //{
        //    if (Request.IsAuthenticated)
        //    {
        //        var user = this.data.Users.Find(x => x.Username == User.Identity.Name).First();
        //        return RedirectToAction("Profile", "Users", new {id = user.Id});
        //    }
        //    return View();
        //}
        //[Route("Register")]
        //[HttpPost]
        //public ActionResult Register(RegisterUserBindingModel user)
        //{
        //    User currentUser = new User();
        //    var errors = this.service.ValidationRegisteruser(user);
        //    if (!errors.Any())
        //    {
        //        currentUser = new User()
        //        {
        //            Email = user.Email,
        //            BirthDate = user.BirthDate,
        //            ConfirmPassword = user.ConfirmPassword,
        //            FullName = user.FullName,
        //            Password = user.Password,
        //            Username = user.Username
        //        };
        //        this.data.Users.InsertOrUpdate(currentUser);
        //        this.data.SaveChanges();
        //        LoginUserBindingModel loginUser = new LoginUserBindingModel()
        //        {
        //            Username = currentUser.Username,
        //            Password = currentUser.Password
        //        };
        //        return RedirectToAction("LoginAfterRegister", "Users", new {username = currentUser.Username});
        //    }
        //    foreach (var error in errors)
        //    {
        //        ModelState.AddModelError(error.Key,error.Value);
        //    }

        //    return View();
        //}
        //[Route("Login")]
        //[HttpGet]
        //public ActionResult Login()
        //{
        //    if (Request.IsAuthenticated)
        //    {
        //        var user = this.data.Users.Find(x => x.Username == User.Identity.Name).First();
        //        return RedirectToAction("Profile", "Users", new { id = user.Id });
        //    }
        //    return View();
        //}
        //[Route("Login")]
        //[HttpPost]
        //public ActionResult Login(LoginUserBindingModel lubm)
        //{
        //    if (this.service.IsregisteredUser(lubm))
        //    {
        //        var user = this.data.Users.Find(x => x.Username == lubm.Username).First();
        //        ViewBag.id = user.Id;
        //        var ident = new ClaimsIdentity(
        //            new[] { 
        //            // adding following 2 claim just for supporting default antiforgery provider
        //            new Claim(ClaimTypes.NameIdentifier, lubm.Username),
        //            //new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

        //            new Claim(ClaimTypes.Name,lubm.Username),

        //            // optionally you could add roles if any
        //            //new Claim(ClaimTypes.Role, "RoleName"),
        //            //new Claim(ClaimTypes.Role, "AnotherRole"),
        //         },
        //            DefaultAuthenticationTypes.ApplicationCookie);

        //        HttpContext.GetOwinContext().Authentication.SignIn(
        //           new AuthenticationProperties { IsPersistent = false }, ident);
        //        return RedirectToAction("Profile","Users", new {id=user.Id}); // auth succeed 
        //    }

        //    // invalid username or password
        //    ModelState.AddModelError("Error","Invalid Username or Password");
        //    return View();
        //}
        //[HttpGet]
        //[Route("LoginAfterRegitser/{username}")]
        //public ActionResult LoginAfterRegister(string username)
        //{
        //    if (Request.IsAuthenticated)
        //    {
        //        var userLogin = this.data.Users.Find(x => x.Username == User.Identity.Name).First();
        //        return RedirectToAction("Profile", "Users", new { id = userLogin.Id });
        //    }
        //    var user = this.data.Users.Find(x => x.Username == username).First();
        //    LoginUserBindingModel lubm = new LoginUserBindingModel()
        //    {
        //        Username = user.Username,
        //        Password = user.Password
        //    };
        //    if (this.service.IsregisteredUser(lubm))
        //    {
        //        ViewBag.id = user.Id;
        //        var ident = new ClaimsIdentity(
        //            new[] { 
        //            // adding following 2 claim just for supporting default antiforgery provider
        //            new Claim(ClaimTypes.NameIdentifier, lubm.Username),
        //            //new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

        //            new Claim(ClaimTypes.Name,lubm.Username),

        //            // optionally you could add roles if any
        //            //new Claim(ClaimTypes.Role, "RoleName"),
        //            //new Claim(ClaimTypes.Role, "AnotherRole"),
        //         },
        //            DefaultAuthenticationTypes.ApplicationCookie);

        //        HttpContext.GetOwinContext().Authentication.SignIn(
        //           new AuthenticationProperties { IsPersistent = false }, ident);
        //        return RedirectToAction("Profile", "Users", new{id = user.Id}); // auth succeed 
        //    }

        //    // invalid username or password
        //    ModelState.AddModelError("", "Invalid Username or Password");
        //    return View();
        //}
        //[Route("Logout")]
        //public ActionResult Logout()
        //{
        //    if (Request.IsAuthenticated)
        //    {
        //        HttpContext.GetOwinContext().Authentication.SignOut();
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return RedirectToAction("Login", "Users");

        //}
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
            var userId = User.Identity.GetUserId();
            if (Request.IsAuthenticated)
            {
                var user = this.data.Users.Find(x => x.Id == userId).First();
                string fileExt = System.IO.Path.GetExtension(file.FileName);
                var imageName = user.Email + ".png";
                if (fileExt.ToLower().EndsWith(".png") || fileExt.ToLower().EndsWith(".jpg"))
                {
                    var filePath = HostingEnvironment.MapPath("~/Content/Images/") + imageName;
                    var directory = new DirectoryInfo(HostingEnvironment.MapPath("~/Content/Images/"));
                    if (directory.Exists == false)
                    {
                        directory.Create();
                    }
                    user.Image = imageName;
                    file.SaveAs(filePath);
                    this.data.SaveChanges();
                }

                return RedirectToAction("Profile", "Users", new { id = user.Id });
            }
            return RedirectToAction("Login", "Account");

        }
        [HttpPost]
        [Route("FollowUser/{id}")]
        public ActionResult FollowUser(string id)
        {
            var userId = User.Identity.GetUserId();
            if (Request.IsAuthenticated)
            {
                var user = this.data.Users.Find(x => x.Id == userId).FirstOrDefault();
                var userSelected = this.data.Users.Find(x => x.Id == id).First();
                if (user.OwnerFollowers.Contains(userSelected))
                {
                    user.OwnerFollowers.Remove(userSelected);
                }
                else
                {
                    user.OwnerFollowers.Add(userSelected);
                }

                this.data.SaveChanges();
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
        //[Route("FindPronosticById/{id}")]
        //public JsonResult FindPronosticById(string id)
        //{
        //    var pronostic = this.data.Pronostics.Find(x => x.Id == id).First();
        //    var currentPronostic
        //}
    }
}