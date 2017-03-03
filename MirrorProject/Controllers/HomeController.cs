using System.Web.Mvc;
using MirrorProject.Models;
using Newtonsoft.Json;

namespace MirrorProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WeatherUpdate()
        {
            return Json(JsonConvert.SerializeObject(DataService.getWunderground()),JsonRequestBehavior.AllowGet);
        }

        public ActionResult RssUpdate()
        {
            return Json(JsonConvert.SerializeObject(DataService.getRss().getRssItems()), JsonRequestBehavior.AllowGet);
        }
    }
}
