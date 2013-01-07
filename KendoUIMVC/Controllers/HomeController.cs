using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using KendoUIMVC.Models;
using XRisk;

namespace KendoUIMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IXRiskServices _services;

        public HomeController(
            IXRiskServices services)
        {
            _services = services;
        }

        public ActionResult Index()
        {
            //ViewBag.Message = "欢迎使用 ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult FormDemo()
        {
            return View(HttpContext.Timestamp);
        }

        public ActionResult GridDemo()
        {
            return View();
        }

        public ActionResult MvvmDemo()
        {
            return View();
        }

        public ActionResult MvvmDataSource()
        {
            return View();
        }

        [HttpGet]
        public new JsonResult User(int id, string Name)
        {
            return Json(new User() { Id = id, Name = string.Format("junwei.胡_{0}", id), Birthday = new DateTime(1968, 9, 21), Password = "123", Sex = true, Weight = 66d, Email = "junwei.hu@xquant.com" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UserUpdate(User user)
        {

            return null;
        }

        public JsonResult UserCreate(object user)
        {
            return null;
        }
    }
}
