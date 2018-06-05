using System.Web.Mvc;

namespace LaRutaDelSoftware.RestAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new RedirectResult("~/swagger/ui/index");
        }
    }
}
