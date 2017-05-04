﻿using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Tipsters.Data;
using Tipsters.Data.Interfaces;
using Tipsters.Models.ViewModels.UsersViewMode;
using Tipsters.Services;

namespace Tipsters.Web.Controllers
{
    //[RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private TipstersData data;
        private TipsService tipsService;
        public HomeController() : this(new TipstersData())
        {
            this.data = new TipstersData();
        }
        public HomeController(ITipstersData data)
        {
            this.tipsService = new TipsService(data);
        }
        [HttpGet]
        //[Route("Index")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = data.Users.Find(x => x.Id == userId).FirstOrDefault();
            ViewBag.Image = user != null ? user.Image : null;
            ViewBag.FullName = user != null ? user.FullName : null;
            return View(this.tipsService.GetAllPronosticViewModels());
        }

        public ActionResult Demo()
        {


            var user = this.data.Users.GetAll().First(x=>x.Id == "a257e9d0-f589-4db5-945b-d1659b88d979");
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            UserViewModel userModel = Mapper.Map<UserViewModel>(user);
            return View(userModel);
        }
    }
}