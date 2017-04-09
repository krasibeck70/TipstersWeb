using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Tipsters.Data;

namespace Tipsters.Web.Controllers
{
    //[RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private TipstersData data;
        public HomeController()
        {
            this.data = new TipstersData();
        }
        [HttpGet]
        //[Route("Index")]
        public ActionResult Index()
        {
            var tips = data.Pronostics.GetAll().Include("User").Include("OwnerComments").ToList();
            return View(tips);
        }

        public ActionResult Demo()
        {
            return View();
        }
    }
}