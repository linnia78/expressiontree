using CSharpPlay.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CSharpPlay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _svc;

        public HomeController(ProductService svc)
        {
            this._svc = svc;
        }
        public async Task<ActionResult> Index()
        {
            await _svc.DoSomething();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}