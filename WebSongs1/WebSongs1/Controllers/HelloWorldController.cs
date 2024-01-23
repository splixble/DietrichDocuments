using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSongs1.Controllers
{
    public class HelloWorldController : Controller
    {
        // built according to tutorial at https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/adding-a-controller.
        // 
        // GET: /HelloWorld/ 

        public ActionResult Index()
        {
            return View();
        }


        // 
        // GET: /HelloWorld/Welcome/ 

        public ActionResult Welcome(string name, int numTimes)
        {
            ViewBag.Message = "Hello " + name;
            ViewBag.NumTimes = numTimes;

            return View();
        }
    }
}