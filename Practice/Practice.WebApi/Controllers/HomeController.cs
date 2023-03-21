using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practice.WebApi.Controllers
{
    public class HomeController : Controller
    {

        //private readonly ILogger<HomeController> _logger;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public ActionResult Index()
        {
            //_logger.LogInformation("This is the home page");

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
