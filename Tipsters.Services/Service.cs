using Tipsters.Data.Interfaces;

namespace Tipsters.Services
{
    public class Service
    {
        protected ITipstersData data;

        public Service(ITipstersData data)
        {
            this.data = data;
        }

        //public LoggedInUserViewModel CheckedForLoggedInUser(HttpSession session)
        //{
        //    var login = this.data.Logins.FindByPredicate(l => l.SessionId == session.Id && l.IsActive);
        //    if (login != null)
        //    {
        //        LoggedInUserViewModel liuvm = new LoggedInUserViewModel()
        //        {
        //            Username = login.User.UserName
        //        };
        //        return liuvm;
        //    }
        //    else
        //    {
        //        return new LoggedInUserViewModel();
        //    }
        //}
    }
}
