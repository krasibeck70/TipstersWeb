using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Tipsters.Models.Models;
using Tipsters.Models.ViewModels.CommentsViewModel;
using Tipsters.Models.ViewModels.PronosticsViewModel;
using Tipsters.Models.ViewModels.UsersViewMode;

namespace Tipsters.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.RegisterMaps();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterMaps()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<ApplicationUser, UserViewModel>();
                x.CreateMap<UserViewModel, ApplicationUser>();
                /*.ForMember(action => action.OwnerPronostics, config => config.MapFrom(user => user.OwnerPronostics))*/;
                x.CreateMap<Pronostic, PronosticViewModel>();
                x.CreateMap<PronosticViewModel, Pronostic>();
                x.CreateMap<Comment, CommentViewModel>();
                x.CreateMap<CommentViewModel,Comment>();
            });
        } 
    }
}
