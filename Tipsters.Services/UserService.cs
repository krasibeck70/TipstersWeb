﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using AutoMapper;
using Tipsters.Data.Interfaces;
using Tipsters.Models.Models;
using Tipsters.Models.ViewModels.UsersViewMode;

namespace Tipsters.Services
{
    public class UserService : Service
    {
        public UserService(ITipstersData data) : base(data)
        {

        }

        public void FollowUser(string userLoginId, string userFollowedId)
        {
            var userLogin = data.Users.Find(x => x.Id == userLoginId).First();
            var userFollowed = data.Users.Find(x => x.Id == userFollowedId).First();
            if (userLogin.OwnerFollowers.Contains(userFollowed))
            {
                userLogin.OwnerFollowers.Remove(userFollowed);
            }
            else
            {
                userLogin.OwnerFollowers.Add(userFollowed);
            }
            data.SaveChanges();
        }

        public void ChangeProfilePicture(string userId, HttpPostedFileBase file)
        {
            var user = data.Users.Find(x => x.Id == userId).First();
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
                data.SaveChanges();
            }
        }

        public List<UserViewModel> UserViewModels()
        {
            var users = data.Users.GetAll().ToList();
            var userViewModel = new List<UserViewModel>();
            foreach (ApplicationUser applicationUser in users)
            {
                userViewModel.Add(Mapper.Map<UserViewModel>(applicationUser));
            }
            return userViewModel;
        }

        public ApplicationUser FindUser(string id)
        {
            return this.data.Users.Find(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<ApplicationUser> AllUsers()
        {
            return this.data.Users.GetAll().ToList();
        }
    }
}
