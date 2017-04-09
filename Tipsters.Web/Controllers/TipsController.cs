using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.ApplicationInsights.Web;
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
        [Route("PostTips")]
        public ActionResult PostTips()
        {
            if (Request.IsAuthenticated)
            {
                var tip = this.data.Tips.GetAll().ToList();
                var countries = this.data.Countries.GetAll().ToList();
                Tuple<IEnumerable<County>, IEnumerable<Tip>> items = new Tuple<IEnumerable<County>, IEnumerable<Tip>>(countries, tip);
                return View(items);
            }
            return RedirectToAction("Login", "Account");

        }
        [HttpPost]
        [Route("PostTips")]
        public ActionResult PostTips(PostTipsBindingModel ptbm)
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                this.tipsService.PostTips(ptbm,userId);
                return RedirectToAction("Profile", "Users", new { id = userId });
            }
            return RedirectToAction("Login", "Account");

        }
       
        [HttpPost]
        [Route("VotesTips/{id}")]
        public ActionResult VotesTips(string id)
        {
            var newId = id.Split('¿')[0];
            var parameter = id.Split('¿')[1];

            if (Request.IsAuthenticated)
            {
                if (id == "undefined¿Down" || id == "undefined¿Up")
                {
                    return null;
                }
                var userId = User.Identity.GetUserId();
                var user = this.data.Users.Find(x => x.Id == userId).First();
                var pronostic = this.data.Pronostics.Find(x => x.Id == newId).First();
                if (user.LikesPronostics.Contains(pronostic))
                {
                    return null;
                }
                user.LikesPronostics.Add(pronostic);
                if (parameter =="Up")
                {
                    pronostic.VotesUp++;
                    this.data.SaveChanges();
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
                    this.data.SaveChanges();
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Top10Tips")]
        public ActionResult Top10Tips()
        {
            var tips = this.data.Pronostics.GetAll().ToList();
            return View(tips);
        }
        [HttpPost]
        [Route("PostComment/{id}")]
        public ActionResult PostComment(string id,PostCommentBindingModel model)
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = this.data.Users.Find(x => x.Id == userId).First();
                Guid g = Guid.NewGuid();
                string GuidString = Convert.ToBase64String(g.ToByteArray());
                GuidString = GuidString.Replace("=", "");
                var pronostic = this.data.Pronostics.Find(x => x.Id == id).First();
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
                this.data.Comments.InsertOrUpdate(comment);
                this.data.SaveChanges();
                var resultPronostic = this.data.Pronostics.Find(x => x.Id == pronostic.Id).First();
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
            var pronostic = this.data.Pronostics.Find(x => x.Id == id).First();
            var comments = pronostic.OwnerComments.Select(x => new
            {
                comment = x.Message,
                timePosted = x.TimePostedComment,
                pronosticId = x.PronosticId,
                userId = x.UserId
             });
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
    }
}