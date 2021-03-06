﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Tipsters.Data;
using Tipsters.Data.Interfaces;
using Tipsters.Models.BindingModels;
using Tipsters.Models.Models;
using Tipsters.Models.ViewModels.UsersViewMode;

namespace Tipsters.Services.AdminServices
{
    public class AdminUserServices : Service
    {
        public AdminUserServices(ITipstersData data) : base(data)
        {
        }

        public List<ApplicationUser> GetAllUsers()
        {
            var users = data.Users.GetAll().ToList();
            return users;
        }

        public bool RemoveUser(string id)
        {
            if (id != null)
            {
                var user = data.Users.Find(x => x.Id == id).First();
                var userTips = user.OwnerPronostics.ToList();
                user.OwnerFollowers.Clear();
                user.OwnerFollowing.Clear();
                user.LikesPronostics.Clear();
                for (var i = 0; i < userTips.Count(); i++)
                {
                    userTips[i].UsersLikes.Clear();
                    var pronosticId = userTips[i].Id;
                    var comments = data.Comments.Find(x => x.Pronostic.Id == pronosticId);
                    foreach (Comment comment in comments)
                    {
                        data.Comments.Delete(comment);
                    }
                    data.Pronostics.Delete(userTips[i]);
                }
                var userComments = user.CommentsPronostics.ToList();
                foreach (var comment in userComments)
                {
                    data.Comments.Delete(comment);
                }
                data.Users.Delete(user);
                data.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveTips(string id)
        {
            if (id != null)
            {
                var tip = data.Pronostics.Find(x => x.Id == id).First();
                data.Pronostics.Delete(tip);
                data.SaveChanges();
                return true;
            }
            return false;
        }

        public UserViewModel UserById(string id)
        {
            var user = data.Users.Find(x => x.Id == id).First();
            UserViewModel userModel = Mapper.Map<UserViewModel>(user);
            return userModel;
        }
        public ApplicationUser UserByIdEdit(string id)
        {
            var user = data.Users.Find(x => x.Id == id).First();
            return user;
        }

        public void EditUserAndSave(string id, EditUserBindingModel eubm)
        {
            var user = UserByIdEdit(id);
            user.FullName = eubm.FullName;
            user.Image = eubm.Image;
            user.Email = eubm.Email;
            data.Users.InsertOrUpdate(user);
            data.SaveChanges();
        }
    }
}
