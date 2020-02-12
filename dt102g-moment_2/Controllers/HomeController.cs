using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dt102g_moment_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dt102g_moment_2.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            // Get session and save to ViewData
            ViewBag.viewName = HttpContext.Session.GetString("nameSess");

            return View();
        }
    }
}
