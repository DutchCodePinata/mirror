using System;
using System.Web;
using System.Web.Mvc;
using MirrorProject.Models;
using Newtonsoft.Json;
using WebGrease.Css.ImageAssemblyAnalysis.LogModel;

namespace MirrorProject.Controllers
{
    public class HomeController : Controller
    {

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WeatherUpdate()
        {
            return Json(JsonConvert.SerializeObject(Services.WundergroundQuery()),JsonRequestBehavior.AllowGet);
        }

        public ActionResult RssUpdate()
        {
            return Json(JsonConvert.SerializeObject(Services.RssQuery().GetRssItems()), JsonRequestBehavior.AllowGet);
        }

    }
}
