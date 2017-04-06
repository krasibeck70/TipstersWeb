using System;
using System.Collections.Generic;
using Tipsters.Data.Interfaces;
using Tipsters.Models.ViewModels.AccountViewModel;

namespace Tipsters.Services
{
    public class UserService : Service
    {
        public UserService(ITipstersData data) : base(data)
        {
        }

        //public bool IsregisteredUser(LoginViewModel lumb)
        //{
        //    var user = this.data.Users.FindByPredicate(x => x.Username == lumb.Username && x.Password == lumb.Password);
        //    if (user != null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public void Logout(string sessionId)
        //{
        //    Login login = this.data.Logins.Find(x => x.SessionId == sessionId && x.IsActive == true).First();
        //    login.IsActive = false;
        //    this.data.SaveChanges();
        //}

        //public bool IsExistUsername(string username)
        //{
        //    var user = this.data.Users.Find(x => x.Username == username).FirstOrDefault();
        //    if (user != null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //public bool IsExistEmail(string email)
        //{
        //    var user = this.data.Users.Find(x => x.Email == email).FirstOrDefault();
        //    if (user != null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public Dictionary<string, string> ValidationRegisteruser(RegisterUserBindingModel user)
        //{
        //    Dictionary<string, string> errors = new Dictionary<string, string>();
        //    if (!string.IsNullOrEmpty(user.Username))
        //    {
        //        if (IsExistUsername(user.Username))
        //        {
        //            errors.Add("CustomUsernameExist", "The Username has been alredy exist");
        //        }
        //    }
        //    else
        //    {
        //        errors.Add("CustomUsername", "Username Required");
        //    }

        //    if (!string.IsNullOrEmpty(user.Email))
        //    {
        //        if (IsExistUsername(user.Email))
        //        {
        //            errors.Add("CustomEmailExist", "The Email has been alredy exist");
        //        }
        //    }
        //    else
        //    {
        //        errors.Add("CustomEmail", "Email Required");
        //    }
        //    if (!string.IsNullOrEmpty(user.FullName))
        //    {
        //        if (!user.FullName.Contains(" "))
        //        {
        //            errors.Add("CustomFullName", "Full name required one white space");
        //        }
        //    }
        //    else
        //    {
        //        errors.Add("CustomFullName", "Full Name Required");
        //    }
        //    if (user.BirthDate != null)
        //    {
        //        var years = DateTime.Now.AddTicks(user.BirthDate.Ticks).Year - 1;
        //        if (years < 13)
        //        {
        //            errors.Add("CustomBirthdate", "You don't have 13 years");
        //        }
        //    }
        //    else
        //    {
        //        errors.Add("CustomBirthdate", "Birthdate Required");
        //    }
        //    if (!string.IsNullOrEmpty(user.Password))
        //    {
        //        if (user.Password != user.ConfirmPassword)
        //        {
        //            errors.Add("CustomPassword", "Passwords do not match");
        //        }
        //    }
        //    else
        //    {
        //        errors.Add("CustomPassword", "Password Required");
        //    }
        //    return errors;
        //}
    }
}
