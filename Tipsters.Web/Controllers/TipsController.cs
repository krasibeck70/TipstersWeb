using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Tipsters.Data;
using Tipsters.Data.Interfaces;
using Tipsters.Models.BindingModels;
using Tipsters.Models.Models;
using Tipsters.Services;


namespace Tipsters.Web.Controllers
{
    [RoutePrefix("Tips")]
    public class TipsController : Controller
    {
        private TipstersData data;
        private UserService userService;
        private TipsService tipsService;
        public TipsController():this(new TipstersData())
        {
            this.data = new TipstersData();
        }
        public TipsController(ITipstersData data)
        {
            this.userService = new UserService(data);
            this.tipsService = new TipsService(data);
        }
        [HttpGet]
        [Route("PostTips/{message?}")]
        public ActionResult PostTips(string message)
        {
            NavbarInfo();
            if (Request.IsAuthenticated)
            {
                var tip = this.tipsService.GetAllTypeOfTips();
                var countries = this.tipsService.GetAllCountries();
                if (message != null)
                {
                    var errors = message.Split(',').ToList();
                    Tuple<IEnumerable<County>, IEnumerable<Tip>, IEnumerable<string>> items = new Tuple<IEnumerable<County>, IEnumerable<Tip>, IEnumerable<string>>(countries, tip, errors);
                    return View(items);
                }
                else
                {
                    Tuple<IEnumerable<County>, IEnumerable<Tip>,IEnumerable<string>> items = new Tuple<IEnumerable<County>, IEnumerable<Tip>,IEnumerable<string>>(countries, tip,new List<string>());
                    return View(items);
                }
            }
            return RedirectToAction("Login", "Account");

        }
        [HttpPost]
        [Route("PostTips")]
        public ActionResult PostTips(PostTipsBindingModel ptbm)
        {
            NavbarInfo();
            if (Request.IsAuthenticated)
            {
                var errors = this.tipsService.Errors(ptbm);
                if (!errors.Any())
                {
                    var userId = User.Identity.GetUserId();
                    this.tipsService.PostTips(ptbm, userId);
                    return RedirectToAction("Profile", "Users", new { id = userId });
                }
                StringBuilder errorsBuilder = new StringBuilder();
                foreach (string error in errors)
                {
                    errorsBuilder.Append(error);
                    errorsBuilder.Append(",");
                }
                return RedirectToAction("PostTips", "Tips", new {message = errorsBuilder.ToString() });
            }
            return RedirectToAction("Login", "Account");

        }
       
        [HttpPost]
        [Route("VotesTips/{id}")]
        public ActionResult VotesTips(string id)
        {
            NavbarInfo();
            var newId = id.Split('¿')[0];
            var parameter = id.Split('¿')[1];

            if (Request.IsAuthenticated)
            {
                if (id == "undefined¿Down" || id == "undefined¿Up")
                {
                    return null;
                }
                var userId = User.Identity.GetUserId();
                var user = this.userService.FindUser(userId);
                var pronostic = this.tipsService.FindPronosticById(newId);
                if (user.LikesPronostics.Contains(pronostic))
                {
                    return null;
                }
                user.LikesPronostics.Add(pronostic);
                if (parameter =="Up")
                {
                    pronostic.VotesUp++;
                    this.tipsService.SaveChanges();
                    var votes = new
                    {
                        VotesUp_json = pronostic.VotesUp,
                        Percentage = pronostic.PercentageWin
                    };
                    var json = JsonConvert.SerializeObject(votes);
                    return Json(json);
                }
                else
                {
                    pronostic.VotesDown++;
                    this.tipsService.SaveChanges();
                    var votes = new
                    {
                        VotesDown_json = pronostic.VotesDown,
                        Percentage = pronostic.PercentageWin
                    };
                    var json = JsonConvert.SerializeObject(votes);
                    return Json(json);
                }
            }
            return null;
        }
        [HttpGet]
        [Route("Top10Tips")]
        public ActionResult Top10Tips()
        {
            NavbarInfo();
            return View(this.tipsService.GetAllPronosticViewModels());
        }
        [HttpPost]
        [Route("PostComment/{id}")]
        public ActionResult PostComment(string id,PostCommentBindingModel model)
        {
            NavbarInfo();
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = this.userService.FindUser(userId);
                Guid g = Guid.NewGuid();
                string GuidString = Convert.ToBase64String(g.ToByteArray());
                GuidString = GuidString.Replace("=", "");
                var pronostic = this.tipsService.FindPronosticById(id);
                Comment comment = new Comment
                {
                    Id = g.ToString(),
                    Message = model.Comment,
                    UserId = user.Id,
                    PronosticId = pronostic.Id,
                    Pronostic = pronostic,
                    User = user,
                    TimePosted = DateTime.Now
                };
                this.tipsService.InsertOrUpdate(comment);
                this.tipsService.SaveChanges();
                var resultPronostic = this.tipsService.FindPronosticById(pronostic.Id);
                var coment = new
                {
                    Id = comment.Id,
                    Message = comment.Message,
                    UserId = comment.UserId,
                    PronosticId = comment.PronosticId,
                    TimePosted = comment.TimePostedComment,
                    TotalComments = pronostic.OwnerComments.Count
                };
                var json = JsonConvert.SerializeObject(coment);
                return Json(json);

            }
            return RedirectToAction("Login", "Account");
        }
        [Route("GetAllCommentsForThisTip/{id}")]
        public ActionResult GetAllCommentsForThisTip(string id)
        {
            NavbarInfo();
            var pronostic = this.tipsService.FindPronosticById(id);
            var comments = pronostic.OwnerComments.Select(x => new
            {
                comment = x.Message,
                timePosted = x.TimePostedComment,
                pronosticId = x.PronosticId,
                userId = x.UserId
             });
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Route("GetOrderBy/{parameter}")]
        public ActionResult GetOrderBy(string parameter)
        {
            return this.PartialView("Top10Tips_Partial", this.tipsService.Get10PronosticsSorted(parameter));
        }
        private void NavbarInfo()
        {
            var userId = User.Identity.GetUserId();
            var user = this.userService.FindUser(userId);
            ViewBag.Image = user != null ? user.Image : null;
            ViewBag.FullName = user != null ? user.FullName : null;
        }
    }
}