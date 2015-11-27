using System;
using System.Web;
using System.Web.Mvc;
using MirrorProject.Models;
using Newtonsoft.Json;

namespace MirrorProject.Controllers
{
    public class HomeController : Controller
    {

        //
        // GET: /Home/

        public ActionResult Index()
        {
            ServiceSingleton.GetInstance();
            return View();
        }

        public ActionResult WeatherUpdate()
        {
            return Json(JsonConvert.SerializeObject(ServiceSingleton.GetInstance().GetWeatherReport()),JsonRequestBehavior.AllowGet);
        }

    }
}
